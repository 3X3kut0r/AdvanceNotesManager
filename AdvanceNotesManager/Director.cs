using DataBaseDLL;
using Npgsql;
using System.Data;

namespace AdvanceNotesManager
{
    public partial class Director : Form
    {
        private readonly Database db;
        private readonly int DirectorId;
        private readonly NpgsqlConnection connString = new NpgsqlConnection("Server=localhost;Port=5432;User Id=postgres;Password=123;Database=advancenotesmanager");

        public Director(int directorId)
        {
            InitializeComponent();
            db = new Database();
            DirectorId = directorId;
            // Загружаем список сотрудников в comboBoxWorker
            LoadEmployees();
            // Загружаем заметки в DataGridView
            LoadNotes();
            comboBox1.Items.AddRange(new[] { "Низкий", "Средний", "Высокий" });
            comboBox3.Items.AddRange(new[] { "В работе", "Выполнено", "Отказано", "Провалено" });
            comboBox4.Items.AddRange(new[] { "Низкий", "Средний", "Высокий" });
            dataGridView1.SelectionChanged += dataGridView1_SelectionChanged;
            // Подписываемся на событие FormClosed
            this.FormClosed += Director_FormClosed;
        }
        private void LoadEmployees()
        {
            // Загружаем сотрудников (не руководителей) в comboBoxWorker
            using (var conn = new NpgsqlConnection(connString.ConnectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("SELECT id, full_name FROM users_data WHERE is_director = FALSE", conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            comboBox2.Items.Add(new { Id = reader.GetInt32(0), Name = reader.GetString(1) });
                            comboBox5.Items.Add(new { Id = reader.GetInt32(0), Name = reader.GetString(1) });
                        }
                    }
                }
            }
            comboBox2.DisplayMember = "Name";
            comboBox2.ValueMember = "Id";
            comboBox5.DisplayMember = "Name";
            comboBox5.ValueMember = "Id";
        }

        private void LoadNotes()
        {
            // Загружаем заметки в DataGridView с учетом фильтров
            dataGridView1.Rows.Clear();
            string status = comboBox3.SelectedItem?.ToString();
            string priority = comboBox4.SelectedItem?.ToString();
            int? assigneeId = comboBox5.SelectedItem != null ? (int?)((dynamic)comboBox5.SelectedItem).Id : null;

            DataTable dt = db.GetFilteredNotes(DirectorId, status, priority, assigneeId);
            foreach (DataRow row in dt.Rows)
            {
                dataGridView1.Rows.Add(
                    row["id"], // id
                    row["title"], // title
                    row["description"] == DBNull.Value ? "" : row["description"], // description
                    row["priority"], // priority
                    row["status"], // status
                    row["full_name"] == DBNull.Value ? "" : row["full_name"], // assignee
                    row["due_date"] == DBNull.Value ? "" : Convert.ToDateTime(row["due_date"]).ToString("yyyy-MM-dd") // due_date
                );
            }
        }

        private void Director_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Закрываем приложение полностью
            Application.Exit();
        }

        private void buttonAddNote_Click(object sender, EventArgs e)
        {
            try
            {
                string title = textBox1.Text.Trim();
                string description = textBox2.Text.Trim();
                string priority = comboBox1.SelectedItem?.ToString();
                int? assigneeId = comboBox2.SelectedItem != null ? (int?)((dynamic)comboBox2.SelectedItem).Id : null;
                DateTime? dueDate = dateTimePicker1.Value;

                // Проверка всех полей
                if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(description) ||
                    string.IsNullOrEmpty(priority) || assigneeId == null || dueDate == null)
                {
                    MessageBox.Show("Все поля (название, описание, приоритет, ответственный, дата выполнения) обязательны.");
                    return;
                }

                // Проверка, что дата выполнения больше текущей даты
                if (dueDate.Value.Date <= DateTime.Today)
                {
                    MessageBox.Show("Дата выполнения должна быть больше текущей даты");
                    return;
                }

                bool success = db.AddNote(title, description, priority, assigneeId, dueDate, DirectorId);
                if (success)
                {
                    MessageBox.Show("Заметка успешно добавлена");
                    LoadNotes(); // Обновляем таблицу
                    ClearForm(); // Очищаем форму
                }
                else
                {
                    MessageBox.Show("Не удалось добавить заметку");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        private void buttonChangeNote_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Выберите заметку для изменения");
                    return;
                }
                int noteId = (int)dataGridView1.SelectedRows[0].Cells[0].Value; // ID заметки из первой колонки
                string title = textBox1.Text.Trim();
                string description = textBox2.Text.Trim();
                string priority = comboBox1.SelectedItem?.ToString();
                int? assigneeId = comboBox2.SelectedItem != null ? (int?)((dynamic)comboBox2.SelectedItem).Id : null;
                DateTime? dueDate = dateTimePicker1.Value;

                // Проверка всех полей
                if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(description) ||
                    string.IsNullOrEmpty(priority) || assigneeId == null || dueDate == null)
                {
                    MessageBox.Show("Все поля (название, описание, приоритет, ответственный, дата выполнения) обязательны");
                    return;
                }

                // Проверка, что дата выполнения больше текущей даты
                if (dueDate.Value.Date <= DateTime.Today)
                {
                    MessageBox.Show("Дата выполнения должна быть больше текущей даты");
                    return;
                }

                bool success = db.UpdateNote(noteId, title, description, priority, assigneeId, dueDate);
                if (success)
                {
                    MessageBox.Show("Заметка успешно обновлена");
                    LoadNotes(); // Обновляем таблицу
                    ClearForm(); // Очищаем форму
                }
                else
                {
                    MessageBox.Show("Не удалось обновить заметку");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        private void buttonDeleteNote_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Выберите заметку для удаления");
                    return;
                }

                int noteId = (int)dataGridView1.SelectedRows[0].Cells[0].Value; // ID заметки из первой колонки
                bool success = db.DeleteNote(noteId);
                if (success)
                {
                    MessageBox.Show("Заметка успешно удалена");
                    LoadNotes(); // Обновляем таблицу
                    ClearForm(); // Очищаем форму
                }
                else
                {
                    MessageBox.Show("Не удалось удалить заметку");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        private void ClearForm()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
            dateTimePicker1.Checked = false;
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                try
                {
                    var selectedRow = dataGridView1.SelectedRows[0];
                    textBox1.Text = selectedRow.Cells["title"].Value?.ToString() ?? "";
                    textBox2.Text = selectedRow.Cells["description"].Value?.ToString() ?? "";
                    string priority = selectedRow.Cells["priority"].Value?.ToString();
                    comboBox1.SelectedItem = !string.IsNullOrEmpty(priority) ? priority : null;

                    // Устанавливаем ответственного в comboBox2
                    string assigneeName = selectedRow.Cells["assignee"].Value?.ToString();
                    comboBox2.SelectedIndex = -1; // Сбрасываем выбор
                    if (!string.IsNullOrEmpty(assigneeName))
                    {
                        foreach (var item in comboBox2.Items)
                        {
                            if (((dynamic)item).Name?.ToString() == assigneeName)
                            {
                                comboBox2.SelectedItem = item;
                                break;
                            }
                        }
                    }

                    // Устанавливаем дату выполнения
                    string dueDateStr = selectedRow.Cells["due_date"].Value?.ToString();
                    if (!string.IsNullOrEmpty(dueDateStr) && DateTime.TryParse(dueDateStr, out DateTime dueDate))
                    {
                        dateTimePicker1.Value = dueDate;
                        dateTimePicker1.Checked = true;
                    }
                    else
                    {
                        dateTimePicker1.Checked = false;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при подстановке данных: {ex.Message}");
                }
            }
        }

        private void buttonSort_Click(object sender, EventArgs e)
        {
            // Применяем фильтры и сортировку
            LoadNotes();
        }

        private void buttonUnSort_Click(object sender, EventArgs e)
        {
            // Сбрасываем фильтры
            comboBox3.SelectedIndex = -1;
            comboBox4.SelectedIndex = -1;
            comboBox5.SelectedIndex = -1;
            // Загружаем все заметки без фильтров
            LoadNotes();
        }
    }
}

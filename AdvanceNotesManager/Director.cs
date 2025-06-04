using DataBaseDLL;
using Npgsql;
using System.Data;

namespace AdvanceNotesManager
{
    /// <summary>
    /// Форма для работы руководителя с заметками в системе.
    /// </summary>
    public partial class Director : Form
    {
        private readonly Database db;
        private readonly int DirectorId;
        private readonly NpgsqlConnection connString = new NpgsqlConnection("Server=localhost;Port=5432;UserId=postgres;Password=123;Database=advancenotesmanager");

        /// <summary>
        /// Конструктор формы для руководителя.
        /// Инициализирует компоненты, загружает данные и настраивает элементы управления.
        /// </summary>
        /// <param name="directorId">ID руководителя.</param>
        public Director(int directorId)
        {
            InitializeComponent();
            db = new Database();
            DirectorId = directorId;
            // Загружаем список сотрудников в comboBoxWorker
            LoadEmployees();
            // Загружаем заметки в DataGridView
            LoadNotes();
            // Заполнение выпадающих списков для приоритета и статуса
            comboBox1.Items.AddRange(new[] { "Низкий", "Средний", "Высокий" });
            comboBox3.Items.AddRange(new[] { "В работе", "Выполнено", "Отказано", "Провалено" });
            comboBox4.Items.AddRange(new[] { "Низкий", "Средний", "Высокий" });
            // Подписка на событие изменения выбора в DataGridView1
            dataGridView1.SelectionChanged += dataGridView1_SelectionChanged;
            // Подписываемся на событие FormClosed
            this.FormClosed += Director_FormClosed;
        }

        /// <summary>
        /// Загружает список сотрудников (не руководителей) в выпадающие списки comboBox2 и comboBox5.
        /// </summary>
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
                        // Добавление сотрудников в выпадающие списки
                        while (reader.Read())
                        {
                            comboBox2.Items.Add(new { Id = reader.GetInt32(0), Name = reader.GetString(1) });
                            comboBox5.Items.Add(new { Id = reader.GetInt32(0), Name = reader.GetString(1) });
                        }
                    }
                }
            }
            // Настройка отображаемого значения (имя) и значения (ID) для выпадающих списков
            comboBox2.DisplayMember = "Name";
            comboBox2.ValueMember = "Id";
            comboBox5.DisplayMember = "Name";
            comboBox5.ValueMember = "Id";
        }

        /// <summary>
        /// Загружает заметки в таблицу DataGridView с учетом текущих фильтров.
        /// </summary>
        private void LoadNotes()
        {
            // Загружаем заметки в DataGridView с учетом фильтров
            dataGridView1.Rows.Clear();
            string status = comboBox3.SelectedItem?.ToString();
            string priority = comboBox4.SelectedItem?.ToString();
            int? assigneeId = comboBox5.SelectedItem != null ? (int?)((dynamic)comboBox5.SelectedItem).Id : null;

            DataTable dt = db.GetFilteredNotesForDirector(DirectorId, status, priority, assigneeId); // Метод для фильтрации заметок
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

        /// <summary>
        /// Обработчик события закрытия формы.
        /// Завершает работу приложения при закрытии формы.
        /// </summary>
        /// <param name="sender">Объект, вызвавший событие.</param>
        /// <param name="e">Аргументы события закрытия формы.</param>
        private void Director_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Закрываем приложение полностью
            Application.Exit();
        }

        /// <summary>
        /// Обработчик клика по кнопке добавления новой заметки.
        /// </summary>
        /// <param name="sender">Объект, вызвавший событие.</param>
        /// <param name="e">Аргументы события клика.</param>
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

                bool success = db.AddNote(title, description, priority, assigneeId, dueDate, DirectorId); // Метод добавления заметки
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

        /// <summary>
        /// Обработчик клика по кнопке изменения заметки.
        /// </summary>
        /// <param name="sender">Объект, вызвавший событие.</param>
        /// <param name="e">Аргументы события клика.</param>
        private void buttonChangeNote_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Выберите заметку для изменения");
                    return;
                }
                int noteId = (int)dataGridView1.SelectedRows[0].Cells[0].Value; // Получение ID заметки
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

                bool success = db.UpdateNote(noteId, title, description, priority, assigneeId, dueDate); // Метод обновления заметки
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

        /// <summary>
        /// Обработчик клика по кнопке удаления заметки.
        /// </summary>
        /// <param name="sender">Объект, вызвавший событие.</param>
        /// <param name="e">Аргументы события клика.</param>
        private void buttonDeleteNote_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count == 0) // Если не выбрана заетка
                {
                    MessageBox.Show("Выберите заметку для удаления");
                    return;
                }

                int noteId = (int)dataGridView1.SelectedRows[0].Cells[0].Value; // Получение ID заметки
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

        /// <summary>
        /// Очищает поля формы для добавления/изменения заметки.
        /// </summary>
        private void ClearForm()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
            dateTimePicker1.Checked = false;
        }

        /// <summary>
        /// Обработчик события изменения выбора строки в DataGridView.
        /// Заполняет поля формы данными выбранной заметки.
        /// </summary>
        /// <param name="sender">Объект, вызвавший событие.</param>
        /// <param name="e">Аргументы события изменения выбора.</param>
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                try
                {
                    // Заполнение полей при выборе заметки
                    var selectedRow = dataGridView1.SelectedRows[0];
                    textBox1.Text = selectedRow.Cells["title"].Value?.ToString() ?? "";
                    textBox2.Text = selectedRow.Cells["description"].Value?.ToString() ?? "";
                    string priority = selectedRow.Cells["priority"].Value?.ToString();
                    comboBox1.SelectedItem = !string.IsNullOrEmpty(priority) ? priority : null;

                    // Устанавливаем ответственного в comboBoxWorker
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

        /// <summary>
        /// Обработчик клика по кнопке применения фильтров.
        /// Обновляет таблицу с учетом выбранных фильтров.
        /// </summary>
        /// <param name="sender">Объект, вызвавший событие.</param>
        /// <param name="e">Аргументы события клика.</param>
        private void buttonSort_Click(object sender, EventArgs e)
        {
            // Применяем фильтры и сортировку
            LoadNotes();
        }

        /// <summary>
        /// Обработчик клика по кнопке сброса фильтров.
        /// Сбрасывает фильтры и загружает все заметки.
        /// </summary>
        /// <param name="sender">Объект, вызвавший событие.</param>
        /// <param name="e">Аргументы события клика.</param>
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

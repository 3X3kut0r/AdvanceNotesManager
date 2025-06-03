using DataBaseDLL;
using System.Data;
namespace AdvanceNotesManager
{
    public partial class Worker : Form
    {
        private readonly Database db;
        private readonly int WorkerId;

        public Worker(int workerId)
        {
            db = new Database();
            WorkerId = workerId;
            InitializeComponent();
            comboBox1.Items.AddRange(new[] { "Низкий", "Средний", "Высокий" });
            comboBox2.Items.AddRange(new[] { "Низкий", "Средний", "Высокий" });
            comboBox3.Items.AddRange(new[] { "В работе", "Выполнено", "Отказано", "Провалено" });
            comboBox4.Items.AddRange(new[] { "В работе", "Выполнено", "Отказано" });
            // Загружаем заметки
            LoadActiveNotes();
            LoadAllNotes();
            dataGridView1.SelectionChanged += dataGridView1_SelectionChanged_1;
            // Подписываемся на событие FormClosed
            this.FormClosed += Worker_FormClosed;
        }

        private void LoadActiveNotes()
        {
            // Загружаем активные заметки (status = "В работе") в dataGridView1
            dataGridView1.Rows.Clear();
            string priority = comboBox1.SelectedItem?.ToString();
            DataTable dt = db.GetFilteredNotesForWorker(0, "В работе", priority, WorkerId);
            foreach (DataRow row in dt.Rows)
            {
                dataGridView1.Rows.Add(
                    row["id"],
                    row["title"],
                    row["description"] == DBNull.Value ? "" : row["description"],
                    row["priority"],
                    row["status"],
                    row["full_name"] == DBNull.Value ? "" : row["full_name"], // Director's name
                    row["due_date"] == DBNull.Value ? "" : Convert.ToDateTime(row["due_date"]).ToString("yyyy-MM-dd")
                );
            }
        }

        private void LoadAllNotes()
        {
            // Загружаем все заметки в dataGridView2
            dataGridView2.Rows.Clear();
            string priority = comboBox2.SelectedItem?.ToString();
            string status = comboBox3.SelectedItem?.ToString();
            DataTable dt = db.GetFilteredNotesForWorker(0, status, priority, WorkerId);
            foreach (DataRow row in dt.Rows)
            {
                dataGridView2.Rows.Add(
                    row["id"],
                    row["title"],
                    row["description"] == DBNull.Value ? "" : row["description"],
                    row["priority"],
                    row["status"],
                    row["full_name"] == DBNull.Value ? "" : row["full_name"], // Director's name
                    row["due_date"] == DBNull.Value ? "" : Convert.ToDateTime(row["due_date"]).ToString("yyyy-MM-dd")
                );
            }
        }

        private void Worker_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Закрываем приложение полностью
            Application.Exit();
        }

        private void buttonSortActiveNotes_Click(object sender, EventArgs e)
        {
            // Применяем фильтр по приоритету для активных заметок
            LoadActiveNotes();
        }

        private void buttonUnsortActiveNotes_Click(object sender, EventArgs e)
        {
            // Сбрасываем фильтр для активных заметок
            comboBox1.SelectedIndex = -1;
            LoadActiveNotes();
        }

        private void buttonChangeStatus_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Выберите заметку для изменения статуса");
                    return;
                }

                if (comboBox4.SelectedItem == null)
                {
                    MessageBox.Show("Выберите новый статус в comboBox");
                    return;
                }

                int noteId = (int)dataGridView1.SelectedRows[0].Cells["id"].Value;
                string newStatus = comboBox4.SelectedItem.ToString();
                bool success = db.UpdateNoteStatus(noteId, newStatus);
                if (success)
                {
                    MessageBox.Show("Статус заметки успешно изменен");
                    LoadActiveNotes(); // Обновляем активные заметки
                    LoadAllNotes(); // Обновляем все заметки
                    comboBox4.SelectedIndex = -1; // Сбрасываем выбор статуса
                }
                else
                {
                    MessageBox.Show("Не удалось изменить статус заметки");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        private void buttonSortNotes_Click(object sender, EventArgs e)
        {
            // Применяем фильтр по приоритету и статусу для всех заметок
            LoadAllNotes();
        }

        private void buttonUnsortNotes_Click(object sender, EventArgs e)
        {
            // Сбрасываем фильтры для всех заметок
            comboBox2.SelectedIndex = -1;
            comboBox3.SelectedIndex = -1;
            LoadAllNotes();
        }

        private void dataGridView1_SelectionChanged_1(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                try
                {
                    var selectedRow = dataGridView1.SelectedRows[0];
                    string status = selectedRow.Cells["status"].Value?.ToString();
                    comboBox4.SelectedItem = !string.IsNullOrEmpty(status) && (status == "Выполнено" || status == "Отказано" || status == "В работе")
                        ? status
                        : null;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при подстановке статуса: {ex.Message}");
                }
            }
        }
    }
}

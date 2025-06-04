using DataBaseDLL;
using System.Data;
namespace AdvanceNotesManager
{
    /// <summary>
    /// Форма для работы сотрудника с заметками в системе.
    /// </summary>
    public partial class Worker : Form
    {
        private readonly Database db;
        private readonly int WorkerId;

        /// <summary>
        /// Конструктор формы для сотрудника.
        /// Инициализирует компоненты, загружает данные и настраивает элементы управления.
        /// </summary>
        /// <param name="workerId">ID сотрудника.</param>
        public Worker(int workerId)
        {
            db = new Database();
            WorkerId = workerId;
            InitializeComponent();
            // Загрузка активных заметок (в работе) в dataGridView1
            LoadActiveNotes();
            // Загрузка всех заметок в dataGridView2
            LoadAllNotes();           
            // Заполнение выпадающих списков для приоритета и статуса
            comboBox1.Items.AddRange(new[] { "Низкий", "Средний", "Высокий" });
            comboBox2.Items.AddRange(new[] { "Низкий", "Средний", "Высокий" });
            comboBox3.Items.AddRange(new[] { "В работе", "Выполнено", "Отказано", "Провалено" });
            comboBox4.Items.AddRange(new[] { "В работе", "Выполнено", "Отказано" });
            // Подписка на событие изменения выбора в dataGridView1
            dataGridView1.SelectionChanged += dataGridView1_SelectionChanged_1;
            // Подписываемся на событие FormClosed
            this.FormClosed += Worker_FormClosed;
        }

        /// <summary>
        /// Загружает активные заметки (со статусом "В работе") в dataGridView1 с учетом фильтра по приоритету.
        /// </summary>
        private void LoadActiveNotes()
        {
            // Загружаем активные заметки (status = "В работе") в dataGridView1
            dataGridView1.Rows.Clear();
            string priority = comboBox1.SelectedItem?.ToString();
            DataTable dt = db.GetFilteredNotesForWorker(0, "В работе", priority, WorkerId); // Метод для фильтрации заметок
            foreach (DataRow row in dt.Rows)
            {
                dataGridView1.Rows.Add(
                    row["id"],
                    row["title"],
                    row["description"] == DBNull.Value ? "" : row["description"],
                    row["priority"],
                    row["status"],
                    row["full_name"] == DBNull.Value ? "" : row["full_name"], // Имя руководителя
                    row["due_date"] == DBNull.Value ? "" : Convert.ToDateTime(row["due_date"]).ToString("yyyy-MM-dd")
                );
            }
        }

        /// <summary>
        /// Загружает все заметки сотрудника в dataGridView2 с учетом фильтров по приоритету и статусу.
        /// </summary>
        private void LoadAllNotes()
        {
            // Загружаем все заметки в dataGridView2
            dataGridView2.Rows.Clear();
            string priority = comboBox2.SelectedItem?.ToString();
            string status = comboBox3.SelectedItem?.ToString();
            DataTable dt = db.GetFilteredNotesForWorker(0, status, priority, WorkerId); // Метод для фильтрации заметок
            foreach (DataRow row in dt.Rows)
            {
                dataGridView2.Rows.Add(
                    row["id"],
                    row["title"],
                    row["description"] == DBNull.Value ? "" : row["description"],
                    row["priority"],
                    row["status"],
                    row["full_name"] == DBNull.Value ? "" : row["full_name"], // Имя руководителя
                    row["due_date"] == DBNull.Value ? "" : Convert.ToDateTime(row["due_date"]).ToString("yyyy-MM-dd")
                );
            }
        }

        /// <summary>
        /// Обработчик события закрытия формы.
        /// Завершает работу приложения при закрытии формы.
        /// </summary>
        /// <param name="sender">Объект, вызвавший событие.</param>
        /// <param name="e">Аргументы события закрытия формы.</param>
        private void Worker_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Закрываем приложение полностью
            Application.Exit();
        }

        /// <summary>
        /// Обработчик клика по кнопке применения фильтра для активных заметок.
        /// Обновляет таблицу активных заметок с учетом выбранного приоритета.
        /// </summary>
        /// <param name="sender">Объект, вызвавший событие.</param>
        /// <param name="e">Аргументы события клика.</param>
        private void buttonSortActiveNotes_Click(object sender, EventArgs e)
        {
            // Применяем фильтр по приоритету для активных заметок
            LoadActiveNotes();
        }

        /// <summary>
        /// Обработчик клика по кнопке сброса фильтра для активных заметок.
        /// Сбрасывает фильтр по приоритету и обновляет таблицу.
        /// </summary>
        /// <param name="sender">Объект, вызвавший событие.</param>
        /// <param name="e">Аргументы события клика.</param>
        private void buttonUnsortActiveNotes_Click(object sender, EventArgs e)
        {
            // Сбрасываем фильтр для активных заметок
            comboBox1.SelectedIndex = -1;
            // Загрузка всех заметок
            LoadActiveNotes();
        }

        /// <summary>
        /// Обработчик клика по кнопке изменения статуса заметки.
        /// Обновляет статус выбранной заметки в базе данных.
        /// </summary>
        /// <param name="sender">Объект, вызвавший событие.</param>
        /// <param name="e">Аргументы события клика.</param>
        private void buttonChangeStatus_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count == 0) // Если не выбрана заметка
                {
                    MessageBox.Show("Выберите заметку для изменения статуса");
                    return;
                }

                if (comboBox4.SelectedItem == null) // Если статус не выбран
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

        /// <summary>
        /// Обработчик клика по кнопке применения фильтров для всех заметок.
        /// Обновляет таблицу всех заметок с учетом выбранных приоритета и статуса.
        /// </summary>
        /// <param name="sender">Объект, вызвавший событие.</param>
        /// <param name="e">Аргументы события клика.</param>
        private void buttonSortNotes_Click(object sender, EventArgs e)
        {
            // Применяем фильтр по приоритету и статусу для всех заметок
            LoadAllNotes();
        }

        /// <summary>
        /// Обработчик клика по кнопке сброса фильтров для всех заметок.
        /// Сбрасывает фильтры и обновляет таблицу.
        /// </summary>
        /// <param name="sender">Объект, вызвавший событие.</param>
        /// <param name="e">Аргументы события клика.</param>
        private void buttonUnsortNotes_Click(object sender, EventArgs e)
        {
            // Сбрасываем фильтры для всех заметок
            comboBox2.SelectedIndex = -1;
            comboBox3.SelectedIndex = -1;
            // Загрузка всех заметок
            LoadAllNotes();
        }

        /// <summary>
        /// Обработчик события изменения выбора строки в dataGridView1.
        /// Устанавливает текущий статус заметки в comboBox4.
        /// </summary>
        /// <param name="sender">Объект, вызвавший событие.</param>
        /// <param name="e">Аргументы события изменения выбора.</param>
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

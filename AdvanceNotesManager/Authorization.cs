using AuthoRegDLL;
namespace AdvanceNotesManager
{
    /// <summary>
    /// Форма для авторизации пользователей в системе.
    /// </summary>
    public partial class Authorization : Form
    {
        private readonly AuthoReg auth;

        /// <summary>
        /// Конструктор формы авторизации.
        /// Инициализирует компоненты формы, задает символ маскировки пароля и подписывается на событие закрытия формы.
        /// </summary>
        public Authorization()
        {
            InitializeComponent();
            textBox2.PasswordChar = '*';
            auth = new AuthoReg();
            // Подписываемся на событие FormClosed
            this.FormClosed += Authorization_FormClosed;
        }

        /// <summary>
        /// Обработчик клика по ссылке для перехода к форме регистрации.
        /// </summary>
        /// <param name="sender">Объект, вызвавший событие.</param>
        /// <param name="e">Аргументы события клика по ссылке.</param>
        private void linkLabelAuthorization_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Registration registration = new Registration();
            registration.Show();
            this.Hide();
        }

        /// <summary>
        /// Обработчик клика по кнопке авторизации.
        /// Выполняет проверку введенных данных и перенаправляет пользователя на соответствующую форму (Director или Worker).
        /// </summary>
        /// <param name="sender">Объект, вызвавший событие.</param>
        /// <param name="e">Аргументы события клика.</param>
        private void buttonAuthorize_Click(object sender, EventArgs e)
        {
            try
            {
                string login = textBox1.Text.Trim();
                string password = textBox2.Text;

                if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password)) // Проверка, что логин и пароль не пустые
                {
                    MessageBox.Show("Введите логин и пароль");
                    return;
                }

                var (user, errorMessage) = auth.Authenticate(login, password); // Вызов метода аутентификации
                if (user != null)
                {
                    this.Hide();
                    if (user.IsDirector)
                    {
                        Director director = new Director(user.Id); // Передаем ID пользователя
                        director.Show();
                    }
                    else
                    {
                        Worker worker = new Worker(user.Id); // Предполагается, что Worker также принимает ID
                        worker.Show();
                    }
                }
                else
                {
                    MessageBox.Show(errorMessage, "Ошибка авторизации");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        /// <summary>
        /// Обработчик клика по кнопке для переключения видимости пароля.
        /// </summary>
        /// <param name="sender">Объект, вызвавший событие.</param>
        /// <param name="e">Аргументы события клика.</param>
        private void buttonShowPasswordAuthorization_Click(object sender, EventArgs e)
        {
            // Переключение между скрытым и открытым текстом
            if (textBox2.PasswordChar == '*')
            {
                // Показать текст
                textBox2.PasswordChar = '\0'; // Ноль символов для отображения текста
                button1.Text = "Скрыть";
            }
            else
            {
                // Скрыть текст
                textBox2.PasswordChar = '*';
                button1.Text = "Показать";
            }
            textBox2.Focus();
        }

        /// <summary>
        /// Обработчик события закрытия формы.
        /// Завершает работу приложения при закрытии формы.
        /// </summary>
        /// <param name="sender">Объект, вызвавший событие.</param>
        /// <param name="e">Аргументы события закрытия формы.</param>
        private void Authorization_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Закрываем приложение полностью
            Application.Exit();
        }
    }
}

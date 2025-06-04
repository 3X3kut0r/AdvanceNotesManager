using AuthoRegDLL;
namespace AdvanceNotesManager
{
    /// <summary>
    /// Форма для регистрации пользователей в системе.
    /// </summary>
    public partial class Registration : Form
    {
        private readonly AuthoReg auth;

        /// <summary>
        /// Конструктор формы регистрации.
        /// Инициализирует компоненты формы и объект AuthoReg, а также подписывается на событие закрытия формы.
        /// </summary>
        public Registration()
        {
            InitializeComponent();
            auth = new AuthoReg();
            // Подписываемся на событие FormClosed
            this.FormClosed += Registration_FormClosed;
        }

        /// <summary>
        /// Обработчик клика по ссылке для перехода к форме авторизации.
        /// </summary>
        /// <param name="sender">Объект, вызвавший событие.</param>
        /// <param name="e">Аргументы события клика по ссылке.</param>
        private void linkLabelRegistration_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Authorization authorization = new Authorization();
            authorization.Show();
            this.Hide();
        }

        /// <summary>
        /// Обработчик клика по кнопке регистрации.
        /// Выполняет регистрацию пользователя с введенными данными.
        /// </summary>
        /// <param name="sender">Объект, вызвавший событие.</param>
        /// <param name="e">Аргументы события клика.</param>
        private void buttonRegister_Click(object sender, EventArgs e)
        {
            string login = textBox1.Text;
            string password = textBox2.Text;
            string name = textBox3.Text;
            bool isDirector = checkBox1.Checked;

            var (success, errorMessage) = auth.Register(login, password, name, isDirector); // Вызов метода регистрации
            if (success)
            {
                MessageBox.Show("Регистрация успешна!");
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                checkBox1.Checked = false;
            }
            else
            {
                MessageBox.Show(errorMessage);
            }
        }

        /// <summary>
        /// Обработчик клика по кнопке для переключения видимости пароля.
        /// </summary>
        /// <param name="sender">Объект, вызвавший событие.</param>
        /// <param name="e">Аргументы события клика.</param>
        private void buttonShowPasswordRegistration_Click(object sender, EventArgs e)
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
        private void Registration_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Закрываем приложение полностью
            Application.Exit();
        }
    }
}

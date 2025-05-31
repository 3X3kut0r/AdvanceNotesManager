using System.IO;

namespace AdvanceNotesManager
{
    public partial class Authorization : Form
    {
        public Authorization()
        {
            InitializeComponent();
            textBox2.PasswordChar = '*';
        }

        private void linkLabelAuthorization_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Registration registration = new Registration();
            registration.Show();
            this.Hide();
        }

        private void buttonAuthorize_Click(object sender, EventArgs e)
        {
            string login = textBox1.Text;
            string password = textBox2.Text;

            // Валидация
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Введите логин и пароль.");
                return;
            }

            // Попытка авторизовать пользователя
            AuthorizationLib.User user = UserWork.AuthenticateUser(login, password);

            if (user != null)
            {

                // Открытие соответствующего окна в зависимости от типа пользователя
                if (user.IsManager)
                {
                    // Если это руководитель, открываем форму для руководителя
                    this.Hide();
                    Director director = new Director();
                    director.Show();

                }
                else
                {
                    // Если это подчиненный, открываем форму для подчиненного
                    this.Hide();
                    Worker worker = new Worker();
                    worker.Show();

                }

                // Закрываем текущую форму (форма авторизации)
                this.Hide();
            }
            else
            {
                // Неверные данные
                MessageBox.Show("Неверный логин или пароль");
            }
        }

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
    }
}

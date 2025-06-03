using AuthoRegDLL;
namespace AdvanceNotesManager
{
    public partial class Authorization : Form
    {
        private readonly AuthoReg auth;
        public Authorization()
        {
            InitializeComponent();
            textBox2.PasswordChar = '*';
            auth = new AuthoReg();
            // Подписываемся на событие FormClosed
            this.FormClosed += Authorization_FormClosed;
        }

        private void linkLabelAuthorization_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Registration registration = new Registration();
            registration.Show();
            this.Hide();
        }

        private void buttonAuthorize_Click(object sender, EventArgs e)
        {
            try
            {
                string login = textBox1.Text.Trim();
                string password = textBox2.Text;

                if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
                {
                    MessageBox.Show("Введите логин и пароль");
                    return;
                }

                var (user, errorMessage) = auth.Authenticate(login, password);
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

        private void Authorization_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Закрываем приложение полностью
            Application.Exit();
        }
    }
}

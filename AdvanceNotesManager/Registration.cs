using AuthoRegDLL;
namespace AdvanceNotesManager
{
    public partial class Registration : Form
    {
        private readonly AuthoReg auth;

        public Registration()
        {
            InitializeComponent();
            auth = new AuthoReg();
            // Подписываемся на событие FormClosed
            this.FormClosed += Registration_FormClosed;
        }

        private void linkLabelRegistration_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Authorization authorization = new Authorization();
            authorization.Show();
            this.Hide();
        }

        private void buttonRegister_Click(object sender, EventArgs e)
        {
            string login = textBox1.Text;
            string password = textBox2.Text;
            string name = textBox3.Text;
            bool isDirector = checkBox1.Checked;

            var (success, errorMessage) = auth.Register(login, password, name, isDirector);
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

        private void Registration_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Закрываем приложение полностью
            Application.Exit();
        }
    }
}

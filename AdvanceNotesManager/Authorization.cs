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

            var (user, errorMessage) = auth.Authenticate(login, password);
            if (user != null)
            {
                if (user.IsDirector)
                {
                    this.Hide();
                    Director director = new Director();
                    director.Show();
                }
                else
                {
                    this.Hide();
                    Worker worker = new Worker();
                    worker.Show();
                }
            }
            else
            {
                MessageBox.Show(errorMessage);
            }
        }

        private void buttonShowPasswordAuthorization_Click(object sender, EventArgs e)
        {
            // ������������ ����� ������� � �������� �������
            if (textBox2.PasswordChar == '*')
            {
                // �������� �����
                textBox2.PasswordChar = '\0'; // ���� �������� ��� ����������� ������
                button1.Text = "������";
            }
            else
            {
                // ������ �����
                textBox2.PasswordChar = '*';
                button1.Text = "��������";
            }
            textBox2.Focus();
        }
    }
}

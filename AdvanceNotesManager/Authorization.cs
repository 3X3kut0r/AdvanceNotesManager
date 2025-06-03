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
            // ������������� �� ������� FormClosed
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
                    MessageBox.Show("������� ����� � ������");
                    return;
                }

                var (user, errorMessage) = auth.Authenticate(login, password);
                if (user != null)
                {
                    this.Hide();
                    if (user.IsDirector)
                    {
                        Director director = new Director(user.Id); // �������� ID ������������
                        director.Show();
                    }
                    else
                    {
                        Worker worker = new Worker(user.Id); // ��������������, ��� Worker ����� ��������� ID
                        worker.Show();
                    }
                }
                else
                {
                    MessageBox.Show(errorMessage, "������ �����������");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"������: {ex.Message}");
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

        private void Authorization_FormClosed(object sender, FormClosedEventArgs e)
        {
            // ��������� ���������� ���������
            Application.Exit();
        }
    }
}

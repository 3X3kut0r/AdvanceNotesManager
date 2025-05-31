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

            // ���������
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("������� ����� � ������.");
                return;
            }

            // ������� ������������ ������������
            AuthorizationLib.User user = UserWork.AuthenticateUser(login, password);

            if (user != null)
            {

                // �������� ���������������� ���� � ����������� �� ���� ������������
                if (user.IsManager)
                {
                    // ���� ��� ������������, ��������� ����� ��� ������������
                    this.Hide();
                    Director director = new Director();
                    director.Show();

                }
                else
                {
                    // ���� ��� �����������, ��������� ����� ��� ������������
                    this.Hide();
                    Worker worker = new Worker();
                    worker.Show();

                }

                // ��������� ������� ����� (����� �����������)
                this.Hide();
            }
            else
            {
                // �������� ������
                MessageBox.Show("�������� ����� ��� ������");
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

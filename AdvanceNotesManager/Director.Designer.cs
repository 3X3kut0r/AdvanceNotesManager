namespace AdvanceNotesManager
{
    partial class Director
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Director));
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            comboBox6 = new ComboBox();
            label8 = new Label();
            dateTimePicker1 = new DateTimePicker();
            button5 = new Button();
            button4 = new Button();
            button1 = new Button();
            comboBox2 = new ComboBox();
            comboBox1 = new ComboBox();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            textBox2 = new TextBox();
            textBox1 = new TextBox();
            tabPage2 = new TabPage();
            dataGridView1 = new DataGridView();
            id = new DataGridViewTextBoxColumn();
            title = new DataGridViewTextBoxColumn();
            description = new DataGridViewTextBoxColumn();
            priority = new DataGridViewTextBoxColumn();
            status = new DataGridViewTextBoxColumn();
            assignee = new DataGridViewTextBoxColumn();
            due_date = new DataGridViewTextBoxColumn();
            button3 = new Button();
            button2 = new Button();
            comboBox5 = new ComboBox();
            label7 = new Label();
            comboBox4 = new ComboBox();
            comboBox3 = new ComboBox();
            label6 = new Label();
            label5 = new Label();
            label9 = new Label();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Location = new Point(12, 12);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(698, 453);
            tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(label9);
            tabPage1.Controls.Add(comboBox6);
            tabPage1.Controls.Add(label8);
            tabPage1.Controls.Add(dateTimePicker1);
            tabPage1.Controls.Add(button5);
            tabPage1.Controls.Add(button4);
            tabPage1.Controls.Add(button1);
            tabPage1.Controls.Add(comboBox2);
            tabPage1.Controls.Add(comboBox1);
            tabPage1.Controls.Add(label4);
            tabPage1.Controls.Add(label3);
            tabPage1.Controls.Add(label2);
            tabPage1.Controls.Add(label1);
            tabPage1.Controls.Add(textBox2);
            tabPage1.Controls.Add(textBox1);
            tabPage1.Location = new Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(690, 425);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Создание заметок";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // comboBox6
            // 
            comboBox6.FormattingEnabled = true;
            comboBox6.Location = new Point(13, 396);
            comboBox6.Name = "comboBox6";
            comboBox6.Size = new Size(141, 23);
            comboBox6.TabIndex = 13;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label8.Location = new Point(225, 274);
            label8.Name = "label8";
            label8.Size = new Size(126, 21);
            label8.TabIndex = 12;
            label8.Text = "Дата окончания";
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Location = new Point(13, 273);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(200, 23);
            dateTimePicker1.TabIndex = 11;
            // 
            // button5
            // 
            button5.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            button5.Location = new Point(424, 369);
            button5.Name = "button5";
            button5.Size = new Size(260, 39);
            button5.TabIndex = 10;
            button5.Text = "Удалить заметку";
            button5.UseVisualStyleBackColor = true;
            button5.Click += buttonDeleteNote_Click;
            // 
            // button4
            // 
            button4.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            button4.Location = new Point(424, 326);
            button4.Name = "button4";
            button4.Size = new Size(260, 39);
            button4.TabIndex = 9;
            button4.Text = "Изменить заметку";
            button4.UseVisualStyleBackColor = true;
            button4.Click += buttonChangeNote_Click;
            // 
            // button1
            // 
            button1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            button1.Location = new Point(424, 284);
            button1.Name = "button1";
            button1.Size = new Size(260, 39);
            button1.TabIndex = 8;
            button1.Text = "Создать заметку";
            button1.UseVisualStyleBackColor = true;
            button1.Click += buttonAddNote_Click;
            // 
            // comboBox2
            // 
            comboBox2.FormattingEnabled = true;
            comboBox2.Location = new Point(178, 342);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(201, 23);
            comboBox2.TabIndex = 7;
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(13, 342);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(141, 23);
            comboBox1.TabIndex = 6;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label4.Location = new Point(223, 311);
            label4.Name = "label4";
            label4.Size = new Size(120, 21);
            label4.TabIndex = 5;
            label4.Text = "Ответственный";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label3.Location = new Point(38, 311);
            label3.Name = "label3";
            label3.Size = new Size(88, 21);
            label3.TabIndex = 4;
            label3.Text = "Приоритет";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label2.Location = new Point(9, 96);
            label2.Name = "label2";
            label2.Size = new Size(101, 21);
            label2.TabIndex = 3;
            label2.Text = "Примечания";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label1.Location = new Point(9, 16);
            label1.Name = "label1";
            label1.Size = new Size(140, 21);
            label1.TabIndex = 2;
            label1.Text = "Название заметки";
            // 
            // textBox2
            // 
            textBox2.Location = new Point(6, 120);
            textBox2.Multiline = true;
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(678, 126);
            textBox2.TabIndex = 1;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(6, 40);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(678, 44);
            textBox1.TabIndex = 0;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(dataGridView1);
            tabPage2.Controls.Add(button3);
            tabPage2.Controls.Add(button2);
            tabPage2.Controls.Add(comboBox5);
            tabPage2.Controls.Add(label7);
            tabPage2.Controls.Add(comboBox4);
            tabPage2.Controls.Add(comboBox3);
            tabPage2.Controls.Add(label6);
            tabPage2.Controls.Add(label5);
            tabPage2.Location = new Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(690, 425);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Все заметки";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { id, title, description, priority, status, assignee, due_date });
            dataGridView1.Location = new Point(8, 75);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(676, 344);
            dataGridView1.TabIndex = 16;
            // 
            // id
            // 
            id.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            id.HeaderText = "id";
            id.Name = "id";
            id.ReadOnly = true;
            id.Width = 42;
            // 
            // title
            // 
            title.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            title.HeaderText = "title";
            title.Name = "title";
            title.ReadOnly = true;
            title.Width = 52;
            // 
            // description
            // 
            description.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            description.HeaderText = "description";
            description.Name = "description";
            description.ReadOnly = true;
            description.Width = 91;
            // 
            // priority
            // 
            priority.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            priority.HeaderText = "priority";
            priority.Name = "priority";
            priority.ReadOnly = true;
            priority.Width = 70;
            // 
            // status
            // 
            status.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            status.HeaderText = "status";
            status.Name = "status";
            status.ReadOnly = true;
            status.Width = 63;
            // 
            // assignee
            // 
            assignee.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            assignee.HeaderText = "assignee";
            assignee.Name = "assignee";
            assignee.ReadOnly = true;
            assignee.Width = 77;
            // 
            // due_date
            // 
            due_date.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            due_date.HeaderText = "due_date";
            due_date.Name = "due_date";
            due_date.ReadOnly = true;
            due_date.Width = 80;
            // 
            // button3
            // 
            button3.Location = new Point(504, 43);
            button3.Name = "button3";
            button3.Size = new Size(137, 23);
            button3.TabIndex = 15;
            button3.Text = "Отменить сортировку";
            button3.UseVisualStyleBackColor = true;
            button3.Click += buttonUnSort_Click;
            // 
            // button2
            // 
            button2.Location = new Point(504, 14);
            button2.Name = "button2";
            button2.Size = new Size(137, 23);
            button2.TabIndex = 14;
            button2.Text = "Отсортировать";
            button2.UseVisualStyleBackColor = true;
            button2.Click += buttonSort_Click;
            // 
            // comboBox5
            // 
            comboBox5.FormattingEnabled = true;
            comboBox5.Location = new Point(330, 40);
            comboBox5.Name = "comboBox5";
            comboBox5.Size = new Size(141, 23);
            comboBox5.TabIndex = 13;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label7.Location = new Point(342, 16);
            label7.Name = "label7";
            label7.Size = new Size(120, 21);
            label7.TabIndex = 12;
            label7.Text = "Ответственный";
            // 
            // comboBox4
            // 
            comboBox4.FormattingEnabled = true;
            comboBox4.Location = new Point(170, 40);
            comboBox4.Name = "comboBox4";
            comboBox4.Size = new Size(141, 23);
            comboBox4.TabIndex = 11;
            // 
            // comboBox3
            // 
            comboBox3.FormattingEnabled = true;
            comboBox3.Location = new Point(6, 40);
            comboBox3.Name = "comboBox3";
            comboBox3.Size = new Size(141, 23);
            comboBox3.TabIndex = 10;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label6.Location = new Point(200, 16);
            label6.Name = "label6";
            label6.Size = new Size(88, 21);
            label6.TabIndex = 4;
            label6.Text = "Приоритет";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label5.Location = new Point(46, 16);
            label5.Name = "label5";
            label5.Size = new Size(57, 21);
            label5.TabIndex = 3;
            label5.Text = "Статус";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label9.Location = new Point(53, 372);
            label9.Name = "label9";
            label9.Size = new Size(57, 21);
            label9.TabIndex = 14;
            label9.Text = "Статус";
            // 
            // Director
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(714, 477);
            Controls.Add(tabControl1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Director";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Личный кабинет директора";
            FormClosed += Director_FormClosed;
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabControl1;
        private TabPage tabPage1;
        private Button button1;
        private ComboBox comboBox2;
        private ComboBox comboBox1;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label label1;
        private TextBox textBox2;
        private TextBox textBox1;
        private TabPage tabPage2;
        private Button button3;
        private Button button2;
        private ComboBox comboBox5;
        private Label label7;
        private ComboBox comboBox4;
        private ComboBox comboBox3;
        private Label label6;
        private Label label5;
        private Button button5;
        private Button button4;
        private DateTimePicker dateTimePicker1;
        private DataGridView dataGridView1;
        private DataGridViewTextBoxColumn id;
        private DataGridViewTextBoxColumn title;
        private DataGridViewTextBoxColumn description;
        private DataGridViewTextBoxColumn priority;
        private DataGridViewTextBoxColumn status;
        private DataGridViewTextBoxColumn assignee;
        private DataGridViewTextBoxColumn due_date;
        private Label label8;
        private ComboBox comboBox6;
        private Label label9;
    }
}
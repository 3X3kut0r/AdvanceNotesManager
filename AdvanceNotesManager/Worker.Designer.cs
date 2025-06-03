namespace AdvanceNotesManager
{
    partial class Worker
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Worker));
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            comboBox4 = new ComboBox();
            label4 = new Label();
            button5 = new Button();
            button2 = new Button();
            button1 = new Button();
            label1 = new Label();
            comboBox1 = new ComboBox();
            dataGridView1 = new DataGridView();
            id = new DataGridViewTextBoxColumn();
            title = new DataGridViewTextBoxColumn();
            description = new DataGridViewTextBoxColumn();
            priority = new DataGridViewTextBoxColumn();
            status = new DataGridViewTextBoxColumn();
            director_name = new DataGridViewTextBoxColumn();
            due_date = new DataGridViewTextBoxColumn();
            tabPage2 = new TabPage();
            dataGridView2 = new DataGridView();
            dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn4 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn5 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn6 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn7 = new DataGridViewTextBoxColumn();
            button3 = new Button();
            button4 = new Button();
            label3 = new Label();
            comboBox3 = new ComboBox();
            label2 = new Label();
            comboBox2 = new ComboBox();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).BeginInit();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Location = new Point(12, 12);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(612, 454);
            tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(comboBox4);
            tabPage1.Controls.Add(label4);
            tabPage1.Controls.Add(button5);
            tabPage1.Controls.Add(button2);
            tabPage1.Controls.Add(button1);
            tabPage1.Controls.Add(label1);
            tabPage1.Controls.Add(comboBox1);
            tabPage1.Controls.Add(dataGridView1);
            tabPage1.Location = new Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(604, 426);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Текущие заметки";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // comboBox4
            // 
            comboBox4.FormattingEnabled = true;
            comboBox4.Location = new Point(313, 50);
            comboBox4.Name = "comboBox4";
            comboBox4.Size = new Size(137, 23);
            comboBox4.TabIndex = 7;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label4.Location = new Point(354, 21);
            label4.Name = "label4";
            label4.Size = new Size(52, 20);
            label4.TabIndex = 6;
            label4.Text = "Статус";
            // 
            // button5
            // 
            button5.Location = new Point(466, 21);
            button5.Name = "button5";
            button5.Size = new Size(122, 52);
            button5.TabIndex = 5;
            button5.Text = "Изменить состояние";
            button5.UseVisualStyleBackColor = true;
            button5.Click += buttonChangeStatus_Click;
            // 
            // button2
            // 
            button2.Location = new Point(157, 50);
            button2.Name = "button2";
            button2.Size = new Size(141, 23);
            button2.TabIndex = 4;
            button2.Text = "Отменить сортировку";
            button2.UseVisualStyleBackColor = true;
            button2.Click += buttonUnsortActiveNotes_Click;
            // 
            // button1
            // 
            button1.Location = new Point(157, 21);
            button1.Name = "button1";
            button1.Size = new Size(141, 23);
            button1.TabIndex = 3;
            button1.Text = "Отсортировать";
            button1.UseVisualStyleBackColor = true;
            button1.Click += buttonSortActiveNotes_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label1.Location = new Point(41, 23);
            label1.Name = "label1";
            label1.Size = new Size(85, 20);
            label1.TabIndex = 2;
            label1.Text = "Приоритет";
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(14, 50);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(137, 23);
            comboBox1.TabIndex = 1;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { id, title, description, priority, status, director_name, due_date });
            dataGridView1.Location = new Point(8, 87);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(590, 331);
            dataGridView1.TabIndex = 0;
            dataGridView1.SelectionChanged += dataGridView1_SelectionChanged_1;
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
            // director_name
            // 
            director_name.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            director_name.HeaderText = "director_name";
            director_name.Name = "director_name";
            director_name.ReadOnly = true;
            director_name.Width = 108;
            // 
            // due_date
            // 
            due_date.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            due_date.HeaderText = "due_date";
            due_date.Name = "due_date";
            due_date.ReadOnly = true;
            due_date.Width = 80;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(dataGridView2);
            tabPage2.Controls.Add(button3);
            tabPage2.Controls.Add(button4);
            tabPage2.Controls.Add(label3);
            tabPage2.Controls.Add(comboBox3);
            tabPage2.Controls.Add(label2);
            tabPage2.Controls.Add(comboBox2);
            tabPage2.Location = new Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(604, 426);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Все заметки";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // dataGridView2
            // 
            dataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView2.Columns.AddRange(new DataGridViewColumn[] { dataGridViewTextBoxColumn1, dataGridViewTextBoxColumn2, dataGridViewTextBoxColumn3, dataGridViewTextBoxColumn4, dataGridViewTextBoxColumn5, dataGridViewTextBoxColumn6, dataGridViewTextBoxColumn7 });
            dataGridView2.Location = new Point(6, 92);
            dataGridView2.Name = "dataGridView2";
            dataGridView2.Size = new Size(592, 331);
            dataGridView2.TabIndex = 9;
            // 
            // dataGridViewTextBoxColumn1
            // 
            dataGridViewTextBoxColumn1.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewTextBoxColumn1.HeaderText = "id";
            dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            dataGridViewTextBoxColumn1.ReadOnly = true;
            dataGridViewTextBoxColumn1.Width = 42;
            // 
            // dataGridViewTextBoxColumn2
            // 
            dataGridViewTextBoxColumn2.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewTextBoxColumn2.HeaderText = "title";
            dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            dataGridViewTextBoxColumn2.ReadOnly = true;
            dataGridViewTextBoxColumn2.Width = 52;
            // 
            // dataGridViewTextBoxColumn3
            // 
            dataGridViewTextBoxColumn3.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewTextBoxColumn3.HeaderText = "description";
            dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            dataGridViewTextBoxColumn3.ReadOnly = true;
            dataGridViewTextBoxColumn3.Width = 91;
            // 
            // dataGridViewTextBoxColumn4
            // 
            dataGridViewTextBoxColumn4.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewTextBoxColumn4.HeaderText = "priority";
            dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            dataGridViewTextBoxColumn4.ReadOnly = true;
            dataGridViewTextBoxColumn4.Width = 70;
            // 
            // dataGridViewTextBoxColumn5
            // 
            dataGridViewTextBoxColumn5.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewTextBoxColumn5.HeaderText = "status";
            dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            dataGridViewTextBoxColumn5.ReadOnly = true;
            dataGridViewTextBoxColumn5.Width = 63;
            // 
            // dataGridViewTextBoxColumn6
            // 
            dataGridViewTextBoxColumn6.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewTextBoxColumn6.HeaderText = "director_name";
            dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            dataGridViewTextBoxColumn6.ReadOnly = true;
            dataGridViewTextBoxColumn6.Width = 108;
            // 
            // dataGridViewTextBoxColumn7
            // 
            dataGridViewTextBoxColumn7.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewTextBoxColumn7.HeaderText = "due_date";
            dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            dataGridViewTextBoxColumn7.ReadOnly = true;
            dataGridViewTextBoxColumn7.Width = 80;
            // 
            // button3
            // 
            button3.Location = new Point(352, 46);
            button3.Name = "button3";
            button3.Size = new Size(141, 23);
            button3.TabIndex = 8;
            button3.Text = "Отменить сортировку";
            button3.UseVisualStyleBackColor = true;
            button3.Click += buttonUnsortNotes_Click;
            // 
            // button4
            // 
            button4.Location = new Point(352, 17);
            button4.Name = "button4";
            button4.Size = new Size(141, 23);
            button4.TabIndex = 7;
            button4.Text = "Отсортировать";
            button4.UseVisualStyleBackColor = true;
            button4.Click += buttonSortNotes_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label3.Location = new Point(227, 17);
            label3.Name = "label3";
            label3.Size = new Size(52, 20);
            label3.TabIndex = 6;
            label3.Text = "Статус";
            // 
            // comboBox3
            // 
            comboBox3.FormattingEnabled = true;
            comboBox3.Location = new Point(184, 40);
            comboBox3.Name = "comboBox3";
            comboBox3.Size = new Size(137, 23);
            comboBox3.TabIndex = 5;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label2.Location = new Point(42, 17);
            label2.Name = "label2";
            label2.Size = new Size(85, 20);
            label2.TabIndex = 4;
            label2.Text = "Приоритет";
            // 
            // comboBox2
            // 
            comboBox2.FormattingEnabled = true;
            comboBox2.Location = new Point(19, 40);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(137, 23);
            comboBox2.TabIndex = 3;
            // 
            // Worker
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(632, 474);
            Controls.Add(tabControl1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Worker";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Личный кабинет работника";
            FormClosed += Worker_FormClosed;
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabControl1;
        private TabPage tabPage1;
        private Button button2;
        private Button button1;
        private Label label1;
        private ComboBox comboBox1;
        private DataGridView dataGridView1;
        private TabPage tabPage2;
        private Button button3;
        private Button button4;
        private Label label3;
        private ComboBox comboBox3;
        private Label label2;
        private ComboBox comboBox2;
        private Button button5;
        private Label label4;
        private ComboBox comboBox4;
        private DataGridViewTextBoxColumn id;
        private DataGridViewTextBoxColumn title;
        private DataGridViewTextBoxColumn description;
        private DataGridViewTextBoxColumn priority;
        private DataGridViewTextBoxColumn status;
        private DataGridViewTextBoxColumn director_name;
        private DataGridViewTextBoxColumn due_date;
        private DataGridView dataGridView2;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
    }
}
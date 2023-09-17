namespace testing_program
{
    partial class TeacherForm
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
            this.components = new System.ComponentModel.Container();
            this.test_constructor = new System.Windows.Forms.TabPage();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.create_test_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.edit_test_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.create_version_questions_groupBox = new System.Windows.Forms.GroupBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.answer_textBox3 = new System.Windows.Forms.TextBox();
            this.answer_textBox2 = new System.Windows.Forms.TextBox();
            this.answer_textBox1 = new System.Windows.Forms.TextBox();
            this.question_textBox = new System.Windows.Forms.TextBox();
            this.question_type_comboBox = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.create_test_groupBox = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.add_new_test_to_db = new System.Windows.Forms.Button();
            this.new_timer_textBox = new System.Windows.Forms.TextBox();
            this.new_test_name_textBox = new System.Windows.Forms.TextBox();
            this.create_next_version = new System.Windows.Forms.Button();
            this.add_new_version_button = new System.Windows.Forms.Button();
            this.profile_page = new System.Windows.Forms.TabPage();
            this.teacher_identity_check_panel = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.cancel_teacher_change_password_button = new System.Windows.Forms.Button();
            this.teacher_old_password_textBox = new System.Windows.Forms.TextBox();
            this.continue_teacher_change_password_button = new System.Windows.Forms.Button();
            this.change_teacher_login_panel = new System.Windows.Forms.Panel();
            this.cancel_change_teacher_login_button = new System.Windows.Forms.Button();
            this.new_teacher_login_button = new System.Windows.Forms.Button();
            this.new_teacher_login_textbox = new System.Windows.Forms.TextBox();
            this.change_teacher_login_label = new System.Windows.Forms.Label();
            this.change_teacher_password_panel = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.new_teacher_password_button = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.cancel_change_teacher_password_button = new System.Windows.Forms.Button();
            this.teacher_new_password_textBox = new System.Windows.Forms.TextBox();
            this.teacher_test_new_password_textBox = new System.Windows.Forms.TextBox();
            this.teacher_profile_main_panel = new System.Windows.Forms.Panel();
            this.change_teacher_password_button = new System.Windows.Forms.Button();
            this.change_teacher_login_button = new System.Windows.Forms.Button();
            this.password_teacher_profile = new System.Windows.Forms.TextBox();
            this.full_name_teacher_profile_label = new System.Windows.Forms.Label();
            this.login_teacher_profile = new System.Windows.Forms.TextBox();
            this.full_name_teacher_profile_textBox = new System.Windows.Forms.TextBox();
            this.password_teacher_profile_label = new System.Windows.Forms.Label();
            this.login_teacher_profile_label = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.exit_student_profile_button = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.test_constructor.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.create_version_questions_groupBox.SuspendLayout();
            this.create_test_groupBox.SuspendLayout();
            this.profile_page.SuspendLayout();
            this.teacher_identity_check_panel.SuspendLayout();
            this.change_teacher_login_panel.SuspendLayout();
            this.change_teacher_password_panel.SuspendLayout();
            this.teacher_profile_main_panel.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // test_constructor
            // 
            this.test_constructor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.test_constructor.ContextMenuStrip = this.contextMenuStrip1;
            this.test_constructor.Controls.Add(this.create_version_questions_groupBox);
            this.test_constructor.Controls.Add(this.button1);
            this.test_constructor.Controls.Add(this.create_test_groupBox);
            this.test_constructor.Controls.Add(this.create_next_version);
            this.test_constructor.Controls.Add(this.add_new_version_button);
            this.test_constructor.Location = new System.Drawing.Point(4, 30);
            this.test_constructor.Name = "test_constructor";
            this.test_constructor.Padding = new System.Windows.Forms.Padding(3);
            this.test_constructor.Size = new System.Drawing.Size(858, 569);
            this.test_constructor.TabIndex = 1;
            this.test_constructor.Text = "Конструктор тестов";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.create_test_MenuItem,
            this.edit_test_MenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(215, 52);
            // 
            // create_test_MenuItem
            // 
            this.create_test_MenuItem.Name = "create_test_MenuItem";
            this.create_test_MenuItem.Size = new System.Drawing.Size(214, 24);
            this.create_test_MenuItem.Text = "Создать новый тест";
            this.create_test_MenuItem.Click += new System.EventHandler(this.Create_test_MenuItem_Click);
            // 
            // edit_test_MenuItem
            // 
            this.edit_test_MenuItem.Name = "edit_test_MenuItem";
            this.edit_test_MenuItem.Size = new System.Drawing.Size(214, 24);
            this.edit_test_MenuItem.Text = "Редактировать тест";
            // 
            // create_version_questions_groupBox
            // 
            this.create_version_questions_groupBox.Controls.Add(this.checkBox3);
            this.create_version_questions_groupBox.Controls.Add(this.checkBox2);
            this.create_version_questions_groupBox.Controls.Add(this.checkBox1);
            this.create_version_questions_groupBox.Controls.Add(this.answer_textBox3);
            this.create_version_questions_groupBox.Controls.Add(this.answer_textBox2);
            this.create_version_questions_groupBox.Controls.Add(this.answer_textBox1);
            this.create_version_questions_groupBox.Controls.Add(this.question_textBox);
            this.create_version_questions_groupBox.Controls.Add(this.question_type_comboBox);
            this.create_version_questions_groupBox.Location = new System.Drawing.Point(6, 18);
            this.create_version_questions_groupBox.Name = "create_version_questions_groupBox";
            this.create_version_questions_groupBox.Size = new System.Drawing.Size(805, 403);
            this.create_version_questions_groupBox.TabIndex = 4;
            this.create_version_questions_groupBox.TabStop = false;
            this.create_version_questions_groupBox.Visible = false;
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Location = new System.Drawing.Point(625, 331);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(174, 25);
            this.checkBox3.TabIndex = 9;
            this.checkBox3.Text = "Правильный ответ?";
            this.checkBox3.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(625, 273);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(174, 25);
            this.checkBox2.TabIndex = 8;
            this.checkBox2.Text = "Правильный ответ?";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(625, 216);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(174, 25);
            this.checkBox1.TabIndex = 7;
            this.checkBox1.Text = "Правильный ответ?";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // answer_textBox3
            // 
            this.answer_textBox3.Location = new System.Drawing.Point(6, 329);
            this.answer_textBox3.Name = "answer_textBox3";
            this.answer_textBox3.Size = new System.Drawing.Size(601, 28);
            this.answer_textBox3.TabIndex = 5;
            this.answer_textBox3.Text = "Введите текст варианта ответа";
            // 
            // answer_textBox2
            // 
            this.answer_textBox2.Location = new System.Drawing.Point(6, 271);
            this.answer_textBox2.Name = "answer_textBox2";
            this.answer_textBox2.Size = new System.Drawing.Size(601, 28);
            this.answer_textBox2.TabIndex = 4;
            this.answer_textBox2.Text = "Введите текст варианта ответа";
            // 
            // answer_textBox1
            // 
            this.answer_textBox1.Location = new System.Drawing.Point(6, 214);
            this.answer_textBox1.Name = "answer_textBox1";
            this.answer_textBox1.Size = new System.Drawing.Size(601, 28);
            this.answer_textBox1.TabIndex = 3;
            this.answer_textBox1.Text = "Введите текст варианта ответа";
            // 
            // question_textBox
            // 
            this.question_textBox.Location = new System.Drawing.Point(6, 91);
            this.question_textBox.Multiline = true;
            this.question_textBox.Name = "question_textBox";
            this.question_textBox.Size = new System.Drawing.Size(793, 86);
            this.question_textBox.TabIndex = 1;
            this.question_textBox.Text = "Введите текст вопроса";
            // 
            // question_type_comboBox
            // 
            this.question_type_comboBox.FormattingEnabled = true;
            this.question_type_comboBox.Items.AddRange(new object[] {
            "Вопрос с одним правильным вариантом ответа",
            "Вопрос с несколькими правильными вариантами ответа"});
            this.question_type_comboBox.Location = new System.Drawing.Point(6, 38);
            this.question_type_comboBox.Name = "question_type_comboBox";
            this.question_type_comboBox.Size = new System.Drawing.Size(549, 29);
            this.question_type_comboBox.TabIndex = 0;
            this.question_type_comboBox.Text = "Выберите тип вопроса";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(559, 439);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(252, 40);
            this.button1.TabIndex = 3;
            this.button1.Text = "Завершить тест";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // create_test_groupBox
            // 
            this.create_test_groupBox.Controls.Add(this.label2);
            this.create_test_groupBox.Controls.Add(this.add_new_test_to_db);
            this.create_test_groupBox.Controls.Add(this.new_timer_textBox);
            this.create_test_groupBox.Controls.Add(this.new_test_name_textBox);
            this.create_test_groupBox.Location = new System.Drawing.Point(6, 18);
            this.create_test_groupBox.Name = "create_test_groupBox";
            this.create_test_groupBox.Size = new System.Drawing.Size(805, 242);
            this.create_test_groupBox.TabIndex = 3;
            this.create_test_groupBox.TabStop = false;
            this.create_test_groupBox.Text = "Создание теста";
            this.create_test_groupBox.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(2, 91);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(334, 21);
            this.label2.TabIndex = 4;
            this.label2.Text = "Введите ограничение по времени в минутах:";
            // 
            // add_new_test_to_db
            // 
            this.add_new_test_to_db.Location = new System.Drawing.Point(530, 183);
            this.add_new_test_to_db.Name = "add_new_test_to_db";
            this.add_new_test_to_db.Size = new System.Drawing.Size(252, 40);
            this.add_new_test_to_db.TabIndex = 2;
            this.add_new_test_to_db.Text = "Создать тест";
            this.add_new_test_to_db.UseVisualStyleBackColor = true;
            this.add_new_test_to_db.Click += new System.EventHandler(this.Add_new_test_to_db_Click);
            // 
            // new_timer_textBox
            // 
            this.new_timer_textBox.Location = new System.Drawing.Point(6, 125);
            this.new_timer_textBox.Name = "new_timer_textBox";
            this.new_timer_textBox.Size = new System.Drawing.Size(125, 28);
            this.new_timer_textBox.TabIndex = 1;
            this.new_timer_textBox.TextChanged += new System.EventHandler(this.New_timer_textBox_TextChanged);
            this.new_timer_textBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.New_timer_textBox_KeyPress);
            // 
            // new_test_name_textBox
            // 
            this.new_test_name_textBox.Location = new System.Drawing.Point(6, 38);
            this.new_test_name_textBox.Name = "new_test_name_textBox";
            this.new_test_name_textBox.Size = new System.Drawing.Size(776, 28);
            this.new_test_name_textBox.TabIndex = 0;
            this.new_test_name_textBox.Text = "Введите название теста";
            this.new_test_name_textBox.Click += new System.EventHandler(this.New_test_name_textBox_Click);
            this.new_test_name_textBox.TextChanged += new System.EventHandler(this.New_test_name_textBox_TextChanged);
            this.new_test_name_textBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.New_test_name_textBox_KeyPress);
            this.new_test_name_textBox.Leave += new System.EventHandler(this.New_test_name_textBox_Leave);
            // 
            // create_next_version
            // 
            this.create_next_version.Location = new System.Drawing.Point(288, 439);
            this.create_next_version.Name = "create_next_version";
            this.create_next_version.Size = new System.Drawing.Size(252, 40);
            this.create_next_version.TabIndex = 2;
            this.create_next_version.Text = "Следующий вариант";
            this.create_next_version.UseVisualStyleBackColor = true;
            this.create_next_version.Click += new System.EventHandler(this.Create_next_version_Click);
            // 
            // add_new_version_button
            // 
            this.add_new_version_button.Location = new System.Drawing.Point(12, 439);
            this.add_new_version_button.Name = "add_new_version_button";
            this.add_new_version_button.Size = new System.Drawing.Size(252, 40);
            this.add_new_version_button.TabIndex = 1;
            this.add_new_version_button.Text = "Следующий вопрос";
            this.add_new_version_button.UseVisualStyleBackColor = true;
            this.add_new_version_button.Click += new System.EventHandler(this.Add_next_question_button_Click);
            // 
            // profile_page
            // 
            this.profile_page.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.profile_page.Controls.Add(this.teacher_identity_check_panel);
            this.profile_page.Controls.Add(this.change_teacher_login_panel);
            this.profile_page.Controls.Add(this.change_teacher_password_panel);
            this.profile_page.Controls.Add(this.teacher_profile_main_panel);
            this.profile_page.Location = new System.Drawing.Point(4, 30);
            this.profile_page.Name = "profile_page";
            this.profile_page.Padding = new System.Windows.Forms.Padding(3);
            this.profile_page.Size = new System.Drawing.Size(858, 569);
            this.profile_page.TabIndex = 0;
            this.profile_page.Text = "Профиль";
            // 
            // teacher_identity_check_panel
            // 
            this.teacher_identity_check_panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.teacher_identity_check_panel.Controls.Add(this.label1);
            this.teacher_identity_check_panel.Controls.Add(this.cancel_teacher_change_password_button);
            this.teacher_identity_check_panel.Controls.Add(this.teacher_old_password_textBox);
            this.teacher_identity_check_panel.Controls.Add(this.continue_teacher_change_password_button);
            this.teacher_identity_check_panel.Location = new System.Drawing.Point(218, 23);
            this.teacher_identity_check_panel.Name = "teacher_identity_check_panel";
            this.teacher_identity_check_panel.Size = new System.Drawing.Size(357, 173);
            this.teacher_identity_check_panel.TabIndex = 12;
            this.teacher_identity_check_panel.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(184, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "Введите старый пароль:";
            // 
            // cancel_teacher_change_password_button
            // 
            this.cancel_teacher_change_password_button.BackColor = System.Drawing.Color.MintCream;
            this.cancel_teacher_change_password_button.FlatAppearance.BorderColor = System.Drawing.Color.MintCream;
            this.cancel_teacher_change_password_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cancel_teacher_change_password_button.Location = new System.Drawing.Point(86, 116);
            this.cancel_teacher_change_password_button.Name = "cancel_teacher_change_password_button";
            this.cancel_teacher_change_password_button.Size = new System.Drawing.Size(90, 36);
            this.cancel_teacher_change_password_button.TabIndex = 10;
            this.cancel_teacher_change_password_button.Text = "Отмена";
            this.cancel_teacher_change_password_button.UseVisualStyleBackColor = false;
            this.cancel_teacher_change_password_button.Click += new System.EventHandler(this.Cancel_teacher_change_password_button_Click);
            // 
            // teacher_old_password_textBox
            // 
            this.teacher_old_password_textBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.teacher_old_password_textBox.Location = new System.Drawing.Point(23, 63);
            this.teacher_old_password_textBox.Name = "teacher_old_password_textBox";
            this.teacher_old_password_textBox.Size = new System.Drawing.Size(311, 28);
            this.teacher_old_password_textBox.TabIndex = 1;
            // 
            // continue_teacher_change_password_button
            // 
            this.continue_teacher_change_password_button.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.continue_teacher_change_password_button.FlatAppearance.BorderColor = System.Drawing.Color.DarkSeaGreen;
            this.continue_teacher_change_password_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.continue_teacher_change_password_button.Location = new System.Drawing.Point(182, 116);
            this.continue_teacher_change_password_button.Name = "continue_teacher_change_password_button";
            this.continue_teacher_change_password_button.Size = new System.Drawing.Size(152, 36);
            this.continue_teacher_change_password_button.TabIndex = 9;
            this.continue_teacher_change_password_button.Text = "Продолжить";
            this.continue_teacher_change_password_button.UseVisualStyleBackColor = false;
            this.continue_teacher_change_password_button.Click += new System.EventHandler(this.Continue_teacher_change_password_button_Click);
            // 
            // change_teacher_login_panel
            // 
            this.change_teacher_login_panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.change_teacher_login_panel.Controls.Add(this.cancel_change_teacher_login_button);
            this.change_teacher_login_panel.Controls.Add(this.new_teacher_login_button);
            this.change_teacher_login_panel.Controls.Add(this.new_teacher_login_textbox);
            this.change_teacher_login_panel.Controls.Add(this.change_teacher_login_label);
            this.change_teacher_login_panel.Location = new System.Drawing.Point(6, 23);
            this.change_teacher_login_panel.Name = "change_teacher_login_panel";
            this.change_teacher_login_panel.Size = new System.Drawing.Size(805, 194);
            this.change_teacher_login_panel.TabIndex = 10;
            this.change_teacher_login_panel.Visible = false;
            // 
            // cancel_change_teacher_login_button
            // 
            this.cancel_change_teacher_login_button.Location = new System.Drawing.Point(474, 122);
            this.cancel_change_teacher_login_button.Name = "cancel_change_teacher_login_button";
            this.cancel_change_teacher_login_button.Size = new System.Drawing.Size(99, 42);
            this.cancel_change_teacher_login_button.TabIndex = 3;
            this.cancel_change_teacher_login_button.Text = "Отмена";
            this.cancel_change_teacher_login_button.UseVisualStyleBackColor = true;
            this.cancel_change_teacher_login_button.Click += new System.EventHandler(this.Cancel_change_teacher_login_button_Click);
            // 
            // new_teacher_login_button
            // 
            this.new_teacher_login_button.Location = new System.Drawing.Point(579, 122);
            this.new_teacher_login_button.Name = "new_teacher_login_button";
            this.new_teacher_login_button.Size = new System.Drawing.Size(184, 42);
            this.new_teacher_login_button.TabIndex = 2;
            this.new_teacher_login_button.Text = "Изменить логин";
            this.new_teacher_login_button.UseVisualStyleBackColor = true;
            this.new_teacher_login_button.Click += new System.EventHandler(this.New_teacher_login_button_Click);
            // 
            // new_teacher_login_textbox
            // 
            this.new_teacher_login_textbox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.new_teacher_login_textbox.Location = new System.Drawing.Point(303, 40);
            this.new_teacher_login_textbox.Name = "new_teacher_login_textbox";
            this.new_teacher_login_textbox.Size = new System.Drawing.Size(460, 28);
            this.new_teacher_login_textbox.TabIndex = 1;
            // 
            // change_teacher_login_label
            // 
            this.change_teacher_login_label.AutoSize = true;
            this.change_teacher_login_label.Location = new System.Drawing.Point(57, 47);
            this.change_teacher_login_label.Name = "change_teacher_login_label";
            this.change_teacher_login_label.Size = new System.Drawing.Size(169, 21);
            this.change_teacher_login_label.TabIndex = 0;
            this.change_teacher_login_label.Text = "Введите новый логин:";
            // 
            // change_teacher_password_panel
            // 
            this.change_teacher_password_panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.change_teacher_password_panel.Controls.Add(this.label3);
            this.change_teacher_password_panel.Controls.Add(this.new_teacher_password_button);
            this.change_teacher_password_panel.Controls.Add(this.label4);
            this.change_teacher_password_panel.Controls.Add(this.cancel_change_teacher_password_button);
            this.change_teacher_password_panel.Controls.Add(this.teacher_new_password_textBox);
            this.change_teacher_password_panel.Controls.Add(this.teacher_test_new_password_textBox);
            this.change_teacher_password_panel.Location = new System.Drawing.Point(6, 23);
            this.change_teacher_password_panel.Name = "change_teacher_password_panel";
            this.change_teacher_password_panel.Size = new System.Drawing.Size(805, 250);
            this.change_teacher_password_panel.TabIndex = 10;
            this.change_teacher_password_panel.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 62);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(179, 21);
            this.label3.TabIndex = 4;
            this.label3.Text = "Введите новый пароль:";
            // 
            // new_teacher_password_button
            // 
            this.new_teacher_password_button.Location = new System.Drawing.Point(579, 198);
            this.new_teacher_password_button.Name = "new_teacher_password_button";
            this.new_teacher_password_button.Size = new System.Drawing.Size(184, 42);
            this.new_teacher_password_button.TabIndex = 2;
            this.new_teacher_password_button.Text = "Изменить пароль";
            this.new_teacher_password_button.UseVisualStyleBackColor = true;
            this.new_teacher_password_button.Click += new System.EventHandler(this.New_teacher_password_button_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 141);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(253, 21);
            this.label4.TabIndex = 6;
            this.label4.Text = "Повторно введите новый пароль:";
            // 
            // cancel_change_teacher_password_button
            // 
            this.cancel_change_teacher_password_button.Location = new System.Drawing.Point(475, 198);
            this.cancel_change_teacher_password_button.Name = "cancel_change_teacher_password_button";
            this.cancel_change_teacher_password_button.Size = new System.Drawing.Size(89, 42);
            this.cancel_change_teacher_password_button.TabIndex = 3;
            this.cancel_change_teacher_password_button.Text = "Отмена";
            this.cancel_change_teacher_password_button.UseVisualStyleBackColor = true;
            this.cancel_change_teacher_password_button.Click += new System.EventHandler(this.Cancel_change_teacher_password_button_Click);
            // 
            // teacher_new_password_textBox
            // 
            this.teacher_new_password_textBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.teacher_new_password_textBox.Location = new System.Drawing.Point(303, 55);
            this.teacher_new_password_textBox.Name = "teacher_new_password_textBox";
            this.teacher_new_password_textBox.Size = new System.Drawing.Size(460, 28);
            this.teacher_new_password_textBox.TabIndex = 5;
            // 
            // teacher_test_new_password_textBox
            // 
            this.teacher_test_new_password_textBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.teacher_test_new_password_textBox.Location = new System.Drawing.Point(303, 134);
            this.teacher_test_new_password_textBox.Name = "teacher_test_new_password_textBox";
            this.teacher_test_new_password_textBox.Size = new System.Drawing.Size(460, 28);
            this.teacher_test_new_password_textBox.TabIndex = 7;
            // 
            // teacher_profile_main_panel
            // 
            this.teacher_profile_main_panel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.teacher_profile_main_panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.teacher_profile_main_panel.Controls.Add(this.button2);
            this.teacher_profile_main_panel.Controls.Add(this.exit_student_profile_button);
            this.teacher_profile_main_panel.Controls.Add(this.change_teacher_password_button);
            this.teacher_profile_main_panel.Controls.Add(this.change_teacher_login_button);
            this.teacher_profile_main_panel.Controls.Add(this.password_teacher_profile);
            this.teacher_profile_main_panel.Controls.Add(this.full_name_teacher_profile_label);
            this.teacher_profile_main_panel.Controls.Add(this.login_teacher_profile);
            this.teacher_profile_main_panel.Controls.Add(this.full_name_teacher_profile_textBox);
            this.teacher_profile_main_panel.Controls.Add(this.password_teacher_profile_label);
            this.teacher_profile_main_panel.Controls.Add(this.login_teacher_profile_label);
            this.teacher_profile_main_panel.Location = new System.Drawing.Point(6, 23);
            this.teacher_profile_main_panel.Name = "teacher_profile_main_panel";
            this.teacher_profile_main_panel.Size = new System.Drawing.Size(805, 441);
            this.teacher_profile_main_panel.TabIndex = 9;
            // 
            // change_teacher_password_button
            // 
            this.change_teacher_password_button.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.change_teacher_password_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.change_teacher_password_button.Location = new System.Drawing.Point(523, 301);
            this.change_teacher_password_button.Name = "change_teacher_password_button";
            this.change_teacher_password_button.Size = new System.Drawing.Size(201, 39);
            this.change_teacher_password_button.TabIndex = 9;
            this.change_teacher_password_button.Text = "Поменять пароль";
            this.change_teacher_password_button.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.change_teacher_password_button.UseVisualStyleBackColor = true;
            this.change_teacher_password_button.Click += new System.EventHandler(this.Change_teacher_password_button_Click);
            // 
            // change_teacher_login_button
            // 
            this.change_teacher_login_button.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.change_teacher_login_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.change_teacher_login_button.Location = new System.Drawing.Point(523, 207);
            this.change_teacher_login_button.Name = "change_teacher_login_button";
            this.change_teacher_login_button.Size = new System.Drawing.Size(201, 39);
            this.change_teacher_login_button.TabIndex = 8;
            this.change_teacher_login_button.Text = "Поменять логин";
            this.change_teacher_login_button.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.change_teacher_login_button.UseVisualStyleBackColor = true;
            this.change_teacher_login_button.Click += new System.EventHandler(this.Change_teacher_login_button_Click);
            // 
            // password_teacher_profile
            // 
            this.password_teacher_profile.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.password_teacher_profile.Location = new System.Drawing.Point(261, 267);
            this.password_teacher_profile.Name = "password_teacher_profile";
            this.password_teacher_profile.Size = new System.Drawing.Size(463, 28);
            this.password_teacher_profile.TabIndex = 7;
            this.password_teacher_profile.UseSystemPasswordChar = true;
            // 
            // full_name_teacher_profile_label
            // 
            this.full_name_teacher_profile_label.AutoSize = true;
            this.full_name_teacher_profile_label.Location = new System.Drawing.Point(50, 91);
            this.full_name_teacher_profile_label.Name = "full_name_teacher_profile_label";
            this.full_name_teacher_profile_label.Size = new System.Drawing.Size(60, 21);
            this.full_name_teacher_profile_label.TabIndex = 0;
            this.full_name_teacher_profile_label.Text = "Ф.И.О.:";
            // 
            // login_teacher_profile
            // 
            this.login_teacher_profile.Location = new System.Drawing.Point(261, 173);
            this.login_teacher_profile.Name = "login_teacher_profile";
            this.login_teacher_profile.Size = new System.Drawing.Size(463, 28);
            this.login_teacher_profile.TabIndex = 6;
            // 
            // full_name_teacher_profile_textBox
            // 
            this.full_name_teacher_profile_textBox.Location = new System.Drawing.Point(261, 84);
            this.full_name_teacher_profile_textBox.Name = "full_name_teacher_profile_textBox";
            this.full_name_teacher_profile_textBox.Size = new System.Drawing.Size(463, 28);
            this.full_name_teacher_profile_textBox.TabIndex = 2;
            this.full_name_teacher_profile_textBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Full_name_teacher_profile_texBox_KeyPress);
            // 
            // password_teacher_profile_label
            // 
            this.password_teacher_profile_label.AutoSize = true;
            this.password_teacher_profile_label.Location = new System.Drawing.Point(50, 274);
            this.password_teacher_profile_label.Name = "password_teacher_profile_label";
            this.password_teacher_profile_label.Size = new System.Drawing.Size(69, 21);
            this.password_teacher_profile_label.TabIndex = 5;
            this.password_teacher_profile_label.Text = "Пароль:";
            // 
            // login_teacher_profile_label
            // 
            this.login_teacher_profile_label.AutoSize = true;
            this.login_teacher_profile_label.Location = new System.Drawing.Point(50, 180);
            this.login_teacher_profile_label.Name = "login_teacher_profile_label";
            this.login_teacher_profile_label.Size = new System.Drawing.Size(58, 21);
            this.login_teacher_profile_label.TabIndex = 4;
            this.login_teacher_profile_label.Text = "Логин:";
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.profile_page);
            this.tabControl1.Controls.Add(this.test_constructor);
            this.tabControl1.Font = new System.Drawing.Font("Calibri", 10.2F);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(866, 603);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.Click += new System.EventHandler(this.TabControl1_Click);
            // 
            // exit_student_profile_button
            // 
            this.exit_student_profile_button.AutoSize = true;
            this.exit_student_profile_button.BackColor = System.Drawing.SystemColors.Control;
            this.exit_student_profile_button.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.exit_student_profile_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.exit_student_profile_button.Font = new System.Drawing.Font("Calibri", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.exit_student_profile_button.ForeColor = System.Drawing.SystemColors.ControlText;
            this.exit_student_profile_button.Location = new System.Drawing.Point(42, 397);
            this.exit_student_profile_button.Name = "exit_student_profile_button";
            this.exit_student_profile_button.Size = new System.Drawing.Size(158, 39);
            this.exit_student_profile_button.TabIndex = 11;
            this.exit_student_profile_button.Text = "Выйти из профиля";
            this.exit_student_profile_button.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.exit_student_profile_button.UseVisualStyleBackColor = false;
            this.exit_student_profile_button.Click += new System.EventHandler(this.exit_student_profile_button_Click);
            // 
            // button2
            // 
            this.button2.AutoSize = true;
            this.button2.BackColor = System.Drawing.SystemColors.Control;
            this.button2.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Calibri", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button2.Location = new System.Drawing.Point(605, 397);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(158, 39);
            this.button2.TabIndex = 12;
            this.button2.Text = "Удалить профиль";
            this.button2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button2.UseVisualStyleBackColor = false;
            // 
            // TeacherForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(829, 523);
            this.Controls.Add(this.tabControl1);
            this.MinimumSize = new System.Drawing.Size(847, 570);
            this.Name = "TeacherForm";
            this.Text = "TeacherForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.TeacherForm_FormClosed);
            this.Load += new System.EventHandler(this.TeacherForm_Load);
            this.test_constructor.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.create_version_questions_groupBox.ResumeLayout(false);
            this.create_version_questions_groupBox.PerformLayout();
            this.create_test_groupBox.ResumeLayout(false);
            this.create_test_groupBox.PerformLayout();
            this.profile_page.ResumeLayout(false);
            this.teacher_identity_check_panel.ResumeLayout(false);
            this.teacher_identity_check_panel.PerformLayout();
            this.change_teacher_login_panel.ResumeLayout(false);
            this.change_teacher_login_panel.PerformLayout();
            this.change_teacher_password_panel.ResumeLayout(false);
            this.change_teacher_password_panel.PerformLayout();
            this.teacher_profile_main_panel.ResumeLayout(false);
            this.teacher_profile_main_panel.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage test_constructor;
        private System.Windows.Forms.TabPage profile_page;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.Panel teacher_profile_main_panel;
        private System.Windows.Forms.Button change_teacher_password_button;
        private System.Windows.Forms.Button change_teacher_login_button;
        private System.Windows.Forms.TextBox password_teacher_profile;
        private System.Windows.Forms.Label full_name_teacher_profile_label;
        private System.Windows.Forms.TextBox login_teacher_profile;
        private System.Windows.Forms.TextBox full_name_teacher_profile_textBox;
        private System.Windows.Forms.Label password_teacher_profile_label;
        private System.Windows.Forms.Label login_teacher_profile_label;
        private System.Windows.Forms.Panel change_teacher_login_panel;
        private System.Windows.Forms.Button cancel_change_teacher_login_button;
        private System.Windows.Forms.Button new_teacher_login_button;
        private System.Windows.Forms.TextBox new_teacher_login_textbox;
        private System.Windows.Forms.Label change_teacher_login_label;
        private System.Windows.Forms.Panel teacher_identity_check_panel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button cancel_teacher_change_password_button;
        private System.Windows.Forms.TextBox teacher_old_password_textBox;
        private System.Windows.Forms.Button continue_teacher_change_password_button;
        private System.Windows.Forms.Panel change_teacher_password_panel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button new_teacher_password_button;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button cancel_change_teacher_password_button;
        private System.Windows.Forms.TextBox teacher_new_password_textBox;
        private System.Windows.Forms.TextBox teacher_test_new_password_textBox;
        private System.Windows.Forms.GroupBox create_test_groupBox;
        private System.Windows.Forms.TextBox new_timer_textBox;
        private System.Windows.Forms.TextBox new_test_name_textBox;
        private System.Windows.Forms.Button add_new_version_button;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button add_new_test_to_db;
        private System.Windows.Forms.Button create_next_version;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem edit_test_MenuItem;
        private System.Windows.Forms.ToolStripMenuItem create_test_MenuItem;
        private System.Windows.Forms.GroupBox create_version_questions_groupBox;
        private System.Windows.Forms.ComboBox question_type_comboBox;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.TextBox answer_textBox3;
        private System.Windows.Forms.TextBox answer_textBox2;
        private System.Windows.Forms.TextBox answer_textBox1;
        private System.Windows.Forms.TextBox question_textBox;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button exit_student_profile_button;
    }
}
namespace testing_program
{
    partial class RegistrationForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RegistrationForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.enter_button = new System.Windows.Forms.Button();
            this.sign_in_password = new System.Windows.Forms.TextBox();
            this.sign_in_login = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.login_registration = new System.Windows.Forms.TextBox();
            this.password_registration = new System.Windows.Forms.TextBox();
            this.test_password_registration = new System.Windows.Forms.TextBox();
            this.sign_up_button = new System.Windows.Forms.Button();
            this.choose_role = new System.Windows.Forms.ComboBox();
            this.teacher_name = new System.Windows.Forms.TextBox();
            this.select_group_registration = new System.Windows.Forms.ComboBox();
            this.select_full_name_registration = new System.Windows.Forms.ComboBox();
            this.sign_in_button = new System.Windows.Forms.Button();
            this.registration_button = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.Honeydew;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.checkBox2);
            this.panel1.Controls.Add(this.enter_button);
            this.panel1.Controls.Add(this.sign_in_password);
            this.panel1.Controls.Add(this.sign_in_login);
            this.panel1.Location = new System.Drawing.Point(0, 47);
            this.panel1.MinimumSize = new System.Drawing.Size(405, 392);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(405, 392);
            this.panel1.TabIndex = 1;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Font = new System.Drawing.Font("Calibri", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkBox2.Location = new System.Drawing.Point(21, 191);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(154, 25);
            this.checkBox2.TabIndex = 11;
            this.checkBox2.Text = "Показать пароль";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // enter_button
            // 
            this.enter_button.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.enter_button.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.enter_button.FlatAppearance.BorderColor = System.Drawing.Color.DarkSeaGreen;
            this.enter_button.FlatAppearance.BorderSize = 2;
            this.enter_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.enter_button.Font = new System.Drawing.Font("Calibri", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.enter_button.ForeColor = System.Drawing.Color.White;
            this.enter_button.Location = new System.Drawing.Point(21, 322);
            this.enter_button.Name = "enter_button";
            this.enter_button.Size = new System.Drawing.Size(363, 46);
            this.enter_button.TabIndex = 2;
            this.enter_button.Text = "ВОЙТИ";
            this.enter_button.UseVisualStyleBackColor = false;
            this.enter_button.Click += new System.EventHandler(this.enter_button_Click);
            // 
            // sign_in_password
            // 
            this.sign_in_password.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sign_in_password.BackColor = System.Drawing.Color.MintCream;
            this.sign_in_password.Font = new System.Drawing.Font("Calibri", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.sign_in_password.Location = new System.Drawing.Point(21, 143);
            this.sign_in_password.MinimumSize = new System.Drawing.Size(228, 35);
            this.sign_in_password.Name = "sign_in_password";
            this.sign_in_password.Size = new System.Drawing.Size(363, 29);
            this.sign_in_password.TabIndex = 1;
            this.sign_in_password.Text = "Введите пароль";
            this.sign_in_password.Click += new System.EventHandler(this.sign_in_password_Click);
            this.sign_in_password.TextChanged += new System.EventHandler(this.sign_in_password_TextChanged);
            this.sign_in_password.Leave += new System.EventHandler(this.sign_in_password_Leave);
            // 
            // sign_in_login
            // 
            this.sign_in_login.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sign_in_login.BackColor = System.Drawing.Color.MintCream;
            this.sign_in_login.Font = new System.Drawing.Font("Calibri", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.sign_in_login.Location = new System.Drawing.Point(20, 72);
            this.sign_in_login.MinimumSize = new System.Drawing.Size(228, 35);
            this.sign_in_login.Name = "sign_in_login";
            this.sign_in_login.Size = new System.Drawing.Size(363, 29);
            this.sign_in_login.TabIndex = 0;
            this.sign_in_login.Text = "Введите логин";
            this.sign_in_login.Click += new System.EventHandler(this.sign_in_login_Click);
            this.sign_in_login.TextChanged += new System.EventHandler(this.sign_in_login_TextChanged);
            this.sign_in_login.Leave += new System.EventHandler(this.sign_in_login_Leave);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.Honeydew;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Controls.Add(this.sign_up_button);
            this.panel2.Controls.Add(this.choose_role);
            this.panel2.Controls.Add(this.select_group_registration);
            this.panel2.Controls.Add(this.select_full_name_registration);
            this.panel2.Controls.Add(this.teacher_name);
            this.panel2.Location = new System.Drawing.Point(0, 47);
            this.panel2.MinimumSize = new System.Drawing.Size(405, 392);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(405, 392);
            this.panel2.TabIndex = 2;
            this.panel2.Visible = false;
            this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.Controls.Add(this.checkBox1);
            this.panel3.Controls.Add(this.login_registration);
            this.panel3.Controls.Add(this.password_registration);
            this.panel3.Controls.Add(this.test_password_registration);
            this.panel3.Location = new System.Drawing.Point(21, 178);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(371, 151);
            this.panel3.TabIndex = 10;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Font = new System.Drawing.Font("Calibri", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkBox1.Location = new System.Drawing.Point(0, 123);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(154, 25);
            this.checkBox1.TabIndex = 7;
            this.checkBox1.Text = "Показать пароль";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // login_registration
            // 
            this.login_registration.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.login_registration.BackColor = System.Drawing.Color.MintCream;
            this.login_registration.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.login_registration.Font = new System.Drawing.Font("Calibri", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.login_registration.Location = new System.Drawing.Point(0, 0);
            this.login_registration.MinimumSize = new System.Drawing.Size(363, 28);
            this.login_registration.Name = "login_registration";
            this.login_registration.Size = new System.Drawing.Size(363, 28);
            this.login_registration.TabIndex = 0;
            this.login_registration.Text = "Введите логин";
            this.login_registration.Visible = false;
            this.login_registration.Click += new System.EventHandler(this.login_registration_Click);
            this.login_registration.TextChanged += new System.EventHandler(this.login_registration_TextChanged);
            this.login_registration.Leave += new System.EventHandler(this.login_registration_Leave);
            // 
            // password_registration
            // 
            this.password_registration.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.password_registration.BackColor = System.Drawing.Color.MintCream;
            this.password_registration.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.password_registration.Font = new System.Drawing.Font("Calibri", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.password_registration.Location = new System.Drawing.Point(0, 44);
            this.password_registration.MinimumSize = new System.Drawing.Size(363, 28);
            this.password_registration.Name = "password_registration";
            this.password_registration.Size = new System.Drawing.Size(363, 28);
            this.password_registration.TabIndex = 5;
            this.password_registration.Text = "Введите пароль";
            this.password_registration.Visible = false;
            this.password_registration.Click += new System.EventHandler(this.password_registration_Click);
            this.password_registration.TextChanged += new System.EventHandler(this.password_registration_TextChanged);
            this.password_registration.Leave += new System.EventHandler(this.password_registration_Leave);
            // 
            // test_password_registration
            // 
            this.test_password_registration.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.test_password_registration.BackColor = System.Drawing.Color.MintCream;
            this.test_password_registration.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.test_password_registration.Font = new System.Drawing.Font("Calibri", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.test_password_registration.Location = new System.Drawing.Point(0, 89);
            this.test_password_registration.MinimumSize = new System.Drawing.Size(363, 28);
            this.test_password_registration.Name = "test_password_registration";
            this.test_password_registration.Size = new System.Drawing.Size(363, 28);
            this.test_password_registration.TabIndex = 6;
            this.test_password_registration.Text = "Повторно введите пароль";
            this.test_password_registration.Visible = false;
            this.test_password_registration.Click += new System.EventHandler(this.test_password_registration_Click);
            this.test_password_registration.TextChanged += new System.EventHandler(this.test_password_registration_TextChanged);
            this.test_password_registration.Leave += new System.EventHandler(this.test_password_registration_Leave);
            // 
            // sign_up_button
            // 
            this.sign_up_button.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sign_up_button.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.sign_up_button.FlatAppearance.BorderColor = System.Drawing.Color.DarkSeaGreen;
            this.sign_up_button.FlatAppearance.BorderSize = 2;
            this.sign_up_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.sign_up_button.Font = new System.Drawing.Font("Calibri", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.sign_up_button.ForeColor = System.Drawing.Color.White;
            this.sign_up_button.Location = new System.Drawing.Point(20, 335);
            this.sign_up_button.Name = "sign_up_button";
            this.sign_up_button.Size = new System.Drawing.Size(363, 46);
            this.sign_up_button.TabIndex = 7;
            this.sign_up_button.Text = "РЕГИСТРАЦИЯ";
            this.sign_up_button.UseVisualStyleBackColor = false;
            this.sign_up_button.Click += new System.EventHandler(this.sign_up_button_Click);
            // 
            // choose_role
            // 
            this.choose_role.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.choose_role.BackColor = System.Drawing.Color.MintCream;
            this.choose_role.Font = new System.Drawing.Font("Calibri", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.choose_role.FormattingEnabled = true;
            this.choose_role.Items.AddRange(new object[] {
            "Преподаватель",
            "Студент"});
            this.choose_role.Location = new System.Drawing.Point(21, 37);
            this.choose_role.Name = "choose_role";
            this.choose_role.Size = new System.Drawing.Size(362, 29);
            this.choose_role.TabIndex = 8;
            this.choose_role.Text = "Выберите роль";
            this.choose_role.SelectedIndexChanged += new System.EventHandler(this.choose_role_SelectedIndexChanged);
            this.choose_role.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.choose_role_KeyPress);
            // 
            // teacher_name
            // 
            this.teacher_name.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.teacher_name.BackColor = System.Drawing.Color.MintCream;
            this.teacher_name.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.teacher_name.Font = new System.Drawing.Font("Calibri", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.teacher_name.Location = new System.Drawing.Point(21, 84);
            this.teacher_name.MinimumSize = new System.Drawing.Size(363, 28);
            this.teacher_name.Name = "teacher_name";
            this.teacher_name.Size = new System.Drawing.Size(363, 28);
            this.teacher_name.TabIndex = 9;
            this.teacher_name.Text = "Введите ФИО";
            this.teacher_name.Visible = false;
            this.teacher_name.Click += new System.EventHandler(this.teacher_name_Click);
            this.teacher_name.TextChanged += new System.EventHandler(this.teacher_name_TextChanged);
            this.teacher_name.Leave += new System.EventHandler(this.teacher_name_Leave);
            // 
            // select_group_registration
            // 
            this.select_group_registration.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.select_group_registration.BackColor = System.Drawing.Color.MintCream;
            this.select_group_registration.Font = new System.Drawing.Font("Calibri", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.select_group_registration.FormattingEnabled = true;
            this.select_group_registration.ItemHeight = 21;
            this.select_group_registration.Location = new System.Drawing.Point(21, 84);
            this.select_group_registration.MinimumSize = new System.Drawing.Size(332, 0);
            this.select_group_registration.Name = "select_group_registration";
            this.select_group_registration.Size = new System.Drawing.Size(363, 29);
            this.select_group_registration.TabIndex = 3;
            this.select_group_registration.Text = "Выберите группу из списка";
            this.select_group_registration.Visible = false;
            this.select_group_registration.SelectedIndexChanged += new System.EventHandler(this.select_group_registration_SelectedIndexChanged);
            this.select_group_registration.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.select_group_registration_KeyPress);
            // 
            // select_full_name_registration
            // 
            this.select_full_name_registration.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.select_full_name_registration.BackColor = System.Drawing.Color.MintCream;
            this.select_full_name_registration.Font = new System.Drawing.Font("Calibri", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.select_full_name_registration.ForeColor = System.Drawing.SystemColors.WindowText;
            this.select_full_name_registration.FormattingEnabled = true;
            this.select_full_name_registration.ItemHeight = 21;
            this.select_full_name_registration.Location = new System.Drawing.Point(21, 131);
            this.select_full_name_registration.MinimumSize = new System.Drawing.Size(363, 0);
            this.select_full_name_registration.Name = "select_full_name_registration";
            this.select_full_name_registration.Size = new System.Drawing.Size(363, 29);
            this.select_full_name_registration.TabIndex = 4;
            this.select_full_name_registration.Text = "Выберите свое имя из списка группы";
            this.select_full_name_registration.Visible = false;
            this.select_full_name_registration.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.select_full_name_registration_KeyPress);
            // 
            // sign_in_button
            // 
            this.sign_in_button.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.sign_in_button.FlatAppearance.BorderColor = System.Drawing.Color.DarkSeaGreen;
            this.sign_in_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.sign_in_button.Font = new System.Drawing.Font("Calibri", 10.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.sign_in_button.ForeColor = System.Drawing.Color.White;
            this.sign_in_button.Location = new System.Drawing.Point(68, 11);
            this.sign_in_button.Name = "sign_in_button";
            this.sign_in_button.Size = new System.Drawing.Size(102, 36);
            this.sign_in_button.TabIndex = 3;
            this.sign_in_button.Text = "ВОЙТИ";
            this.sign_in_button.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.sign_in_button.UseVisualStyleBackColor = false;
            this.sign_in_button.Click += new System.EventHandler(this.sign_in_button_Click);
            // 
            // registration_button
            // 
            this.registration_button.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.registration_button.FlatAppearance.BorderColor = System.Drawing.Color.DarkSeaGreen;
            this.registration_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.registration_button.Font = new System.Drawing.Font("Calibri", 10.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.registration_button.ForeColor = System.Drawing.Color.White;
            this.registration_button.Location = new System.Drawing.Point(196, 11);
            this.registration_button.Name = "registration_button";
            this.registration_button.Size = new System.Drawing.Size(147, 36);
            this.registration_button.TabIndex = 4;
            this.registration_button.Text = "РЕГИСТРАЦИЯ";
            this.registration_button.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.registration_button.UseVisualStyleBackColor = false;
            this.registration_button.Click += new System.EventHandler(this.registration_button_Click);
            // 
            // RegistrationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.ClientSize = new System.Drawing.Size(405, 441);
            this.Controls.Add(this.sign_in_button);
            this.Controls.Add(this.registration_button);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(403, 441);
            this.Name = "RegistrationForm";
            this.Text = "Регистрация";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button enter_button;
        private System.Windows.Forms.TextBox sign_in_password;
        private System.Windows.Forms.TextBox sign_in_login;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox login_registration;
        private System.Windows.Forms.ComboBox select_group_registration;
        private System.Windows.Forms.Button sign_up_button;
        private System.Windows.Forms.TextBox test_password_registration;
        private System.Windows.Forms.TextBox password_registration;
        private System.Windows.Forms.ComboBox select_full_name_registration;
        private System.Windows.Forms.TextBox teacher_name;
        private System.Windows.Forms.ComboBox choose_role;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button sign_in_button;
        private System.Windows.Forms.Button registration_button;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBox2;
    }
}


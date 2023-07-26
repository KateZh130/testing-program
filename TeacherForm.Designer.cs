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
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.profile_page = new System.Windows.Forms.TabPage();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.teacher_profile_main_panel = new System.Windows.Forms.Panel();
            this.change_teacher_password_button = new System.Windows.Forms.Button();
            this.change_teacher_login_button = new System.Windows.Forms.Button();
            this.password_teacher_profile = new System.Windows.Forms.TextBox();
            this.full_name_teacher_profile_label = new System.Windows.Forms.Label();
            this.login_teacher_profile = new System.Windows.Forms.TextBox();
            this.full_name_teacher_profile = new System.Windows.Forms.TextBox();
            this.password_teacher_profile_label = new System.Windows.Forms.Label();
            this.login_teacher_profile_label = new System.Windows.Forms.Label();
            this.change_teacher_login_panel = new System.Windows.Forms.Panel();
            this.cancel_change_teacher_login_button = new System.Windows.Forms.Button();
            this.new_teacher_login_button = new System.Windows.Forms.Button();
            this.new_teacher_login_textbox = new System.Windows.Forms.TextBox();
            this.change_teacher_login_label = new System.Windows.Forms.Label();
            this.teacher_identity_check_panel = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.cancel_teacher_change_password_button = new System.Windows.Forms.Button();
            this.teacher_old_password_textBox = new System.Windows.Forms.TextBox();
            this.continue_teacher_change_password_button = new System.Windows.Forms.Button();
            this.change_teacher_password_panel = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.new_teacher_password_button = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.cancel_change_teacher_password_button = new System.Windows.Forms.Button();
            this.teacher_new_password_textBox = new System.Windows.Forms.TextBox();
            this.teacher_test_new_password_textBox = new System.Windows.Forms.TextBox();
            this.profile_page.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.teacher_profile_main_panel.SuspendLayout();
            this.change_teacher_login_panel.SuspendLayout();
            this.teacher_identity_check_panel.SuspendLayout();
            this.change_teacher_password_panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 30);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(858, 569);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "?";
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
            this.profile_page.Click += new System.EventHandler(this.profile_page_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.profile_page);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Font = new System.Drawing.Font("Calibri", 10.2F);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(866, 603);
            this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControl1.TabIndex = 0;
            // 
            // teacher_profile_main_panel
            // 
            this.teacher_profile_main_panel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.teacher_profile_main_panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.teacher_profile_main_panel.Controls.Add(this.change_teacher_password_button);
            this.teacher_profile_main_panel.Controls.Add(this.change_teacher_login_button);
            this.teacher_profile_main_panel.Controls.Add(this.password_teacher_profile);
            this.teacher_profile_main_panel.Controls.Add(this.full_name_teacher_profile_label);
            this.teacher_profile_main_panel.Controls.Add(this.login_teacher_profile);
            this.teacher_profile_main_panel.Controls.Add(this.full_name_teacher_profile);
            this.teacher_profile_main_panel.Controls.Add(this.password_teacher_profile_label);
            this.teacher_profile_main_panel.Controls.Add(this.login_teacher_profile_label);
            this.teacher_profile_main_panel.Location = new System.Drawing.Point(6, 23);
            this.teacher_profile_main_panel.Name = "teacher_profile_main_panel";
            this.teacher_profile_main_panel.Size = new System.Drawing.Size(805, 441);
            this.teacher_profile_main_panel.TabIndex = 9;
            // 
            // change_teacher_password_button
            // 
            this.change_teacher_password_button.FlatAppearance.BorderColor = System.Drawing.Color.MintCream;
            this.change_teacher_password_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.change_teacher_password_button.Location = new System.Drawing.Point(523, 301);
            this.change_teacher_password_button.Name = "change_teacher_password_button";
            this.change_teacher_password_button.Size = new System.Drawing.Size(201, 39);
            this.change_teacher_password_button.TabIndex = 9;
            this.change_teacher_password_button.Text = "Поменять пароль";
            this.change_teacher_password_button.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.change_teacher_password_button.UseVisualStyleBackColor = true;
            this.change_teacher_password_button.Click += new System.EventHandler(this.change_teacher_password_button_Click);
            // 
            // change_teacher_login_button
            // 
            this.change_teacher_login_button.FlatAppearance.BorderColor = System.Drawing.Color.MintCream;
            this.change_teacher_login_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.change_teacher_login_button.Location = new System.Drawing.Point(523, 207);
            this.change_teacher_login_button.Name = "change_teacher_login_button";
            this.change_teacher_login_button.Size = new System.Drawing.Size(201, 39);
            this.change_teacher_login_button.TabIndex = 8;
            this.change_teacher_login_button.Text = "Поменять логин";
            this.change_teacher_login_button.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.change_teacher_login_button.UseVisualStyleBackColor = true;
            this.change_teacher_login_button.Click += new System.EventHandler(this.change_teacher_login_button_Click);
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
            // full_name_teacher_profile
            // 
            this.full_name_teacher_profile.Location = new System.Drawing.Point(261, 84);
            this.full_name_teacher_profile.Name = "full_name_teacher_profile";
            this.full_name_teacher_profile.Size = new System.Drawing.Size(463, 28);
            this.full_name_teacher_profile.TabIndex = 2;
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
            this.cancel_change_teacher_login_button.Click += new System.EventHandler(this.cancel_change_teacher_login_button_Click);
            // 
            // new_teacher_login_button
            // 
            this.new_teacher_login_button.Location = new System.Drawing.Point(579, 122);
            this.new_teacher_login_button.Name = "new_teacher_login_button";
            this.new_teacher_login_button.Size = new System.Drawing.Size(184, 42);
            this.new_teacher_login_button.TabIndex = 2;
            this.new_teacher_login_button.Text = "Изменить логин";
            this.new_teacher_login_button.UseVisualStyleBackColor = true;
            this.new_teacher_login_button.Click += new System.EventHandler(this.new_teacher_login_button_Click);
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
            this.teacher_identity_check_panel.Paint += new System.Windows.Forms.PaintEventHandler(this.teacher_identity_check_panel_Paint);
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
            this.cancel_teacher_change_password_button.Click += new System.EventHandler(this.cancel_teacher_change_password_button_Click);
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
            this.continue_teacher_change_password_button.Click += new System.EventHandler(this.continue_teacher_change_password_button_Click);
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
            this.new_teacher_password_button.Click += new System.EventHandler(this.new_teacher_password_button_Click);
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
            this.cancel_change_teacher_password_button.Click += new System.EventHandler(this.cancel_change_teacher_password_button_Click);
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
            // TeacherForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(829, 523);
            this.Controls.Add(this.tabControl1);
            this.MinimumSize = new System.Drawing.Size(847, 570);
            this.Name = "TeacherForm";
            this.Text = "TeacherForm";
            this.Load += new System.EventHandler(this.TeacherForm_Load);
            this.profile_page.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.teacher_profile_main_panel.ResumeLayout(false);
            this.teacher_profile_main_panel.PerformLayout();
            this.change_teacher_login_panel.ResumeLayout(false);
            this.change_teacher_login_panel.PerformLayout();
            this.teacher_identity_check_panel.ResumeLayout(false);
            this.teacher_identity_check_panel.PerformLayout();
            this.change_teacher_password_panel.ResumeLayout(false);
            this.change_teacher_password_panel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage profile_page;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.Panel teacher_profile_main_panel;
        private System.Windows.Forms.Button change_teacher_password_button;
        private System.Windows.Forms.Button change_teacher_login_button;
        private System.Windows.Forms.TextBox password_teacher_profile;
        private System.Windows.Forms.Label full_name_teacher_profile_label;
        private System.Windows.Forms.TextBox login_teacher_profile;
        private System.Windows.Forms.TextBox full_name_teacher_profile;
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
    }
}
using System;
using System.Drawing;
using System.Windows.Forms;

namespace testing_program
{
    public partial class RegistrationForm : Form
    {
        public int user_id = 0;
        private const int student_code = 1;
        private const int teacher_code = 0;
        readonly DatabaseClass database;
        readonly TextboxClass textbox = new TextboxClass();
        readonly ComboboxClass combobox = new ComboboxClass();
        readonly CheckboxClass checkbox = new CheckboxClass();

        public RegistrationForm(DatabaseClass database)
        {
            InitializeComponent();
            this.database = database;
            database.Fill_groups_collection(select_group_registration, false, false);
        }
        //*********************закрытие программы**********************
        private void RegistrationForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
        //*******************шифрование TextBox*********************************
        private void Show_password_sign_in_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            bool is_hidden = true;
            if (show_password_sign_in_checkBox.Checked || !textbox.Check_text_is_changed(sign_in_password, "Введите пароль"))
            {
                is_hidden = false;
            }
            sign_in_password.UseSystemPasswordChar = is_hidden;
        }

        private void Show_password_sign_up_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            bool is_hidden1 = true;
            bool is_hidden2 = true;

            if (show_password_sign_up_checkBox.Checked || !textbox.Check_text_is_changed(password_registration, "Введите пароль"))
            {
                is_hidden1 = false;
            }
            if (show_password_sign_up_checkBox.Checked || !textbox.Check_text_is_changed(test_password_registration, "Повторно введите пароль"))
            {
                is_hidden2 = false;
            }
            password_registration.UseSystemPasswordChar = is_hidden1;
            test_password_registration.UseSystemPasswordChar = is_hidden2;
        }
        //********открытие панелей регистрации в зависимости от роли пользователя**************
        private void Registration_role_SelectionChangeCommitted(object sender, EventArgs e)
        {
            login_password_panel.Visible = true;
            ComboBox[] comboBoxes = { select_group_registration, select_full_name_registration };
            string selected_state = registration_role.SelectedItem.ToString();
            if (selected_state == "Преподаватель")
            {
                combobox.Change_visible(comboBoxes, false);
                teacher_name.Visible = true;
                login_password_panel.Size = new Size(278, 170);
                login_password_panel.Location = new Point(16, 105);
                password_registration.Location = new Point(0, 40);
                test_password_registration.Location = new Point(0, 80);
                show_password_sign_up_checkBox.Location = new Point(0, 110);
            }
            else if (selected_state == "Студент")
            {
                combobox.Change_visible(comboBoxes, true);
                teacher_name.Visible = false;
                login_password_panel.Size = new Size(278, 123);
                login_password_panel.Location = new Point(16, 140);
                password_registration.Location = new Point(0, 36);
                test_password_registration.Location = new Point(0, 72);
                show_password_sign_up_checkBox.Location = new Point(0, 102);
            }
        }
        //******************очищение панелей при закрытии****************
        private void Registration_panel_VisibleChanged(object sender, EventArgs e)
        {
            if (!registration_panel.Visible)
            {
                login_password_panel.Visible = false;
                ComboBox[] comboBoxes = { registration_role, select_group_registration, select_full_name_registration };
                string[] comboBoxes_text = { "Выберите роль", "Выберите группу из списка", "Выберите свое имя из списка группы" };
                for (int i = 0; i < comboBoxes.Length; ++i)
                {
                    combobox.Return_original_text(comboBoxes[i], comboBoxes_text[i]);
                    if (i > 0)
                    {
                        comboBoxes[i].Visible = false;
                    }
                }
                combobox.Delete_collection(select_full_name_registration);
                TextBox[] textBoxes = { teacher_name, login_registration, password_registration, test_password_registration };
                string[] textBoxes_text = { "Введите ФИО", "Введите логин", "Введите пароль", "Повторно введите пароль" };
                teacher_name.Visible = false;
                textbox.Clear_textboxes(textBoxes);
                textbox.Fill_textboxes(textBoxes, textBoxes_text);
                checkbox.Clear(show_password_sign_up_checkBox);
                password_registration.UseSystemPasswordChar = false;
                test_password_registration.UseSystemPasswordChar = false;
            }
        }

        private void Sign_in_panel_VisibleChanged(object sender, EventArgs e)
        {
            if (!sign_in_panel.Visible)
            {
                combobox.Return_original_text(sign_in_role, "Выберите роль");
                TextBox[] textBoxes = { sign_in_login, sign_in_password };
                textbox.Clear_textboxes(textBoxes);
                textbox.Fill_textboxes(textBoxes, new string[] { "Введите логин", "Введите пароль" });
                checkbox.Clear(show_password_sign_in_checkBox);
                sign_in_password.UseSystemPasswordChar = false;
            }
        }
        //********************заполнение comboBox с ФИО студентов****************
        private void Select_group_registration_SelectionChangeCommitted(object sender, EventArgs e)
        {
            combobox.Return_original_text(select_full_name_registration, "Выберите свое имя из списка группы");
            combobox.Delete_collection(select_full_name_registration);
            database.Fill_student_name_collection(select_group_registration.SelectedItem.ToString(), select_full_name_registration, false);
        }
        //*****проверка введенных данных, сохранение данных и вызов функции перехода на форму при регистрации/входе****
        private void Sign_up_button_Click(object sender, EventArgs e)
        {
            TextBox[] textBoxes = { login_registration, password_registration, test_password_registration };
            string[] text = { "Введите логин", "Введите пароль", "Повторно введите пароль" };
            if (!textbox.Check_textboxes_text_are_changed(text, textBoxes) || !combobox.Check_is_changed(registration_role))
            {
                MessageBox.Show("Заполните обязательные поля!");
            }
            else if (!database.Check_login_exists(login_registration.Text))
            {
                if (registration_role.SelectedIndex == student_code)
                {
                    if (!combobox.Check_selected_indexes_are_changed(new ComboBox[] { select_group_registration, select_full_name_registration }))
                    {
                        MessageBox.Show("Заполните обязательные поля!");
                    }
                    else if (textbox.Check_passwords_are_matching(password_registration.Text, test_password_registration.Text))
                    {
                        user_id = database.Save_registrationForm_new_student(login_registration, password_registration, select_full_name_registration, select_group_registration);
                        Go_to_user_form(student_code);
                    }
                }
                else
                {
                    if (!textbox.Check_text_is_changed(teacher_name, "Введите ФИО"))
                    {
                        MessageBox.Show("Заполните обязательные поля!");
                    }
                    else if (textbox.Check_full_name_correct(teacher_name) &&
                        textbox.Check_passwords_are_matching(password_registration.Text, test_password_registration.Text))
                    {
                        user_id = database.Save_registrationForm_new_teacher(teacher_name, login_registration, password_registration);
                        Go_to_user_form(teacher_code);
                    }
                }
            }
        }

        private void Enter_button_Click(object sender, EventArgs e)
        {
            TextBox[] textBoxes = { sign_in_login, sign_in_password };
            string[] text = { "Введите логин", "Введите пароль" };
            if (!combobox.Check_is_changed(sign_in_role) || !textbox.Check_textboxes_text_are_changed(text, textBoxes))
            {
                MessageBox.Show("Заполните обязательные поля!");
            }
            else
            {
                user_id = database.Get_sign_in_user_id(sign_in_login, sign_in_password, sign_in_role.SelectedIndex);
                if (user_id != 0)
                {
                    Go_to_user_form(sign_in_role.SelectedIndex);
                }
                else
                {
                    MessageBox.Show("Неправильно указан логин и/или пароль");
                }
            }
        }
        //***********переход на форму студента/преподавателя*****************
        private void Go_to_user_form(int code)
        {
            if (code == 1)
            {
                StudentForm studentForm = new StudentForm(database, user_id);
                studentForm.Show();
            }
            else
            {
                TeacherForm teacherForm = new TeacherForm(database, user_id);
                teacherForm.Show();
            }
            this.Hide();
        }
        //************открытие панелей входа/регистрации**************
        private void Sign_in_button_Click(object sender, EventArgs e)
        {
            registration_panel.Visible = false;
            sign_in_panel.Visible = true;
        }

        private void Registration_button_Click(object sender, EventArgs e)
        {
            sign_in_panel.Visible = false;
            registration_panel.Visible = true;
        }
        //****************взаимодействие с TextBox с шифрованием*************************************
        bool signInPasswordTextIsChanged = false;
        private void Sign_in_password_TextChanged(object sender, EventArgs e)
        {
            signInPasswordTextIsChanged = textbox.Check_text_is_changed(sign_in_password, "Введите пароль");
        }

        private void Sign_in_password_Click(object sender, EventArgs e)
        {
            sign_in_password.UseSystemPasswordChar = !show_password_sign_in_checkBox.Checked;
            signInPasswordTextIsChanged = textbox.Check_is_cleared(sign_in_password, "Введите пароль", signInPasswordTextIsChanged);
        }

        private void Sign_in_password_Leave(object sender, EventArgs e)
        {
            sign_in_password.UseSystemPasswordChar = !textbox.Return_original_text(sign_in_password, "Введите пароль");
        }
        //****************взаимодействие с обычным TextBox*************************************
        bool signInLoginTextIsChanged = false;
        private void Sign_in_login_Click(object sender, EventArgs e)
        {
            signInLoginTextIsChanged = textbox.Check_is_cleared(sign_in_login, "Введите логин", signInLoginTextIsChanged);
        }

        private void Sign_in_login_TextChanged(object sender, EventArgs e)
        {
            signInLoginTextIsChanged = textbox.Check_text_is_changed(sign_in_login, "Введите логин");
        }
        private void Sign_in_login_Leave(object sender, EventArgs e)
        {
            textbox.Return_original_text(sign_in_login, "Введите логин");
        }

        bool registrationLoginTextIsChanged = false;
        private void Login_registration_Click(object sender, EventArgs e)
        {
            registrationLoginTextIsChanged = textbox.Check_is_cleared(login_registration, "Введите логин", registrationLoginTextIsChanged);
        }

        private void Login_registration_Leave(object sender, EventArgs e)
        {
            textbox.Return_original_text(login_registration, "Введите логин");
        }

        private void Login_registration_TextChanged(object sender, EventArgs e)
        {
            registrationLoginTextIsChanged = textbox.Check_text_is_changed(login_registration, "Введите логин");
        }

        bool registrationPasswordTextIsChanged = false;
        private void Password_registration_Click(object sender, EventArgs e)
        {
            password_registration.UseSystemPasswordChar = !show_password_sign_up_checkBox.Checked;
            registrationPasswordTextIsChanged = textbox.Check_is_cleared(password_registration, "Введите пароль", registrationPasswordTextIsChanged);
        }

        private void Password_registration_Leave(object sender, EventArgs e)
        {
            password_registration.UseSystemPasswordChar = !textbox.Return_original_text(password_registration, "Введите пароль");
        }

        private void Password_registration_TextChanged(object sender, EventArgs e)
        {
            registrationPasswordTextIsChanged = textbox.Check_text_is_changed(password_registration, "Введите пароль");
        }

        bool secondPasswordTextIsChanged = false;
        private void Test_password_registration_Leave(object sender, EventArgs e)
        {
            test_password_registration.UseSystemPasswordChar = !textbox.Return_original_text(test_password_registration, "Повторно введите пароль");
        }

        private void Test_password_registration_Click(object sender, EventArgs e)
        {
            test_password_registration.UseSystemPasswordChar = !show_password_sign_up_checkBox.Checked;
            secondPasswordTextIsChanged = textbox.Check_is_cleared(test_password_registration, "Повторно введите пароль", secondPasswordTextIsChanged);
        }

        private void Test_password_registration_TextChanged(object sender, EventArgs e)
        {
            secondPasswordTextIsChanged = textbox.Check_text_is_changed(test_password_registration, "Повторно введите пароль");
        }

        bool teacherNameIsChanged = false;
        private void Teacher_name_TextChanged(object sender, EventArgs e)
        {
            teacherNameIsChanged = textbox.Check_text_is_changed(teacher_name, "Введите ФИО");
        }

        private void Teacher_name_Click(object sender, EventArgs e)
        {
            teacherNameIsChanged = textbox.Check_is_cleared(teacher_name, "Введите ФИО", teacherNameIsChanged);
        }

        private void Teacher_name_Leave(object sender, EventArgs e)
        {
            textbox.Return_original_text(teacher_name, "Введите ФИО");
        }
        //*************запрет на ввод в comboBox*******************************
        private void Select_group_registration_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void Select_full_name_registration_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void Choose_role_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void Sign_in_role_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
    }
}

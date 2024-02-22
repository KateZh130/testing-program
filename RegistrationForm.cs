using System;
using System.Windows.Forms;

namespace testing_program
{
    public partial class RegistrationForm : Form
    {
        #region Variables
        public int user_id = 0;
        private const int student_code = 1;
        private const int teacher_code = 0;
        #endregion
        #region Class instancees
        readonly DatabaseClass database;
        readonly TextboxClass textbox = new TextboxClass();
        readonly ComboboxClass combobox = new ComboboxClass();
        readonly CheckboxClass checkbox = new CheckboxClass();
        readonly GroupboxPanelClass panel = new GroupboxPanelClass();
        #endregion


        public RegistrationForm(DatabaseClass database)
        {
            InitializeComponent();
            this.database = database;
        }

        private void RegistrationForm_Activated(object sender, EventArgs e)
        {
            if (!registration_panel.Visible)
                panel.Change_visible(sign_in_panel, true);
        }

        #region Close application
        private void RegistrationForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
        #endregion

        #region Managing panels

        #region Opening registration panels
        private void Registration_role_SelectionChangeCommitted(object sender, EventArgs e)
        {
            panel.Change_visible(login_password_panel, true);
            ComboBox[] comboBoxes = { select_group_registration, select_full_name_registration };
            string selected_state = registration_role.SelectedItem.ToString();
            int size = 123;
            int panel_location = 140;
            int checkbox_location = 102;
            int textbox_location = 36;
            bool show = true;
            if (selected_state == "Преподаватель")
            {
                show = false;

                size = 170;
                panel_location = 105;
                textbox_location = 40;
                checkbox_location = 110;
            }
            combobox.Change_visible(comboBoxes, show);
            textbox.Change_visible(teacher_name, !show);
            panel.Change_size(login_password_panel, 278, size);
            panel.Change_location(login_password_panel, 16, panel_location);
            textbox.Change_location(new TextBox[] { password_registration, test_password_registration }, 0, new int[] { textbox_location, textbox_location * 2 });
            checkbox.Change_location(show_password_sign_up_checkBox, 0, checkbox_location);
        }
        #endregion

        #region Cleaning the panels when closing

        private void Registration_panel_VisibleChanged(object sender, EventArgs e)
        {
            if (!registration_panel.Visible)
            {
                panel.Change_visible(login_password_panel, false);
                ComboBox[] comboBoxes = { registration_role, select_group_registration, select_full_name_registration };
                string[] comboBoxes_text = { "Выберите роль", "Выберите группу из списка", "Выберите свое имя из списка группы" };
                combobox.Return_original_text(comboBoxes, comboBoxes_text);
                combobox.Change_visible(new ComboBox[] { select_group_registration, select_full_name_registration }, false);
                combobox.Delete_collection(select_full_name_registration);
                TextBox[] textBoxes = { teacher_name, login_registration, password_registration, test_password_registration };
                string[] textBoxes_text = { "Введите ФИО", "Введите логин", "Введите пароль", "Повторно введите пароль" };
                textbox.Change_visible(teacher_name, false);
                textbox.Clear(textBoxes);
                textbox.Fill(textBoxes, textBoxes_text);
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
                textbox.Clear(textBoxes);
                textbox.Fill(textBoxes, new string[] { "Введите логин", "Введите пароль" });
                checkbox.Clear(show_password_sign_in_checkBox);
                sign_in_password.UseSystemPasswordChar = false;
            }
        }
        #endregion

        #region Opening sign in/registration panels
        private void Sign_in_button_Click(object sender, EventArgs e)
        {
            panel.Change_visible(new Panel[] { registration_panel, sign_in_panel }, new bool[] { false, true });
        }

        private void Registration_button_Click(object sender, EventArgs e)
        {
            panel.Change_visible(new Panel[] { sign_in_panel, registration_panel }, new bool[] { false, true });
        }
        #endregion

        #endregion

        #region Filling in ComboBox with students names
        private void Select_group_registration_SelectionChangeCommitted(object sender, EventArgs e)
        {
            combobox.Return_original_text(select_full_name_registration, "Выберите свое имя из списка группы");
            combobox.Delete_collection(select_full_name_registration);
            database.Fill_student_name_collection(select_group_registration.SelectedItem.ToString(), select_full_name_registration);
        }
        #endregion

        #region User registration
        private void Sign_up_button_Click(object sender, EventArgs e)
        {
            TextBox[] textBoxes = { login_registration, password_registration, test_password_registration };
            string[] text = { "Введите логин", "Введите пароль", "Повторно введите пароль" };

            if (!textbox.Check_text_changed(textBoxes, text) || !combobox.Check_selected_index_changed(registration_role))
            {
                MessageBox.Show("Заполните обязательные поля!");
                return;
            }

            if (!(textbox.Check_invalid_characters_with_space(textBoxes) && !database.Check_login_exists(login_registration.Text)))
                return;

            if (registration_role.SelectedIndex == student_code)
            {
                if (!combobox.Check_selected_index_changed(new ComboBox[] { select_group_registration, select_full_name_registration }))
                {
                    MessageBox.Show("Заполните обязательные поля!");
                }
                else if (textbox.Check_passwords_are_matching(password_registration.Text, test_password_registration.Text))
                {
                    user_id = database.Save_registrationForm_new_user(login_registration.Text, password_registration.Text,
                        select_full_name_registration.SelectedItem.ToString(), select_group_registration.SelectedItem.ToString());
                    panel.Change_visible(registration_panel, false);
                    Go_to_user_form(student_code);
                }
            }
            else
            {
                if (!textbox.Check_text_changed(teacher_name, "Введите ФИО"))
                {
                    MessageBox.Show("Заполните обязательные поля!");
                }
                else if (textbox.Check_passwords_are_matching(password_registration.Text, test_password_registration.Text) &&
                    textbox.Check_invalid_characters(teacher_name, true))
                {
                    user_id = database.Save_registrationForm_new_user(teacher_name.Text, login_registration.Text, password_registration.Text);
                    panel.Change_visible(registration_panel, false);
                    Go_to_user_form(teacher_code);
                }
            }
        }
        #endregion

        #region User log in
        private void Enter_button_Click(object sender, EventArgs e)
        {
            TextBox[] textBoxes = { sign_in_login, sign_in_password };
            string[] text = { "Введите логин", "Введите пароль" };
            if (combobox.Check_selected_index_changed(sign_in_role) && textbox.Check_text_changed(textBoxes,text))
            {
                if (textbox.Check_invalid_characters_with_space(textBoxes))
                {
                    user_id = database.Get_signIn_user_id(sign_in_login.Text, sign_in_password.Text, sign_in_role.SelectedIndex);
                    if (user_id != 0)
                    {
                        int code = sign_in_role.SelectedIndex;
                        panel.Change_visible(sign_in_panel, false);
                        Go_to_user_form(code); }
                    else MessageBox.Show("Неправильно указан логин и/или пароль");
                }
            }
            else MessageBox.Show("Заполните обязательные поля!");
        }
        #endregion

        #region Go to student/teacher form
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
        #endregion

        #region Managing TextBoxes

        #region TextBoxes with encryption

        #region Sign in password

        private void Show_password_sign_in_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            bool is_hidden = true;
            if (show_password_sign_in_checkBox.Checked || !textbox.Check_text_changed(sign_in_password, "Введите пароль"))
            {
                is_hidden = false;
            }
            sign_in_password.UseSystemPasswordChar = is_hidden;
        }

        bool signInPasswordTextIsChanged = false;
        private void Sign_in_password_TextChanged(object sender, EventArgs e)
        {
            signInPasswordTextIsChanged = textbox.Check_text_changed(sign_in_password, "Введите пароль");
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
        #endregion

        #region Registration password

        private void Show_password_sign_up_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            bool is_hidden1 = true;
            bool is_hidden2 = true;

            if (show_password_sign_up_checkBox.Checked || !textbox.Check_text_changed(password_registration, "Введите пароль"))
            {
                is_hidden1 = false;
            }
            if (show_password_sign_up_checkBox.Checked || !textbox.Check_text_changed(test_password_registration, "Повторно введите пароль"))
            {
                is_hidden2 = false;
            }
            password_registration.UseSystemPasswordChar = is_hidden1;
            test_password_registration.UseSystemPasswordChar = is_hidden2;
        }

        bool registration_password_text_changed = false;

        private void Password_registration_Click(object sender, EventArgs e)
        {
            password_registration.UseSystemPasswordChar = !show_password_sign_up_checkBox.Checked;
            registration_password_text_changed = textbox.Check_is_cleared(password_registration, "Введите пароль", registration_password_text_changed);
        }

        private void Password_registration_Leave(object sender, EventArgs e)
        {
            password_registration.UseSystemPasswordChar = !textbox.Return_original_text(password_registration, "Введите пароль");
        }

        private void Password_registration_TextChanged(object sender, EventArgs e)
        {
            registration_password_text_changed = textbox.Check_text_changed(password_registration, "Введите пароль");
        }

        bool second_password_text_changed = false;

        private void Test_password_registration_Leave(object sender, EventArgs e)
        {
            test_password_registration.UseSystemPasswordChar = !textbox.Return_original_text(test_password_registration, "Повторно введите пароль");
        }

        private void Test_password_registration_Click(object sender, EventArgs e)
        {
            test_password_registration.UseSystemPasswordChar = !show_password_sign_up_checkBox.Checked;
            second_password_text_changed = textbox.Check_is_cleared(test_password_registration, "Повторно введите пароль", second_password_text_changed);
        }

        private void Test_password_registration_TextChanged(object sender, EventArgs e)
        {
            second_password_text_changed = textbox.Check_text_changed(test_password_registration, "Повторно введите пароль");
        }
        #endregion

        #endregion

        #region Usual TextBoxes

        #region Sign in login
        bool sign_in_login_text_changed = false;
        private void Sign_in_login_Click(object sender, EventArgs e)
        {
            sign_in_login_text_changed = textbox.Check_is_cleared(sign_in_login, "Введите логин", sign_in_login_text_changed);
        }

        private void Sign_in_login_TextChanged(object sender, EventArgs e)
        {
            sign_in_login_text_changed = textbox.Check_text_changed(sign_in_login, "Введите логин");
        }
        private void Sign_in_login_Leave(object sender, EventArgs e)
        {
            textbox.Return_original_text(sign_in_login, "Введите логин");
        }
        #endregion

        #region Registration login
        bool registration_login_text_changed = false;
        private void Login_registration_Click(object sender, EventArgs e)
        {
            registration_login_text_changed = textbox.Check_is_cleared(login_registration, "Введите логин", registration_login_text_changed);
        }

        private void Login_registration_Leave(object sender, EventArgs e)
        {
            textbox.Return_original_text(login_registration, "Введите логин");
        }

        private void Login_registration_TextChanged(object sender, EventArgs e)
        {
            registration_login_text_changed = textbox.Check_text_changed(login_registration, "Введите логин");
        }
        #endregion

        #region Teacher name
        bool teacher_name_changed = false;
        private void Teacher_name_TextChanged(object sender, EventArgs e)
        {
            teacher_name_changed = textbox.Check_text_changed(teacher_name, "Введите ФИО");
        }

        private void Teacher_name_Click(object sender, EventArgs e)
        {
            teacher_name_changed = textbox.Check_is_cleared(teacher_name, "Введите ФИО", teacher_name_changed);
        }

        private void Teacher_name_Leave(object sender, EventArgs e)
        {
            textbox.Return_original_text(teacher_name, "Введите ФИО");
        }
        #endregion
        #endregion

        #endregion

        #region KeyPress events
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

        private void RegistrationForm_Load(object sender, EventArgs e)
        {
            database.Get_groups(select_group_registration);
        }
        #endregion
    }
}

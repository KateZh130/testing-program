using System;
using System.Drawing;
using System.Windows.Forms;

namespace testing_program
{
    public partial class RegistrationForm : Form
    {
        public int user_id;
        private const int student_code = 1;
        private const int teacher_code = 0;
        readonly DatabaseClass database;
        readonly TextboxClass textboxclass = new TextboxClass();
        readonly ComboboxClass comboboxclass = new ComboboxClass();
        readonly CheckboxClass checkboxclass = new CheckboxClass();

        public RegistrationForm(DatabaseClass database)
        {
            InitializeComponent();
            this.database = database;
            database.Fill_registrationForm_collection_select_group(select_group_registration);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        bool signInPasswordTextIsChanged = false;
        private void Sign_in_password_TextChanged(object sender, EventArgs e)
        {

            signInPasswordTextIsChanged = textboxclass.Check_text_is_changed(sign_in_password, "Введите пароль");
        }

        private void Sign_in_password_Click(object sender, EventArgs e)
        {
            sign_in_password.UseSystemPasswordChar = !checkBox2.Checked;
            signInPasswordTextIsChanged = textboxclass.Check_is_cleared(sign_in_password, "Введите пароль", signInPasswordTextIsChanged);
        }

        private void Sign_in_password_Leave(object sender, EventArgs e)
        {
            sign_in_password.UseSystemPasswordChar = !textboxclass.Return_original_text(sign_in_password, "Введите пароль");
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            bool is_hidden = true;
            if (checkBox2.Checked || !textboxclass.Check_text_is_changed(sign_in_password, "Введите пароль"))
            {
                is_hidden = false;
            }
            sign_in_password.UseSystemPasswordChar = is_hidden;
        }

        bool signInLoginTextIsChanged = false;
        private void sign_in_login_Click(object sender, EventArgs e)
        {
            signInLoginTextIsChanged = textboxclass.Check_is_cleared(sign_in_login, "Введите логин", signInLoginTextIsChanged);
        }

        private void sign_in_login_TextChanged(object sender, EventArgs e)
        {
            signInLoginTextIsChanged = textboxclass.Check_text_is_changed(sign_in_login, "Введите логин");
        }
        private void sign_in_login_Leave(object sender, EventArgs e)
        {
            textboxclass.Return_original_text(sign_in_login, "Введите логин");
        }

        private void sign_in_button_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
            panel1.Visible = true;
        }

        private void registration_button_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            panel2.Visible = true;
        }

        private void select_group_registration_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void select_full_name_registration_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
        private void choose_role_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        bool registrationLoginTextIsChanged = false;
        private void login_registration_Click(object sender, EventArgs e)
        {
            registrationLoginTextIsChanged = textboxclass.Check_is_cleared(login_registration, "Введите логин", registrationLoginTextIsChanged);
        }
        private void login_registration_Leave(object sender, EventArgs e)
        {
            textboxclass.Return_original_text(login_registration, "Введите логин");

        }
        private void login_registration_TextChanged(object sender, EventArgs e)
        {
            registrationLoginTextIsChanged = textboxclass.Check_text_is_changed(login_registration, "Введите логин");
        }

        bool registrationPasswordTextIsChanged = false;
        private void password_registration_Click(object sender, EventArgs e)
        {
            password_registration.UseSystemPasswordChar = !checkBox1.Checked;
            registrationPasswordTextIsChanged = textboxclass.Check_is_cleared(password_registration, "Введите пароль", registrationPasswordTextIsChanged);
        }

        private void password_registration_Leave(object sender, EventArgs e)
        {
            password_registration.UseSystemPasswordChar = !textboxclass.Return_original_text(password_registration, "Введите пароль");
        }
        private void password_registration_TextChanged(object sender, EventArgs e)
        {
            registrationPasswordTextIsChanged = textboxclass.Check_text_is_changed(password_registration, "Введите пароль");
        }

        bool secondPasswordTextIsChanged = false;
        private void test_password_registration_Leave(object sender, EventArgs e)
        {
            test_password_registration.UseSystemPasswordChar = !textboxclass.Return_original_text(test_password_registration, "Повторно введите пароль");
        }

        private void test_password_registration_Click(object sender, EventArgs e)
        {
            test_password_registration.UseSystemPasswordChar = !checkBox1.Checked;
            secondPasswordTextIsChanged = textboxclass.Check_is_cleared(test_password_registration, "Повторно введите пароль", secondPasswordTextIsChanged);
        }

        private void Test_password_registration_TextChanged(object sender, EventArgs e)
        {
            secondPasswordTextIsChanged = textboxclass.Check_text_is_changed(test_password_registration, "Повторно введите пароль");
        }

        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            bool is_hidden1 = true;
            bool is_hidden2 = true;

            if (checkBox1.Checked || !textboxclass.Check_text_is_changed(password_registration, "Введите пароль"))
            {
                is_hidden1 = false;
            }
            if (checkBox1.Checked || !textboxclass.Check_text_is_changed(test_password_registration, "Повторно введите пароль"))
            {
                is_hidden2 = false;
            }
            password_registration.UseSystemPasswordChar = is_hidden1;
            test_password_registration.UseSystemPasswordChar = is_hidden2;
        }

        private void Choose_role_SelectedIndexChanged(object sender, EventArgs e)
        {
            textboxclass.Change_visible(new TextBox[] { login_registration, password_registration, test_password_registration }, true);
            ComboBox[] comboBoxes = { select_group_registration, select_full_name_registration };
            string selectedState = registration_role.SelectedItem.ToString();
            if (selectedState == "Преподаватель")
            {
                comboboxclass.Change_visible(comboBoxes, false);
                teacher_name.Visible = true;
                panel3.Location = new Point(16, 105);
            }
            else if (selectedState == "Студент")
            {
                comboboxclass.Change_visible(comboBoxes, true);
                teacher_name.Visible = false;
                panel3.Location = new Point(16, 144);
            }
        }

        bool teacherNameIsChanged = false;
        private void Teacher_name_TextChanged(object sender, EventArgs e)
        {
            teacherNameIsChanged = textboxclass.Check_text_is_changed(teacher_name, "Введите ФИО");
        }

        private void Teacher_name_Click(object sender, EventArgs e)
        {
            teacherNameIsChanged = textboxclass.Check_is_cleared(teacher_name, "Введите ФИО", teacherNameIsChanged);
        }

        private void Teacher_name_Leave(object sender, EventArgs e)
        {
            textboxclass.Return_original_text(teacher_name, "Введите ФИО");
        }

        private void Sign_up_button_Click(object sender, EventArgs e)
        {
            TextBox[] textBoxes = { login_registration, password_registration, test_password_registration };
            string[] text = { "Введите логин", "Введите пароль", "Повторно введите пароль" };

            if (!textboxclass.Check_textboxes_text_are_changed(text, textBoxes) || !comboboxclass.Check_is_changed(registration_role))
            {
                MessageBox.Show("Заполните обязательные поля!");
            }
            else if (database.Check_login_exists(login_registration.Text))
            {
                MessageBox.Show("Логин занят");
            }
            else
            {
                if (registration_role.SelectedIndex == student_code)
                {
                    if (!comboboxclass.Check_selected_indexes_are_changed(new ComboBox[] { select_group_registration, select_full_name_registration }))
                    {
                        MessageBox.Show("Заполните обязательные поля!");
                    }
                    else if (textboxclass.Check_passwords_are_matching(password_registration.Text, test_password_registration.Text))
                    {
                        user_id = database.Save_registrationForm_new_student(login_registration, password_registration, select_full_name_registration, select_group_registration);
                        Go_to_user_form(student_code);
                    }
                }
                else
                {
                    if (textboxclass.Check_text_is_changed(teacher_name, "Введите ФИО"))
                    {
                        MessageBox.Show("Заполните обязательные поля!");
                    }
                    else if (textboxclass.Check_passwords_are_matching(password_registration.Text, test_password_registration.Text))
                    {
                        user_id = database.Save_registrationForm_new_teacher(teacher_name, login_registration, password_registration);
                        Go_to_user_form(teacher_code);
                    }
                }
            }
        }

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

        private void Enter_button_Click(object sender, EventArgs e)
        {
            TextBox[] textBoxes = { sign_in_login, sign_in_password };
            string[] text = { "Введите логин", "Введите пароль" };
            if(!comboboxclass.Check_is_changed(sign_in_role) || !textboxclass.Check_textboxes_text_are_changed(text, textBoxes))
            {
                MessageBox.Show("Заполните обязательные поля!");
            }
            else
            {
                user_id = database.Search_registrationForm_user_id(sign_in_login, sign_in_password, sign_in_role.SelectedIndex);
                if (user_id != -1)
                {
                    Go_to_user_form(sign_in_role.SelectedIndex);
                }
                else
                {
                    MessageBox.Show("Неправильно указан логин и/или пароль");
                }
            }
        }

        private void Select_group_registration_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboboxclass.Return_original_text(select_full_name_registration, "Выберите свое имя из списка группы");
            comboboxclass.Delete_collection(select_full_name_registration);
            database.Fill_registrationForm_collection_select_student_name(select_group_registration, select_full_name_registration);
        }

        private void Sign_in_role_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
    }
}

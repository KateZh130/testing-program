﻿using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace testing_program
{
    public partial class RegistrationForm : Form
    {
        public int user_id;
        private const int student_code = 1;
        private const int teacher_code = 0;
        DatabaseClass database;
        TextboxClass textboxclass = new TextboxClass();
        ComboboxClass comboboxclass = new ComboboxClass();

        /*private readonly NpgsqlConnection connection;
        private const string CONNECTION_STRING = "Host=localhost;" +
     "Username=postgres;" +
     "Password=rjirf567;" +
     "Database=test_prog";*/
        public RegistrationForm(DatabaseClass database)
        {
            InitializeComponent();
            this.database = database;
            /*try
            {
                connection = new NpgsqlConnection(CONNECTION_STRING);
                connection.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return;
            }*/


            //заполнение коллекции выбор группы
            /*NpgsqlCommand select_group_registration_cmd = new NpgsqlCommand("SELECT group_name FROM groups;", connection);
            NpgsqlDataReader reader_select_group_registration_cmd = select_group_registration_cmd.ExecuteReader();

            if (reader_select_group_registration_cmd.HasRows)
            {
                select_group_registration.Items.Clear();

                while (reader_select_group_registration_cmd.Read())
                {
                    select_group_registration.Items.Add(reader_select_group_registration_cmd.GetString(0));
                }
            }

            reader_select_group_registration_cmd.Close();
            */
            database.Fill_registrationForm_collection_select_group(select_group_registration);



        }

        private void Form1_Load(object sender, EventArgs e)
        {
            

        }

        //пароль при входе
        bool signInPasswordTextIsChanged = false;
        private void Sign_in_password_TextChanged(object sender, EventArgs e)
        {
            /* if (sign_in_password.Text != "" && sign_in_password.Text != "Введите пароль")
                 signInPasswordTextIsChanged = true;*/
            signInPasswordTextIsChanged = textboxclass.Check_textBox_text_is_changed(sign_in_password, "Введите пароль");
        }

        private void sign_in_password_Click(object sender, EventArgs e)
        {

            if (signInPasswordTextIsChanged == false || sign_in_password.Text == "Введите пароль")
            {
                if (checkBox2.Checked)
                    sign_in_password.UseSystemPasswordChar = false;
                else
                    sign_in_password.UseSystemPasswordChar = true;
                sign_in_password.Text = "";
                signInPasswordTextIsChanged = false;
            }
        }

        private void sign_in_password_Leave(object sender, EventArgs e)
        {
            if (sign_in_password.Text == "")
            {
                sign_in_password.UseSystemPasswordChar = false;
                sign_in_password.Text = "Введите пароль";
            }
            //textboxclass.TextBox_return_original_text(sign_in_password, "Введите пароль");
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBox2.Checked && sign_in_password.Text != "Введите пароль" )
                sign_in_password.UseSystemPasswordChar = true;
            else
                sign_in_password.UseSystemPasswordChar = false;
        }

        //логин при входе
        bool signInLoginTextIsChanged = false;
        private void sign_in_login_Click(object sender, EventArgs e)
        {
            /* if (signInLoginTextIsChanged == false || sign_in_login.Text == "Введите логин")
             {
                 sign_in_login.Text = "";
                 signInLoginTextIsChanged = false;
             }*/
            signInLoginTextIsChanged = textboxclass.Check_textBox_text_is_cleared(sign_in_login, "Введите логин", signInLoginTextIsChanged);
        }

        private void sign_in_login_TextChanged(object sender, EventArgs e)
        {
           /* if (sign_in_login.Text != "" && sign_in_login.Text != "Введите логин")
                signInLoginTextIsChanged = true;*/
            signInLoginTextIsChanged = textboxclass.Check_textBox_text_is_changed(sign_in_login, "Введите логин");
        }
        private void sign_in_login_Leave(object sender, EventArgs e)
        {
            /*if (sign_in_login.Text == "")
                sign_in_login.Text = "Введите логин";*/
            textboxclass.TextBox_return_original_text(sign_in_login, "Введите логин");
        }

        //выбор кнопки "вход"
        private void sign_in_button_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
            panel1.Visible = true;
        }

        //выбор кнопки "регистрация"
        private void registration_button_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            panel2.Visible = true;
        }
        
        //запрет на изменение значения при выборе группы, имени и роли при регистрации
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

        //логин при регистрации
        bool registrationLoginTextIsChanged = false;
        private void login_registration_Click(object sender, EventArgs e)
        {
            /*if (registrationLoginTextIsChanged == false || login_registration.Text == "Введите логин")
            {
                login_registration.Text = "";
                registrationLoginTextIsChanged = false;
            }*/
            registrationLoginTextIsChanged = textboxclass.Check_textBox_text_is_cleared(login_registration, "Введите логин", registrationLoginTextIsChanged);
        }
        private void login_registration_Leave(object sender, EventArgs e)
        {
            /*if (login_registration.Text == "")
                login_registration.Text = "Введите логин";*/
            textboxclass.TextBox_return_original_text(login_registration, "Введите логин");

        }
        private void login_registration_TextChanged(object sender, EventArgs e)
        {
           /* if (login_registration.Text != "" && login_registration.Text != "Введите логин")
                registrationLoginTextIsChanged = true;*/
            registrationLoginTextIsChanged=textboxclass.Check_textBox_text_is_changed(login_registration, "Введите логин");
        }

        //пароль при регистрации
        bool registrationPasswordTextIsChanged = false;
        private void password_registration_Click(object sender, EventArgs e)
        {
            if (registrationPasswordTextIsChanged == false || password_registration.Text == "Введите пароль")
            {
                if (checkBox1.Checked)
                    password_registration.UseSystemPasswordChar = false;
                else
                    password_registration.UseSystemPasswordChar = true;
                password_registration.Text = "";
                registrationPasswordTextIsChanged = false;
            }
        }

        private void password_registration_Leave(object sender, EventArgs e)
        {
            if (password_registration.Text == "")
            {
                password_registration.UseSystemPasswordChar = false;
                password_registration.Text = "Введите пароль";
            }
        }
        private void password_registration_TextChanged(object sender, EventArgs e)
        {
           /* if (password_registration.Text != "" && password_registration.Text != "Введите пароль")
                registrationPasswordTextIsChanged = true;*/
            registrationPasswordTextIsChanged = textboxclass.Check_textBox_text_is_changed(password_registration, "Введите пароль");

        }

        //повторение пароля при регистрации
        bool secondPasswordTextIsChanged=false;
        private void test_password_registration_Leave(object sender, EventArgs e)
        {
            if (test_password_registration.Text == "")
            {
                test_password_registration.UseSystemPasswordChar = false;
                test_password_registration.Text = "Повторно введите пароль";
            }
        }

        private void test_password_registration_Click(object sender, EventArgs e)
        {
            if (secondPasswordTextIsChanged == false || test_password_registration.Text == "Повторно введите пароль")
            {
                if(checkBox1.Checked)
                    test_password_registration.UseSystemPasswordChar = false;
                else
                    test_password_registration.UseSystemPasswordChar = true;
                test_password_registration.Text = "";
                secondPasswordTextIsChanged = false;
            }
        }

        private void test_password_registration_TextChanged(object sender, EventArgs e)
        {
            /*if (test_password_registration.Text != "" && test_password_registration.Text != "Повторно введите пароль")
                secondPasswordTextIsChanged = true;*/
            secondPasswordTextIsChanged = textboxclass.Check_textBox_text_is_changed(test_password_registration, "Повторно введите пароль");

        }

        //показать пароль при регистрации
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBox1.Checked && test_password_registration.Text != "Повторно введите пароль" && password_registration.Text != "Введите пароль")
            {
                password_registration.UseSystemPasswordChar = true;
                test_password_registration.UseSystemPasswordChar = true;
            }
            else
            {
                password_registration.UseSystemPasswordChar = false;
                test_password_registration.UseSystemPasswordChar = false;
            }

        } 

        //выбор роли (студент/преподаватель)
        private void choose_role_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedState = registration_role.SelectedItem.ToString();
            if (selectedState == "Преподаватель")
            {
                select_group_registration.Visible = false;
                select_full_name_registration.Visible = false;
                teacher_name.Visible = true;
                login_registration.Visible = true;
                password_registration.Visible = true;
                test_password_registration.Visible = true;
                panel3.Location = new Point(16, 105);
            }
            else if (selectedState == "Студент")
            {
                teacher_name.Visible = false;
                select_group_registration.Visible = true;
                select_full_name_registration.Visible = true;
                login_registration.Visible = true;
                password_registration.Visible = true;
                test_password_registration.Visible = true;
                panel3.Location = new Point(16, 144);
            }
        }

        //ФИО преподавателя
        bool teacherNameIsChanged = false;
        private void teacher_name_TextChanged(object sender, EventArgs e)
        {
           /* if (teacher_name.Text != "" && teacher_name.Text != "Введите ФИО")
                teacherNameIsChanged = true;*/
            teacherNameIsChanged=textboxclass.Check_textBox_text_is_changed(teacher_name, "Введите ФИО");
        }

        private void teacher_name_Click(object sender, EventArgs e)
        {
            if (teacherNameIsChanged == false || teacher_name.Text == "Введите ФИО")
            {
                teacher_name.Text = "";
                teacherNameIsChanged = false;
            }
        }

        private void teacher_name_Leave(object sender, EventArgs e)
        {
           /* if (teacher_name.Text == "")
                teacher_name.Text = "Введите ФИО";*/
            textboxclass.TextBox_return_original_text(teacher_name, "Введите ФИО");
        }

        //Проверка на совпадение паролей
        private bool checkingForMatchingPasswords(string first, string second)
        {
            if (first != second)
            {
                MessageBox.Show("Пароли не совпадают");
                return false;
            }
            return true;
        }

      /*  //Сохранение id для дальнейшей работы
        private void SaveId(int index)
        {
            string table_name="teacher";
            if(index == 1)
            {
                table_name = "student";
            }
            NpgsqlCommand save_id = new NpgsqlCommand("SELECT "+ table_name + "_id FROM "+ table_name + " " +
                "WHERE login = '" + login_registration.Text + "' " +
                "AND password ='"+ password_registration.Text + "';", connection);
            user_id = Convert.ToInt32(save_id.ExecuteScalar());
        }
      */

       /* //Занесение логина и пароля в бд для преподавателя
        private void recordingTeachersLoginAndPassword() 
        {
            NpgsqlCommand recording_teachers_login_and_password = new NpgsqlCommand("INSERT INTO " +
                "teacher (full_name, login, password) " +
                "VALUES ('" + teacher_name.Text + "', '"
                + login_registration.Text + "', '" 
                + password_registration.Text + "') RETURNING teacher_id;", connection);
            user_id=Convert.ToInt32(recording_teachers_login_and_password.ExecuteScalar());
            //SaveId(teacher_code);
        }*/
       /*
        //Занесение логина и пароля в бд для студента
        private void recordingStudentsLoginAndPassword()
        {
                NpgsqlCommand recording_students_login_and_password = new NpgsqlCommand("UPDATE student " +
                    "SET login = '" + login_registration.Text + "', " +
                    "password = '" + password_registration.Text + "' " +
                    "WHERE student_id = (select student_id from student " +
                    "where full_name = '" + select_full_name_registration.SelectedItem.ToString() + "') " +
                    "AND group_id = (select group_id from groups " +
                    "where group_name = '" + select_group_registration.SelectedItem.ToString() + "')" +
                    " RETURNING student_id;", connection);
            user_id = Convert.ToInt32(recording_students_login_and_password.ExecuteScalar());
            //SaveId(student_code);
        }*/

        //подтверждение регистрации
        private void sign_up_button_Click(object sender, EventArgs e)
        {
            if (registration_role.SelectedIndex == -1 || login_registration.Text == "Введите логин" || password_registration.Text == "Введите пароль" || test_password_registration.Text == "Повторно введите пароль")
            {
                MessageBox.Show("Заполните обязательные поля!");
            }
            else
            {
                if (registration_role.SelectedIndex == student_code)
                {
                    if(select_group_registration.SelectedIndex == -1 || select_full_name_registration.SelectedIndex == -1 )
                    {
                        MessageBox.Show("Заполните обязательные поля!");
                    }
                    else
                    {
                        if (checkingForMatchingPasswords(password_registration.Text, test_password_registration.Text))
                        {
                            //recordingStudentsLoginAndPassword();
                            user_id=database.Save_registrationForm_new_student(login_registration, password_registration, select_full_name_registration, select_group_registration);
                            goToUserForm(student_code);

                        }
                    }
                   
                }
                else if(registration_role.SelectedIndex == teacher_code)
                {
                    if(teacher_name.Text == "Введите ФИО")
                    {
                        MessageBox.Show("Заполните обязательные поля!");
                    }
                    else
                    {
                        if (checkingForMatchingPasswords(password_registration.Text, test_password_registration.Text))
                        {
                            //recordingTeachersLoginAndPassword();
                            user_id = database.Save_registrationForm_new_teacher(teacher_name, login_registration, password_registration);
                            goToUserForm(teacher_code);
                        }
                    }
                }
            }
            //
        }
       /* //поиск аккаунта среди преподавателей
        private int seachingUserIdInTeacherTable()
        {
            NpgsqlCommand select_user_id = new NpgsqlCommand("SELECT teacher_id FROM teacher " +
                "WHERE login = '" + sign_in_login.Text + "' " +
                "AND password = '" + sign_in_password.Text + "';", connection);
            user_id = Convert.ToInt32(select_user_id.ExecuteScalar());

            if (user_id == 0)
            {
                user_id = -1;
            }
            return user_id;
           
        }*/

       /* //поиск аккаунта среди студентов
        public int seachingUserIdInStudentTable()
        {
            NpgsqlCommand select_user_id1 = new NpgsqlCommand("SELECT student_id FROM student " +
                "WHERE login = '" + sign_in_login.Text + "' " +
                "AND password = '" + sign_in_password.Text + "';", connection);
            user_id = Convert.ToInt32(select_user_id1.ExecuteScalar());

            if (user_id == 0)
            {
                user_id = -1;
            }
            return user_id;
        }*/
        //переход на форму после входа/регистрации
        private void goToUserForm(int code)
        {
            if (code == 1)
            {
                StudentForm studentForm = new StudentForm(database, user_id);
                studentForm.Show();
                this.Hide();
            }
            else
            {
                TeacherForm teacherForm = new TeacherForm(database, user_id);
                teacherForm.Show();
                this.Hide();

            }
        }
        //подтверждение входа
        private void enter_button_Click(object sender, EventArgs e)
        {
            if (sign_in_role.SelectedIndex == -1 || sign_in_login.Text == "Введите логин" || sign_in_password.Text == "Введите пароль")
            {
                MessageBox.Show("Заполните обязательные поля!");
            }
            else
            {
                user_id = database.Search_registrationForm_user_id(sign_in_login, sign_in_password, sign_in_role.SelectedIndex);
                if (user_id != -1)
                {
                    goToUserForm(sign_in_role.SelectedIndex);
                }
                /*if (database.Search_registrationForm_user_id(sign_in_login, sign_in_password) != -1)
                {
                    goToUserForm(teacher_code);
                }
                else if (seachingUserIdInStudentTable() != -1)
                {
                    goToUserForm(student_code);
                }*/
                else
                {
                    MessageBox.Show("Неправильно указан логин и/или пароль");
                }

            }
        }


        private void Select_group_registration_SelectedIndexChanged(object sender, EventArgs e)
        {
            //заполнение коллекции выбор имени

           /*NpgsqlCommand select_full_name_registration_cmd = new NpgsqlCommand("SELECT full_name " +
                "FROM student JOIN groups ON groups.group_id = student.group_id " +
                "WHERE group_name LIKE '" + select_group_registration.SelectedItem.ToString() + "';", connection);
            NpgsqlDataReader reader_select_full_name_registration_cmd = select_full_name_registration_cmd.ExecuteReader();

            if (reader_select_full_name_registration_cmd.HasRows)
            {
                select_full_name_registration.Items.Clear();

                while (reader_select_full_name_registration_cmd.Read())
                {
                    select_full_name_registration.Items.Add(reader_select_full_name_registration_cmd.GetString(0));
                }
            }

            reader_select_full_name_registration_cmd.Close();*/
            database.Fill_registrationForm_collection_select_student_name(select_group_registration, select_full_name_registration);
        }

        private void sign_in_role_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
    }
}

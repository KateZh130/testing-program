using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;


namespace testing_program
{
    public partial class TeacherForm : Form
    {
        readonly TextboxClass textboxclass = new TextboxClass();
        readonly CheckboxClass checkboxclass = new CheckboxClass();
        readonly ComboboxClass comboboxclass = new ComboboxClass();
        readonly DatabaseClass database;

        private int test_id = 0;
        private int version_id = 0;
        private int version_counter = 0;
        private int question_counter = 1;
        readonly int user_id;

        List<string> profil_info_list = new List<string>();

        public TeacherForm(DatabaseClass database, int user_id)
        {
            InitializeComponent();
            this.database = database;
            this.user_id = user_id;
            this.Text = database.Get_name(user_id, "teacher");
            FillTeacherProfileMainPanel();
        }

        private void FillTeacherProfileMainPanel()
        {
            teacher_profile_main_panel.Visible = true;
            change_teacher_login_panel.Visible = false;
            change_teacher_password_panel.Visible = false;
            teacher_identity_check_panel.Visible = false;

            profil_info_list = database.Get_teacherForm_profile_info(user_id)?.Split(' ').ToList();
            full_name_teacher_profile_textBox.Text = profil_info_list[0] + " " + profil_info_list[1] + " " + profil_info_list[2];
            login_teacher_profile.Text = profil_info_list[3];
            password_teacher_profile.Text = profil_info_list[4];

        }
        private void TeacherForm_Load(object sender, EventArgs e)
        {
        }

        private void Change_teacher_login_button_Click(object sender, EventArgs e)
        {
            change_teacher_login_panel.Visible = true;
            teacher_profile_main_panel.Visible = false;
        }

        private void New_teacher_login_button_Click(object sender, EventArgs e)
        {
            if (new_teacher_login_textbox.Text != "")
            {
                database.Update_user_login_or_password(new_teacher_login_textbox, user_id, "login", "teacher");
                FillTeacherProfileMainPanel();
                MessageBox.Show("Логин успешно изменен!");
                new_teacher_login_textbox.Clear();
            }
            else
            {
                MessageBox.Show("Логин не введен", "Введите логин");
            }
        }

        private void Cancel_change_teacher_login_button_Click(object sender, EventArgs e)
        {
            change_teacher_login_panel.Visible = false;
            teacher_profile_main_panel.Visible = true;
        }

        private void Change_teacher_password_button_Click(object sender, EventArgs e)
        {
            teacher_identity_check_panel.Visible = true;
            teacher_profile_main_panel.Visible = false;
        }

        private void New_teacher_password_button_Click(object sender, EventArgs e)
        {
            if (teacher_test_new_password_textBox.Text == "" || teacher_new_password_textBox.Text == "")
            {
                MessageBox.Show("Введите пароль.");
            }
            else if (textboxclass.Check_passwords_are_matching(teacher_test_new_password_textBox.Text, teacher_new_password_textBox.Text))
            {
                database.Update_user_login_or_password(teacher_test_new_password_textBox, user_id, "password", "teacher");
                FillTeacherProfileMainPanel();
                MessageBox.Show("Пароль успешно изменен!");
                teacher_test_new_password_textBox.Clear();
                teacher_new_password_textBox.Clear();
            }
        }

        private void Cancel_change_teacher_password_button_Click(object sender, EventArgs e)
        {
            change_teacher_password_panel.Visible = false;
            teacher_profile_main_panel.Visible = true;
        }

        private void Continue_teacher_change_password_button_Click(object sender, EventArgs e)
        {
            if (teacher_old_password_textBox.Text == "")
            {
                MessageBox.Show("Введите пароль.");
            }
            else if (textboxclass.Check_passwords_are_matching(teacher_old_password_textBox.Text, profil_info_list[4]))
            {
                teacher_old_password_textBox.Clear();
                teacher_identity_check_panel.Visible = false;
                change_teacher_password_panel.Visible = true;
            }
        }

        private void Cancel_teacher_change_password_button_Click(object sender, EventArgs e)
        {
            teacher_old_password_textBox.Clear();
            teacher_identity_check_panel.Visible = false;
            teacher_profile_main_panel.Visible = true;
        }

        private void New_timer_textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!char.IsDigit(number) && number != 8) // цифры и клавиша BackSpace
            {
                e.Handled = true;
            }
        }

        private void Create_new_test_buttton_Click(object sender, EventArgs e)
        {
        }

        private void New_test_name_textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void New_timer_textBox_TextChanged(object sender, EventArgs e)
        {
        }

        bool NewTestNameIsChanged = false;
        private void New_test_name_textBox_TextChanged(object sender, EventArgs e)
        {
            NewTestNameIsChanged = textboxclass.Check_text_is_changed(new_test_name_textBox, "Введите название теста");
        }

        private void New_test_name_textBox_Click(object sender, EventArgs e)
        {
            NewTestNameIsChanged = textboxclass.Check_is_cleared(new_test_name_textBox, "Введите название теста", NewTestNameIsChanged);
        }

        private void New_test_name_textBox_Leave(object sender, EventArgs e)
        {
            textboxclass.Return_original_text(new_test_name_textBox, "Введите название теста");
        }

        private void Full_name_teacher_profile_texBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void Add_new_test_to_db_Click(object sender, EventArgs e)
        {
            if (textboxclass.Check_text_is_changed(new_test_name_textBox, "Введите название теста"))
            {
                test_id = database.Add_new_test_to_database(new_test_name_textBox, new_timer_textBox);
                create_test_groupBox.Visible = true;
                create_version_questions_groupBox.Visible = true;
                create_version_questions_groupBox.Text = "Вопрос " + question_counter;
                ++version_counter;
                version_id = database.Create_new_test_version(test_id, version_counter);
                //автоматом создать 1 вариант

                //окно выбора типа вопроса
                //окно создания вопроса
                //кнопка добавить следующий вопрос(тип вопроса и новое окно создания вопроса)
                //следующий вариант с всплывающей подсказкой "Завершить создание этого варианта и создать еще один "(должно появиться окно с кнопкой добавить новый вариант в тест / кнопка завершить создание тестов с уведомлением)
                //закончить создание теста(уведомление что если надо позже добавить еще вариант или вопрос то необходимо это делать через редактирование тестов)
            }
            else
            {
                MessageBox.Show("Заполните обязательные поля!");
            }

        }

        private void Add_next_question_button_Click(object sender, EventArgs e)
        {
            string q_text = "Введите текст вопроса";
            string a_text = "Введите текст варианта ответа";
            
            if (!comboboxclass.Check_is_changed(question_type_comboBox) ||
                !textboxclass.Check_textboxes_text_are_changed(new string[] { q_text, a_text, a_text, a_text },
                new TextBox[] { question_textBox, answer_textBox1, answer_textBox2, answer_textBox3 }))
            {
                MessageBox.Show("Заполните обязательные поля!");
            }
            else
            {
                int question_id = database.Create_question_text(question_type_comboBox, question_textBox.Text);
                question_textBox.Clear();
                comboboxclass.Clear_selection(question_type_comboBox);
                database.Create_version_question_connection(question_id, version_id);

                TextBox[] answers_textboxes = { answer_textBox1, answer_textBox2, answer_textBox3 };
                CheckBox[] is_answer_correct = { checkBox1, checkBox2, checkBox3 };
                int answer_id;
                for (int i = 0; i < answers_textboxes.Length; ++i)
                {
                    answer_id = database.Create_answer_text(answers_textboxes[i].Text);
                    database.Create_question_answer_connection(question_id, answer_id, is_answer_correct[i].Checked);
                    answers_textboxes[i].Clear();
                    checkboxclass.Clear(is_answer_correct[i]);
                }
                ++question_counter;
                create_version_questions_groupBox.Text = "Вопрос " + question_counter;
            }
        }

        private void TabControl1_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab.Name == "test_constructor")
            {
                TabControl tabcon = (TabControl)sender;
                contextMenuStrip1.Show(tabcon, new Point(66, 28));
            }
        }

        private void Create_test_MenuItem_Click(object sender, EventArgs e)
        {
            contextMenuStrip1.Visible = false;
            create_test_groupBox.Visible = true;
        }

        private void Create_next_version_Click(object sender, EventArgs e)
        {
        }

        private void TeacherForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void exit_student_profile_button_Click(object sender, EventArgs e)
        {
            Application.OpenForms[0].Show();
            this.Hide();
        }
    }
}

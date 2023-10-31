using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace testing_program
{
    public partial class StudentForm : Form
    {
        readonly DatabaseClass database;
        readonly CheckboxClass checkbox = new CheckboxClass();
        readonly ComboboxClass combobox = new ComboboxClass();
        readonly DataGridViewClass data = new DataGridViewClass();
        readonly TextboxClass textbox = new TextboxClass();
        readonly RadiobuttonClass radiobutton = new RadiobuttonClass();
        readonly GroupboxPanelClass panel = new GroupboxPanelClass();
        readonly Stopwatch stopWatch = new Stopwatch();
        List<string> right_answers = new List<string>();
        List<string> answers_text = new List<string>();
        List<string> questions = new List<string>();
        readonly int user_id;
        int test_id = 0;
        int number_of_questions;
        int scores = 0;
        int timer;
        int version_id = 0;
        int question_index = 0;
        int question_type = 0;
        int student_test_id = 0;

        public StudentForm(DatabaseClass database, int user_id)
        {
            InitializeComponent();
            this.database = database;
            this.user_id = user_id;
            student_profile_main_panel.Visible = true;
        }
        //*********************закрытие программы**********************
        private void StudentForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
        //*********************выход из профиля************************
        private void Exit_student_profile_button_Click(object sender, EventArgs e)
        {
            Application.OpenForms[0].Show();
            this.Hide();
        }
        //****************начало тестирования********************
        private void Start_test_button_Click(object sender, EventArgs e)
        {
            if (test_id == 0)
            {
                MessageBox.Show("Выберите тест из списка");
            }
            else
            {
                string test_name = available_test_table.SelectedCells[1].Value.ToString();
                timer = database.Get_test_timer(test_id);
                test_timer.Interval = timer * 60000;
                DialogResult dialogResult = MessageBox.Show("Ограничение по времени: " +
                    "" + timer + " мин.\n" + test_name +
                    " можно пройти только 1 раз. Вы уверены, что хотите начать прямо сейчас? ",
                    "Начать попытку", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    panel.Change_visible(choose_test_panel, false);
                    student_test_id = database.Get_student_test_id(test_name, user_id);
                    Get_test_version();
                    number_of_questions = database.Get_number_of_questions(version_id);
                    questions = database.Get_test_questions(version_id, questions);
                    Get_answers_and_open_task_panel();
                    test_timer.Start();
                    stopWatch.Start();
                }
            }
        }
        //***********получение данных о тесте/задании************
        private void Get_test_version()
        {
            List<int> versions = database.Get_test_versions(test_id);
            int index = user_id % versions.Count;
            version_id = versions[index];
            versions.Clear();
        }

        private void Get_answers_and_open_task_panel()
        {
            if (question_index < number_of_questions)
            {
                question_type = database.Get_question_type(version_id, questions[question_index]);
                answers_text = database.Get_answers_text(version_id, questions[question_index]);
                right_answers = database.Get_right_answers(version_id, questions[question_index]);
                if (question_type == 1) //вопрос с одним правильным ответом
                    panel.Change_visible(one_answer_panel, true);
                else if (question_type == 2)  //вопрос с несколькими правильными ответами
                    panel.Change_visible(many_answer_panel, true);
            }
            else
            {
                test_timer.Stop();
                stopWatch.Stop();
                Finish_testing();
            }
        }
        //***************подсчет оценки и завершение тестирования************************
        private int Calculate_mark()
        {
            int percent;
            percent = scores * 100 / number_of_questions;
            if (percent < 50)
                return 2;
            if (percent >= 50 && percent < 70)
                return 3;
            if (percent >= 70 && percent < 85)
                return 4;
            return 5;
        }

        private void Finish_testing()
        {
            int mark = Calculate_mark();
            decimal time = (decimal)stopWatch.ElapsedMilliseconds / (decimal)60000;
            MessageBox.Show("Тест пройден!\nВаша оценка: " + mark + "\n" +
                "Чтобы получить больше информации зайдите в раздел Результаты", "Результат");
            database.Add_mark_to_database(mark, student_test_id, time);
            questions.Clear();
            stopWatch.Reset();
            test_id = 0;
            version_id = 0;
            scores = 0;
            question_index = 0;
            student_test_id = 0;
            panel.Change_visible(choose_test_panel, true);
        }
        //**********проверка и сохранение ответов, переход на следующий вопрос************
        private void One_answer_button_Click(object sender, EventArgs e)
        {
            Save_and_count_scores(Check_one_answer_task());
            panel.Change_visible(one_answer_panel, false);
            ++question_index;
            Get_answers_and_open_task_panel();
        }

        private bool Check_one_answer_task()
        {
            RadioButton[] radioButtons = { radioButton1, radioButton2, radioButton3, radioButton4 };
            for (int i = 0; i < radioButtons.Length; ++i)
            {
                if (radioButtons[i].Text == right_answers[0])
                {
                    return radioButtons[i].Checked;
                }
            }
            return false;
        }

        public bool Check_many_answers_task()
        {
            List<string> choosen_answers = new List<string>();
            CheckBox[] checkBoxes = { checkBox1, checkBox2, checkBox3, checkBox4 };
            for (int i = 0; i < checkBoxes.Length; ++i)
            {
                if (checkBoxes[i].Checked == true)
                    choosen_answers.Add(checkBoxes[i].Text);
            }
            return choosen_answers.OrderBy(m => m).SequenceEqual(right_answers.OrderBy(m => m));
        }

        private void Many_answers_button_Click(object sender, EventArgs e)
        {
            Save_and_count_scores(Check_many_answers_task());
            panel.Change_visible(many_answer_panel, false);
            ++question_index;
            Get_answers_and_open_task_panel();
        }

        private void Save_and_count_scores(bool answer)
        {
            if (answer)
                ++scores;
            database.Save_student_answer(user_id, version_id, questions[question_index], answer, student_test_id);
        }

        //**************ограничение по времени*************************
        private void Test_timer_Tick(object sender, EventArgs e)
        {
            test_timer.Stop();
            stopWatch.Stop();
            if (question_type == 1)
            {
                Save_and_count_scores(Check_one_answer_task());
                panel.Change_visible(one_answer_panel, false);
            }
            else if (question_type == 2)
            {
                Save_and_count_scores(Check_many_answers_task());
                panel.Change_visible(many_answer_panel, false);
            }
            Finish_testing();
        }
        //****************отмена изменения данных профиля******************

        private void Cancel_change_name_button_Click(object sender, EventArgs e)
        {
            panel.Change_visible(new Panel[] { change_full_name_panel, student_profile_main_panel }, new bool[] { false, true });
        }

        private void Cancel_student_change_password_button_Click(object sender, EventArgs e)
        {
            panel.Change_visible(new Panel[] { student_identity_check_panel, student_profile_main_panel }, new bool[] { false, true });
        }

        private void Cancel_change_student_login_button_Click(object sender, EventArgs e)
        {
            panel.Change_visible(new Panel[] { change_student_login_panel, student_profile_main_panel }, new bool[] { false, true });
        }

        private void Cancel_change_student_password_button_Click(object sender, EventArgs e)
        {
            panel.Change_visible(new Panel[] { change_student_password_panel, student_profile_main_panel }, new bool[] { false, true });
        }
        //************открытие панелей изменения данных профиля***************
        private void Change_student_login_button_Click(object sender, EventArgs e)
        {
            panel.Change_visible(new Panel[] { change_student_login_panel, student_profile_main_panel }, new bool[] { true, false });
        }

        private void Change_student_password_button_Click(object sender, EventArgs e)
        {
            panel.Change_visible(new Panel[] { student_identity_check_panel, student_profile_main_panel }, new bool[] { true, false });
        }

        private void Change_name_button_Click(object sender, EventArgs e)
        {
            panel.Change_visible(new Panel[] { change_full_name_panel, student_profile_main_panel }, new bool[] { true, false });
        }
        //**************изменение данных профиля****************
        private void Change_full_name_button_Click(object sender, EventArgs e)
        {
            TextBox[] textBoxes = { new_surname_textBox, new_student_name_textBox, new_patronymic_textBox };
            if (textbox.Check_text_changed(textBoxes))
            {
                if (textbox.Check_invalid_characters(textBoxes, true))
                {
                    database.Update_user_full_name(new_surname_textBox.Text + " " + new_student_name_textBox.Text + " " + new_patronymic_textBox.Text, user_id, "student");
                    panel.Change_visible(new Panel[] { change_full_name_panel, student_profile_main_panel }, new bool[] { false, true });
                }
            }
            else MessageBox.Show("Заполните обязательные поля!");
        }

        private void New_student_password_button_Click(object sender, EventArgs e)
        {
            TextBox[] textBoxes = { student_test_new_password_textBox, student_new_password_textBox };
            if (textbox.Check_text_changed(textBoxes))
            {
                if (textbox.Check_passwords_are_matching(student_test_new_password_textBox.Text, student_new_password_textBox.Text) &&
                    textbox.Check_invalid_characters_with_space(textBoxes))
                {
                    database.Update_user_login_or_password(student_test_new_password_textBox.Text, user_id, "password", "student");
                    panel.Change_visible(new Panel[] { change_student_password_panel, student_profile_main_panel }, new bool[] { false, true });
                    MessageBox.Show("Пароль успешно изменен.");
                }
            }
            else MessageBox.Show("Заполните обязательные поля!");
        }

        private void Continue_student_change_password_button_Click(object sender, EventArgs e)
        {
            if (textbox.Check_text_changed(student_old_password_textBox))
            {
                if (textbox.Check_passwords_are_matching(student_old_password_textBox.Text, password_student_profile.Text) &&
                    textbox.Check_invalid_characters_with_space(student_old_password_textBox))
                {
                    panel.Change_visible(new Panel[] { student_identity_check_panel, change_student_password_panel }, new bool[] { false, true });
                }
            }
            else MessageBox.Show("Заполните обязательные поля!");
        }

        private void New_student_login_button_Click(object sender, EventArgs e)
        {
            if (textbox.Check_text_changed(new_student_login_textbox))
            {
                if (textbox.Check_invalid_characters_with_space(new_student_login_textbox))
                {
                    database.Update_user_login_or_password(new_student_login_textbox.Text, user_id, "login", "student");
                    panel.Change_visible(new Panel[] { change_student_login_panel, student_profile_main_panel }, new bool[] { false, true });
                    MessageBox.Show("Логин успешно изменен.");
                }
            }
            else MessageBox.Show("Заполните обязательные поля!");
        }
        //***************заполнение панели профиля*************
        private void Student_profile_main_panel_VisibleChanged(object sender, EventArgs e)
        {
            if (student_profile_main_panel.Visible)
            {
                List<string> profil_info_list = new List<string>();
                database.Get_studentForm_profil_info(user_id, profil_info_list);
                textbox.Fill(new TextBox[] { full_name_student_profile, group_student_profile, login_student_profile, password_student_profile },
                    new string[] { profil_info_list[0], profil_info_list[1], profil_info_list[2], profil_info_list[3] });
                Text = database.Get_user_name(user_id, "student");
            }
        }
        //********заполнение/очищение ComboBox с решёнными тестами**********
        private void Results_panel_VisibleChanged(object sender, EventArgs e)
        {
            if (results_panel.Visible)
            {
                database.Get_student_completed_tests(user_id, passed_tests);
            }
            else
            {
                combobox.Return_original_text(passed_tests, "Выберите тест");
                combobox.Delete_collection(passed_tests);
                radiobutton.Clear_selection(new RadioButton[] { show_list_of_completed_tests, show_one_test_result });
            }
        }
        //****************управление вкладками*******************
        private void TabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
                panel.Change_visible(student_profile_main_panel, true);
            else if (tabControl1.SelectedIndex == 1)
                panel.Change_visible(choose_test_panel, true);
            else
                panel.Change_visible(results_panel, true);
        }

        private void Test_page_Leave(object sender, EventArgs e)
        {
            panel.Change_visible(choose_test_panel, false);
        }

        private void Profile_page_Leave(object sender, EventArgs e)
        {
            panel.Change_visible(student_profile_main_panel, false);
        }

        private void Result_page_Leave(object sender, EventArgs e)
        {
            panel.Change_visible(results_panel, false);
        }
        //***********заполнение/очищение панелей изменения данных профиля***********
        private void Change_student_login_panel_VisibleChanged(object sender, EventArgs e)
        {
            if (!change_student_login_panel.Visible)
            {
                new_student_login_textbox.Clear();
            }
        }

        private void Change_student_password_panel_VisibleChanged(object sender, EventArgs e)
        {
            if (!change_student_password_panel.Visible)
            {
                textbox.Clear(new TextBox[] { student_test_new_password_textBox, student_new_password_textBox });
            }
        }

        private void Student_identity_check_panel_VisibleChanged(object sender, EventArgs e)
        {
            if (!student_identity_check_panel.Visible)
            {
                student_old_password_textBox.Clear();
            }
        }

        private void Change_full_name_panel_VisibleChanged(object sender, EventArgs e)
        {
            if (!change_full_name_panel.Visible)
                textbox.Clear(new TextBox[] { new_surname_textBox, new_student_name_textBox, new_patronymic_textBox });
        }
        //***********заполнение/очищение панелей с вопросами***********
        private void One_answer_panel_VisibleChanged(object sender, EventArgs e)
        {
            if (one_answer_panel.Visible)
            {
                radiobutton.Fill_text(new RadioButton[] { radioButton1, radioButton2, radioButton3, radioButton4 }, answers_text);
                string button_text = "Следующий вопрос";
                if (question_index + 1 == number_of_questions)
                    button_text = "Закончить попытку";
                question_number_label2.Text = "Вопрос " + (question_index + 1);
                question_text_label2.Text = questions[question_index];
                one_answer_button.Text = button_text;
            }
            else
            {
                RadioButton[] radioButtons = { radioButton1, radioButton2, radioButton3, radioButton4 };
                for (int i = 0; i < radioButtons.Length; ++i)
                    radioButtons[i].Checked = false;
                right_answers.Clear();
                answers_text.Clear();
            }
        }

        private void Many_answer_panel_VisibleChanged(object sender, EventArgs e)
        {
            if (many_answer_panel.Visible)
            {
                checkbox.Fill_text(new CheckBox[] { checkBox1, checkBox2, checkBox3, checkBox4 }, answers_text);
                string button_text = "Следующий вопрос";
                if (question_index + 1 == number_of_questions)
                    button_text = "Закончить попытку";
                question_number_label.Text = "Вопрос " + (question_index + 1);
                question_text_label.Text = questions[question_index];
                many_answer_button.Text = button_text;
            }
            else
            {
                CheckBox[] checkBoxes = { checkBox1, checkBox2, checkBox3, checkBox4 };
                for (int i = 0; i < checkBoxes.Length; ++i)
                    checkbox.Clear(checkBoxes[i]);
                right_answers.Clear();
                answers_text.Clear();
            }

        }
        //*************заполнение таблиц и взаимодействия с ними***************************
        private void Passed_tests_SelectionChangeCommitted(object sender, EventArgs e)
        {
            student_test_id = database.Get_student_test_id(passed_tests.SelectedItem.ToString(), user_id, true);
            data.Reset_one_test_result_table(database, one_result_dataGridView, student_test_id);
        }

        private void Choose_test_panel_VisibleChanged(object sender, EventArgs e)
        {
            if (choose_test_panel.Visible)
            {
                data.Reset_student_available_test_table(database, user_id, available_test_table);
            }
        }

        private void Show_list_of_completed_tests_CheckedChanged(object sender, EventArgs e)
        {
            if (show_list_of_completed_tests.Checked)
                data.Reset_completed_tests_list_table(database, user_id, results_dataGridView);
            else data.Hide(results_dataGridView);
        }

        private void Show_one_test_result_CheckedChanged(object sender, EventArgs e)
        {
            if (show_one_test_result.Checked)
                combobox.Change_visible(passed_tests, true);
            else
            {
                combobox.Return_original_text(passed_tests, "Выберите тест");
                combobox.Change_visible(passed_tests, false);
                data.Hide(one_result_dataGridView);
            }
        }

        private void Available_test_table_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
                return;
            test_id = Convert.ToInt32(available_test_table.SelectedCells[0].Value);
            data.Change_back_color(available_test_table, Color.LightSteelBlue);
            data.Change_fore_color(available_test_table, Color.Black);
        }
        //*************запрет на ввод в comboBox*******************************
        private void Choose_test_result_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void Full_name_student_profile_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void Group_student_profile_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void Login_student_profile_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void Password_student_profile_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
    }

}



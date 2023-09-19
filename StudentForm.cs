using System;
using System.Collections.Generic;
using System.Data;
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
        readonly int user_id;
        int test_id = -1;
        int number_of_questions;
        int scores = 0;
        int timer;
        int version_id = -1;

        List<string> right_answers = new List<string>();
        List<string> answers_text = new List<string>();
        List<string> questions = new List<string>();
        List<string> profil_info_list = new List<string>();
        int question_index = 0;

        public StudentForm(DatabaseClass database, int user_id)
        {
            InitializeComponent();
            this.database = database;
            this.user_id = user_id;
            this.Text = database.Get_name(user_id, "student");
            FillTestPage();
            FillStudentProfileMainPanel();
        }

        private void ResetSelection(int question_type)
        {
            RadioButton[] radioButtons = { radioButton1, radioButton2, radioButton3 };
            CheckBox[] checkBoxes = { checkBox1, checkBox2, checkBox3 };
            for (int i = 0; i < radioButtons.Length; ++i)
            {
                if (question_type == 1)
                {
                    radioButtons[i].Checked = false;
                }
                else
                {
                    checkbox.Clear(checkBoxes[i]);
                }
            }
        }

        private void FillStudentProfileMainPanel()
        {
            student_profile_main_panel.Visible = true;
            change_student_login_panel.Visible = false;
            change_student_password_panel.Visible = false;
            student_identity_check_panel.Visible = false;

            profil_info_list = database.Get_studentForm_profil_info(user_id)?.Split(' ').ToList();
            TextBox[] textBoxes = { full_name_student_profile, group_student_profile, login_student_profile, password_student_profile };
            string[] text = { profil_info_list[0] + " " + profil_info_list[1] + " " + profil_info_list[2], profil_info_list[3], profil_info_list[4], profil_info_list[5] };
            textbox.Fill_textboxes(textBoxes, text);
        }

        public void FillTestPage()
        {
            choose_test_panel.Visible = true;
            data.Reset_test_table(database, user_id, available_test_table);
            data.Change_column_header_text(available_test_table, 1, "Название теста");
            data.Change_column_width(available_test_table, 1, 605);
            this.available_test_table.Columns[0].Visible = false;
            data.Change_back_color(available_test_table, Color.White);
            data.Change_fore_color(available_test_table, Color.Black);
        }

        private void StudentForm_Load(object sender, EventArgs e)
        {
        }
        //ДОБАВИТЬ ВЫБОР ВАРИАНТОВ ПО ФОРМУЛЕ( ЕСТЬ В ФАЙЛЕ ФУНКЦИИ ПРОГИ) *
        //ДАЛЕЕ ИСПОЛЬЗУЕМ ID ВАРИАНТА(ID ВАРИАНТА УНИКАЛЕН) *
        //ДЛЯ ТЕСТИРОВАНИЯ ID ТЕСТА В ПРИНЦИПЕ НАВЕРНОЕ НЕ НУЖЕН, ТОЛЬКО ДЛЯ ЗАПИСИ РЕЗУЛЬТАТОВ И ФОРМЫ УЧИТЕЛЯ *
        //ОН НУЖЕН И ДЛЯ ФОРМЫ УЧИТЕЛЯ ПРИ СОЗДАНИИ ТЕСТОВ, УДАЛЕНИИ ВАРИАНТОВ, РЕДАКТИРОВАНИИ) *

        //ПОМЕНЯТЬ ВСЕ ЗАПРОСЫ *

        /// СТОИТ ЛИ ДЕЛАТЬ ЗАПРОС НА ПОЛУЧЕНИЕ test_version_id ЧТОБЫ ВО ВСЕ ЗАПРОСАХ ПИСАТЬ ЭТО А НЕ test_id version_id???? нет

        private void GetTestVersion()
        {
            List<int> versions = database.Get_test_versions(test_id);
            int index = user_id % versions.Count;
            version_id = versions[index];
            versions.Clear();
        }

        private void Start_test_button_Click(object sender, EventArgs e)
        {
            if (test_id == -1)
            {
                MessageBox.Show("Выберите тест", "Ошибка");
            }
            else
            {
                timer = database.Get_test_timer(test_id);
                test_timer.Interval = timer * 60000;
                DialogResult dialogResult = MessageBox.Show("Ограничение по времени: " +
                    "" + timer + " мин.\n" + available_test_table.SelectedCells[1].Value.ToString() +
                    " можно пройти только 1 раз. Вы уверены, что хотите начать прямо сейчас? ",
                    "Начать попытку", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    choose_test_panel.Visible = false;
                    GetTestVersion();
                    number_of_questions = database.Get_number_of_questions(version_id);
                    questions = database.Get_test_questions(version_id, questions);
                    FillTestingPanelAndGetRightAnswer();
                    test_timer.Start();
                }
                else if (dialogResult == DialogResult.No)
                {
                }
            }
        }
        private void FillTestingPanelAndGetRightAnswer()
        {
            int question_type;
            if (question_index < number_of_questions)
            {
                question_type = database.Get_question_type(version_id, questions[question_index]);
                try
                {
                    FillLabelsAndButtons(question_type);
                    ResetSelection(question_type);
                    answers_text = database.Get_answers_text(version_id, questions[question_index], answers_text);
                    right_answers = database.Get_answers(version_id, questions[question_index], right_answers);
                }
                catch { MessageBox.Show("Ошибка получения ответов"); }
                
                if (question_type == 1) //вопрос с одним правильным ответом
                {
                    one_answer_panel.Visible = true;
                    RadioButton[] radioButtons = { radioButton1, radioButton2, radioButton3 };
                    radiobutton.Fill_text(radioButtons, answers_text);
                }
                else if (question_type == 2)  //вопрос с несколькими правильными ответами
                {
                    many_answer_panel.Visible = true;
                    CheckBox[] checkBoxes = { checkBox1, checkBox2, checkBox3 };
                    checkbox.Fill_text(checkBoxes, answers_text);
                }
            }
            else
            {
                test_timer.Stop();
                Finish_testing();
            }
        }
        private void FillLabelsAndButtons(int type)
        {
            string button_text = "Следующий вопрос";
            if (question_index + 1 == number_of_questions)
                button_text = "Закончить попытку";

            if (type == 1)
            {
                question_number_label2.Text = "Вопрос " + (question_index + 1);
                question_text_label2.Text = questions[question_index];
                one_answer_button.Text = button_text;
            }
            else
            {
                question_number_label.Text = "Вопрос " + (question_index + 1);
                question_text_label.Text = questions[question_index];
                many_answer_button.Text = button_text;
            }
        }
        private void Finish_testing()
        {
            int mark = Calculate_mark();
            MessageBox.Show("Тест пройден!\nВаша оценка: " + mark + "\n" +
                "Чтобы получить больше информации зайдите в раздел Результаты", "Результат");
            database.Add_mark_to_database(mark, test_id, user_id);
            data.Reset_test_table(database, user_id, available_test_table);
            questions.Clear();
            Clear_test_variables();
            choose_test_panel.Visible = true;
        }
        private void Clear_test_variables()
        {
            test_id = -1;
            version_id = -1;
            scores = 0;
            question_index = 0;
        }

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

        private void Available_test_table_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            test_id = Convert.ToInt32(available_test_table.SelectedCells[0].Value);
            data.Change_back_color(available_test_table, Color.SkyBlue);
            data.Change_fore_color(available_test_table, Color.Black);
        }

        private void One_answer_button_Click(object sender, EventArgs e)
        {
            Save_and_count_scores(Check_one_answer());
            one_answer_panel.Visible = false;
            Clear_lists();
            FillTestingPanelAndGetRightAnswer();
        }

        private bool Check_one_answer()
        {
            RadioButton[] radioButtons = { radioButton1, radioButton2, radioButton3 };
            for(int i=0; i < radioButtons.Length; ++i)
            {
                if( radioButtons[i].Text==right_answers[0])
                {
                    return radioButtons[i].Checked;
                }
            }
            return false;
        }
        public bool Check_many_answers()
        {
            List<string> choosen_answers = new List<string>();
            CheckBox[] checkBoxes = { checkBox1, checkBox2, checkBox3 };
            for(int i = 0; i < checkBoxes.Length; ++i)
            {
                if (checkBoxes[i].Checked == true)
                    choosen_answers.Add(checkBoxes[i].Text);
            }
           bool result= choosen_answers.OrderBy(m => m).SequenceEqual(right_answers.OrderBy(m => m));
            choosen_answers.Clear();
            return result;
        }

        private void Many_answers_button_Click(object sender, EventArgs e)
        {
            Save_and_count_scores(Check_many_answers());
            many_answer_panel.Visible = false;
            Clear_lists();
            FillTestingPanelAndGetRightAnswer();
        }

        private void Save_and_count_scores(bool answer)
        {
            if (answer)
                ++scores;
            database.Save_student_answer(user_id, version_id, questions[question_index], answer);
        }

        private void Clear_lists()
        {
            right_answers.Clear();
            answers_text.Clear();
            ++question_index;
        }

        private void StudentForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
                    }

        private void Show_list_of_completed_tests_CheckedChanged(object sender, EventArgs e)
        {
            passed_tests.Visible = false;
            data.Hide(one_result_dataGridView);
            data.Reset_completed_tests_list_table(database, user_id, results_dataGridView);
        }

        private void Show_one_test_result_CheckedChanged(object sender, EventArgs e)
        {
            passed_tests.Visible = true;
            database.Fill_studentForm_collection_passed_tests(user_id, passed_tests);
        }

        private void Choose_test_result_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void Passed_tests_SelectedIndexChanged(object sender, EventArgs e)
        {
            data.Hide(results_dataGridView);
            data.Reset_one_test_result_table(database, user_id, one_result_dataGridView, passed_tests);
        }

        private void group_student_profile_label_Click(object sender, EventArgs e)
        {
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

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void change_student_login_label_Click(object sender, EventArgs e)
        {
        }

        private void Change_student_login_button_Click(object sender, EventArgs e)
        {
            change_student_login_panel.Visible = true;
            student_profile_main_panel.Visible = false;
        }

        private void New_student_login_button_Click(object sender, EventArgs e)
        {
            if (new_student_login_textbox.Text != "")
            {
                database.Update_user_login_or_password(new_student_login_textbox, user_id, "login", "student");
                FillStudentProfileMainPanel();
                MessageBox.Show("Логин успешно изменен!");
            }
            else
            {
                MessageBox.Show("Логин не введен", "Введите логин");
            }
        }

        private void Cancel_change_student_login_button_Click(object sender, EventArgs e)
        {
            change_student_login_panel.Visible = false;
            student_profile_main_panel.Visible = true;
        }

        private void label3_Click(object sender, EventArgs e)
        {
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
        }

        private void label4_Click(object sender, EventArgs e)
        {
        }

        private void Change_student_password_button_Click(object sender, EventArgs e)
        {
            student_identity_check_panel.Visible = true;
            student_profile_main_panel.Visible = false;
        }

        private void Cancel_change_student_password_button_Click(object sender, EventArgs e)
        {
            change_student_password_panel.Visible = false;
            student_profile_main_panel.Visible = true;
        }

        private void New_student_password_button_Click(object sender, EventArgs e)
        {
            if (student_test_new_password_textBox.Text == "" || student_new_password_textBox.Text == "")
            {
                MessageBox.Show("Введите пароль.");
            }
            else if (textbox.Check_passwords_are_matching(student_test_new_password_textBox.Text, student_new_password_textBox.Text))
            {
                database.Update_user_login_or_password(student_test_new_password_textBox, user_id, "password", "student");
                FillStudentProfileMainPanel();
                MessageBox.Show("Пароль успешно изменен!");
            }
        }

        private void Student_old_password_textBox_TextChanged(object sender, EventArgs e)
        {
        }

        private void Cancel_student_change_password_button_Click(object sender, EventArgs e)
        {
            student_old_password_textBox.Clear();
            student_identity_check_panel.Visible = false;
            student_profile_main_panel.Visible = true;
        }

        private void Continue_student_change_password_button_Click(object sender, EventArgs e)
        {
            if (student_old_password_textBox.Text == "")
            {
                MessageBox.Show("Введите пароль.");
            }
            else if (textbox.Check_passwords_are_matching(student_old_password_textBox.Text, profil_info_list[5]))
            {
                student_old_password_textBox.Clear();
                student_identity_check_panel.Visible = false;
                change_student_password_panel.Visible = true;
            }
        }
        //******************************************************************************
        //вспомогательная кнопка
        //УДАЛИТЬ ПОСЛЕ ТЕСТИРОВАНИЯ
        private void button1_Click(object sender, EventArgs e)
        {
            database.Delete_results_and_mark();
            data.Hide(one_result_dataGridView);
            data.Reset_completed_tests_list_table(database, user_id, results_dataGridView);
            data.Hide(results_dataGridView);
            data.Reset_one_test_result_table(database, user_id, one_result_dataGridView, passed_tests);
            data.Reset_test_table(database, user_id, available_test_table);
        }
        //*****************************************************************************************

        private void Test_timer_Tick(object sender, EventArgs e)
        {
            test_timer.Stop();
            bool result;
            if (database.Get_question_type(version_id, questions[question_index]) == 1)
            {
                result = Check_one_answer();
                one_answer_panel.Visible = false;
            }
            else
            {
                result = Check_many_answers();
                many_answer_panel.Visible = false;
            }
            Save_and_count_scores(result);
            Finish_testing();
        }

        private void profile_page_Click(object sender, EventArgs e)
        {

        }

        private void student_profile_main_panel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void exit_student_profile_button_Click(object sender, EventArgs e)
        {

            Application.OpenForms[0].Show();
            this.Hide();
        }
    }
}



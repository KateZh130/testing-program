using Npgsql;
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
    public partial class StudentForm : Form
    {
        DatabaseClass database;
        CheckboxClass checkboxclass = new CheckboxClass();
        ComboboxClass comboboxclass = new ComboboxClass();

        DataGridViewClass data = new DataGridViewClass();
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

        /*private DataSet set1 = new DataSet();
        private DataTable table1 = new DataTable();
        private DataSet set2 = new DataSet();
        private DataTable table2 = new DataTable();
        private DataSet set3 = new DataSet();
        private DataTable table3 = new DataTable();*/

        public StudentForm(DatabaseClass database, int user_id)
        {
            InitializeComponent();
            this.database = database;
            this.user_id = user_id;
            this.Text = database.Get_name(user_id, "student");
            fillTestPage();
            fillStudentProfileMainPanel();
        }
        //обнуление выбора кнопок(чтобы в след вопросе такого же типа не были выбраны)
        private void resetSelection(int question_type)
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
                    checkboxclass.Clear(checkBoxes[i]);
                    //checkBoxes[i].Checked = false;
                }
            }
        }

        private void fillStudentProfileMainPanel()
        {
            //string profile_info="";
            student_profile_main_panel.Visible = true;
            change_student_login_panel.Visible = false;
            change_student_password_panel.Visible = false;
            student_identity_check_panel.Visible = false;

            /*NpgsqlCommand get_profile_info = new NpgsqlCommand("SELECT " +
                "student.full_name, groups.group_name, student.login, student.password FROM student " +
                "JOIN groups ON groups.group_id = student.group_id " +
                "WHERE student.student_id = "+user_id+"; ", connection);
           
            NpgsqlDataReader reader = get_profile_info.ExecuteReader();
            while (reader.Read())
            {
                for(int s = 0; s < 4; ++s) 
                { 
                profile_info += reader.GetValue(s).ToString() +" ";
                }
            }
            reader.Close();*/
            profil_info_list = database.Get_studentForm_profil_info(user_id)?.Split(' ').ToList();
           // string[] profil_info_list = profile_info.Split(' ');
            full_name_student_profile.Text = profil_info_list[0] + " " + profil_info_list[1] + " " + profil_info_list[2];
            group_student_profile.Text = profil_info_list[3];
            login_student_profile.Text = profil_info_list[4];
            password_student_profile.Text = profil_info_list[5];

        }
        /*public void Reset_test_table()
        {
            NpgsqlCommand fill_available_test_table = new NpgsqlCommand("SELECT test.test_id, test_name " +
                "FROM test " +
                "JOIN student_test ON student_test.test_id = test.test_id " +
                "WHERE mark IS NULL " +
                "AND student_id = " + user_id + " " +
                "GROUP BY test.test_id;", connection);
            fill_available_test_table.ExecuteNonQuery();
            NpgsqlDataAdapter da1 = new NpgsqlDataAdapter(fill_available_test_table);
            set1.Reset();
            da1.Fill(set1);
            table1 = set1.Tables[0];
            available_test_table.DataSource = table1;
        }*/

       /*public void Reset_completed_tests_list_table()
        {
            one_result_dataGridView.Visible = false;
            results_dataGridView.Visible = true;

            NpgsqlCommand reset_list_of_completed_tests_table = new NpgsqlCommand("SELECT " +
                "test_name, mark, time FROM test, student_test " +
                "WHERE student_test.test_id = test.test_id " +
                "AND student_id=" + user_id + " " +
                "AND mark is not null;", connection);
            reset_list_of_completed_tests_table.ExecuteNonQuery();
            NpgsqlDataAdapter da2 = new NpgsqlDataAdapter(reset_list_of_completed_tests_table);

            set2.Reset();
            da2.Fill(set2);
            table2 = set2.Tables[0];
            results_dataGridView.DataSource = table2;

            results_dataGridView.Columns[0].HeaderText = "Название теста";
            results_dataGridView.Columns[0].Width = 350;
            results_dataGridView.Columns[1].HeaderText = "Оценка";
            results_dataGridView.Columns[1].Width = 70;
            results_dataGridView.Columns[2].HeaderText = "Дата и время прохождения";
            results_dataGridView.Columns[2].Width = 140;
        }*/
        //таблица подробных результатов *
       /* public void resetOneTestResultTable()
        {
            one_result_dataGridView.Visible = true;
            results_dataGridView.Visible = false;
            NpgsqlCommand reset_one_test_result_table = new NpgsqlCommand("SELECT question_text, results.answer FROM questions " +
                "JOIN version_question ON version_question.question_id = questions.question_id " +
                "JOIN results ON version_question.version_question_id = results.version_question_id " +
                "JOIN version ON version.version_id = version_question.version_id " +
                "JOIN test ON test.test_id = version.test_id " +
                "JOIN student_test ON student_test.test_id = test.test_id " +
                "WHERE student_test.student_id = " + user_id + " " +
                "AND test.test_name = '" + choose_test_result.Text + "' " +
                "GROUP BY question_text, results.answer;", connection);

            reset_one_test_result_table.ExecuteNonQuery();
            NpgsqlDataAdapter da3 = new NpgsqlDataAdapter(reset_one_test_result_table);
            set3.Reset();
            da3.Fill(set3);
            table3 = set3.Tables[0];
            one_result_dataGridView.DataSource = table3;
            one_result_dataGridView.Columns[0].HeaderText = "Вопрос";
            one_result_dataGridView.Columns[0].Width = 460;
            one_result_dataGridView.Columns[1].HeaderText = "Правильность выполнения";
            one_result_dataGridView.Columns[1].Width = 100;
        }*/

        public void fillTestPage()
        {
            choose_test_panel.Visible = true;
            data.Reset_test_table( database, user_id, available_test_table);
            data.Change_column_header_text(available_test_table, 1, "Название теста");
            //available_test_table.Columns[1].HeaderText = "Название теста";
            data.Change_column_width(available_test_table, 1, 605);
            //available_test_table.Columns[1].Width = 605;
            this.available_test_table.Columns[0].Visible = false;
            data.Change_back_color(available_test_table, Color.White);
            //available_test_table.RowsDefaultCellStyle.SelectionBackColor = Color.White;
            data.Change_fore_color(available_test_table, Color.Black);
            //available_test_table.RowsDefaultCellStyle.SelectionForeColor = Color.Black;
        }

        /*private void fillFormName()
        {
            NpgsqlCommand select_student_name = new NpgsqlCommand("SELECT full_name FROM student " +
                "WHERE student_id = " + user_id + ";", connection);
            this.Text = select_student_name.ExecuteScalar().ToString();
        }*/
       
        private void StudentForm_Load(object sender, EventArgs e)
        {
        }
        //ДОБАВИТЬ ВЫБОР ВАРИАНТОВ ПО ФОРМУЛЕ( ЕСТЬ В ФАЙЛЕ ФУНКЦИИ ПРОГИ) *
        //ДАЛЕЕ ИСПОЛЬЗУЕМ ID ВАРИАНТА(ID ВАРИАНТА УНИКАЛЕН) *
        //ДЛЯ ТЕСТИРОВАНИЯ ID ТЕСТА В ПРИНЦИПЕ НАВЕРНОЕ НЕ НУЖЕН, ТОЛЬКО ДЛЯ ЗАПИСИ РЕЗУЛЬТАТОВ И ФОРМЫ УЧИТЕЛЯ *
        //ОН НУЖЕН И ДЛЯ ФОРМЫ УЧИТЕЛЯ ПРИ СОЗДАНИИ ТЕСТОВ, УДАЛЕНИИ ВАРИАНТОВ, РЕДАКТИРОВАНИИ) *

        //ПОМЕНЯТЬ ВСЕ ЗАПРОСЫ *

        /// СТОИТ ЛИ ДЕЛАТЬ ЗАПРОС НА ПОЛУЧЕНИЕ test_version_id ЧТОБЫ ВО ВСЕ ЗАПРОСАХ ПИСАТЬ ЭТО А НЕ test_id version_id???? нет

        //запрос на получение варианта *
        private void getTestVersion()
        {
            List<int> versions=database.Get_test_versions(test_id);
            int index= user_id % versions.Count;
            version_id = versions[index];
            versions.Clear();
        }


        //*********************************************************************************

       /* //запрос на тип вопроса *
        private int Get_question_type()
        {
            NpgsqlCommand get_type_of_questions = new NpgsqlCommand("SELECT question_type FROM questions " +
                "JOIN version_question ON questions.question_id = version_question.question_id " +
                "WHERE version_id = " + version_id + " " +
                "AND question_text = '" + questions[question_index] + "';", connection);
            return Convert.ToInt32(get_type_of_questions.ExecuteScalar());
            
        }*/
        //**********************************************************************************
        /*//запрос на количество вопросов в тесте *
        private void Get_number_of_questions()
        {
            NpgsqlCommand get_number_of_questions = new NpgsqlCommand("SELECT COUNT(question_id) FROM version_question " +
                "WHERE version_id = '" + version_id + "';", connection);
            number_of_questions = Convert.ToInt32(get_number_of_questions.ExecuteScalar());
        }*/
        /*//список вопросов для теста *
        private void Get_test_questions()
        {
            NpgsqlCommand get_questions = new NpgsqlCommand("SELECT question_text FROM questions " +
                "JOIN version_question ON questions.question_id = version_question.question_id " +
                "WHERE version_id = " + version_id + "; ", connection);
            NpgsqlDataReader reader = get_questions.ExecuteReader();
           // int s = 0;
            while (reader.Read())
            {
                questions.Add(reader.GetValue(0).ToString());
              //  MessageBox.Show(questions[s]);
               // ++s;
            }
            reader.Close();
        }*/

        /*private void Get_test_timer()
        {
            NpgsqlCommand get_timer = new NpgsqlCommand("SELECT timer FROM test WHERE test_id = "+test_id+"; ", connection);
            timer = Convert.ToInt32(get_timer.ExecuteScalar());
            test_timer.Interval = timer * 60000;
        }*/

        private void start_test_button_Click(object sender, EventArgs e)
        {

            if (test_id == -1)
            {
                MessageBox.Show("Выберите тест", "Ошибка");
            }
            else
            {
                timer = database.Get_test_timer(test_id);
                test_timer.Interval = timer * 60000;
                //getTestTimer();
                DialogResult dialogResult = MessageBox.Show("Ограничение по времени: " +
                    ""+timer+" мин.\n"+ available_test_table.SelectedCells[1].Value.ToString() + 
                    " можно пройти только 1 раз. Вы уверены, что хотите начать прямо сейчас? ",
                    "Начать попытку", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    choose_test_panel.Visible = false;
                    getTestVersion();
                    number_of_questions= database.Get_number_of_questions(version_id);
                    questions = database.Get_test_questions(version_id, questions);
                    fillTestingPanelAndGetRightAnswer();
                    //запустить счетчик
                    test_timer.Start();
                }
                else if (dialogResult == DialogResult.No)
                {

                }
            }
        }
        private void fillTestingPanelAndGetRightAnswer()
        {
            int question_type;
            if (question_index < number_of_questions)
            {
                question_type = database.Get_question_type(version_id, questions[question_index]);
                if (question_type == 1) //вопрос с одним правильным ответом
                {
                    one_answer_panel.Visible = true;

                    try
                    {
                        fillLabelsAndButtons(question_type);
                        answers_text = database.Get_answers_text( version_id, questions[question_index], answers_text);
                        resetSelection(question_type);
                        radioButton1.Text = answers_text[0];
                        radioButton2.Text = answers_text[1];
                        radioButton3.Text = answers_text[2];
                    }
                    catch { MessageBox.Show("Ошибка получения текста ответов"); }
                    try
                    {
                        right_answers = database.Get_answers(version_id, questions[question_index], right_answers);
                    }
                    catch { MessageBox.Show("Ошибка получения ответов"); }
                }
                else if (question_type == 2)  //вопрос с несколькими правильными ответами
                {
                    many_answer_panel.Visible = true;
                    try
                    {
                        fillLabelsAndButtons(question_type);
                        answers_text = database.Get_answers_text(version_id, questions[question_index], answers_text);
                        resetSelection(question_type);
                        // MessageBox.Show("Тут");
                        checkBox1.Text = answers_text[0];
                        checkBox2.Text = answers_text[1];
                        checkBox3.Text = answers_text[2];
                    }
                    catch { MessageBox.Show("Ошибка получения текста ответов"); }
                    try
                    {
                        //getRightAnswer();
                        right_answers = database.Get_answers(version_id, questions[question_index], right_answers);
                    }
                    catch { MessageBox.Show("Ошибка получения ответов"); }
                }
            }
            else
            {
                //добавить сохранение и остановку времени
                test_timer.Stop();
                finishTesting();
            }
        }
        private void fillLabelsAndButtons(int type)
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
        // *
        private void finishTesting()
        {
            int mark = calculateMarkForTest();
            MessageBox.Show("Тест пройден!\nВаша оценка: " + mark + "\n" +
                "Чтобы получить больше информации зайдите в раздел Результаты", "Результат");
            //добавление результата в Результаты
            /*NpgsqlCommand addMarkAtDatabase = new NpgsqlCommand("UPDATE student_test " +
                "SET mark = " + mark + ", time=TIMESTAMP(0)'now' " +
                "WHERE test_id = " + test_id + " " +
                "AND student_id = " + user_id + ";", connection);
            addMarkAtDatabase.ExecuteNonQuery();*/
            database.Add_mark_to_database( mark,  test_id,  user_id);
            data.Reset_test_table(database, user_id, available_test_table);
            questions.Clear();
            test_id = -1;
            version_id = -1;
            scores = 0;
            question_index = 0;
            choose_test_panel.Visible = true;
        }
        
        private int calculateMarkForTest()
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
        /*//запрос на получение ответов *
        private void Get_answers_text()
        {
            string str = "SELECT answers.answers_text FROM answers " +
                "JOIN question_answer ON question_answer.answer_id = answers.answer_id " +
                "JOIN version_question ON version_question.question_id = question_answer.question_id " +
                "JOIN questions ON questions.question_id = question_answer.question_id " +
                "WHERE questions.question_text = '"+questions[question_index]+"' " +
                "AND version_question.version_id = " + version_id + " " +
                "GROUP BY answers.answers_text; ";
            //MessageBox.Show(str);
            NpgsqlCommand get_answers = new NpgsqlCommand(str, connection);
            NpgsqlDataReader reader = get_answers.ExecuteReader();
           // int s = 0;
            while (reader.Read())
            {
                answers_text.Add(reader.GetValue(0).ToString());
                // MessageBox.Show(answers_text[s], "answers_text");
                //++s;
            }
            reader.Close();
        }*/
        //запрос на получение правильных ответов *
        /*private void getRightAnswer()
        {
            NpgsqlCommand get_right_answer = new NpgsqlCommand("SELECT answers_text FROM answers " +
                "JOIN question_answer ON question_answer.answer_id = answers.answer_id " +
                "JOIN version_question ON version_question.question_id = question_answer.question_id " +
                "JOIN questions ON questions.question_id = question_answer.question_id " +
                "WHERE questions.question_text = '"+questions[question_index]+"' " +
                "AND version_id = "+version_id+" " +
                "AND right_answer = 1 " +
                "GROUP BY answers_text;", connection);
            NpgsqlDataReader reader_get_right_answer = get_right_answer.ExecuteReader();
           // int s = 0;
            while (reader_get_right_answer.Read())
            {
                answers.Add((string)reader_get_right_answer.GetValue(0));
               // MessageBox.Show(answers[s], "answers");
              //  ++s;
            }
            reader_get_right_answer.Close();
        }*/

        
        

        private void available_test_table_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            test_id = Convert.ToInt32(available_test_table.SelectedCells[0].Value);
            data.Change_back_color(available_test_table, Color.SkyBlue);
            //available_test_table.RowsDefaultCellStyle.SelectionBackColor = Color.SkyBlue;
            data.Change_fore_color(available_test_table, Color.Black);
            //available_test_table.RowsDefaultCellStyle.SelectionForeColor = Color.Black;
        }


        private void one_answer_button_Click(object sender, EventArgs e)
        {
            Save_and_count_scores(checkOneAnswer());
            //MessageBox.Show(scores.ToString());
            one_answer_panel.Visible = false;
            clearListsAndCallNextQuestion();
        }

        private bool checkOneAnswer()
        {
            bool result=false;
            for (int j = 0; j < right_answers.Count; j++)
            {
                if (radioButton1.Checked == true && right_answers[j] == radioButton1.Text)
                {
                    result = true;
                }
                else if (radioButton2.Checked == true && right_answers[j] == radioButton2.Text)
                {
                    result = true;
                }
                else if (radioButton3.Checked == true && right_answers[j] == radioButton3.Text)
                {
                    result = true;
                }
            }
            return result;
        }
        public bool checkManyAnswer()
        {
            List<string> choosen_answers = new List<string>();
            
            if (checkBox1.Checked == true)
                choosen_answers.Add(checkBox1.Text);
            if (checkBox2.Checked == true)
                choosen_answers.Add(checkBox2.Text);
            if (checkBox3.Checked == true)
                choosen_answers.Add(checkBox3.Text);
           
            if (choosen_answers.OrderBy(m => m).SequenceEqual(right_answers.OrderBy(m => m)))
            {
                choosen_answers.Clear(); //возможно его не нужно чистить т к он локальный. нужно проверить при отладке
                return true;
            }
            else
            {
                choosen_answers.Clear(); //возможно его не нужно чистить т к он локальный. нужно проверить при отладке
                return false;
            }
            /*int flag = 0;
            for (int j = 0; j < answers_text.Count; j++)
            {
                if (checkBox1.Checked == true && answers[j] == checkBox1.Text)
                    ++flag;
                if (checkBox2.Checked == true && right_answers[j] == checkBox2.Text)
                    ++flag;
                if (checkBox3.Checked == true && right_answers[j] == checkBox3.Text)
                    ++flag;
            }
            if (flag == right_answers.Count)
            {
                return true;
            }
            return false;*/
        }
        private void many_answer_button_Click(object sender, EventArgs e)
        {
            Save_and_count_scores(checkManyAnswer());
            many_answer_panel.Visible = false;
            clearListsAndCallNextQuestion();
        }
      private void Save_and_count_scores(bool answer) 
        {
            if(answer)
                ++scores;
            database.Save_student_answer(user_id, version_id, questions[question_index], answer);


        }
        private void clearListsAndCallNextQuestion()
        {
            right_answers.Clear();
            answers_text.Clear();
            ++question_index;
            fillTestingPanelAndGetRightAnswer();
        }

        private void StudentForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void show_list_of_completed_tests_CheckedChanged(object sender, EventArgs e)
        {
            //общий список тестов
            passed_tests.Visible = false;
            data.Hide(one_result_dataGridView);
            data.Reset_completed_tests_list_table(database, user_id, results_dataGridView);
        }

        private void show_one_test_result_CheckedChanged(object sender, EventArgs e)
        {
            comboboxclass.Change_visible(passed_tests, true);
            database.Fill_studentForm_collection_passed_tests(user_id, passed_tests);
        }

        private void choose_test_result_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void passed_tests_SelectedIndexChanged(object sender, EventArgs e)
        {
            data.Hide(results_dataGridView);
            data.Reset_one_test_result_table(database, user_id, one_result_dataGridView, passed_tests);
        }

        private void group_student_profile_label_Click(object sender, EventArgs e)
        {

        }

        private void full_name_student_profile_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void group_student_profile_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void login_student_profile_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void password_student_profile_KeyPress(object sender, KeyPressEventArgs e)
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

        private void change_student_login_button_Click(object sender, EventArgs e)
        {
            change_student_login_panel.Visible = true;
            student_profile_main_panel.Visible = false;
        }

        private void new_student_login_button_Click(object sender, EventArgs e)
        {
            if(new_student_login_textbox.Text!="")
            {
                database.Update_user_login_or_password(new_student_login_textbox, user_id, "login","student");
                //запись логина в бд
                /* NpgsqlCommand updateStudentLoginAtDatabase = new NpgsqlCommand("UPDATE student " +
                     "SET login = '" + new_student_login_textbox.Text + "' " +
                     "WHERE student_id = " + user_id + "; ", connection);
                 updateStudentLoginAtDatabase.ExecuteNonQuery();*/
                //обновить панель
                fillStudentProfileMainPanel();
                //логин изменен
                MessageBox.Show("Логин успешно изменен!");
            }
            else
            {
                MessageBox.Show("Логин не введен", "Введите логин");
            }
        }

        private void cancel_change_student_login_button_Click(object sender, EventArgs e)
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

        private void change_student_password_button_Click(object sender, EventArgs e)
        {
            student_identity_check_panel.Visible = true;
            student_profile_main_panel.Visible = false;

            // panel2.Visible = false;
            //new_student_password_button.Visible = false;
        }

        private void cancel_change_student_password_button_Click(object sender, EventArgs e)
        {
            change_student_password_panel.Visible = false;
            student_profile_main_panel.Visible = true;

        }

        private void new_student_password_button_Click(object sender, EventArgs e)
        {
            if (student_test_new_password_textBox.Text == "" || student_new_password_textBox.Text == "")
            {
                MessageBox.Show("Введите пароль.");
            }
            else if (student_test_new_password_textBox.Text!= student_new_password_textBox.Text)
            {
                MessageBox.Show("Пароли не совпадают.");
            }
            else
            {
                database.Update_user_login_or_password(student_test_new_password_textBox, user_id, "password","student");
                
                //обновить панель
                fillStudentProfileMainPanel();
                //логин изменен
                MessageBox.Show("Пароль успешно изменен!");
            }
        }

        private void student_old_password_textBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void cancel_student_change_password_button_Click(object sender, EventArgs e)
        {
            student_old_password_textBox.Clear();
            student_identity_check_panel.Visible = false;
            student_profile_main_panel.Visible = true;

        }

        private void continue_student_change_password_button_Click(object sender, EventArgs e)
        {
            //проверка совпадения старого пароля
            if (student_old_password_textBox.Text == "")
            {
                MessageBox.Show("Введите пароль.");
            }
            else if (student_old_password_textBox.Text != profil_info_list[5])
            {
                MessageBox.Show("Указан неверный пароль.");
            }
            else
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

        private void test_timer_Tick(object sender, EventArgs e)
        {
            test_timer.Stop();
            if (database.Get_question_type(version_id, questions[question_index]) == 1)
            {
                Save_and_count_scores(checkOneAnswer());
                one_answer_panel.Visible = false;
            }
            else
            {
                Save_and_count_scores(checkManyAnswer());
                many_answer_panel.Visible = false;
            }
            finishTesting();
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



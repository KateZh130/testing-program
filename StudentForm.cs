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
        NpgsqlConnection connection = new NpgsqlConnection();
        readonly int user_id;
        int test_id = -1;
        int number_of_questions;
        int scores = 0;
        int timer;

        List<string> right_answers = new List<string>();
        List<string> answers = new List<string>();
        List<string> questions = new List<string>();
        List<string> words = new List<string>();
        int question_index = 0;

        private DataSet set1 = new DataSet();
        private DataTable table1 = new DataTable();
        private DataSet set2 = new DataSet();
        private DataTable table2 = new DataTable();
        private DataSet set3 = new DataSet();
        private DataTable table3 = new DataTable();

        public StudentForm(NpgsqlConnection connection, int user_id)
        {
            InitializeComponent();
            this.connection = connection;
            this.user_id = user_id;
            fillFormName();
            fillTestPage();
            fillStudentProfileMainPanel();
            //главная панель профиля
        }

        private void fillStudentProfileMainPanel()
        {
            string profile_info="";
            student_profile_main_panel.Visible = true;
            change_student_login_panel.Visible = false;
            change_student_password_panel.Visible = false;
            student_identity_check_panel.Visible = false;

            NpgsqlCommand get_profile_info = new NpgsqlCommand("SELECT " +
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
            reader.Close();
            words = profile_info?.Split(' ').ToList();
           // string[] words = profile_info.Split(' ');
            full_name_student_profile.Text = words[0] + " " + words[1] + " " + words[2];
            group_student_profile.Text = words[3];
            login_student_profile.Text = words[4];
            password_student_profile.Text = words[5];

        }
        public void resetTestTable()
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
        }

        public void resetListOfCompletedTestsTable()
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
        }

        public void resetOneTestResultTable()
        {
            one_result_dataGridView.Visible = true;
            results_dataGridView.Visible = false;
            NpgsqlCommand reset_one_test_result_table = new NpgsqlCommand("SELECT question_text, results.answer FROM questions " +
                "JOIN test_question ON test_question.question_id = questions.question_id " +
                "JOIN results ON test_question.test_question_id = results.test_question_id " +
                "JOIN student_test ON student_test.test_id = test_question.test_id " +
                "JOIN student ON student.student_id = results.student_id " +
                "JOIN test ON test.test_id = test_question.test_id " +
                "WHERE student.student_id = " + user_id + " " +
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
        }

        public void fillTestPage()
        {
            choose_test_panel.Visible = true;
            resetTestTable();
            available_test_table.Columns[1].HeaderText = "Название теста";
            available_test_table.Columns[1].Width = 605;
            this.available_test_table.Columns[0].Visible = false;
            available_test_table.RowsDefaultCellStyle.SelectionBackColor = Color.White;
            available_test_table.RowsDefaultCellStyle.SelectionForeColor = Color.Black;
        }

        //Заполнение текста формы именем пользователя
        //********************************************************
        //ХОЧУ ПОМЕНЯТЬ
        private void fillFormName()
        {
            NpgsqlCommand select_student_name = new NpgsqlCommand("SELECT full_name FROM student " +
                "WHERE student_id = " + user_id + ";", connection);
            this.Text = select_student_name.ExecuteScalar().ToString();
        }
        //*********************************************************
        private void StudentForm_Load(object sender, EventArgs e)
        {
        }

        //*********************************************************************************
        
        //запрос на тип вопроса
        private int getTypeOfQuestion()
        {
            NpgsqlCommand get_type_of_questions = new NpgsqlCommand("SELECT question_type FROM questions " +
                "JOIN test_question ON questions.question_id = test_question.question_id " +
                "WHERE test_id = " + test_id + " " +
                "AND question_text = '" + questions[question_index] + "';", connection);
            return Convert.ToInt32(get_type_of_questions.ExecuteScalar());
        }
        //**********************************************************************************
        //запрос на количество вопросов в тесте
        private void getNumberOfQuestions()
        {
            NpgsqlCommand get_number_of_questions = new NpgsqlCommand("SELECT COUNT(question_id) FROM test_question " +
                "WHERE test_id = '" + test_id + "';", connection);
            number_of_questions = Convert.ToInt32(get_number_of_questions.ExecuteScalar());
        }
        //список вопросов для теста
        private void getTestQuestions()
        {
            NpgsqlCommand get_questions = new NpgsqlCommand("SELECT question_text FROM questions " +
                "JOIN test_question ON questions.question_id = test_question.question_id " +
                "WHERE test_id = " + test_id + "; ", connection);
            NpgsqlDataReader reader = get_questions.ExecuteReader();
           // int s = 0;
            while (reader.Read())
            {
                questions.Add(reader.GetValue(0).ToString());
              //  MessageBox.Show(questions[s]);
               // ++s;
            }
            reader.Close();
        }

        private void getTestTimer()
        {
            NpgsqlCommand get_timer = new NpgsqlCommand("SELECT timer FROM test WHERE test_id = "+test_id+"; ", connection);
            timer = Convert.ToInt32(get_timer.ExecuteScalar());
            test_timer.Interval = timer * 60000;
        }

        private void start_test_button_Click(object sender, EventArgs e)
        {

            if (test_id == -1)
            {
                MessageBox.Show("Выберите тест", "Ошибка");
            }
            else
            {
                getTestTimer();
                DialogResult dialogResult = MessageBox.Show("Ограничение по времени: "+timer+" мин.\n"+ available_test_table.SelectedCells[1].Value.ToString() + " можно пройти только 1 раз. Вы уверены, что хотите начать прямо сейчас? ", "Начать попытку", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    choose_test_panel.Visible = false;
                    
                    getNumberOfQuestions();
                    getTestQuestions();
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
                question_type = getTypeOfQuestion();
                if (question_type == 1) //вопрос с одним правильным ответом
                {
                    one_answer_panel.Visible = true;

                    try
                    {
                        fillLabelsAndButtons(question_type);
                        getAnswers();
                        radioButton1.Text = answers[0];
                        radioButton2.Text = answers[1];
                        radioButton3.Text = answers[2];
                    }
                    catch { MessageBox.Show("error1/1"); }
                    try
                    {
                        getRightAnswer();
                    }
                    catch { MessageBox.Show("error1/2"); }
                }
                else if (question_type == 2)  //вопрос с несколькими правильными ответами
                {
                    many_answer_panel.Visible = true;
                    try
                    {
                        fillLabelsAndButtons(question_type);
                        getAnswers();
                        MessageBox.Show("Тут");
                        checkBox1.Text = answers[0];
                        checkBox2.Text = answers[1];
                        checkBox3.Text = answers[2];
                    }
                    catch (Exception e) { MessageBox.Show("error1" + e); }
                    try
                    {
                        getRightAnswer();
                    }
                    catch { MessageBox.Show("error2"); }
                }
            }
            else
            {
                //добавить сохранение и остановку времени
                test_timer.Stop();
                finishTesting();
               
            }
        }
        private void finishTesting()
        {
            int mark;
            mark = calculateMarkForTest();
            MessageBox.Show("Тест пройден!\nВаша оценка: " + mark + "\nЧтобы получить больше информации зайдите в раздел Результаты", "Результат");
            //добавление результата в Результаты
            NpgsqlCommand addMarkAtDatabase = new NpgsqlCommand("UPDATE student_test " +
                "SET mark = " + mark + ", time=TIMESTAMP(0)'now' " +
                "WHERE test_id = " + test_id + " " +
                "AND student_id = " + user_id + ";", connection);
            addMarkAtDatabase.ExecuteNonQuery();
            resetTestTable();
            test_id = -1;
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
        private void getAnswers()
        {
            string str = "SELECT answers.answers_text FROM answers " +
                "LEFT JOIN question_answer ON question_answer.answer_id = answers.answer_id " +
                "LEFT JOIN test_question ON test_question.question_id = question_answer.question_id " +
                "LEFT JOIN questions ON questions.question_id = question_answer.question_id " +
                "WHERE questions.question_text = '"+questions[question_index]+"' " +
                "AND test_question.test_id = "+test_id+" " +
                "GROUP BY answers.answers_text; ";
            MessageBox.Show(str);

            NpgsqlCommand get_answers = new NpgsqlCommand(str, connection);
            NpgsqlDataReader reader = get_answers.ExecuteReader();
           // int s = 0;
            while (reader.Read())
            {
                answers.Add(reader.GetValue(0).ToString());
                // MessageBox.Show(answers[s], "answers");
                //++s;
            }
            reader.Close();
        }
        private void getRightAnswer()
        {
            NpgsqlCommand get_right_answer = new NpgsqlCommand("SELECT answers_text FROM answers " +
                "JOIN question_answer ON question_answer.answer_id = answers.answer_id " +
                "JOIN test_question ON test_question.question_id = question_answer.question_id " +
                "JOIN questions ON questions.question_id = question_answer.question_id " +
                "WHERE questions.question_text = '"+questions[question_index]+"' " +
                "AND test_id = "+test_id+" " +
                "AND right_answer = 1 " +
                "GROUP BY answers_text;", connection);
            NpgsqlDataReader reader_get_right_answer = get_right_answer.ExecuteReader();
           // int s = 0;
            while (reader_get_right_answer.Read())
            {
                right_answers.Add((string)reader_get_right_answer.GetValue(0));
               // MessageBox.Show(right_answers[s], "right_answers");
              //  ++s;
            }
            reader_get_right_answer.Close();
        }

        private void fillLabelsAndButtons(int type)
        {
            string button_text = "Следующий вопрос";
            if (question_index+1 == number_of_questions)
                button_text = "Закончить попытку";

            if(type==1)
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
        

        private void available_test_table_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            test_id = Convert.ToInt32(available_test_table.SelectedCells[0].Value);
            available_test_table.RowsDefaultCellStyle.SelectionBackColor = Color.SkyBlue;
            available_test_table.RowsDefaultCellStyle.SelectionForeColor = Color.Black;
        }


        private void one_answer_button_Click(object sender, EventArgs e)
        {
            addStudentAnswerToDatabase(checkOneAnswer());
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
        private bool checkManyAnswer()
        {
            int flag = 0;
            for (int j = 0; j < right_answers.Count; j++)
            {
                if (checkBox1.Checked == true && right_answers[j] == checkBox1.Text)
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
            return false;
        }
        private void many_answer_button_Click(object sender, EventArgs e)
        {
            addStudentAnswerToDatabase(checkManyAnswer());
            many_answer_panel.Visible = false;
            clearListsAndCallNextQuestion();
        }
        private void addStudentAnswerToDatabase(bool answer)
        {
            if(answer)
                ++scores;
            NpgsqlCommand add_student_answer_to_database = new NpgsqlCommand("INSERT " +
                "INTO results(student_id, test_question_id, answer) VALUES('1', " +
                "(SELECT test_question_id FROM test_question " +
                "JOIN questions ON questions.question_id = test_question.question_id " +
                "WHERE test_id = "+test_id+" " +
                "AND question_text = '"+questions[question_index]+"')," +
                " '"+answer+"'); ", connection);
            add_student_answer_to_database.ExecuteNonQuery();
        }
        private void clearListsAndCallNextQuestion()
        {
            right_answers.Clear();
            answers.Clear();
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
            choose_test_result.Visible = false;
            resetListOfCompletedTestsTable();
        }

        private void show_one_test_result_CheckedChanged(object sender, EventArgs e)
        {
            //показываем чекбокс
            choose_test_result.Visible = true;
            //список тестов в чекбокс
            NpgsqlCommand choose_test_result_combobox = new NpgsqlCommand("SELECT test_name FROM test " +
                "JOIN student_test ON student_test.test_id = test.test_id " +
                "WHERE student_id = " + user_id + " " +
                "AND mark is not null; ", connection);
            NpgsqlDataReader reader_choose_test_result_combobox = choose_test_result_combobox.ExecuteReader();

            if (reader_choose_test_result_combobox.HasRows)
            {
                choose_test_result.Items.Clear();

                while (reader_choose_test_result_combobox.Read())
                {
                    choose_test_result.Items.Add(reader_choose_test_result_combobox.GetString(0));
                }
            }

            reader_choose_test_result_combobox.Close();
            
            //показываем выбранные тест
        }

        private void choose_test_result_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void choose_test_result_SelectedIndexChanged(object sender, EventArgs e)
        {
                resetOneTestResultTable();
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
                //запись логина в бд
                NpgsqlCommand updateStudentLoginAtDatabase = new NpgsqlCommand("UPDATE student " +
                    "SET login = '" + new_student_login_textbox.Text + "' " +
                    "WHERE student_id = " + user_id + "; ", connection);
                updateStudentLoginAtDatabase.ExecuteNonQuery();
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
                //запись логина в бд
                NpgsqlCommand updateStudentPasswordAtDatabase = new NpgsqlCommand("UPDATE student " +
                    "SET password = '" + student_test_new_password_textBox.Text + "' " +
                    "WHERE student_id = " + user_id + "; ", connection);
                updateStudentPasswordAtDatabase.ExecuteNonQuery();
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
            else if (student_old_password_textBox.Text != words[5])
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
            NpgsqlCommand deleteResultsAtDatabase = new NpgsqlCommand("delete from results;", connection);
            deleteResultsAtDatabase.ExecuteNonQuery();
            NpgsqlCommand updateMarksAtDatabase = new NpgsqlCommand("update student_test set mark = null, time=null;", connection);
            updateMarksAtDatabase.ExecuteNonQuery();
            resetListOfCompletedTestsTable();
            resetOneTestResultTable();
            resetTestTable();
        }
        //*****************************************************************************************

        private void test_timer_Tick(object sender, EventArgs e)
        {
            test_timer.Stop();
            if (getTypeOfQuestion() == 1)
            {
                addStudentAnswerToDatabase(checkOneAnswer());
                one_answer_panel.Visible = false;
            }
            else
            {
                addStudentAnswerToDatabase(checkManyAnswer());
                many_answer_panel.Visible = false;
            }
            finishTesting();
        }
       
    }
}



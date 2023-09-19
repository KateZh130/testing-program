using Npgsql;
using System;
using System.Collections.Generic;
using System.Windows.Forms;



namespace testing_program
{
    public class DatabaseClass
    {
        private NpgsqlConnection connection;
        private const string CONNECTION_STRING = "Host=localhost;" +
     "Username=postgres;" +
     "Password=rjirf567;" +
     "Database=test_prog";
        readonly ComboboxClass c = new ComboboxClass();


        public void Connect_to_database()
        {
            try
            {
                connection = new NpgsqlConnection(CONNECTION_STRING);
                connection.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return;
            }
        }
        public void Fill_registrationForm_collection_select_group(ComboBox combobox)
        {
            NpgsqlCommand group_names = new NpgsqlCommand("SELECT group_name FROM groups;", connection);
            NpgsqlDataReader reader_group_names = group_names.ExecuteReader();

            if (reader_group_names.HasRows)
            {
                c.Delete_collection(combobox);
                while (reader_group_names.Read())
                {
                    c.Add_item(combobox, reader_group_names.GetString(0));
                }
            }

            reader_group_names.Close();
        }
        public void Fill_registrationForm_collection_select_student_name(ComboBox group_combobox, ComboBox name_combobox)
        {
            NpgsqlCommand student_names = new NpgsqlCommand("SELECT full_name " +
                "FROM student JOIN groups ON groups.group_id = student.group_id " +
                "WHERE group_name LIKE '" + group_combobox.SelectedItem.ToString() + "'" +
                "AND login is null AND password is null;", connection);
            NpgsqlDataReader reader_student_names = student_names.ExecuteReader();

            if (reader_student_names.HasRows)
            {
                c.Delete_collection(name_combobox);
                while (reader_student_names.Read())
                {
                    c.Add_item(name_combobox, reader_student_names.GetString(0));
                }
            }
            reader_student_names.Close();
        }

        public int Save_registrationForm_new_teacher(TextBox name, TextBox login, TextBox password)
        {
            NpgsqlCommand new_teacher = new NpgsqlCommand("INSERT INTO " +
                "teacher (full_name, login, password) " +
                "VALUES ('" + name.Text + "', '"
                + login.Text + "', '"
                + password.Text + "') RETURNING teacher_id;", connection);
            return Convert.ToInt32(new_teacher.ExecuteScalar());
        }

        public int Save_registrationForm_new_student(TextBox login, TextBox password, ComboBox name, ComboBox group)
        {
            NpgsqlCommand new_student = new NpgsqlCommand("UPDATE student " +
                "SET login = '" + login.Text + "', " +
                "password = '" + password.Text + "' " +
                "WHERE student_id = (select student_id from student " +
                "where full_name = '" + name.SelectedItem.ToString() + "') " +
                "AND group_id = (select group_id from groups " +
                "where group_name = '" + group.SelectedItem.ToString() + "')" +
                " RETURNING student_id;", connection);
            return Convert.ToInt32(new_student.ExecuteScalar());
        }

        public bool Check_login_exists(string login)
        {
            NpgsqlCommand exist = new NpgsqlCommand("SELECT EXISTS " +
                "(SELECT * FROM student, teacher " +
                "WHERE student.login = '"+login+"' " +
                "OR teacher.login = '"+ login + "'); ", connection);
            return (bool)exist.ExecuteScalar();
        }

        public int Search_registrationForm_user_id(TextBox login, TextBox password, int user_code)
        {
            string table = "teacher";
            if (user_code == 1)
            {
                table = "student";
            }
            NpgsqlCommand get_user_id = new NpgsqlCommand("SELECT " + table + "_id FROM " + table + " " +
                "WHERE login = '" + login.Text + "' " +
                "AND password = '" + password.Text + "';", connection);
            int user_id = Convert.ToInt32(get_user_id.ExecuteScalar());

            if (user_id == 0)
            {
                return -1;
            }
            return user_id;
        }

        public string Get_studentForm_profil_info(int user_id)
        {
            NpgsqlCommand student_profile_info = new NpgsqlCommand("SELECT " +
               "student.full_name, groups.group_name, student.login, student.password FROM student " +
               "JOIN groups ON groups.group_id = student.group_id " +
               "WHERE student.student_id = " + user_id + "; ", connection);

            NpgsqlDataReader reader = student_profile_info.ExecuteReader();
            string profile_info = "";
            while (reader.Read())
            {
                for (int s = 0; s < 4; ++s)
                {
                    profile_info += reader.GetValue(s).ToString() + " ";
                }
            }
            reader.Close();
            return profile_info;
        }

        public string Get_teacherForm_profile_info(int user_id)
        {
            NpgsqlCommand teacher_profile_info = new NpgsqlCommand("SELECT " +
               "full_name, login, password FROM teacher " +
               "WHERE teacher_id = " + user_id + "; ", connection);

            NpgsqlDataReader reader = teacher_profile_info.ExecuteReader();
            string profile_info = "";
            while (reader.Read())
            {
                for (int s = 0; s < 3; ++s)
                {
                    profile_info += reader.GetValue(s).ToString() + " ";
                }
            }
            reader.Close();
            return profile_info;
        }

        public string Get_name(int user_id, string role)
        {
            NpgsqlCommand user_name = new NpgsqlCommand("SELECT full_name FROM " + role + " " +
                "WHERE " + role + "_id = " + user_id + ";", connection);
            return user_name.ExecuteScalar().ToString();
        }

        public List<int> Get_test_versions(int test_id)
        {
            NpgsqlCommand test_versions = new NpgsqlCommand("SELECT version_id " +
                "FROM version WHERE test_id = " + test_id + "; ", connection);
            NpgsqlDataReader test_versions_reader = test_versions.ExecuteReader();
            List<int> versions = new List<int>();
            while (test_versions_reader.Read())
            {
                versions.Add(Convert.ToInt32(test_versions_reader.GetValue(0)));
            }
            test_versions_reader.Close();
            return versions;
        }

        public int Get_question_type(int version_id, string question)
        {
            NpgsqlCommand type = new NpgsqlCommand("SELECT question_type FROM questions " +
                "JOIN version_question ON questions.question_id = version_question.question_id " +
                "WHERE version_id = " + version_id + " " +
                "AND question_text = '" + question + "';", connection);
            return Convert.ToInt32(type.ExecuteScalar());
        }

        public int Get_number_of_questions(int version_id)
        {
            NpgsqlCommand number = new NpgsqlCommand("SELECT COUNT(question_id) FROM version_question " +
                "WHERE version_id = '" + version_id + "';", connection);
            return Convert.ToInt32(number.ExecuteScalar());
        }

        public List<string> Get_test_questions(int version_id, List<string> questions)
        {
            NpgsqlCommand get_questions = new NpgsqlCommand("SELECT question_text FROM questions " +
                "JOIN version_question ON questions.question_id = version_question.question_id " +
                "WHERE version_id = " + version_id + "; ", connection);
            NpgsqlDataReader reader = get_questions.ExecuteReader();
            while (reader.Read())
            {
                questions.Add(reader.GetValue(0).ToString());
            }
            reader.Close();
            return questions;
        }
        public void Add_mark_to_database(int mark, int test_id, int user_id)
        {
            NpgsqlCommand add_mark = new NpgsqlCommand("UPDATE student_test " +
               "SET mark = " + mark + ", time=TIMESTAMP(0)'now' " +
               "WHERE test_id = " + test_id + " " +
               "AND student_id = " + user_id + ";", connection);
            add_mark.ExecuteNonQuery();
        }

        public int Add_new_test_to_database(TextBox name, TextBox timer)
        {
            NpgsqlCommand new_test = new NpgsqlCommand("INSERT INTO " +
               "test (test_name,timer) " +
               "VALUES ('" + name.Text + "'," + timer.Text + ") " +
               "RETURNING test_id", connection);
            return Convert.ToInt32(new_test.ExecuteScalar());
        }

        public int Create_new_test_version(int test_id, int number)
        {
            NpgsqlCommand add_version_test_to_database = new NpgsqlCommand("INSERT INTO " +
               "version (version_number,test_id) " +
               "VALUES (" + number + "," + test_id + ") " +
               "RETURNING version_id", connection);
            return Convert.ToInt32(add_version_test_to_database.ExecuteScalar());

        }

        public int Create_question_text(ComboBox type, string text)
        {

            NpgsqlCommand add_question_to_database = new NpgsqlCommand("INSERT INTO " +
           "questions (question_text, question_type) " +
           "VALUES ('" + text + "'," + (type.SelectedIndex + 1) + ") " +
           "RETURNING question_id", connection);
            return Convert.ToInt32(add_question_to_database.ExecuteScalar());

        }

        public void Create_question_answer_connection(int question_id, int answer_id, bool is_correct)
        {
            NpgsqlCommand question_answer_connection = new NpgsqlCommand("INSERT INTO " +
              "question_answer (question_id, version_id, right_answer) " +
              "VALUES (" + question_id + "," + answer_id + "," + is_correct + ")", connection);
            question_answer_connection.ExecuteNonQuery();
        }

        public void Create_version_question_connection(int question_id, int version_id)
        {
            NpgsqlCommand version_question_connection = new NpgsqlCommand("INSERT INTO " +
             "version_question (question_id, version_id) " +
             "VALUES (" + question_id + "," + version_id + ")", connection);
            version_question_connection.ExecuteNonQuery();
        }

        public int Create_answer_text(string text)
        {
            NpgsqlCommand add_answer_to_database = new NpgsqlCommand("INSERT INTO " +
              "answers (answers_text) " +
              "VALUES ('" + text + "') " +
              "RETURNING answer_id", connection);
            return Convert.ToInt32(add_answer_to_database.ExecuteScalar());
        }

        public List<string> Get_answers_text(int version_id, string question, List<string> answers)
        {
            string sql_string = "SELECT answers.answers_text FROM answers " +
                "JOIN question_answer ON question_answer.answer_id = answers.answer_id " +
                "JOIN version_question ON version_question.question_id = question_answer.question_id " +
                "JOIN questions ON questions.question_id = question_answer.question_id " +
                "WHERE questions.question_text = '" + question + "' " +
                "AND version_question.version_id = " + version_id + " " +
                "GROUP BY answers.answers_text; ";
            NpgsqlCommand get_answers = new NpgsqlCommand(sql_string, connection);
            NpgsqlDataReader reader = get_answers.ExecuteReader();
            while (reader.Read())
            {
                answers.Add(reader.GetValue(0).ToString());
            }
            reader.Close();
            return answers;
        }

        public List<string> Get_answers(int version_id, string question, List<string> answers)
        {
            NpgsqlCommand get_right_answer = new NpgsqlCommand("SELECT answers_text FROM answers " +
                "JOIN question_answer ON question_answer.answer_id = answers.answer_id " +
                "JOIN version_question ON version_question.question_id = question_answer.question_id " +
                "JOIN questions ON questions.question_id = question_answer.question_id " +
                "WHERE questions.question_text = '" + question + "' " +
                "AND version_id = " + version_id + " " +
                "AND right_answer = 1 " +
                "GROUP BY answers_text;", connection);
            NpgsqlDataReader reader_get_right_answer = get_right_answer.ExecuteReader();
            while (reader_get_right_answer.Read())
            {
                answers.Add((string)reader_get_right_answer.GetValue(0));
            }
            reader_get_right_answer.Close();
            return answers;
        }

        public void Fill_studentForm_collection_passed_tests(int user_id, ComboBox combobox)
        {
            NpgsqlCommand choose_test_result_combobox = new NpgsqlCommand("SELECT test_name FROM test " +
                "JOIN student_test ON student_test.test_id = test.test_id " +
                "WHERE student_id = " + user_id + " " +
                "AND mark is not null; ", connection);
            NpgsqlDataReader reader_choose_test_result_combobox = choose_test_result_combobox.ExecuteReader();

            if (reader_choose_test_result_combobox.HasRows)
            {
                combobox.Items.Clear();

                while (reader_choose_test_result_combobox.Read())
                {
                    combobox.Items.Add(reader_choose_test_result_combobox.GetString(0));
                }
            }

            reader_choose_test_result_combobox.Close();
        }

        public void Update_user_login_or_password(TextBox input, int user_id, string item, string table)
        {
            NpgsqlCommand new_user_login_or_password = new NpgsqlCommand("UPDATE " + table + " " +
                    "SET " + item + " = '" + input.Text + "' " +
                    "WHERE " + table + "_id = " + user_id + "; ", connection);
            new_user_login_or_password.ExecuteNonQuery();
        }

        public void Save_student_answer(int user_id, int version_id, string question, bool answer)
        {
            NpgsqlCommand student_answer = new NpgsqlCommand("INSERT " +
                "INTO results(student_id, version_question_id, answer) VALUES('" + user_id + "', " +
                "(SELECT version_question_id FROM version_question " +
                "JOIN questions ON questions.question_id = version_question.question_id " +
                "JOIN version ON version.version_id = version_question.version_id " +
                "WHERE version.version_id = " + version_id + " " +
                "AND question_text = '" + question + "')," +
                " '" + answer + "'); ", connection);
            student_answer.ExecuteNonQuery();
        }

        public int Get_test_timer(int test_id)
        {
            NpgsqlCommand timer = new NpgsqlCommand("SELECT timer " +
                "FROM test WHERE test_id = " + test_id + "; ", connection);

            return Convert.ToInt32(timer.ExecuteScalar());
        }

        public NpgsqlDataAdapter Get_available_tests(int user_id)
        {
            NpgsqlCommand available_tests = new NpgsqlCommand("SELECT test.test_id, test_name " +
                "FROM test " +
                "JOIN student_test ON student_test.test_id = test.test_id " +
                "WHERE mark IS NULL " +
                "AND student_id = " + user_id + " " +
                "GROUP BY test.test_id;", connection);
            available_tests.ExecuteNonQuery();
            return new NpgsqlDataAdapter(available_tests);
        }

        public NpgsqlDataAdapter Get_completed_tests(int user_id)
        {
            NpgsqlCommand completed_tests = new NpgsqlCommand("SELECT " +
                "test_name, mark, time FROM test, student_test " +
                "WHERE student_test.test_id = test.test_id " +
                "AND student_id=" + user_id + " " +
                "AND mark is not null;", connection);
            completed_tests.ExecuteNonQuery();
            return new NpgsqlDataAdapter(completed_tests);
        }

        public NpgsqlDataAdapter Get_test_analysis(int user_id, ComboBox test_name)
        {
            NpgsqlCommand test_analysis = new NpgsqlCommand("SELECT question_text, results.answer FROM questions " +
                "JOIN version_question ON version_question.question_id = questions.question_id " +
                "JOIN results ON version_question.version_question_id = results.version_question_id " +
                "JOIN version ON version.version_id = version_question.version_id " +
                "JOIN test ON test.test_id = version.test_id " +
                "JOIN student_test ON student_test.test_id = test.test_id " +
                "WHERE student_test.student_id = " + user_id + " " +
                "AND test.test_name = '" + test_name.Text + "' " +
                "GROUP BY question_text, results.answer;", connection);

            test_analysis.ExecuteNonQuery();
            return new NpgsqlDataAdapter(test_analysis);
        }

        //******************************************************************************
        //вспомогательная кнопка
        //УДАЛИТЬ ПОСЛЕ ТЕСТИРОВАНИЯ
        public void Delete_results_and_mark()
        {
            NpgsqlCommand deleteResultsAtDatabase = new NpgsqlCommand("delete from results;", connection);
            deleteResultsAtDatabase.ExecuteNonQuery();
            NpgsqlCommand updateMarksAtDatabase = new NpgsqlCommand("update student_test set mark = null, time=null;", connection);
            updateMarksAtDatabase.ExecuteNonQuery();
            //*****************************************************************************************
        }
    }
}


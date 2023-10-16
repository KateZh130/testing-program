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

        public bool Check_test_exist(string name, int user_id)
        {
            NpgsqlCommand exist = new NpgsqlCommand("select exists " +
                "(select test_id from test where test_name='" + name + "' and teacher_id=" + user_id + ");", connection);
            return (bool)exist.ExecuteScalar();
        }
        public bool Check_student_exist(string name, string group)
        {
            NpgsqlCommand exist = new NpgsqlCommand("select exists (select student_id from student " +
                "join groups on groups.group_id = student.group_id " +
                "where full_name = '"+name+"' and group_name = '"+group+"'); ", connection);
            return (bool)exist.ExecuteScalar();
        }
        
        public bool Check_group_exist(string group)
        {
            NpgsqlCommand exist = new NpgsqlCommand("select exists (select group_id from groups " +
                "where group_name = '"+group+"'); ", connection);
            return (bool)exist.ExecuteScalar();
        }
        public bool Check_question_exist(string question, int type, int user_id, int question_id)
        {
            string extra = "";
            if (question_id != -1)
            {
                extra = " and q.question_id!="+ question_id;
            }
            NpgsqlCommand exist = new NpgsqlCommand("select exists (select q.question_id from questions q " +
                "join version_question vq on vq.question_id = q.question_id " +
                "join version v on v.version_id = vq.version_id " +
                "join test t on t.test_id = v.test_id " +
                "where teacher_id = " + user_id + " and question_text = '" + question + "' " +
                "and question_type = " + type + extra + " ); ", connection);
            return (bool)exist.ExecuteScalar();
        }
        public bool Check_version_have_another_tasks(string question, int version, int user_id )
        {
            NpgsqlCommand exist = new NpgsqlCommand("select exists (select q.question_id from questions q " +
                "join version_question vq on vq.question_id = q.question_id " +
                "join version v on v.version_id = vq.version_id " +
                "join test t on t.test_id = v.test_id " +
                "where teacher_id = "+user_id+" and question_text != '"+question+"' " +
                "and version_number = "+version+");", connection);
            return (bool)exist.ExecuteScalar();
        }
        public bool Check_test_have_another_version(string test, int version, int user_id)
        {
            NpgsqlCommand exist = new NpgsqlCommand("select exists (select v.version_id from version v " +
                "join version_question vq on v.version_id = vq.version_id " +
                "join questions q on vq.question_id = q.question_id " +
                "join test t on t.test_id = v.test_id " +
                "where teacher_id = "+user_id+" and test_name = '"+test+"' " +
                "and version_number != "+version+"); ; ", connection);
            return (bool)exist.ExecuteScalar();
        }

        public void Fill_groups_collection(ComboBox combobox, bool groups_and_students_page, bool marks_page)
        {
            string extra = "";
            if (marks_page)
            {
                extra = "g join student s on g.group_id=s.group_id join student_test st on s.student_id = st.student_id " +
                    "group by  group_name";
            }
            NpgsqlCommand group_names = new NpgsqlCommand("SELECT group_name FROM groups "+extra+";", connection);
            NpgsqlDataReader reader_group_names = group_names.ExecuteReader();
            extra = "";
            c.Delete_collection(combobox);
            if (groups_and_students_page)
            {
                c.Add_item(combobox, "Список всех студентов");
                c.Add_item(combobox, "Список всех групп");
                c.Add_item(combobox, "Список групп с доступом к тестам");
                c.Add_item(combobox, "Список студентов с персональным доступом к тестам");
                extra = "Список группы ";
            }
            if (reader_group_names.HasRows)
            {
                while (reader_group_names.Read())
                {
                    c.Add_item(combobox, extra + reader_group_names.GetString(0));
                }
            }

            reader_group_names.Close();
        }

        public void Fill_student_name_collection(string group, ComboBox name_combobox, bool marks_page)
        {
            string extra1 = "";
            string extra2 = " AND login is null AND password is null ";
            if (marks_page)
            {
                extra1 = " JOIN student_test ON student_test.student_id=student.student_id ";
                extra2 = "";
            }
            NpgsqlCommand student_names = new NpgsqlCommand("SELECT full_name " +
                "FROM student JOIN groups ON groups.group_id = student.group_id "+extra1+" " +
                "WHERE group_name = '" + group + "'" +extra2+
                " group by full_name; ", connection);
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
                "WHERE student.login = '" + login + "' " +
                "OR teacher.login = '" + login + "'); ", connection);
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
        public List<string> Get_teacher_questions(int user_id)
        {

            List<string> questions_text = new List<string>();
            NpgsqlCommand teacher_questions = new NpgsqlCommand("select question_text " +
                "from questions " +
                "join version_question on version_question.question_id = questions.question_id " +
                "join version on version.version_id = version_question.version_id " +
                "join test on test.test_id = version.test_id " +
                "where teacher_id = " + user_id + " group by question_text; ", connection);
            NpgsqlDataReader teacher_questions_reader = teacher_questions.ExecuteReader();
            while (teacher_questions_reader.Read())
            {

                questions_text.Add(teacher_questions_reader.GetValue(0).ToString());
            }
            teacher_questions_reader.Close();
            return questions_text;
        }
        public int Get_teacher_question_version_id(int user_id, string question)
        {
            NpgsqlCommand type = new NpgsqlCommand("select version.version_id from version " +
                "join version_question on version_question.version_id = version.version_id " +
                "join questions on version_question.question_id = questions.question_id " +
                "join test on test.test_id = version.test_id " +
                "where question_text = '" + question + "' and teacher_id = " + user_id + " " +
                "limit 1; ", connection);
            return Convert.ToInt32(type.ExecuteScalar());
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
        public List<int> Get_students_id(int group, List<int> id )
        {
            NpgsqlCommand get_students_list = new NpgsqlCommand("SELECT student_id FROM student " +
                "WHERE group_id = " + group + "; ", connection);
            NpgsqlDataReader reader = get_students_list.ExecuteReader();
            while (reader.Read())
            {
                id.Add(Convert.ToInt32(reader.GetValue(0)));
            }
            reader.Close();
            return id;
        }
        public int Get_question_id(int version_id, string question)
        {
            NpgsqlCommand new_test = new NpgsqlCommand("select questions.question_id from questions " +
                "join version_question on version_question.question_id = questions.question_id " +
                "where question_text = '" + question + "' and version_id = " + version_id + ";", connection);
            return Convert.ToInt32(new_test.ExecuteScalar());
        }

        public int Get_group_by_name(string name)
        {
            NpgsqlCommand group_id = new NpgsqlCommand("SELECT group_id FROM groups " +
                "WHERE group_name='"+name+"';", connection);
            return Convert.ToInt32(group_id.ExecuteScalar());
        }

        public void Add_mark_to_database(int mark, int test_id, int user_id, double time )
        {
            NpgsqlCommand add_mark = new NpgsqlCommand("UPDATE student_test " +
               "SET mark = " + mark + ", time="+time+", access_status= false " +
               "WHERE test_id = " + test_id + " " +
               "AND student_id = " + user_id + ";", connection);
            add_mark.ExecuteNonQuery();
        }

        public int Add_new_test_to_database(TextBox name, TextBox timer, TextBox comment, int user_id)
        {
            NpgsqlCommand new_test = new NpgsqlCommand("INSERT INTO " +
               "test (test_name,timer, teacher_id, comment) " +
               "VALUES ('" + name.Text + "'," + timer.Text + "," + user_id + ",'" + comment.Text + "') " +
               "RETURNING test_id", connection);
            return Convert.ToInt32(new_test.ExecuteScalar());
        }
        public void Create_student(string name, string group)
        {
            NpgsqlCommand create_student = new NpgsqlCommand("INSERT INTO " +
               "student (full_name, group_id) " +
               "VALUES ('" + name + "',(SELECT group_id FROM groups " +
               "WHERE group_name='" + group + "')) ;", connection);
            create_student.ExecuteNonQuery();
        }
        public void Create_group(string name)
        {
            NpgsqlCommand create_group = new NpgsqlCommand("INSERT INTO " +
               "groups (group_name) " +
               "VALUES ('" + name + "')) ;", connection);
            create_group.ExecuteNonQuery();

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
            int value = 0;
            if (is_correct)
                value = 1;
            NpgsqlCommand question_answer_connection = new NpgsqlCommand("INSERT INTO " +
              "question_answer (question_id, answer_id, right_answer) " +
              "VALUES (" + question_id + "," + answer_id + "," + value + ")", connection);
            question_answer_connection.ExecuteNonQuery();
        }

        public void Create_version_question_connection(int question_id, int version_id)
        {
            NpgsqlCommand version_question_connection = new NpgsqlCommand("INSERT INTO " +
             "version_question (question_id, version_id) " +
             "VALUES (" + question_id + "," + version_id + ");", connection);
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

        public List<string> Get_answers_text(int version_id, string question)
        {
            List<string> answers = new List<string>();
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

        public List<string> Get_right_answers(int version_id, string question)
        {
            List<string> answers = new List<string>();
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

        public void Fill_version_number_comboBox(ComboBox combobox, int user_id, string test)
        {
            NpgsqlCommand delete_mode_version_comboBox = new NpgsqlCommand("select version_number from version " +
                "join test on test.test_id = version.test_id " +
                "where teacher_id = " + user_id + " and test_name = '" + test + "'; ", connection);
            NpgsqlDataReader reader_delete_mode_version_comboBox = delete_mode_version_comboBox.ExecuteReader();

            if (reader_delete_mode_version_comboBox.HasRows)
            {
                combobox.Items.Clear();

                while (reader_delete_mode_version_comboBox.Read())
                {
                    combobox.Items.Add(reader_delete_mode_version_comboBox.GetInt32(0).ToString());
                }
            }
            reader_delete_mode_version_comboBox.Close();
        }

        public void Fill_delete_task_comboBox(ComboBox combobox, int user_id, string test, int version)
        {
            NpgsqlCommand delete_task_comboBox = new NpgsqlCommand("select question_text from questions " +
                "join version_question on version_question.question_id = questions.question_id " +
                "join version on version.version_id = version_question.version_id " +
                "join test on test.test_id = version.test_id " +
                "where teacher_id = " + user_id + " " +
                "and test_name = '" + test + "' " +
                "and version_number = " + version + " ", connection);
            NpgsqlDataReader reader_delete_task_comboBox = delete_task_comboBox.ExecuteReader();

            if (reader_delete_task_comboBox.HasRows)
            {
                combobox.Items.Clear();

                while (reader_delete_task_comboBox.Read())
                {
                    combobox.Items.Add(reader_delete_task_comboBox.GetString(0));
                }
            }
            reader_delete_task_comboBox.Close();
        }

        public void Fill_teacher_collection_available_tests(int user_id, ComboBox combobox)
        {
            NpgsqlCommand teacher_available_tests = new NpgsqlCommand("SELECT " +
                     "test_name FROM test " +
                     "WHERE teacher_id = " + user_id + " " +
                     "GROUP BY test.test_id; ", connection);
            NpgsqlDataReader reader_teacher_available_tests = teacher_available_tests.ExecuteReader();

            if (reader_teacher_available_tests.HasRows)
            {
                combobox.Items.Clear();

                while (reader_teacher_available_tests.Read())
                {
                    combobox.Items.Add(reader_teacher_available_tests.GetString(0));
                }
            }
            reader_teacher_available_tests.Close();
        }
        public void Fill_teacher_collection_available_tasks(int user_id, ComboBox combobox)
        {
            NpgsqlCommand teacher_available_tasks = new NpgsqlCommand("select question_text from questions " +
                "join version_question on version_question.question_id = questions.question_id " +
                "join version on version_question.version_id = version.version_id " +
                "join test on version.test_id = test.test_id " +
                "WHERE test.teacher_id = " + user_id + " " +
                "group by question_text ", connection);
            NpgsqlDataReader reader_teacher_available_tasks = teacher_available_tasks.ExecuteReader();

            if (reader_teacher_available_tasks.HasRows)
            {
                combobox.Items.Clear();

                while (reader_teacher_available_tasks.Read())
                {
                    combobox.Items.Add(reader_teacher_available_tasks.GetString(0));
                }
            }
            reader_teacher_available_tasks.Close();
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
        public int Get_last_version_number(int user_id, string test)
        {
            NpgsqlCommand number = new NpgsqlCommand("select version_number from version " +
                "join test on test.test_id = version.test_id " +
                "where test_name = '" + test + "' " +
                "  and teacher_id=" + user_id + "" +
                "order by version_number DESC limit 1; ", connection);
            return Convert.ToInt32(number.ExecuteScalar());
        }
        public int Get_last_question_number(int user_id, int test, int version) //можно и с названием теста
        {
            NpgsqlCommand number = new NpgsqlCommand("select count(questions.question_id) from questions " +
                "join version_question on version_question.question_id = questions.question_id " +
                "join version on version.version_id = version_question.version_id " +
                "join test on test.test_id = version.test_id " +
                "where teacher_id = " + user_id + " and test.test_id = " + test + " " +
                "and version_number = " + version + "; ", connection);
            return Convert.ToInt32(number.ExecuteScalar());
        }

        public int Get_test_id(int user_id, string test)
        {
            NpgsqlCommand id = new NpgsqlCommand("SELECT test_id " +
                "FROM test WHERE test_name = '" + test + "'" +
                "and teacher_id=" + user_id + "; ", connection);
            return Convert.ToInt32(id.ExecuteScalar());
        }
        public int Get_version_id_by_number(int user_id, int test, int number) // можно и с названием теста
        {
            NpgsqlCommand id = new NpgsqlCommand("select version_id from version " +
                "join test on test.test_id = version.test_id " +
                "where test.test_id = " + test + " and version_number = " + number + " " +
                "and teacher_id = " + user_id + "; ", connection);
            return Convert.ToInt32(id.ExecuteScalar());
        }

        public NpgsqlDataAdapter Get_student_available_tests(int user_id)
        {
            NpgsqlCommand student_available_tests = new NpgsqlCommand("SELECT test.test_id, test_name, comment " +
                "FROM test " +
                "JOIN student_test ON student_test.test_id = test.test_id " +
                "WHERE mark IS NULL AND access_status=true " +
                "AND student_id = " + user_id + " " +
                "GROUP BY test.test_id;", connection);
            student_available_tests.ExecuteNonQuery();
            return new NpgsqlDataAdapter(student_available_tests);
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

        public NpgsqlDataAdapter Get_groups()
        {
            NpgsqlCommand groups = new NpgsqlCommand("SELECT " +
                "group_id, group_name FROM groups;", connection);
            groups.ExecuteNonQuery();
            return new NpgsqlDataAdapter(groups);
        }

        public NpgsqlDataAdapter Get_students(int user_id, string group)
        {
            string extra_string = "";
            if (group != "Список всех студентов")
            {
                group = group.Replace("Список группы ", "");
                extra_string = " and group_name = '" + group + "'";
            }
            NpgsqlCommand students = new NpgsqlCommand("select s.student_id, full_name, group_name from student s " +
                "join groups g on s.group_id = g.group_id " +
                "join group_teacher gt on gt.group_id = g.group_id " +
                "where teacher_id = " + user_id + extra_string + "; ", connection);
            students.ExecuteNonQuery();
            return new NpgsqlDataAdapter(students);
        }

        public NpgsqlDataAdapter Get_students_access(int user_id, string group)
        {
            string extra1 = "full";
            string extra2 = "";
            string extra3 = "_id not";
            string extra4 = "s.student";

            if (group == "Список групп с доступом к тестам")
            {
                extra1 = "group";
                extra2 = "join groups on s.group_id=groups.group_id";
                extra3 = "s.group_id";
                extra4 = "groups.group";
            }
            NpgsqlCommand access = new NpgsqlCommand("SELECT " + extra4 + "_id, test_name, " + extra1 + "_name, " +
                "access_status FROM test t " +
                "join student_test on student_test.test_id = t.test_id " +
                "join student s on s.student_id = student_test.student_id " + extra2 + " " +
                "where teacher_id = " + user_id + " and group" + extra3 + " in " +
                "(SELECT g.group_id FROM groups g " +
                "join student s on s.group_id = g.group_id " +
                "where(select student_id from student where group_id = g.group_id limit 1) not in " +
                "((SELECT student.student_id FROM student) EXCEPT ALL " +
                "(SELECT st.student_id FROM student_test st " +
                "join student on student.student_id= st.student_id where test_id = t.test_id)) " +
                "group by g.group_id) group by " + extra4 + "_id, test_name, " + extra1 + "_name, access_status; ", connection);
            access.ExecuteNonQuery();
            return new NpgsqlDataAdapter(access);
        }

        public NpgsqlDataAdapter Get_teacher_one_test(int version_id)
        {
            NpgsqlCommand one_test = new NpgsqlCommand("select vq.question_id, question_text, " +
                "array_to_string(array_agg(answers_text), '; ', ''), (select answers_text from answers " +
                "join question_answer on question_answer.answer_id = answers.answer_id " +
                "join version_question on version_question.question_id = question_answer.question_id " +
                "where right_answer = 1 and version_id = " + version_id + " and " +
                "vq.question_id = question_answer.question_id " +
                "group by version_question.question_id, answers_text limit 1) " +
                "from version_question vq inner join questions q on vq.question_id = q.question_id " +
                "inner join question_answer qa on qa.question_id = q.question_id " +
                "inner join answers a on a.answer_id = qa.answer_id where version_id = " + version_id + " " +
                "group by vq.version_question_id,question_text; ", connection);
            one_test.ExecuteNonQuery();
            return new NpgsqlDataAdapter(one_test);
        }

        public NpgsqlDataAdapter Get_teacher_tasks(int user_id)
        {
            NpgsqlCommand teacher_tasks = new NpgsqlCommand("select distinct on (vq.question_id) " +
                "vq.question_id, question_text, array_to_string(array_agg(answers_text), '; ', ''), " +
                "(select answers_text from answers " +
                "join question_answer on question_answer.answer_id = answers.answer_id " +
                "join version_question on version_question.question_id = question_answer.question_id " +
                "join version on version.version_id = version_question.version_id " +
                "join test on test.test_id = version.test_id where right_answer = 1  and " +
                "vq.question_id = question_answer.question_id " +
                "group by version_question.question_id, answers_text limit 1) from version_question vq " +
                "inner join questions q on vq.question_id = q.question_id " +
                "inner join question_answer qa on qa.question_id = q.question_id " +
                "inner join answers a on a.answer_id = qa.answer_id " +
                "inner join version v on v.version_id = vq.version_id " +
                "inner join test t on t.test_id = v.test_id " +
                "where teacher_id = " + user_id + " group by vq.version_question_id,question_text; ", connection);
            teacher_tasks.ExecuteNonQuery();
            return new NpgsqlDataAdapter(teacher_tasks);
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
        public NpgsqlDataAdapter Get_teacher_available_tests(int user_id)
        {
            NpgsqlCommand teacher_available_tests = new NpgsqlCommand("SELECT " +
                "test.test_id, test_name, timer, count(version_id) FROM test " +
                "join version on version.test_id = test.test_id " +
                "WHERE teacher_id = " + user_id + " " +
                "GROUP BY test.test_id; ", connection);
            teacher_available_tests.ExecuteNonQuery();
            return new NpgsqlDataAdapter(teacher_available_tests);
        }

        public void Get_marks(string group, List<int> id, List<string[]> marks)
        {
            string sql_string = "select full_name";
            foreach (int test in id)
            {
                
                sql_string += ", COALESCE((select CAST (mark AS text) from student_test " +
                    "where test_id=" + test + " and student_test. student_id=s.student_id), '-' )";
            }
            sql_string += "from student s join groups g on g.group_id = s.group_id where group_name = '"+group+"';";
            NpgsqlCommand get_marks = new NpgsqlCommand(sql_string, connection);
            NpgsqlDataReader reader_get_marks = get_marks.ExecuteReader();
            
            List<string> one_string = new List<string>();
            int i = 0;
            if (reader_get_marks.HasRows)
            {
                while (reader_get_marks.Read())
                {
                    for (int j = 0; j < id.Count + 1; ++j)
                {
                    one_string.Add(reader_get_marks.GetString(j).ToString());
                }
                    ++i;
                    marks.Add(one_string.ToArray());
                    one_string.Clear();
                }
            }
            reader_get_marks.Close();
        }
        public void Get_student_marks(string group, string student, List<string[]> marks)
        {
            string sql_string = "select test_name, COALESCE((select CAST(mark AS text) from student_test " +
                "where student_test.student_id = s.student_id and student_test.test_id = t.test_id), '-' ) " +
                " from test t JOIN student_test st ON st.test_id = t.test_id " +
                "join student s on s.student_id = st.student_id " +
                "join groups g on g.group_id = s.group_id " +
                "where group_name = '"+group+"' and full_name = '"+student+"'";
            NpgsqlCommand get_marks = new NpgsqlCommand(sql_string, connection);
            NpgsqlDataReader reader_get_marks = get_marks.ExecuteReader();

            List<string> one_string = new List<string>();
            int i = 0;
            if (reader_get_marks.HasRows)
            {
                while (reader_get_marks.Read())
                {
                    for (int j = 0; j < 2; ++j)
                    {
                        one_string.Add(reader_get_marks.GetString(j).ToString());
                    }
                    ++i;
                    marks.Add(one_string.ToArray());
                    one_string.Clear();
                }
            }
            reader_get_marks.Close();
        }
        public void Get_tests(string group, List<string> tests, List<int> id)
        {
            NpgsqlCommand get_tests = new NpgsqlCommand("select t.test_id, test_name from test t " +
                "join student_test st on t.test_id = st.test_id " +
                "join student s on s.student_id = st.student_id " +
                "join groups g on g.group_id = s.group_id " +
                "where group_name = '" + group + "' group by  t.test_id", connection);
            NpgsqlDataReader reader_get_tests = get_tests.ExecuteReader();
            
            if (reader_get_tests.HasRows)
            {
                while (reader_get_tests.Read())
                {
                    id.Add(reader_get_tests.GetInt32(0));
                    tests.Add(reader_get_tests.GetString(1));
                }
            }
            reader_get_tests.Close();
        }
        public void Get_average(string group,  List<int> id, List<string> average_mark, string value)
        {
            string sql_string = "select 'Средний результат по группе'";
            if(value=="time")
                sql_string = "select 'Среднее время прохождения'";
            foreach (int test in id)
            {
                sql_string += ", COALESCE((select round(avg("+value+"),2) from student_test st " +
                    "join student s on s.student_id = st.student_id " +
                    "join groups g on g.group_id = s.group_id " +
                    "where group_name = '" + group + "' and test_id = " + test + "),0)";
            }
            NpgsqlCommand get_average_mark = new NpgsqlCommand(sql_string, connection);
            NpgsqlDataReader reader_get_average_mark = get_average_mark.ExecuteReader();
            if (reader_get_average_mark.HasRows)
            {
                while (reader_get_average_mark.Read())
                {
                    average_mark.Add(reader_get_average_mark.GetString(0).ToString());
                    for (int j = 1; j < id.Count + 1; ++j)
                    {
                        average_mark.Add(reader_get_average_mark.GetDouble(j).ToString());
                    }
                }
            }
            reader_get_average_mark.Close();
        }

        public void Get_student_average_mark(string group, List<string> average_mark, string student_name)
        {
            string sql_string = "select 'Средний балл', COALESCE((select round(avg(mark),2) from student_test st " +
                    "join student s on s.student_id = st.student_id " +
                    "join groups g on g.group_id = s.group_id " +
                    "where group_name = '" + group + "' and full_name='" + student_name + "' ),0)";
          
            NpgsqlCommand get_average_mark = new NpgsqlCommand(sql_string, connection);
            NpgsqlDataReader reader_get_average_mark = get_average_mark.ExecuteReader();
            if (reader_get_average_mark.HasRows)
            {
                while (reader_get_average_mark.Read())
                {
                    average_mark.Add(reader_get_average_mark.GetString(0).ToString());
                    average_mark.Add(reader_get_average_mark.GetDouble(1).ToString());
                }
            }
            reader_get_average_mark.Close();
        }


        public void Update_test_name(int user_id, string old_name, string new_name)
        {
            NpgsqlCommand test_name = new NpgsqlCommand("update test " +
                "set test_name = '" + new_name + "' " +
                "where test_name = '" + old_name + "' and teacher_id = " + user_id + "; ", connection);
            test_name.ExecuteNonQuery();
        }
        public void Update_test_timer(int user_id, int timer, string name)
        {
            NpgsqlCommand test_timer = new NpgsqlCommand("update test " +
                "set timer = " + timer + " " +
                "where test_name = '" + name + "' and teacher_id = " + user_id + "; ", connection);
            test_timer.ExecuteNonQuery();
        }
        public void Update_test_comment(int user_id, string name, string comment)
        {
            NpgsqlCommand test_comment = new NpgsqlCommand("update test " +
                "set comment = '" + comment + "' " +
                "where test_name = '" + name + "' and teacher_id = " + user_id + "; ", connection);
            test_comment.ExecuteNonQuery();
        }
        public void Update_question_name(int id, int type, string name)
        {
            NpgsqlCommand question_name = new NpgsqlCommand("update questions " +
                "set question_text = '" + name + "', " +
                "question_type=" + type + " " +
                "where question_id = " + id + "; ", connection);
            question_name.ExecuteNonQuery();
        }
        public void Update_answer_name(int id, string answer)
        {
            NpgsqlCommand answer_name = new NpgsqlCommand("update answers " +
                "set answers_text='" + answer + "' where answer_id=" + id + ";", connection);
            answer_name.ExecuteNonQuery();
        }

        public void Update_user_full_name(TextBox[] full_name, int user_id, string role)
        {
            NpgsqlCommand student_name = new NpgsqlCommand("update " + role + " " +
                "set full_name='" + full_name[0].Text + "'||' '||'" + full_name[1].Text + "'||' '||'" + full_name[2].Text + "' " +
                "where " + role + "_id=" + user_id + "; ", connection);
            student_name.ExecuteNonQuery();

        }
        public void Update_student_group(string group, int id)
        {
            NpgsqlCommand question_name = new NpgsqlCommand("UPDATE student " +
                "SET group_id = (SELECT group_id FROM groups WHERE group_name = '" + group + "') " +
                "WHERE student_id = " + id + "; ", connection);
            question_name.ExecuteNonQuery();

        }
        public void Update_answer_correctness(int answer_id, int question_id, int is_right)
        {
            NpgsqlCommand answer_correctness = new NpgsqlCommand("update question_answer " +
                "set right_answer=" + is_right + " " +
                "where answer_id=" + answer_id + " " +
                "and question_id=" + question_id + ";", connection);
            answer_correctness.ExecuteNonQuery();
        
        }
        public void Update_group_name(int id, string name)
        {
            NpgsqlCommand group_name = new NpgsqlCommand("UPDATE groups SET group_name = '"+name+"' " +
                "WHERE group_id ="+id+"; ", connection);
            group_name.ExecuteNonQuery();
        }
        public void Delete_task_from_test_version(int user_id, int version, string test, string question)
        {
            NpgsqlCommand delete_task = new NpgsqlCommand("DELETE FROM version_question " +
                "USING questions, version,test " +
                "WHERE version_question.question_id = questions.question_id AND " +
                "version_question.version_id = version.version_id " +
                "and version.test_id = test.test_id " +
                "and test.teacher_id = " + user_id + " " +
                "and test_name='" + test + "' " +
                "and version_number=" + version + " " +
                "and question_text = '" + question + "'; ", connection);
            delete_task.ExecuteNonQuery();
        }
        private void Delete_questions_from_db(List<int> ids)
        {
            foreach (int id in ids)
            {
                NpgsqlCommand delete_questions = new NpgsqlCommand("delete from questions where question_id="+id+";", connection);
                delete_questions.ExecuteNonQuery();
            }
        }
        private void Delete_answers_from_db(List<int> ids)
        {
            foreach (int id in ids)
            {
                
                NpgsqlCommand delete_answers = new NpgsqlCommand("delete from answers where answer_id = "+id+"; ", connection);
                delete_answers.ExecuteNonQuery();
            }
        }
        public void Delete_tasks_from_db(int user_id)
        {
            List<int> q_ids = Get_questions_id(user_id);
            
            List<int> a_ids = new List<int>();
            a_ids.AddRange(Get_answers_id(user_id));
            
            Delete_questions_from_db(q_ids);
            Delete_answers_from_db(a_ids);

        }
        public void Delete_version(int user_id, int version, string test, int test_id)
        {
           string sql_string = "delete from version using test " +
                "where version.test_id = test.test_id " +
                "and teacher_id = " + user_id + " " +
                "and version_number = " + version + " " +
                "and test_name = '" + test + "'; ";
            if(test=="" && test_id != -1)
            {
                sql_string = "delete from version using test " +
                "where version.test_id = test.test_id " +
                "and teacher_id = " + user_id + " " +
                "and version_id = " + version + " " +
                "and test_id = '" + test_id + "'; ";
            }
            NpgsqlCommand delete_version = new NpgsqlCommand(sql_string, connection);
            delete_version.ExecuteNonQuery();
        }

        public void Delete_test(int user_id, int test)
        {
            NpgsqlCommand delete_test = new NpgsqlCommand("DELETE FROM test " +
                "WHERE test.teacher_id = " + user_id + " and test_id = " + test + "; ", connection);
            delete_test.ExecuteNonQuery();
        }
        public void Delete_group(int id)
        {
            NpgsqlCommand delete_group = new NpgsqlCommand("DELETE FROM groups " +
                "WHERE group_id = " + id + "; ", connection);
            delete_group.ExecuteNonQuery();
        }
        public void Delete_user(int id, string role)
        {
            NpgsqlCommand delete_user = new NpgsqlCommand("DELETE FROM "+role+" WHERE "+role+"_id = "+id+"; ", connection);
            delete_user.ExecuteNonQuery();
        }
        private bool Need_to_update_access(int id, string test)
        {
            NpgsqlCommand access = new NpgsqlCommand("select exists(select student_id, st.test_id from student_test st " +
                "join test t on t.test_id = st.test_id " +
                "where student_id = "+id+" and test_name = '"+test+"' and mark is null and access_status = false); ", connection);
            return (bool)access.ExecuteScalar();
        }
        public void Allow_or_prohibit_student_access_to_test(int id, string test, bool allow)
        {
            string sql_string = "INSERT INTO student_test (student_id,test_id,) " +
                "VALUES(" + id + ", (SELECT test_id FROM test WHERE test_name = '" + test + "')); ";
            if (!allow || Need_to_update_access(id, test))
                sql_string = "update student_test set access_status = "+allow+" where student_id = " + id + " " +
                    "and test_id = (select test_id from test where test_name = '" + test + "') and mark is null";
            
            NpgsqlCommand access = new NpgsqlCommand(sql_string, connection);
            access.ExecuteNonQuery();
        }
        public void Add_teacher_group_connection(int teacher_id, int id, bool is_student)
        {
            string extra1 = "";
            string extra2 = "";

            if (is_student)
            {
                extra1 = "(SELECT group_id FROM student WHERE student_id =";
                extra2 = ")";
            }
            NpgsqlCommand teacher_group_connection = new NpgsqlCommand("INSERT INTO group_teacher (group_id, teacher_id) " +
                "VALUES(" + extra1 + id + extra2 + ", " + teacher_id + ") " +
                "ON CONFLICT(group_id, teacher_id) DO NOTHING; ", connection);
            teacher_group_connection.ExecuteNonQuery();
        }

        public List<int> Get_answers_id(int version_id, string question)
        {
            List<int> answers_id = new List<int>();
            NpgsqlCommand get_answers_id = new NpgsqlCommand("SELECT answers.answer_id FROM answers " +
                "JOIN question_answer ON question_answer.answer_id = answers.answer_id " +
                "JOIN version_question ON version_question.question_id = question_answer.question_id " +
                "JOIN questions ON questions.question_id = question_answer.question_id " +
                "WHERE questions.question_text = '" + question + "' " +
                "AND version_question.version_id = " + version_id + " " +
                "GROUP BY answers.answer_id; ", connection);
            NpgsqlDataReader reader_get_answers_id = get_answers_id.ExecuteReader();
            while (reader_get_answers_id.Read())
            {
                answers_id.Add((int)reader_get_answers_id.GetValue(0));
            }
            reader_get_answers_id.Close();
            return answers_id;
        }
        public List<int> Get_questions_id(int user_id)
        {
            List<int> id = new List<int>();
            NpgsqlCommand get_answers_id = new NpgsqlCommand("select q.question_id from questions q " +
                "join version_question vq on vq.question_id = q.question_id " +
                "join version v on v.version_id = vq.version_id " +
                "join test t on t.test_id = v.test_id " +
                "where teacher_id = "+user_id+";", connection);
            NpgsqlDataReader reader_get_answers_id = get_answers_id.ExecuteReader();
            while (reader_get_answers_id.Read())
            {
                id.Add((int)reader_get_answers_id.GetValue(0));
            }
            reader_get_answers_id.Close();
            return id;
        }
       
        public List<int> Get_answers_id(int user_id)
        {
            List<int> id = new List<int>();
            NpgsqlCommand answers = new NpgsqlCommand("select a.answer_id from answers a " +
              "join question_answer aq on aq.answer_id = a.answer_id " +
              "join version_question vq on vq.question_id = aq.question_id " +
              "join version v on v.version_id = vq.version_id " +
              "join test t on t.test_id = v.test_id " +
              "where teacher_id = " + user_id + "; ", connection);
            NpgsqlDataReader reader_get_answers_id = answers.ExecuteReader();
            while (reader_get_answers_id.Read())
            {
                id.Add((int)reader_get_answers_id.GetValue(0));
            }
            reader_get_answers_id.Close();
            return id;
        }



        
    }
}


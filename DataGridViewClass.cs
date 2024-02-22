using Npgsql;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace testing_program
{
    class DataGridViewClass
    {
        public void Show(DataGridView table)
        {
            table.Visible = true;
        }

        public void Hide(DataGridView table)
        {
            table.Visible = false;
        }

        public void Clear_table(DataGridView table)
        {
            table.DataSource = null;
        }

        public void Clear_rows(DataGridView table)
        {
            if (table.RowCount > 0)
                table.Rows.Clear();
        }

        public void Change_column_header_text(DataGridView table, int index, string text)
        {
            table.Columns[index].HeaderText = text;
        }

        public void Change_column_header_text(DataGridView table, int skip_index_count, string[] text)
        {
            for (int i = 0; i < text.Length; ++i)
                table.Columns[i + skip_index_count].HeaderText = text[i];
        }

        public string Get_column_name(DataGridView table, DataGridViewCell cell)
        {
            return table.Columns[cell.ColumnIndex].Name;
        }

        public int Get_null_column_hiden_id(DataGridView table, DataGridViewCell cell)
        {
            return System.Convert.ToInt32(table.Rows[cell.RowIndex].Cells[0].Value);
        }

        public void Change_column_width(DataGridView table, int index, int value)
        {
            table.Columns[index].Width = value;
        }

        public void Change_column_width(DataGridView table, int skip_index_count, int[] value)
        {
            for (int i = 0; i < value.Length; ++i)
                table.Columns[i + skip_index_count].Width = value[i];
        }

        public void Change_column_name(DataGridView table, int index, string value)
        {
            table.Columns[index].Name = value;
        }

        public void Change_back_color(DataGridView table, Color color)
        {
            table.RowsDefaultCellStyle.SelectionBackColor = color;
        }

        public void Change_fore_color(DataGridView table, Color color)
        {
            table.RowsDefaultCellStyle.SelectionForeColor = color;
        }

        public void Reset_student_available_test_table(DatabaseClass database, int user_id, DataGridView table)
        {
            Clear_table(table);
            DataSet set = new DataSet();
            NpgsqlDataAdapter adapter = database.Get_student_available_tests(user_id);
            set.Reset();
            adapter.Fill(set);
            DataTable dataTable = set.Tables[0];
            table.DataSource = dataTable;
            Change_column_header_text(table, 1, new string[] { "Название теста", "Комментарий" });
            Change_column_width(table, 1, new int[] { 300, 305 });
            table.Columns[0].Visible = false;
            Change_back_color(table, Color.White);
            Change_fore_color(table, Color.Black);
            table.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
        }

        public void Reset_completed_tests_list_table(DatabaseClass database, int user_id, DataGridView table)
        {
            Clear_table(table);
            DataSet set = new DataSet();
            Show(table);
            NpgsqlDataAdapter adapter = database.Get_student_completed_tests(user_id);
            set.Reset();
            adapter.Fill(set);
            DataTable dataTable = set.Tables[0];
            table.DataSource = dataTable;
            Change_column_header_text(table, 0, new string[] { "Название теста", "Оценка", "Время прохождения в минутах" });
            Change_column_width(table, 0, new int[] { 350, 70, 140 });
        }

        public void Reset_students_list_table(DatabaseClass database, int user_id, string category, DataGridView table)
        {
            Clear_table(table);
            DataSet set = new DataSet();
            Show(table);
            string[] header_text = { "ФИО студента", "Группа" };
            string[] header_name = { "student", "group" };
            string hidden_header_name = "";
            bool one_group = false;
            NpgsqlDataAdapter adapter;
            switch (category)
            {
                case "Список групп с доступом к тестам":
                    adapter = database.Get_students_access(user_id, category);
                    header_text[0] = "Название теста";
                    header_name[0] = "test";
                    break;
                case "Список студентов с персональным доступом к тестам":
                    adapter = database.Get_students_access(user_id, category);
                    header_text[1] = header_text[0];
                    header_text[0] = "Название теста";
                    header_name[1] = header_name[0];
                    header_name[0] = "test";
                    hidden_header_name = "id";
                    break;
                case "Список всех групп":
                    adapter = database.Get_groups();
                    header_text[0] = header_text[1];
                    header_name[0] = header_name[1];
                    break;
                default:
                    adapter = database.Get_students(category);
                    hidden_header_name = "id";
                    one_group = true;
                    break;
            }
            set.Reset();
            adapter.Fill(set);
            DataTable dataTable = set.Tables[0];
            table.DataSource = dataTable;
            table.Columns[0].Visible = false;
            if (one_group && category != "Список всех студентов")
                table.Columns[2].Visible = false;
            Change_column_name(table, 0, hidden_header_name);
            for (int i = 1; i < table.Columns.Count; ++i)
            {
                if (i == 3)
                {
                    Change_column_header_text(table, i, "Доступ к тесту");
                    for (int j = 0; j < table.RowCount; ++j)
                    {
                        table.Rows[j].Cells[3].Value = !(table.Rows[j].Cells[3].Value.ToString() == "closed group" ||
                            table.Rows[j].Cells[3].Value.ToString() == "closed personal") ? "Да" : "Нет";
                    }
                }
                else
                {
                    Change_column_header_text(table, i, header_text[i - 1]);
                    Change_column_name(table, i, header_name[i - 1]);
                }
            }
            table.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
        }

        public void Reset_one_test_result_table(DatabaseClass database, DataGridView table, int attempt_id)
        {
            Clear_table(table);
            DataSet set = new DataSet();
            Show(table);
            NpgsqlDataAdapter adapter = database.Get_test_analysis(attempt_id);
            set.Reset();
            adapter.Fill(set);
            DataTable dataTable = set.Tables[0];
            table.DataSource = dataTable;
            Change_column_header_text(table, 0, new string[] { "Вопрос", "Правильность выполнения" });
            Change_column_width(table, 0, new int[] { 460, 100 });
            table.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
        }

        public void Reset_teacher_test_table(DatabaseClass database, int user_id, DataGridView table)
        {
            Clear_table(table);
            DataSet set = new DataSet();
            Show(table);
            NpgsqlDataAdapter adapter = database.Get_teacher_available_tests(user_id);
            set.Reset();
            adapter.Fill(set);
            DataTable dataTable = set.Tables[0];
            table.DataSource = dataTable;
            Change_column_header_text(table, 1, new string[] { "Название теста", "Ограничение по времени в минутах", "Количество вариантов" });
            Change_column_width(table, 1, new int[] { 460, 100, 100 });
            table.Columns[0].Visible = false;
        }

        public void Reset_student_marks_table(DatabaseClass database, string group, string student, DataGridView table, int user_id)
        {
            Clear_rows(table);
            Show(table);
            List<string[]> marks = new List<string[]>(); //marks
            database.Get_student_marks(user_id, group, student, marks);
            if (marks.Count > 0)
            {
                List<string> average_mark = new List<string>(); //1st row
                database.Get_student_average_mark(group, average_mark, student);
                List<string[]> result = new List<string[]> { average_mark.ToArray() };
                List<string> border_string = new List<string>();
                for (int i = 0; i < 2; ++i)
                    border_string.Add(" ");
                result.Add(border_string.ToArray());
                result.AddRange(marks.ToArray());
                table.ColumnCount = 2;
                foreach (string[] s in result)
                {
                    table.Rows.Add(s);
                }
                Change_column_header_text(table, 0, "Тест");
                Change_column_header_text(table, 1, "Оценка");
            }
            table.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
        }

        public void Reset_group_marks_table(DatabaseClass database, string group, DataGridView table, int user_id)
        {
            Clear_rows(table);
            Show(table);
            List<string> tests = new List<string>();
            List<int> id = database.Get_all_tests(user_id, group, tests);//headers
            if (id.Count <= 0)
                return;
            
            List<string> average_mark = new List<string>(); //1st row
            database.Get_average(group, id, average_mark, "mark");
            List<string> average_time = new List<string>(); //2nd row
            database.Get_average(group, id, average_time, "time");
            List<string[]> marks = new List<string[]>(); //marks
            database.Get_marks(group, id, marks);
            List<string[]> result = new List<string[]> { average_mark.ToArray() };
            result.Add(average_time.ToArray());
            List<string> border_string = new List<string>();
            for (int i = 0; i < id.Count + 1; ++i)
                border_string.Add(" ");
            result.Add(border_string.ToArray());
            result.AddRange(marks.ToArray());
            table.ColumnCount = id.Count + 1;
            foreach (string[] s in result)
                table.Rows.Add(s);
            Change_column_header_text(table, 1, tests.ToArray());
            table.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
        }

        public void Reset_teacher_task_table(DatabaseClass database, int user_id, DataGridView table)
        {
            Clear_rows(table);
            Show(table);
            List<string> questions = new List<string>();
            List<int> id = database.Get_teacher_tasks(user_id, questions);//1st column
            if (id.Count > 0)
            {
                List<string> answers = new List<string>(); //2nd column
                List<string> right_answers = new List<string>(); //3rd column
                List<string[]> result = new List<string[]>();
                for (int i = 0; i < id.Count; ++i)
                {
                    database.Get_task(id[i], answers);
                    database.Get_question_right_answers(id[i], right_answers);
                    result.Add(new string[] { questions[i], answers[i], right_answers[i] });
                }
                table.ColumnCount = 3;
                foreach (string[] s in result)
                    table.Rows.Add(s);
                Change_column_header_text(table, 0, new string[] { "Вопрос", "Варианты ответов", "Правильный ответ" });
                table.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            }
        }

        public void Reset_teacher_one_test_table(DatabaseClass database, int user_id, int version_id, DataGridView table)
        {
            Clear_rows(table);
            Show(table);
            List<string> questions = new List<string>();
            List<int> id = database.Get_teacher_tasks(user_id, questions, version_id);//1st column
            if (id.Count > 0)
            {
                List<string> answers = new List<string>(); //2nd column
                List<string> right_answers = new List<string>(); //3rd column
                List<string[]> result = new List<string[]>();
                for (int i = 0; i < id.Count; ++i)
                {
                    database.Get_task(id[i], answers);
                    database.Get_question_right_answers(id[i], right_answers);
                    result.Add(new string[] { questions[i], answers[i], right_answers[i] });
                }
                table.ColumnCount = 3;
                foreach (string[] s in result)
                    table.Rows.Add(s);
                Change_column_header_text(table, 0, new string[] { "Вопрос", "Варианты ответов", "Правильный ответ" });
                table.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            }
        }
    }
}

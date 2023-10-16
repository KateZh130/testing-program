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
        public void Clear_selection(DataGridView table)//?
        {
            table.ClearSelection();
        }
        public void Change_column_header_text(DataGridView table, int index, string text)
        {
            table.Columns[index].HeaderText = text;
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
            DataSet set = new DataSet();

            NpgsqlDataAdapter adapter = database.Get_student_available_tests(user_id);
            set.Reset();
            adapter.Fill(set);
            DataTable dataTable = set.Tables[0];
            table.DataSource = dataTable;
            string[] text = { "Название теста", "Комментарий" };
            int[] value = { 300, 305 };
            for (int i = 0; i < text.Length; ++i)
            {
                Change_column_header_text(table, i + 1, text[i]);
                Change_column_width(table, i + 1, value[i]);
            }
            table.Columns[0].Visible = false;
            Change_back_color(table, Color.White);
            Change_fore_color(table, Color.Black);
            table.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

        }


        public void Reset_completed_tests_list_table(DatabaseClass database, int user_id, DataGridView table)
        {
            DataSet set = new DataSet();
            Show(table);

            NpgsqlDataAdapter adapter = database.Get_completed_tests(user_id);

            set.Reset();
            adapter.Fill(set);
            DataTable dataTable = set.Tables[0];
            table.DataSource = dataTable;
            string[] text = { "Название теста", "Оценка", "Дата и время прохождения" };
            int[] value = { 350, 70, 140 };
            for (int i = 0; i < text.Length; ++i)
            {
                Change_column_header_text(table, i, text[i]);
                Change_column_width(table, i, value[i]);
            }

        }
        public void Reset_students_list_table(DatabaseClass database, int user_id, string group, DataGridView table)
        {
            DataSet set = new DataSet();
            Show(table);
            string header1 = "ФИО студента";
            string header2 = "Группа";
            string name0 = "id";
            string name1 = "student";
            string name2 = "group";


            NpgsqlDataAdapter adapter;
            switch (group)
            {
                case "Список групп с доступом к тестам":
                    adapter = database.Get_students_access(user_id, group);
                    header1 = "Название теста";
                    name0 = "";
                    name1 = "test";
                    break;
                case "Список студентов с персональным доступом к тестам":
                    adapter = database.Get_students_access(user_id, group);
                    header2 = header1;
                    header1 = "Название теста";
                    name2 = name1;
                    name1 = "test";
                    break;
                case "Список всех групп":
                    adapter = database.Get_groups();
                    header1 = header2;
                    name1 = name2;
                    name0 = "";
                    break;
                default:
                    adapter = database.Get_students(user_id, group);
                    break;
            }
            set.Reset();
            adapter.Fill(set);
            DataTable dataTable = set.Tables[0];
            table.DataSource = dataTable;
            string[] text = { header1, header2 };
            string[] name = { name1, name2 };
            table.Columns[0].Visible = false;
            Change_column_header_text(table, 0, name0);
            for (int i = 1; i < table.Columns.Count; ++i)
            {
                Change_column_header_text(table, i, text[i - 1]);
                Change_column_name(table, i, name[i - 1]);
            }
            if (table.Columns.Count == 4)
                Change_column_header_text(table, 3, "Доступ к тесту");

            table.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

        }


        public void Reset_one_test_result_table(DatabaseClass database, int user_id, DataGridView table, ComboBox test_name)
        {
            DataSet set = new DataSet();
            Show(table);

            NpgsqlDataAdapter adapter = database.Get_test_analysis(user_id, test_name);
            set.Reset();
            adapter.Fill(set);
            DataTable dataTable = set.Tables[0];
            table.DataSource = dataTable;
            string[] text = { "Вопрос", "Правильность выполнения" };
            int[] value = { 460, 100 };
            for (int i = 0; i < text.Length; ++i)
            {
                Change_column_header_text(table, i, text[i]);
                Change_column_width(table, i, value[i]);
            }
        }

        public void Reset_teacher_test_table(DatabaseClass database, int user_id, DataGridView table)
        {
            DataSet set = new DataSet();
            Show(table);

            NpgsqlDataAdapter adapter = database.Get_teacher_available_tests(user_id);
            set.Reset();
            adapter.Fill(set);
            DataTable dataTable = set.Tables[0];
            table.DataSource = dataTable;
            string[] text = { "Название теста", "Ограничение по времени", "Количество вариантов" };
            int[] value = { 460, 100, 100 };
            for (int i = 0; i < text.Length; ++i)
            {
                Change_column_header_text(table, i + 1, text[i]);
                Change_column_width(table, i + 1, value[i]);
            }
            table.Columns[0].Visible = false;
        }

        public void Reset_teacher_one_test_table(DatabaseClass database, int version_id, DataGridView table)
        {
            DataSet set = new DataSet();
            Show(table);

            NpgsqlDataAdapter adapter = database.Get_teacher_one_test(version_id);
            set.Reset();
            adapter.Fill(set);
            DataTable dataTable = set.Tables[0];
            table.DataSource = dataTable;
            string[] text = { "Вопрос", "Ответы", "Правильный ответ" };
            int[] value = { 300, 280, 139 };
            for (int i = 0; i < text.Length; ++i)
            {
                Change_column_header_text(table, i + 1, text[i]);
                Change_column_width(table, i + 1, value[i]);
            }
            table.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            table.Columns[0].Visible = false;
        }
        public void Reset_teacher_task_table(DatabaseClass database, int user_id, DataGridView table)
        {
            DataSet set = new DataSet();
            Show(table);

            NpgsqlDataAdapter adapter = database.Get_teacher_tasks(user_id);
            set.Reset();
            adapter.Fill(set);
            DataTable dataTable = set.Tables[0];
            table.DataSource = dataTable;
            string[] text = { "Вопрос", "Ответы", "Правильный ответ" };
            int[] value = { 300, 280, 139 };
            for (int i = 0; i < text.Length; ++i)
            {
                Change_column_header_text(table, i + 1, text[i]);
                Change_column_width(table, i + 1, value[i]);
            }
            table.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            table.Columns[0].Visible = false;
        }

        public void Reset_student_marks_table(DatabaseClass database, string group, string student, DataGridView table)
        {
            List<string> average_mark = new List<string>(); //1st row
            database.Get_student_average_mark(group, average_mark, student);
            List<string[]> marks = new List<string[]>(); //marks
            database.Get_student_marks(group, student, marks);
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
            
            table.DefaultCellStyle.WrapMode = DataGridViewTriState.True; 
        }

        
        public void Reset_group_marks_table(DatabaseClass database, string group, DataGridView table)
        {
            List<string> tests = new List<string>();//headers
            List<int> id = new List<int>();
            database.Get_tests(group, tests, id);
            List<string> average_mark = new List<string>(); //1st row
            database.Get_average(group, id, average_mark, "mark");
            List<string> average_time = new List<string>(); //2st row
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
            {
                table.Rows.Add(s);
            }
            for (int i = 0; i < table.ColumnCount - 1; ++i)
            {
                Change_column_header_text(table, i + 1, tests[i]);
            }
            table.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
        }
    }
}

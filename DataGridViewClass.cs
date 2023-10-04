using Npgsql;
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
        public void Change_column_header_text(DataGridView table, int index, string text)
        {
            table.Columns[index].HeaderText = text;
        }

        public void Change_column_width(DataGridView table, int index, int value)
        {
            table.Columns[index].Width = value;
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
        DataSet set1 = new DataSet();
            
            NpgsqlDataAdapter da1 = database.Get_student_available_tests(user_id);
            set1.Reset();
            da1.Fill(set1);
            DataTable table1 = set1.Tables[0];
            table.DataSource = table1;
            string[] text = { "Название теста", "Комментарий" };
            int[] value = { 300, 305 };
            for (int i = 0; i < text.Length; ++i)
            {
                Change_column_header_text(table, i+1, text[i]);
                Change_column_width(table, i+1, value[i]);
            }
            table.Columns[0].Visible = false;
            Change_back_color(table, Color.White);
            Change_fore_color(table, Color.Black);
            table.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

        }


        public void Reset_completed_tests_list_table(DatabaseClass database, int user_id, DataGridView table)
        {
           DataSet set2 = new DataSet();
        //DataTable table2 = new DataTable();
        Show(table);

            NpgsqlDataAdapter da2 = database.Get_completed_tests(user_id);

            set2.Reset();
            da2.Fill(set2);
            DataTable table2 = set2.Tables[0];
            table.DataSource = table2;
            string[] text = { "Название теста", "Оценка", "Дата и время прохождения" };
            int[] value = { 350, 70, 140 };
            for (int i = 0; i < text.Length; ++i)
            {
                Change_column_header_text(table, i, text[i]);
                Change_column_width(table, i, value[i]);
            }

        }

        public void Reset_one_test_result_table(DatabaseClass database, int user_id, DataGridView table, ComboBox test_name)
        {
            DataSet set3 = new DataSet();
        Show(table);

            NpgsqlDataAdapter da3 = database.Get_test_analysis(user_id, test_name);
            set3.Reset();
            da3.Fill(set3);
            DataTable table3 = set3.Tables[0];
            table.DataSource = table3;
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

            NpgsqlDataAdapter da = database.Get_teacher_available_tests(user_id);
            set.Reset();
            da.Fill(set);
            DataTable dt = set.Tables[0];
            table.DataSource = dt;
            string[] text = { "Название теста", "Ограничение по времени","Количество вариантов" };
            int[] value = { 460, 100,100 };
            for (int i = 0; i < text.Length; ++i)
            {
                Change_column_header_text(table, i+1, text[i]);
                Change_column_width(table, i+1, value[i]);
            }
            table.Columns[0].Visible = false;
        }

        public void Reset_teacher_one_test_table(DatabaseClass database, int version_id, DataGridView table)
        {
            DataSet set = new DataSet();
            Show(table);

            NpgsqlDataAdapter da = database.Get_teacher_one_test(version_id);
            set.Reset();
            da.Fill(set);
            DataTable dt = set.Tables[0];
            table.DataSource = dt;
            string[] text = { "Вопрос", "Ответы", "Правильный ответ" };
            int[] value = { 300, 280, 139 };
            for (int i = 0; i < text.Length; ++i)
            {
                Change_column_header_text(table, i+1 , text[i]);
                Change_column_width(table, i+1 , value[i]);
            }
            table.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            table.Columns[0].Visible = false;
        }
        public void Reset_teacher_task_table(DatabaseClass database, int user_id, DataGridView table)
        {
            DataSet set = new DataSet();
            Show(table);

            NpgsqlDataAdapter da = database.Get_teacher_tasks(user_id);
            set.Reset();
            da.Fill(set);
            DataTable dt = set.Tables[0];
            table.DataSource = dt;
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
    }
}

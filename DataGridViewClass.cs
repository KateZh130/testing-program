using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Forms;
using Npgsql;

namespace testing_program
{
    class DataGridViewClass
    {
        private DataSet set1 = new DataSet();
        private DataTable table1 = new DataTable();
        private DataSet set2 = new DataSet();
        private DataTable table2 = new DataTable();
        private DataSet set3 = new DataSet();
        private DataTable table3 = new DataTable();
        

        public void Reset_test_table(DatabaseClass database, int user_id, DataGridView table)
        {
            NpgsqlDataAdapter da1 = database.Get_available_tests(user_id);
            set1.Reset();
            da1.Fill(set1);
            table1 = set1.Tables[0];
            table.DataSource = table1;
        }
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
        public void Change_back_color(DataGridView table, System.Drawing.Color color)
        {
            table.RowsDefaultCellStyle.SelectionBackColor = color;
            
        }
        public void Change_fore_color(DataGridView table, System.Drawing.Color color)
        {
            table.RowsDefaultCellStyle.SelectionForeColor = color;
        }

        public void Reset_completed_tests_list_table(DatabaseClass database,int user_id, DataGridView table)
        {
            Show(table);

            NpgsqlDataAdapter da2 = database.Get_completed_tests(user_id);

            set2.Reset();
            da2.Fill(set2);
            table2 = set2.Tables[0];
            table.DataSource = table2;
            string[] text = { "Название теста", "Оценка", "Дата и время прохождения" };
            int[] value = { 350, 70, 140};
            for (int i = 0; i <text.Length; ++i)
            {
                Change_column_header_text(table, i, text[i]);
                Change_column_width(table, i, value[i]);
            }
            
        }

        public void Reset_one_test_result_table(DatabaseClass database, int user_id, DataGridView table, ComboBox test_name)
        {
            Show(table);

            NpgsqlDataAdapter da3 = database.Get_test_analysis( user_id, test_name);
            set3.Reset();
            da3.Fill(set3);
            table3 = set3.Tables[0];
            table.DataSource = table3;
            string[] text = { "Вопрос", "Правильность выполнения"};
            int[] value = { 460, 100 };
            for (int i = 0; i < text.Length; ++i)
            {
                Change_column_header_text(table, i, text[i]);
                Change_column_width(table, i, value[i]);
            }
        }
    }
}

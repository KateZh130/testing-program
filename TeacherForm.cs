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
    public partial class TeacherForm : Form
    {
        NpgsqlConnection connection = new NpgsqlConnection();
        int user_id;
        public TeacherForm(NpgsqlConnection connection, int user_id)
        {
            InitializeComponent();
            this.connection = connection;
            this.user_id = user_id;
            fillFormName();
        }
        //Заполнение текста формы именем пользователя
        private void fillFormName()
        {
            NpgsqlCommand select_teacher_name = new NpgsqlCommand("SELECT full_name FROM teacher WHERE teacher_id = " + user_id + ";", connection);
            this.Text = select_teacher_name.ExecuteScalar().ToString();

        }

        private void TeacherForm_Load(object sender, EventArgs e)
        {

        }
    }
}

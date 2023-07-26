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

        List<string> profil_info_list = new List<string>();

        NpgsqlConnection connection = new NpgsqlConnection();
        readonly int user_id;
        public TeacherForm(NpgsqlConnection connection, int user_id)
        {
            InitializeComponent();
            this.connection = connection;
            this.user_id = user_id;
            fillFormName();
            fillTeacherProfileMainPanel();
        }
        //Заполнение текста формы именем пользователя
        private void fillFormName()
        {
            NpgsqlCommand select_teacher_name = new NpgsqlCommand("SELECT full_name " +
                "FROM teacher WHERE teacher_id = " + user_id + ";", connection);
            this.Text = select_teacher_name.ExecuteScalar().ToString();

        }

        private void fillTeacherProfileMainPanel()
        {
            string profile_info = "";
            teacher_profile_main_panel.Visible = true;
            change_teacher_login_panel.Visible = false;
            change_teacher_password_panel.Visible = false;
            teacher_identity_check_panel.Visible = false;

            NpgsqlCommand get_profile_info = new NpgsqlCommand("SELECT " +
                "full_name, login, password FROM teacher " +
                "WHERE teacher_id = " + user_id + "; ", connection);

            NpgsqlDataReader reader = get_profile_info.ExecuteReader();
            while (reader.Read())
            {
                for (int s = 0; s < 3; ++s)
                {
                    profile_info += reader.GetValue(s).ToString() + " ";
                }
            }
            reader.Close();
            profil_info_list = profile_info?.Split(' ').ToList();
            full_name_teacher_profile.Text = profil_info_list[0] + " " + profil_info_list[1] + " " + profil_info_list[2];
            login_teacher_profile.Text = profil_info_list[3];
            password_teacher_profile.Text = profil_info_list[4];

        }
        private void TeacherForm_Load(object sender, EventArgs e)
        {

        }

        private void cancel_student_change_password_button_Click(object sender, EventArgs e)
        {

        }

        private void student_old_password_textBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void continue_student_change_password_button_Click(object sender, EventArgs e)
        {

        }

        private void cancel_change_student_login_button_Click(object sender, EventArgs e)
        {

        }

        private void new_student_login_button_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void change_student_login_label_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void new_student_password_button_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void cancel_change_student_password_button_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void change_student_password_button_Click(object sender, EventArgs e)
        {

        }

        private void change_student_login_button_Click(object sender, EventArgs e)
        {

        }

        private void group_student_profile_label_Click(object sender, EventArgs e)
        {

        }

        private void password_student_profile_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void login_student_profile_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void full_name_student_profile_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void group_student_profile_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void profile_page_Click(object sender, EventArgs e)
        {

        }

        private void change_teacher_login_button_Click(object sender, EventArgs e)
        {
            change_teacher_login_panel.Visible = true;
            teacher_profile_main_panel.Visible = false;
        }

        private void new_teacher_login_button_Click(object sender, EventArgs e)
        {
            if (new_teacher_login_textbox.Text != "")
            {
                //запись логина в бд
                NpgsqlCommand updateTeacherLoginAtDatabase = new NpgsqlCommand("UPDATE teacher " +
                    "SET login = '" + new_teacher_login_textbox.Text + "'" +
                    "WHERE teacher_id = " + user_id + "; ", connection);
                updateTeacherLoginAtDatabase.ExecuteNonQuery();
                //обновить панель
                fillTeacherProfileMainPanel();
                //логин изменен
                MessageBox.Show("Логин успешно изменен!");
            }
            else
            {
                MessageBox.Show("Логин не введен", "Введите логин");
            }
        }

        private void cancel_change_teacher_login_button_Click(object sender, EventArgs e)
        {
            change_teacher_login_panel.Visible = false;
            teacher_profile_main_panel.Visible = true;
        }

        private void change_teacher_password_button_Click(object sender, EventArgs e)
        {
            teacher_identity_check_panel.Visible = true;
            teacher_profile_main_panel.Visible = false;
        }

        private void new_teacher_password_button_Click(object sender, EventArgs e)
        {
            if (teacher_test_new_password_textBox.Text == "" || teacher_new_password_textBox.Text == "")
            {
                MessageBox.Show("Введите пароль.");
            }
            else if (teacher_test_new_password_textBox.Text != teacher_new_password_textBox.Text)
            {
                MessageBox.Show("Пароли не совпадают.");
            }
            else
            {
                //запись логина в бд
                NpgsqlCommand updateStudentPasswordAtDatabase = new NpgsqlCommand("UPDATE teacher " +
                    "SET password = '" + teacher_test_new_password_textBox.Text + "'" +
                    "WHERE teacher_id = " + user_id + "; ", connection);
                updateStudentPasswordAtDatabase.ExecuteNonQuery();
                //обновить панель
                fillTeacherProfileMainPanel();
                //логин изменен
                MessageBox.Show("Пароль успешно изменен!");
            }
        }

        private void cancel_change_teacher_password_button_Click(object sender, EventArgs e)
        {
            change_teacher_password_panel.Visible = false;
            teacher_profile_main_panel.Visible = true;
        }

        private void continue_teacher_change_password_button_Click(object sender, EventArgs e)
        {
            //проверка совпадения старого пароля
            if (teacher_old_password_textBox.Text == "")
            {
                MessageBox.Show("Введите пароль.");
            }
            else if (teacher_old_password_textBox.Text != profil_info_list[4])
            {
                MessageBox.Show("Указан неверный пароль.");
            }
            else
            {
                teacher_old_password_textBox.Clear();
                teacher_identity_check_panel.Visible = false;
                change_teacher_password_panel.Visible = true;
            }
        }

        private void cancel_teacher_change_password_button_Click(object sender, EventArgs e)
        {
            teacher_old_password_textBox.Clear();
            teacher_identity_check_panel.Visible = false;
            teacher_profile_main_panel.Visible = true;
        }

        private void teacher_identity_check_panel_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}

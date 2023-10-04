using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;


namespace testing_program
{
    public partial class TeacherForm : Form
    {
        readonly TextboxClass textbox = new TextboxClass();
        readonly CheckboxClass checkbox = new CheckboxClass();
        readonly ComboboxClass combobox = new ComboboxClass();
        readonly DataGridViewClass data = new DataGridViewClass();
        readonly RadiobuttonClass radiobutton = new RadiobuttonClass();
        readonly DatabaseClass database;

        private int test_id = 0;
        private int version_id = 0;
        private int version_counter = 0;
        private int question_counter = 1;
        readonly int user_id;
        int edit_method;
        bool edit_mode = false;
        bool contextmenustrip1_enabled = false;


        List<string> profil_info_list = new List<string>();
        List<string> questions_text = new List<string>();
        List<int> versions_id = new List<int>();


        public TeacherForm(DatabaseClass database, int user_id)
        {
            InitializeComponent();
            this.database = database;
            this.user_id = user_id;
            this.Text = database.Get_name(user_id, "teacher");
            FillTeacherProfileMainPanel();
            data.Reset_teacher_test_table(database, user_id, teacher_available_tests_table);
            data.Reset_teacher_task_table(database, user_id, available_teacher_tasks_table);
            //contextMenuStrip1.Enabled = false;
        }

        private void FillTeacherProfileMainPanel()
        {
            teacher_profile_main_panel.Visible = true;
            change_teacher_login_panel.Visible = false;
            change_teacher_password_panel.Visible = false;
            teacher_identity_check_panel.Visible = false;
            change_full_name_panel.Visible = false;


            profil_info_list = database.Get_teacherForm_profile_info(user_id)?.Split(' ').ToList();
            TextBox[] textBoxes = { surname_teacher_profile_textBox, name_teacher_profile_textBox,
                patronimic_teacher_profile_textBox, login_teacher_profile, password_teacher_profile };
            string[] text = { profil_info_list[0], profil_info_list[1], profil_info_list[2], profil_info_list[3],
                profil_info_list[4] };
            textbox.Fill_textboxes(textBoxes, text);

        }
        private void TeacherForm_Load(object sender, EventArgs e)
        {
        }

        private void Change_teacher_login_button_Click(object sender, EventArgs e)
        {
            change_teacher_login_panel.Visible = true;
            teacher_profile_main_panel.Visible = false;
        }

        private void New_teacher_login_button_Click(object sender, EventArgs e)
        {
            if (new_teacher_login_textbox.Text != "")
            {
                database.Update_user_login_or_password(new_teacher_login_textbox, user_id, "login", "teacher");
                FillTeacherProfileMainPanel();
                MessageBox.Show("Логин успешно изменен!");
                new_teacher_login_textbox.Clear();
            }
            else
            {
                MessageBox.Show("Заполните обязательные поля!");
            }
        }

        private void Cancel_change_teacher_login_button_Click(object sender, EventArgs e)
        {
            new_teacher_login_textbox.Clear();
            change_teacher_login_panel.Visible = false;
            teacher_profile_main_panel.Visible = true;
        }

        private void Change_teacher_password_button_Click(object sender, EventArgs e)
        {
            teacher_identity_check_panel.Visible = true;
            teacher_profile_main_panel.Visible = false;
        }

        private void New_teacher_password_button_Click(object sender, EventArgs e)
        {
            if (teacher_test_new_password_textBox.Text == "" || teacher_new_password_textBox.Text == "")
            {
                MessageBox.Show("Заполните обязательные поля!");
            }
            else if (textbox.Check_passwords_are_matching(teacher_test_new_password_textBox.Text, teacher_new_password_textBox.Text))
            {
                database.Update_user_login_or_password(teacher_test_new_password_textBox, user_id, "password", "teacher");
                FillTeacherProfileMainPanel();
                MessageBox.Show("Пароль успешно изменен!");
                teacher_test_new_password_textBox.Clear();
                teacher_new_password_textBox.Clear();
            }
        }

        private void Cancel_change_teacher_password_button_Click(object sender, EventArgs e)
        {
            teacher_test_new_password_textBox.Clear();
            teacher_new_password_textBox.Clear();
            change_teacher_password_panel.Visible = false;
            teacher_profile_main_panel.Visible = true;
        }

        private void Continue_teacher_change_password_button_Click(object sender, EventArgs e)
        {
            if (teacher_old_password_textBox.Text == "")
            {
                MessageBox.Show("Заполните обязательные поля!");
            }
            else if (textbox.Check_passwords_are_matching(teacher_old_password_textBox.Text, profil_info_list[4]))
            {
                teacher_old_password_textBox.Clear();
                teacher_identity_check_panel.Visible = false;
                change_teacher_password_panel.Visible = true;
            }
        }

        private void Cancel_teacher_change_password_button_Click(object sender, EventArgs e)
        {
            teacher_old_password_textBox.Clear();
            teacher_identity_check_panel.Visible = false;
            teacher_profile_main_panel.Visible = true;
        }

        private void New_timer_textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!char.IsDigit(number) && number != 8) // цифры и клавиша BackSpace
            {
                e.Handled = true;
            }
        }

        private void Create_new_test_buttton_Click(object sender, EventArgs e)
        {
        }

        private void New_test_name_textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void New_timer_textBox_TextChanged(object sender, EventArgs e)
        {
        }

        bool NewTestNameIsChanged = false;
        private void New_test_name_textBox_TextChanged(object sender, EventArgs e)
        {
            NewTestNameIsChanged = textbox.Check_text_is_changed(new_test_name_textBox, "Введите название теста");
        }

        private void New_test_name_textBox_Click(object sender, EventArgs e)
        {
            NewTestNameIsChanged = textbox.Check_is_cleared(new_test_name_textBox, "Введите название теста", NewTestNameIsChanged);
        }

        private void New_test_name_textBox_Leave(object sender, EventArgs e)
        {
            textbox.Return_original_text(new_test_name_textBox, "Введите название теста");
        }

        private void Full_name_teacher_profile_texBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void Add_new_test_to_db_Click(object sender, EventArgs e)
        {
            //create_version_questions_groupBox
            //comment_textBox
            if (textbox.Check_text_is_changed(new_test_name_textBox, "Введите название теста") && textbox.Check_text_is_changed(new_timer_textBox, "0"))
            {
                //проверка что такого теста нет
                if (database.Check_test_exist(new_test_name_textBox.Text, user_id))
                {
                    MessageBox.Show("Тест с таким именем уже существует");
                }
                else
                {
                    test_id = database.Add_new_test_to_database(new_test_name_textBox, new_timer_textBox, comment_textBox, user_id);

                    create_test_groupBox.Visible = false;
                    create_version_questions_groupBox.Visible = true;
                    ++version_counter;
                    version_id = database.Create_new_test_version(test_id, version_counter);
                }
            }
            else
            {
                MessageBox.Show("Заполните обязательные поля!");
            }

        }
        private bool Check_right_answers_number(CheckBox[] checkBoxes, int type)
        {
            int counter = 0;

            if (type == -1)
                return false;
            for (int i = 0; i < checkBoxes.Length; ++i)
            {
                if (checkBoxes[i].Checked == true)
                    ++counter;
            }
            if (counter == 0 || type == 0 && counter != 1 || type == 1 && counter < 2)
            {
                MessageBox.Show("Тип вопроса не совпадает с количеством выбранных правильных ответов");
            }
            else return true;

            return false;
        }


        private void Add_next_question_button_Click(object sender, EventArgs e)
        {
            string[] text = { "Введите текст вопроса", "Введите ответ №1", "Введите ответ №2", "Введите ответ №3", "Введите ответ №4" };
            TextBox[] textBoxes = { question_textBox, answer_textBox1, answer_textBox2, answer_textBox3, answer_textBox4 };
            CheckBox[] checkBoxes = { checkBox1, checkBox2, checkBox3, checkBox4 };
            if (new_method_radioButton.Checked == true)
            {
                if (Check_question_panel_is_filled(textBoxes, text) && Check_right_answers_number(checkBoxes, question_type_comboBox.SelectedIndex))
                {
                    Save_test_question(textBoxes, checkBoxes, text);
                    ++question_counter;
                    create_new_question_panel.Visible = false;
                }
            }
            else if (select_existing_method_radioButton.Checked == true)
            {
                if (Check_select_existing_question_panel_filled())
                {
                    database.Create_version_question_connection(Convert.ToInt32(id_textBox.Text), version_id);
                    ++question_counter;
                    select_existing_question_panel.Visible = false;
                }
            }
            else MessageBox.Show("Выберите метод создания тестового задания");
            radiobutton.Clear_selection(new_method_radioButton);
            radiobutton.Clear_selection(select_existing_method_radioButton);
        }

        private void Save_test_question(TextBox[] textBoxes, CheckBox[] checkBoxes, string[] text)
        {
            int answer_id;
            int question_id = database.Create_question_text(question_type_comboBox, question_textBox.Text);
            database.Create_version_question_connection(question_id, version_id);
            for (int i = 1; i < textBoxes.Length; ++i)
            {
                answer_id = database.Create_answer_text(textBoxes[i].Text);
                database.Create_question_answer_connection(question_id, answer_id, checkBoxes[i - 1].Checked);
            }
        }
        /*private void Update_new_question_form(TextBox[] textboxes, string[] text, CheckBox[] checkBoxes, ComboBox comboBox)
        {
            for (int i = 0; i < checkBoxes.Length; ++i)
            {
                checkbox.Clear(checkBoxes[i]);
            }
            combobox.Clear_selection(comboBox);
            combobox.Return_original_text(question_type_comboBox, "Выберите тип вопроса");
            textbox.Fill_textboxes(textboxes, text);
            create_version_questions_groupBox.Text = "Вопрос " + question_counter;
            radiobutton.Clear_selection(new_method_radioButton);
            radiobutton.Clear_selection(select_existing_method_radioButton);
        }*/

        private void TabControl1_Click(object sender, EventArgs e)
        {


        }

        private void Create_test_MenuItem_Click(object sender, EventArgs e)
        {
            //contextMenuStrip1.Visible = false;
            //create_test_groupBox.Visible = true;
        }

        private bool Check_question_panel_is_filled(TextBox[] textBoxes, string[] text)
        {
            if (!combobox.Check_is_changed(question_type_comboBox) ||
                !textbox.Check_textboxes_text_are_changed(text, textBoxes))
            {
                MessageBox.Show("Заполните обязательные поля!");
                return false;
            }
            return true;
        }

        private void Create_next_version_Click(object sender, EventArgs e)
        {
            string[] text = { "Введите текст вопроса", "Введите ответ №1", "Введите ответ №2", "Введите ответ №3", "Введите ответ №4" };
            TextBox[] textBoxes = { question_textBox, answer_textBox1, answer_textBox2, answer_textBox3, answer_textBox4 };
            CheckBox[] checkBoxes = { checkBox1, checkBox2, checkBox3, checkBox4 };
            if (new_method_radioButton.Checked == true)
            {
                if (Check_question_panel_is_filled(textBoxes, text) && Check_right_answers_number(checkBoxes, question_type_comboBox.SelectedIndex))
                {
                    Save_test_question(textBoxes, checkBoxes, text);
                    ++version_counter;
                    question_counter = 1;
                    version_id = database.Create_new_test_version(test_id, version_counter);
                    //Update_new_question_form(textBoxes, text, checkBoxes, question_type_comboBox);
                    create_new_question_panel.Visible = false;
                }
            }
            else if (select_existing_method_radioButton.Checked == true)
            {
                if (Check_select_existing_question_panel_filled())
                {
                    database.Create_version_question_connection(Convert.ToInt32(id_textBox.Text), version_id);
                    ++version_counter;
                    question_counter = 1;
                    version_id = database.Create_new_test_version(test_id, version_counter);
                    select_existing_question_panel.Visible = false;//Update_Select_existing_question_panel();
                }
            }
            else MessageBox.Show("Выберите метод создания тестового задания");
            radiobutton.Clear_selection(new_method_radioButton);
            radiobutton.Clear_selection(select_existing_method_radioButton);
        }

        private void TeacherForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void Exit_student_profile_button_Click(object sender, EventArgs e)
        {
            Application.OpenForms[0].Show();
            this.Hide();
        }

        private void Finish_creating_test_Click(object sender, EventArgs e)
        {
            string[] text = { "Введите текст вопроса", "Введите ответ №1", "Введите ответ №2", "Введите ответ №3", "Введите ответ №4" };
            TextBox[] textBoxes = { question_textBox, answer_textBox1, answer_textBox2, answer_textBox3, answer_textBox4 };
            CheckBox[] checkBoxes = { checkBox1, checkBox2, checkBox3, checkBox4 };
            if (new_method_radioButton.Checked == true && Check_question_panel_is_filled(textBoxes, text) && Check_right_answers_number(checkBoxes, question_type_comboBox.SelectedIndex))
            {
                Save_test_question(textBoxes, checkBoxes, text);
                //test_id = 0;//
                //version_counter = 0;//
                //question_counter = 1;//
                //Update_new_question_form(textBoxes, text, checkBoxes, question_type_comboBox);
                create_new_question_panel.Visible = false;
                create_version_questions_groupBox.Visible = false;
                //
                //contextMenuStrip1.Show(tabControl1, new Point(66, 28));
                data.Reset_teacher_test_table(database, user_id, teacher_available_tests_table); //
                data.Reset_teacher_task_table(database, user_id, available_teacher_tasks_table);
            }

            else if (select_existing_method_radioButton.Checked == true && Check_select_existing_question_panel_filled())
            {
                database.Create_version_question_connection(Convert.ToInt32(id_textBox.Text), version_id);
                //test_id = 0;//
                //version_counter = 0;//
                //question_counter = 1;//
                select_existing_question_panel.Visible = false; //Update_Select_existing_question_panel();
                create_version_questions_groupBox.Visible = false;

                //contextMenuStrip1.Show(tabControl1, new Point(66, 28));
                data.Reset_teacher_test_table(database, user_id, teacher_available_tests_table);//
                data.Reset_teacher_task_table(database, user_id, available_teacher_tasks_table);
            }
            //create_version_questions_groupBox.Visible = false; !mode
            //create_new_question_panel.Visible = false;
            //select_existing_question_panel.Visible = false;
            //data.Reset_teacher_test_table(database, user_id, teacher_available_tests_table);
            //test_id = 0;
            //version_counter = 0;
            //question_counter = 1;

        }

        private void Edit_test_MenuItem_Click(object sender, EventArgs e)
        {

        }

        private void test_constructor_Click(object sender, EventArgs e)
        {

        }

        private void Question_type_comboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void ComboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        bool questionTextIsChanged = false;
        private void Question_textBox_Click(object sender, EventArgs e)
        {
            questionTextIsChanged = textbox.Check_is_cleared(question_textBox, "Введите текст вопроса", questionTextIsChanged);
        }

        private void Question_textBox_TextChanged(object sender, EventArgs e)
        {
            questionTextIsChanged = textbox.Check_text_is_changed(question_textBox, "Введите текст вопроса");
        }

        private void Question_textBox_Leave(object sender, EventArgs e)
        {
            textbox.Return_original_text(question_textBox, "Введите текст вопроса");
        }

        bool answer1TextIsChanged = false;
        private void Answer_textBox1_Click(object sender, EventArgs e)
        {
            answer1TextIsChanged = textbox.Check_is_cleared(answer_textBox1, "Введите ответ №1", answer1TextIsChanged);
        }

        private void Answer_textBox1_TextChanged(object sender, EventArgs e)
        {
            answer1TextIsChanged = textbox.Check_text_is_changed(answer_textBox1, "Введите ответ №1");
        }

        private void Answer_textBox1_Leave(object sender, EventArgs e)
        {
            textbox.Return_original_text(answer_textBox1, "Введите ответ №1");
        }

        bool answer3TextIsChanged = false;
        private void Answer_textBox3_Click(object sender, EventArgs e)
        {
            answer3TextIsChanged = textbox.Check_is_cleared(answer_textBox3, "Введите ответ №3", answer3TextIsChanged);
        }

        private void Answer_textBox3_TextChanged(object sender, EventArgs e)
        {
            answer3TextIsChanged = textbox.Check_text_is_changed(answer_textBox3, "Введите ответ №3");
        }

        private void Answer_textBox3_Leave(object sender, EventArgs e)
        {
            textbox.Return_original_text(answer_textBox3, "Введите ответ №3");
        }

        bool answer2TextIsChanged = false;
        private void Answer_textBox2_Click(object sender, EventArgs e)
        {
            answer2TextIsChanged = textbox.Check_is_cleared(answer_textBox2, "Введите ответ №2", answer2TextIsChanged);
        }

        private void Answer_textBox2_TextChanged(object sender, EventArgs e)
        {
            answer2TextIsChanged = textbox.Check_text_is_changed(answer_textBox2, "Введите ответ №2");
        }

        private void Answer_textBox2_Leave(object sender, EventArgs e)
        {
            textbox.Return_original_text(answer_textBox2, "Введите ответ №2");
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {

        }

        bool answer4TextIsChanged = false;
        private void Answer_textBox4_Click(object sender, EventArgs e)
        {
            answer4TextIsChanged = textbox.Check_is_cleared(answer_textBox4, "Введите ответ №4", answer4TextIsChanged);
        }

        private void Answer_textBox4_TextChanged(object sender, EventArgs e)
        {
            answer4TextIsChanged = textbox.Check_text_is_changed(answer_textBox4, "Введите ответ №4");
        }

        private void Answer_textBox4_Leave(object sender, EventArgs e)
        {
            textbox.Return_original_text(answer_textBox4, "Введите ответ №4");
        }

        private void Cancel_add_new_test_Click(object sender, EventArgs e)
        {
            //create_test_groupBox.Visible = false;
            //new_test_name_textBox.Clear();
            ///new_timer_textBox.Clear();
            //textbox.Return_original_text(new_test_name_textBox, "Введите название теста");

        }

        private void question_type_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void New_method_radioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (new_method_radioButton.Checked)
            {
                create_new_question_panel.Visible = true;
            }
            else
            {
                create_new_question_panel.Visible = false;
            }
        }

        private void Select_existing_method_radioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (select_existing_method_radioButton.Checked)
            {
                select_existing_question_panel.Visible = true;
            }
            else
            {
                select_existing_question_panel.Visible = false;
            }


        }
        /*private void Update_Select_existing_question_panel()
        {
            TextBox[] textBoxes = { existing_question_type_textBox, id_textBox, existing_answer_textBox1, existing_answer_textBox2, existing_answer_textBox3, existing_answer_textBox4 };
            CheckBox[] checkBoxes = { checkBox5, checkBox6, checkBox7, checkBox8 };
            textbox.Clear_textboxes(textBoxes);
            checkbox.Clear_checkboxes(checkBoxes);
            combobox.Clear_selection(select_existing_question_comboBox);
            combobox.Return_original_text(select_existing_question_comboBox, "Выберите вопрос");
            //combobox.Delete_collection(select_existing_question_comboBox);
        }*/
        private bool Check_select_existing_question_panel_filled()
        {
            TextBox[] textBoxes = { existing_question_type_textBox, existing_answer_textBox1, existing_answer_textBox2, existing_answer_textBox3, existing_answer_textBox4 };
            CheckBox[] checkBoxes = { checkBox5, checkBox6, checkBox7, checkBox8 };
            if (select_existing_question_comboBox.SelectedIndex == -1 ||
                !textbox.Check_textboxes_text_are_changed(new string[] { "", "", "", "", "" }, textBoxes))
            {
                MessageBox.Show("Заполните обязательные поля!");
                return false;
            }
            int counter = 0;
            for (int i = 0; i < checkBoxes.Length; ++i)
            {
                if (checkBoxes[i].Checked == true)
                    ++counter;
            }
            if (existing_question_type_textBox.Text == "Вопрос с одним правильным вариантом ответа" && counter != 1 ||
                existing_question_type_textBox.Text == "Вопрос с несколькими правильными вариантами ответа" && counter < 2)
                return false;
            return true;
        }
        private void Select_existing_question_comboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string text = select_existing_question_comboBox.SelectedItem.ToString();
            int id = database.Get_teacher_question_version_id(user_id, text);
            id_textBox.Text = database.Get_question_id(id, text).ToString();
            List<string> existing_answers = new List<string>();
            List<string> existing_right_answers = new List<string>();
            TextBox[] textBoxes = { existing_answer_textBox1, existing_answer_textBox2, existing_answer_textBox3, existing_answer_textBox4 };
            CheckBox[] checkBoxes = { checkBox5, checkBox6, checkBox7, checkBox8 };
            int type = database.Get_question_type(id, text);
            if (type == 1)
                existing_question_type_textBox.Text = "Вопрос с одним правильным вариантом ответа";
            else
                existing_question_type_textBox.Text = "Вопрос с несколькими правильными вариантами ответа";
            existing_answers = database.Get_answers_text(id, text);
            for (int i = 0; i < textBoxes.Length; ++i)
            {
                textBoxes[i].Text = existing_answers[i];
            }
            existing_right_answers = database.Get_right_answers(id, text);
            int counter = 0;
            for (int i = 0; i < textBoxes.Length; ++i)
            {
                checkBoxes[i].Checked = false;
                if (i - counter >= existing_right_answers.Count)
                    break;
                if (textBoxes[i].Text == existing_right_answers[i - counter])
                    checkBoxes[i].Checked = true;
                else ++counter;
            }
            existing_answers.Clear();
            versions_id.Clear();
            existing_right_answers.Clear();
        }

        private void edit_test_groupBox_Enter(object sender, EventArgs e)
        {

        }

        private void просмотрТестовToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void нToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tabControl2_Click(object sender, EventArgs e)
        {
            /*if (tabControl2.SelectedTab.Name == "edit_tests_page")
             {
                 TabControl tabcon = (TabControl)sender;
                 contextMenuStrip1.Show(tabcon, new Point(120, 28));
                 database.Fill_teacher_collection_available_tests(user_id, available_teacher_tests_comboBox);
             }
             else if (tabControl2.SelectedTab.Name == "create_tests_page")
             {
                 create_test_groupBox.Visible = true;
             }*/
        }

        private void create_tests_page_Click(object sender, EventArgs e)
        {

        }

        private void create_test_groupBox_VisibleChanged(object sender, EventArgs e)
        {
            if (create_test_groupBox.Visible == false)
            {
                new_test_name_textBox.Clear();
                comment_textBox.Clear();
                textbox.Return_original_text(new_test_name_textBox, "Введите название теста");
                textbox.Return_original_text(comment_textBox, "Комментарий к тесту (необязательно)");
                new_timer_textBox.Clear();
            }
        }

        private void Confirm_change_button_Click(object sender, EventArgs e)
        {
            if (combobox.Check_is_changed(available_teacher_tests_comboBox) || edit_method == 6)
            {
                string test_name = "";
                if (combobox.Check_is_changed(available_teacher_tests_comboBox))
                { test_name = available_teacher_tests_comboBox.SelectedItem.ToString(); }
                DialogResult dialogResult;
                switch (edit_method)
                {
                    case 0:
                        MessageBox.Show("Выберите раздел редактирования");
                        break;
                    case 1:
                        edit_mode = true;
                        test_id = database.Get_test_id(user_id, test_name);
                        question_counter = 1;
                        version_counter = database.Get_last_version_number(user_id, test_name) + 1;
                        version_id = database.Create_new_test_version(test_id, version_counter);
                        tabControl2.SelectedTab = create_tests_page;
                        create_version_questions_groupBox.Visible = true;
                        break;
                    case 2:
                        if (combobox.Check_is_changed(choose_deleted_version_comboBox)) //проверить что панель заполнена
                        {
                            edit_mode = true;
                            test_id = database.Get_test_id(user_id, test_name);
                            version_id = database.Get_version_id_by_number(user_id, test_id,
                                Convert.ToInt32(choose_deleted_version_comboBox.SelectedItem));
                            question_counter = database.Get_last_question_number(user_id, test_id, version_id) + 1;

                            add_task_or_delete_version_panel.Visible = false;
                            tabControl2.SelectedTab = create_tests_page;
                            create_version_questions_groupBox.Visible = true;
                        }
                        else MessageBox.Show("Заполните обязательные поля!");
                        break;
                    case 3:
                        if (textbox.Check_text_is_changed(new_name_textBox, "Введите новое название"))
                        {
                            if (database.Check_test_exist(new_name_textBox.Text, user_id))
                            {
                                MessageBox.Show("Тест с таким именем уже существует");
                            }
                            else
                            {
                                database.Update_test_name(user_id, test_name, new_name_textBox.Text);
                                combobox.Delete_collection(available_teacher_tests_comboBox);
                                database.Fill_teacher_collection_available_tests(user_id, available_teacher_tests_comboBox);
                                new_name_panel.Visible = false;

                            }
                        }
                        else MessageBox.Show("Заполните обязательные поля!");
                        break;
                    case 4:
                        if (textbox.Check_text_is_changed(edit_timer_textBox, ""))
                        {
                            database.Update_test_timer(user_id, Convert.ToInt32(edit_timer_textBox.Text), test_name);
                            new_timer_panel.Visible = false;
                        }
                        else MessageBox.Show("Заполните обязательные поля!");
                        break;
                    case 5:
                        if (textbox.Check_text_is_changed(new_comment_textBox, "Введите комментарий к тесту"))
                        {
                            database.Update_test_comment(user_id, test_name, new_comment_textBox.Text);
                            new_comment_panel.Visible = false;
                        }
                        else MessageBox.Show("Заполните обязательные поля!");
                        break;
                    case 6:
                        if (combobox.Check_is_changed(choose_task_comboBox))
                        {
                            CheckBox[] checkBoxes = { checkBox9, checkBox10, checkBox11, checkBox12 };
                            if (Check_right_answers_number(checkBoxes, edit_question_type_comboBox.SelectedIndex))
                            {
                                Update_task();
                                edit_task_panel.Visible = false;
                                available_teacher_tests_comboBox.Visible = true;
                            }
                        }
                        else MessageBox.Show("Заполните обязательные поля!");
                        break;
                    case 7:
                        if (combobox.Check_is_changed(delete_mode_choose_version_comboBox) && combobox.Check_is_changed(delete_task_comboBox))
                        {
                            dialogResult = MessageBox.Show("Вы уверены, что хотите удалить выбранное тестовое задание? ", "Предупреждение",
                                MessageBoxButtons.YesNo);
                            if (dialogResult == DialogResult.Yes)
                            {
                                database.Delete_task_from_test_version(user_id,
                                    Convert.ToInt32(delete_mode_choose_version_comboBox.SelectedItem),
                                    test_name, delete_task_comboBox.SelectedItem.ToString());
                                delete_task_panel.Visible = false;
                            }
                            else if (dialogResult == DialogResult.No)
                            {
                            }
                        }
                        else MessageBox.Show("Заполните обязательные поля!");
                        break;
                    case 8:
                        if (combobox.Check_is_changed(choose_deleted_version_comboBox))
                        {
                            dialogResult = MessageBox.Show("Вы уверены, что хотите безвозвратно удалить выбранный вариант?", "Предупреждение",
                                MessageBoxButtons.YesNo);
                            if (dialogResult == DialogResult.Yes)
                            {
                                database.Delete_version(user_id,
                                    Convert.ToInt32(choose_deleted_version_comboBox.SelectedItem), test_name);
                                add_task_or_delete_version_panel.Visible = false;

                            }
                            else if (dialogResult == DialogResult.No)
                            {
                            }
                        }
                        else MessageBox.Show("Заполните обязательные поля!");
                        break;
                    case 9:
                        dialogResult = MessageBox.Show("Вы уверены, что хотите безвозвратно удалить выбранный тест?", "Предупреждение",
                                MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes)
                        {
                            database.Delete_test(user_id, test_name);
                            combobox.Delete_collection(available_teacher_tests_comboBox);
                            database.Fill_teacher_collection_available_tests(user_id, available_teacher_tests_comboBox);
                        }
                        else if (dialogResult == DialogResult.No)
                        {
                        }
                        break;
                }
                data.Reset_teacher_test_table(database, user_id, teacher_available_tests_table);
                data.Reset_teacher_task_table(database, user_id, available_teacher_tasks_table);
                combobox.Return_original_text(available_teacher_tests_comboBox, "Выберите тест");
                contextMenuStrip1.Show();
            }
            else MessageBox.Show("Заполните обязательные поля!");
        }
        private void Create_version_questions_groupBox_VisibleChanged(object sender, EventArgs e)
        {
            if (create_version_questions_groupBox.Visible == true)
            {
                create_version_questions_groupBox.Text = "Вопрос " + question_counter;
                if (edit_mode)
                {
                    finish_creating_test_button.Visible = false;
                    create_next_version.Visible = false;
                    finish_editing_button.Visible = true;
                    if (edit_method == 2)
                    {
                        add_next_question_button.Visible = false;
                    }
                }
            }
            else
            {
                radiobutton.Clear_selection(new_method_radioButton);
                radiobutton.Clear_selection(select_existing_method_radioButton);
                test_id = 0;
                version_counter = 0;
                question_counter = 1;
                //new_method_radioButton
                //select_existing_method_radioButton
                if (edit_mode)
                {
                    finish_creating_test_button.Visible = true;
                    create_next_version.Visible = true;
                    finish_editing_button.Visible = false;
                    tabControl2.SelectedIndex = 2;
                    //tabControl2.SelectedTab = edit_tests_page;
                    //contextMenuStrip1.Show();// (tabControl2, new Point(120, 28));
                }
            }

        }
        private void Update_task()
        {
            //изменить имя и тип вопроса
            database.Update_question_name(Convert.ToInt32(edit_question_id_textBox.Text), edit_question_type_comboBox.SelectedIndex + 1, edit_test_name_textBox.Text);
            //изменить в цикле имена ответов
            //изменить праивльность ответов
            TextBox[] textBoxes = { textBox1, textBox2, textBox3, textBox4 };
            TextBox[] hide_textBoxes = { edit_answer_id_textBox1, edit_answer_id_textBox2, edit_answer_id_textBox3, edit_answer_id_textBox4 };
            CheckBox[] checkBoxes = { checkBox9, checkBox10, checkBox11, checkBox12 };
            int is_right;
            for (int i = 0; i < textBoxes.Length; ++i)
            {
                is_right = 0;
                if (checkBoxes[i].Checked == true)
                    is_right = 1;
                database.Update_answer_name(Convert.ToInt32(hide_textBoxes[i].Text), textBoxes[i].Text);
                database.Update_answer_correctness(Convert.ToInt32(hide_textBoxes[i].Text), Convert.ToInt32(edit_question_id_textBox.Text), is_right);
            }
        }
        private void Fill_edit_task_panel2(string question, int version, List<string> edit_existing_answers,
            List<int> edit_answers_id, List<string> edit_existing_right_answers)
        {
            edit_test_name_textBox.Text = question;
            edit_question_type_comboBox.SelectedIndex = database.Get_question_type(version, question) - 1;
            edit_question_id_textBox.Text = database.Get_question_id(version, question).ToString();

            TextBox[] textBoxes = { textBox1, textBox2, textBox3, textBox4 };
            TextBox[] hide_textBoxes = { edit_answer_id_textBox1, edit_answer_id_textBox2, edit_answer_id_textBox3, edit_answer_id_textBox4 };
            CheckBox[] checkBoxes = { checkBox9, checkBox10, checkBox11, checkBox12 };
            int counter = 0;

            for (int i = 0; i < textBoxes.Length; ++i)
            {
                textBoxes[i].Text = edit_existing_answers[i];
                hide_textBoxes[i].Text = edit_answers_id[i].ToString();
                if (i - counter >= edit_existing_right_answers.Count)
                    continue;
                if (textBoxes[i].Text == edit_existing_right_answers[i - counter])
                    checkBoxes[i].Checked = true;
                else ++counter;
            }
        }

        private void cancel_edit_button_Click(object sender, EventArgs e)
        {
            contextMenuStrip1.Visible = true;
            edit_test_groupBox.Visible = true;
            //закрыть все панели
            //панель создания варианта
            //панель создания вопроса
            delete_task_panel.Visible = false;
            new_name_panel.Visible = false;
            new_timer_panel.Visible = false;
            new_comment_panel.Visible = false;
            edit_task_panel.Visible = false;
            add_task_or_delete_version_panel.Visible = false;
            edit_method = 0; //обнулить переменную типа редакции
        }

        private void add_version_ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            contextMenuStrip1.Visible = false;
            edit_method = 1;
            add_task_or_delete_version_panel.Visible = false;
            new_name_panel.Visible = false;
            new_timer_panel.Visible = false;
            new_comment_panel.Visible = false;
            edit_task_panel.Visible = false;
            delete_task_panel.Visible = false;
            add_task_or_delete_version_panel.Visible = false;

            //открыть панель создания варианта(а лучше открыть вкладку создания и использовать ее,
            //т к алгоритм тот же)
        }

        private void Add_task_ToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            contextMenuStrip1.Visible = false;
            edit_method = 2;
            //add_task_or_delete_version_panel.Visible = true;
            new_name_panel.Visible = false;
            new_timer_panel.Visible = false;
            new_comment_panel.Visible = false;
            edit_task_panel.Visible = false;
            delete_task_panel.Visible = false;
            //**************не сделано**********************
            //открыть панель создания вопроса(в идеале открыть вкладку создания и использовать ее,
            //но нужны другая кнопка завершения)
        }

        private void Edit_name_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            contextMenuStrip1.Visible = false;
            edit_method = 3;
            new_name_panel.Visible = true;
            add_task_or_delete_version_panel.Visible = false;
            new_timer_panel.Visible = false;
            new_comment_panel.Visible = false;
            edit_task_panel.Visible = false;
            delete_task_panel.Visible = false;
        }

        private void edit_timer_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            contextMenuStrip1.Visible = false;
            edit_method = 4;
            new_timer_panel.Visible = true;
            add_task_or_delete_version_panel.Visible = false;
            new_name_panel.Visible = false;
            new_comment_panel.Visible = false;
            edit_task_panel.Visible = false;
            delete_task_panel.Visible = false;
        }

        private void edit_comment_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            contextMenuStrip1.Visible = false;
            edit_method = 5;
            new_comment_panel.Visible = true;
            new_timer_panel.Visible = false;
            add_task_or_delete_version_panel.Visible = false;
            new_name_panel.Visible = false;
            edit_task_panel.Visible = false;
            delete_task_panel.Visible = false;

        }

        private void edit_task_ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            contextMenuStrip1.Visible = false;
            available_teacher_tests_comboBox.Visible = false;
            edit_method = 6;
            edit_task_panel.Visible = true;
            new_comment_panel.Visible = false;
            new_timer_panel.Visible = false;
            add_task_or_delete_version_panel.Visible = false;
            new_name_panel.Visible = false;
            delete_task_panel.Visible = false;

        }

        private void delete_task_ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            contextMenuStrip1.Visible = false;
            edit_method = 7;
            delete_task_panel.Visible = true;

            edit_task_panel.Visible = false;
            new_comment_panel.Visible = false;
            new_timer_panel.Visible = false;
            add_task_or_delete_version_panel.Visible = false;
            new_name_panel.Visible = false;

        }

        private void delete_version_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            contextMenuStrip1.Visible = false;
            edit_method = 8;
            //add_task_or_delete_version_panel.Visible = true;
            delete_task_panel.Visible = false;
            edit_task_panel.Visible = false;
            new_comment_panel.Visible = false;
            new_timer_panel.Visible = false;
            new_name_panel.Visible = false;
        }

        private void delete_test_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            contextMenuStrip1.Visible = false;
            edit_method = 9;
            add_task_or_delete_version_panel.Visible = false;
            delete_task_panel.Visible = false;
            edit_task_panel.Visible = false;
            new_comment_panel.Visible = false;
            new_timer_panel.Visible = false;
            new_name_panel.Visible = false;
        }
        //************добавить очистку новых панелей*************************************************************************************
        /* private void Clear_edit_panels()
         {
             ComboBox[] comboBoxes = { choose_deleted_version_comboBox, choose_task_comboBox, available_teacher_tests_comboBox };
             string[] comboboxes_text = { "Выберите вариант", "Выберите задание", "Выберите тест для редактирования" };
             TextBox[] textboxes = { edit_timer_textBox, new_name_textBox, new_comment_textBox };
             string[] textboxes_text = { "", "Введите новое название", "Введите комментарий к тесту" };
             for (int i = 0; i < comboBoxes.Length; ++i)
             {
                 //combobox.Clear_selection(comboBoxes[i]);
                 combobox.Return_original_text(comboBoxes[i], comboboxes_text[i]);
                 if (i != 3)
                 {
                     combobox.Delete_collection(comboBoxes[i]);
                 }
             }
             textbox.Clear_textboxes(textboxes);
             for (int i = 0; i < textboxes.Length; ++i)
             {
                 textbox.Return_original_text(textboxes[i], textboxes_text[i]);
             }
         }*/
        //********************************************************************************

        private void edit_timer_textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!char.IsDigit(number) && number != 8) // цифры и клавиша BackSpace
            {
                e.Handled = true;
            }
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void choose_task_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (choose_task_comboBox.SelectedIndex != -1)
            {
                edit_task_panel2.Visible = true;
                choose_task_comboBox.Visible = false;
            }
        }


        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {
        }

        private void finish_editing_button_Click(object sender, EventArgs e)
        {
            string[] text = { "Введите текст вопроса", "Введите ответ №1", "Введите ответ №2", "Введите ответ №3", "Введите ответ №4" };
            TextBox[] textBoxes = { question_textBox, answer_textBox1, answer_textBox2, answer_textBox3, answer_textBox4 };
            CheckBox[] checkBoxes = { checkBox1, checkBox2, checkBox3, checkBox4 };

            if (new_method_radioButton.Checked == true)
            {
                if (Check_question_panel_is_filled(textBoxes, text) && Check_right_answers_number(checkBoxes, question_type_comboBox.SelectedIndex))
                {
                    Save_test_question(textBoxes, checkBoxes, text);
                    //test_id = 0;
                    //version_counter = 0;
                    //question_counter = 1;
                    create_new_question_panel.Visible = false;
                    //Update_new_question_form(textBoxes, text, checkBoxes, question_type_comboBox);
                    create_version_questions_groupBox.Visible = false;
                    //finish_creating_test_button.Visible = true;
                    //create_next_version.Visible = true;
                    //finish_editing_button.Visible = false;
                    //
                    //tabControl2.SelectedTab = edit_tests_page;
                    edit_mode = false;

                    data.Reset_teacher_test_table(database, user_id, teacher_available_tests_table); //???????
                    data.Reset_teacher_task_table(database, user_id, available_teacher_tasks_table);

                }
            }
            else if (select_existing_method_radioButton.Checked == true)
            {
                if (Check_select_existing_question_panel_filled())
                {
                    database.Create_version_question_connection(Convert.ToInt32(id_textBox.Text), version_id);
                    //test_id = 0;
                    //version_counter = 0;
                    //question_counter = 1;
                    select_existing_question_panel.Visible = false;
                    create_version_questions_groupBox.Visible = false;
                    edit_mode = false;

                    //  //Update_Select_existing_question_panel();

                    // finish_creating_test_button.Visible = true;
                    //create_next_version.Visible = true;
                    //finish_editing_button.Visible = false;

                    //tabControl2.SelectedTab = edit_tests_page;
                    data.Reset_teacher_test_table(database, user_id, teacher_available_tests_table);//????                 }
                    data.Reset_teacher_task_table(database, user_id, available_teacher_tasks_table);

                }
            }

        }

        private void available_teacher_tests_comboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {

            if (edit_method == 2 || edit_method == 8)//возможно не нужно
            {
                //combobox.Delete_collection(choose_deleted_version_comboBox);
                add_task_or_delete_version_panel.Visible = true;
                database.Fill_version_number_comboBox(choose_deleted_version_comboBox, user_id,
                    available_teacher_tests_comboBox.SelectedItem.ToString());
            }
            if (edit_method == 7)
            {
                //combobox.Delete_collection(delete_mode_choose_version_comboBox);
                database.Fill_version_number_comboBox(delete_mode_choose_version_comboBox, user_id,
                   available_teacher_tests_comboBox.SelectedItem.ToString());
            }
        }

        private void delete_mode_choose_version_comboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            combobox.Delete_collection(delete_task_comboBox);
            if (edit_method == 7)//возможно не нужно
            {
                database.Fill_delete_task_comboBox(delete_task_comboBox, user_id,
                    available_teacher_tests_comboBox.SelectedItem.ToString(),
                    Convert.ToInt32(delete_mode_choose_version_comboBox.SelectedItem));
            }
        }

        private void delete_task_panel_VisibleChanged(object sender, EventArgs e)
        {
            if (delete_task_panel.Visible == false)
            {
                combobox.Delete_collection(delete_mode_choose_version_comboBox);
                combobox.Delete_collection(delete_task_comboBox);
                combobox.Return_original_text(delete_mode_choose_version_comboBox, "Выберите вариант");
                combobox.Return_original_text(delete_task_comboBox, "Выберите вопрос");
            }
        }

        private void delete_version_panel_VisibleChanged(object sender, EventArgs e)
        {
            if (add_task_or_delete_version_panel.Visible == false)
            {
                combobox.Delete_collection(choose_deleted_version_comboBox);
                combobox.Return_original_text(choose_deleted_version_comboBox, "Выберите вариант");
            }

        }

        private void new_name_panel_VisibleChanged(object sender, EventArgs e)
        {
            if (new_name_panel.Visible == false)
            {
                new_name_textBox.Clear();
                textbox.Return_original_text(new_name_textBox, "Введите новое название");
            }
        }

        private void new_timer_panel_VisibleChanged(object sender, EventArgs e)
        {
            if (new_timer_panel.Visible == false)
            {
                edit_timer_textBox.Clear();
            }
        }

        private void new_comment_panel_VisibleChanged(object sender, EventArgs e)
        {
            if (new_comment_panel.Visible == false)
            {
                new_comment_textBox.Clear();
                textbox.Return_original_text(new_comment_textBox, "Введите комментарий к тесту");
            }
        }

        private void edit_task_panel2_VisibleChanged(object sender, EventArgs e)
        {
            if (edit_task_panel2.Visible == true)
            {
                string question = choose_task_comboBox.SelectedItem.ToString();
                int version = database.Get_teacher_question_version_id(user_id, question);
                Fill_edit_task_panel2(question, version,
                    database.Get_answers_text(version, question),
                    database.Get_answers_id(version, question),
                    database.Get_right_answers(version, question));
            }

        }

        private void edit_task_panel_VisibleChanged(object sender, EventArgs e)
        {
            if (edit_task_panel.Visible == false)
            {
                combobox.Delete_collection(choose_task_comboBox);
                combobox.Return_original_text(choose_task_comboBox, "Выберите вопрос для редактирования");
                combobox.Clear_selection(edit_question_type_comboBox);
                textbox.Clear_textboxes(new TextBox[]
                { edit_test_name_textBox, textBox1, textBox2, textBox3, textBox4,edit_question_id_textBox,
                    edit_answer_id_textBox1,edit_answer_id_textBox2,edit_answer_id_textBox3,edit_answer_id_textBox4 });
                checkbox.Clear_checkboxes(new CheckBox[] { checkBox9, checkBox10, checkBox11, checkBox12 });
            }
            else
            {
                database.Fill_teacher_collection_available_tasks(user_id, choose_task_comboBox);
            }
        }

        private void create_new_question_panel_VisibleChanged(object sender, EventArgs e)
        {
            if (create_new_question_panel.Visible == false)
            {
                string[] text = { "Введите текст вопроса", "Введите ответ №1", "Введите ответ №2", "Введите ответ №3", "Введите ответ №4" };
                TextBox[] textboxes = { question_textBox, answer_textBox1, answer_textBox2, answer_textBox3, answer_textBox4 };
                CheckBox[] checkBoxes = { checkBox1, checkBox2, checkBox3, checkBox4 };
                for (int i = 0; i < checkBoxes.Length; ++i)
                {
                    checkbox.Clear(checkBoxes[i]);
                }
                combobox.Clear_selection(question_type_comboBox);
                combobox.Return_original_text(question_type_comboBox, "Выберите тип вопроса");
                textbox.Fill_textboxes(textboxes, text);
                create_version_questions_groupBox.Text = "Вопрос " + question_counter;
            }
        }

        private void Select_existing_question_panel_VisibleChanged(object sender, EventArgs e)
        {
            if (select_existing_question_panel.Visible == false)
            {
                TextBox[] textBoxes = { existing_question_type_textBox, id_textBox, existing_answer_textBox1, existing_answer_textBox2, existing_answer_textBox3, existing_answer_textBox4 };
                CheckBox[] checkBoxes = { checkBox5, checkBox6, checkBox7, checkBox8 };
                textbox.Clear_textboxes(textBoxes);
                checkbox.Clear_checkboxes(checkBoxes);
                combobox.Delete_collection(select_existing_question_comboBox);
                combobox.Return_original_text(select_existing_question_comboBox, "Выберите вопрос");
                create_version_questions_groupBox.Text = "Вопрос " + question_counter;
            }
            else
            {
                questions_text = database.Get_teacher_questions(user_id);
                for (int i = 0; i < questions_text.Count; ++i)
                {
                    combobox.Add_item(select_existing_question_comboBox, questions_text[i]);
                }
            }
        }

        private void tabControl2_Selecting(object sender, TabControlCancelEventArgs e)
        {

        }
        //TabControl tabcon ;
        private void tabControl2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl2.SelectedIndex == 2)
            {
                edit_test_groupBox.Visible = true;
                //TabControl tabcon = (TabControl)sender;
                //contextMenuStrip1.Show(tabcon, new Point(140, 28));
            }
            else contextmenustrip1_enabled = false;
            /* if (tabControl2.SelectedIndex == 1)
             {
                 create_test_groupBox.Visible = true;
             }*/
        }

        private void edit_test_groupBox_VisibleChanged(object sender, EventArgs e)
        {
            if (available_teacher_tests_comboBox.Visible == true)
            {
                contextmenustrip1_enabled = true;
                //TabControl tabcon = (TabControl)sender;
                GroupBox groupBox = (GroupBox)sender;
                contextMenuStrip1.Show(groupBox, new Point(135, 0));
                combobox.Delete_collection(available_teacher_tests_comboBox);
                database.Fill_teacher_collection_available_tests(user_id, available_teacher_tests_comboBox);
                combobox.Return_original_text(available_teacher_tests_comboBox, "Выберите тест");
            }
            else
            {
                contextmenustrip1_enabled = false;

            }
        }

        private void tabControl2_MouseDown(object sender, MouseEventArgs e)
        {
            /* if (e.Button == MouseButtons.Right && tabControl2.SelectedIndex != 2)
             {
                 contextMenuStrip1.Hide();
             }
             else if (e.Button == MouseButtons.Left && tabControl2.SelectedIndex == 2)
                 contextMenuStrip1.Show(tabcon, new Point(140, 28));*/
        }

        private void tests_page_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void tabControl2_Click_1(object sender, EventArgs e)
        {
            //tabcon = (TabControl)sender;
            if (tabControl2.SelectedIndex == 2)
            {
                edit_test_groupBox.Visible = true;
                //contextMenuStrip1.Show(tabcon, new Point(140, 28));
            }
            //else contextMenuStrip1.Hide();
            if (tabControl2.SelectedIndex == 1 && !edit_mode)
            {
                create_test_groupBox.Visible = true;
            }
        }

        private void TeacherForm_MouseDown(object sender, MouseEventArgs e)
        {
        }

        private void TeacherForm_Click(object sender, EventArgs e)
        {

        }

        private void TeacherForm_MouseClick(object sender, MouseEventArgs e)
        {


        }

        private void contextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (contextmenustrip1_enabled)
            {
                e.Cancel = false;
            }
            else e.Cancel = true;
        }

        bool new_name_textBox_is_changed = false;
        private void new_name_textBox_Click(object sender, EventArgs e)
        {
            new_name_textBox_is_changed = textbox.Check_is_cleared(new_name_textBox, "Введите новое название", new_name_textBox_is_changed);
        }

        private void new_name_textBox_TextChanged(object sender, EventArgs e)
        {
            new_name_textBox_is_changed = textbox.Check_text_is_changed(new_name_textBox, "Введите новое название");

        }

        private void new_name_textBox_Leave(object sender, EventArgs e)
        {
            textbox.Return_original_text(new_name_textBox, "Введите новое название");

        }
        bool new_comment_textBox_is_changed = false;

        private void new_comment_textBox_Click(object sender, EventArgs e)
        {
            new_comment_textBox_is_changed = textbox.Check_is_cleared(new_comment_textBox, "Введите комментарий", new_comment_textBox_is_changed);
        }

        private void new_comment_textBox_TextChanged(object sender, EventArgs e)
        {
            new_comment_textBox_is_changed = textbox.Check_text_is_changed(new_comment_textBox, "Введите комментарий");
        }

        private void new_comment_textBox_Leave(object sender, EventArgs e)
        {
            textbox.Return_original_text(new_comment_textBox, "Введите комментарий");
        }

        private void choose_deleted_version_comboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (edit_method != 2)
                e.Handled = true;
        }

        private void available_teacher_tests_comboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void choose_task_comboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void edit_question_type_comboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void select_existing_question_panel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tests_table_page_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void teacher_available_tests_table_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void редактироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void teacher_available_tests_table_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {


        }

        private void contextMenuStrip2_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
        }

        private void teacher_available_tests_table_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            test_id = Convert.ToInt32(teacher_available_tests_table.SelectedCells[0].Value);
            open_test_button.Enabled = true;
        }

        private void teacher_available_tests_table_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        private void teacher_one_test_table_CellClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void teacher_available_tests_table_SelectionChanged(object sender, EventArgs e)
        {

        }

        private void open_test_button_Click(object sender, EventArgs e)
        {
            if (test_id == -1)
            {
                MessageBox.Show("Выберите тест из списка");
            }
            else
            {
                teacher_available_tests_table.Visible = false;
                teacher_one_test_table.Visible = true;
                open_version_in_test_comboBox.Visible = true;
                open_test_button.Visible = false;
                return_to_available_tests_table_button.Visible = true;
                database.Fill_version_number_comboBox(open_version_in_test_comboBox, user_id, teacher_available_tests_table.SelectedCells[1].Value.ToString());
            }

        }

        private void open_version_in_test_comboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            version_id = database.Get_version_id_by_number(user_id, test_id, Convert.ToInt32(open_version_in_test_comboBox.SelectedItem));
            data.Reset_teacher_one_test_table(database, version_id, teacher_one_test_table);
        }

        private void tabControl3_Resize(object sender, EventArgs e)
        {
        }

        private void return_to_available_tests_table_button_Click(object sender, EventArgs e)
        {

            teacher_available_tests_table.Visible = true;
            teacher_available_tests_table.ClearSelection();
            teacher_one_test_table.ClearSelection();
            teacher_one_test_table.Rows.Clear();
            teacher_one_test_table.Visible = false;
            open_version_in_test_comboBox.Visible = false;
            open_test_button.Visible = true;
            return_to_available_tests_table_button.Visible = false;
            open_test_button.Enabled = false;

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            change_full_name_panel.Visible = true;
            teacher_profile_main_panel.Visible = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            TextBox[] textBoxes = { new_surname_textBox, new_teacher_name_textBox, new_patronymic_textBox };
            if (textbox.Check_textboxes_text_are_changed(new string[] { "", "", "" }, textBoxes))
            {
                database.Update_user_full_name(textBoxes, user_id, "teacher");
                FillTeacherProfileMainPanel();
                textbox.Clear_textboxes(textBoxes);
                Text = database.Get_name(user_id, "teacher");

            }
            else
            {
                MessageBox.Show("Заполните обязательные поля!");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textbox.Clear_textboxes(new TextBox[]
            { new_surname_textBox, new_teacher_name_textBox, new_patronymic_textBox });
            change_full_name_panel.Visible = false;
            teacher_profile_main_panel.Visible = true;
        }
    }
}

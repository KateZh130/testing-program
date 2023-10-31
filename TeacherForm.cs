using System;
using System.Collections.Generic;
using System.Drawing;
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
        readonly GroupboxPanelClass panel = new GroupboxPanelClass();
        readonly DatabaseClass database;
        DialogResult dialogResult;
        private int test_id = 0;
        private int version_id = 0;
        private int version_counter = 0;
        private int question_counter = 1;
        readonly int user_id;
        int student_id = -1;
        int group_id = -1;
        int edit_method = 0;
        bool edit_mode = false;
        bool create_mode = false;
        bool contextmenustrip1_enabled = false;
        List<string> questions_text = new List<string>();

        public TeacherForm(DatabaseClass database, int user_id)
        {
            InitializeComponent();
            this.database = database;
            this.user_id = user_id;
            this.Text = database.Get_user_name(user_id, "teacher");
            Update_teacher_profile_panel();
            data.Reset_teacher_test_table(database, user_id, teacher_available_tests_table);
            data.Reset_teacher_task_table(database, user_id, available_teacher_tasks_table);
        }
        //*****************закрытие приложения*********************
        private void TeacherForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (create_mode && test_id != 0)
            {
                dialogResult = MessageBox.Show("Идёт создание теста. Вы уверены, что хотите закрыть приложение? Введенные данные не сохранятся.", "Предупреждение", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    database.Delete_test(user_id, test_id);
                    Application.Exit();
                }
                else if (dialogResult == DialogResult.No)
                {
                    tabControl1.SelectedIndex = 1;
                    tabControl2.SelectedIndex = 1;
                }
            }
            else Application.Exit();
        }
        //********************вкладка профиль*****************************************
        //***********************выход из профиля*********************
        private void Exit_student_profile_button_Click(object sender, EventArgs e)
        {
            Application.OpenForms[0].Show();
            this.Hide();
        }
        //***********************удаление профиля************************
        private void Delete_teacher_profile_button_Click(object sender, EventArgs e)
        {
            dialogResult = MessageBox.Show("Вы уверены, что ходите безвозвратно удалить аккаунт? ", "Предупреждение", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                database.Delete_tasks_from_db(user_id);
                database.Delete_user(user_id, "teacher");
                Application.OpenForms[0].Show();
                this.Hide();
            }
        }
        //*************заполнение/очищение панелей профиля************
        private void Update_teacher_profile_panel()
        {
            panel.Change_visible(teacher_profile_main_panel, true);
            List<string> profil_info_list = new List<string>();
            database.Get_teacherForm_profile_info(user_id, profil_info_list);
            TextBox[] textBoxes = { teacher_name_profile_textBox, login_teacher_profile, password_teacher_profile };
            string[] text = { profil_info_list[0], profil_info_list[1], profil_info_list[2] };
            textbox.Fill(textBoxes, text);
        }

        private void Change_teacher_login_panel_VisibleChanged(object sender, EventArgs e)
        {
            if (!change_teacher_login_panel.Visible)
            {
                new_teacher_login_textbox.Clear();
                Update_teacher_profile_panel();
            }
        }
        //************открытие панелей изменения данных профиля***************
        private void Change_teacher_login_button_Click(object sender, EventArgs e)
        {
            panel.Change_visible(new Panel[] { change_teacher_login_panel, teacher_profile_main_panel }, new bool[] { true, false });
        }

        private void Change_teacher_password_button_Click(object sender, EventArgs e)
        {
            panel.Change_visible(new Panel[] { teacher_identity_check_panel, teacher_profile_main_panel }, new bool[] { true, false });
        }

        private void Change_teacher_name_button_Click(object sender, EventArgs e)
        {
            panel.Change_visible(new Panel[] { change_full_name_panel, teacher_profile_main_panel }, new bool[] { true, false });
        }
        //****************отмена изменения данных профиля******************
        private void Cancel_change_teacher_login_button_Click(object sender, EventArgs e)
        {
            panel.Change_visible(new Panel[] { change_teacher_login_panel, teacher_profile_main_panel }, new bool[] { false, true });
        }

        private void Cancel_change_teacher_password_button_Click(object sender, EventArgs e)
        {
            textbox.Clear(new TextBox[] { teacher_test_new_password_textBox, teacher_new_password_textBox });
            panel.Change_visible(new Panel[] { change_teacher_password_panel, teacher_profile_main_panel }, new bool[] { false, true });
        }

        private void Cancel_teacher_change_password_button_Click(object sender, EventArgs e)
        {
            textbox.Clear(teacher_old_password_textBox);
            panel.Change_visible(new Panel[] { teacher_identity_check_panel, teacher_profile_main_panel }, new bool[] { false, true });
        }

        private void Cancel_change_teacher_name_button_Click(object sender, EventArgs e)
        {
            textbox.Clear(new TextBox[] { new_surname_textBox, new_teacher_name_textBox, new_patronymic_textBox });
            panel.Change_visible(new Panel[] { change_full_name_panel, teacher_profile_main_panel }, new bool[] { false, true });
        }
        //**************изменение данных профиля****************
        private void New_teacher_login_button_Click(object sender, EventArgs e)
        {
            if (textbox.Check_text_changed(new_teacher_login_textbox))
            {
                if (textbox.Check_invalid_characters_with_space(new_teacher_login_textbox))
                {
                    database.Update_user_login_or_password(new_teacher_login_textbox.Text, user_id, "login", "teacher");
                    panel.Change_visible(change_teacher_login_panel, false);
                    MessageBox.Show("Логин успешно изменен!");
                }
            }
            else MessageBox.Show("Заполните обязательные поля!");
        }

        private void New_teacher_password_button_Click(object sender, EventArgs e)
        {
            TextBox[] textBoxes = { teacher_test_new_password_textBox, teacher_new_password_textBox };
            if (textbox.Check_text_changed(teacher_test_new_password_textBox) && textbox.Check_text_changed(teacher_new_password_textBox))
            {
                if (textbox.Check_passwords_are_matching(teacher_test_new_password_textBox.Text, teacher_new_password_textBox.Text) &&
                    textbox.Check_invalid_characters_with_space(textBoxes))
                {
                    database.Update_user_login_or_password(teacher_test_new_password_textBox.Text, user_id, "password", "teacher");
                    Update_teacher_profile_panel();
                    MessageBox.Show("Пароль успешно изменен!");
                    textbox.Clear(textBoxes);
                }
            }
            else MessageBox.Show("Заполните обязательные поля!");
        }

        private void Continue_teacher_change_password_button_Click(object sender, EventArgs e)
        {
            if (textbox.Check_text_changed(teacher_old_password_textBox))
            {
                if (textbox.Check_passwords_are_matching(teacher_old_password_textBox.Text, password_teacher_profile.Text) &&
                    textbox.Check_invalid_characters_with_space(teacher_old_password_textBox))
                {
                    textbox.Clear(teacher_old_password_textBox);
                    panel.Change_visible(new Panel[] { teacher_identity_check_panel, change_teacher_password_panel }, new bool[] { false, true });
                }
            }
            else MessageBox.Show("Заполните обязательные поля!");
        }

        private void Continue_change_teacher_name_button_Click(object sender, EventArgs e)
        {
            TextBox[] textBoxes = { new_surname_textBox, new_teacher_name_textBox, new_patronymic_textBox };
            if (textbox.Check_text_changed(textBoxes))
            {
                if (textbox.Check_invalid_characters(textBoxes, true))
                {
                    database.Update_user_full_name(new_surname_textBox + " " + new_teacher_name_textBox + " " + new_patronymic_textBox, user_id, "teacher");
                    Update_teacher_profile_panel();
                    textbox.Clear(textBoxes);
                    Text = database.Get_user_name(user_id, "teacher");
                }
            }
            else MessageBox.Show("Заполните обязательные поля!");
        }
        //********************вкладка конструктор тестов*************************************
        //*****************вкладка тесты******************
        private void Teacher_available_tests_table_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
                return;
            test_id = Convert.ToInt32(teacher_available_tests_table.SelectedCells[0].Value);
            open_test_button.Enabled = true;
        }

        private void Open_version_in_test_comboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            version_id = database.Get_version_id_by_number(user_id, test_id, Convert.ToInt32(open_version_in_test_comboBox.SelectedItem));
            data.Reset_teacher_one_test_table(database, user_id, version_id, teacher_one_test_table);
        }

        private void Return_to_available_tests_table_button_Click(object sender, EventArgs e)
        {
            data.Show(teacher_available_tests_table);
            teacher_available_tests_table.ClearSelection();
            teacher_one_test_table.ClearSelection();
            data.Clear_table(teacher_one_test_table);
            data.Hide(teacher_one_test_table);
            combobox.Change_visible(open_version_in_test_comboBox, false);
            open_test_button.Visible = true;
            return_to_available_tests_table_button.Visible = false;
            open_test_button.Enabled = false;
        }

        private void Open_test_button_Click(object sender, EventArgs e)
        {
            if (test_id != -1)
            {
                data.Hide(teacher_available_tests_table);
                data.Show(teacher_one_test_table);
                combobox.Change_visible(open_version_in_test_comboBox, true);
                open_test_button.Visible = false;
                return_to_available_tests_table_button.Visible = true;
            }
            else MessageBox.Show("Выберите тест из списка");
        }

        private void Open_version_in_test_comboBox_VisibleChanged(object sender, EventArgs e)
        {
            if (open_version_in_test_comboBox.Visible)
                database.Fill_version_number_comboBox(open_version_in_test_comboBox, user_id, teacher_available_tests_table.SelectedCells[1].Value.ToString());
            else
            {
                combobox.Delete_collection(open_version_in_test_comboBox);
                combobox.Return_original_text(open_version_in_test_comboBox, "Выберите вариант");
            }
        }
        //***************вкладка создания теста*******************
        //****************Добавление в бд основной информации по тесту при создании*********************
        private void Add_new_test_to_db_Click(object sender, EventArgs e)
        {
            if (textbox.Check_text_changed(new_test_name_textBox, "Введите название теста") && textbox.Check_text_changed(new_timer_textBox, "0"))
            {
                if (!database.Check_test_exist(new_test_name_textBox.Text, user_id) &&
                    textbox.Check_invalid_characters(new TextBox[] { new_test_name_textBox, comment_textBox }, true))
                {
                    string comment = "";
                    if (textbox.Check_text_changed(comment_textBox, "Комментарий к тесту (необязательно)"))
                        comment = comment_textBox.Text;
                    test_id = database.Add_new_test_to_database(new_test_name_textBox.Text, new_timer_textBox.Text, comment, user_id);
                    panel.Change_visible(new GroupBox[] { create_test_groupBox, create_version_questions_groupBox }, new bool[] { false, true });
                    ++version_counter;
                    version_id = database.Create_new_test_version(test_id, version_counter);
                    create_mode = true;
                }
            }
            else MessageBox.Show("Заполните обязательные поля!");
        }
        //***************переход на следующий вопрос****************
        private void Add_next_question_button_Click(object sender, EventArgs e)
        {
            string[] text = { "Введите текст вопроса", "Введите ответ №1", "Введите ответ №2", "Введите ответ №3", "Введите ответ №4" };
            TextBox[] textBoxes = { question_textBox, answer_textBox1, answer_textBox2, answer_textBox3, answer_textBox4 };
            CheckBox[] checkBoxes = { checkBox1, checkBox2, checkBox3, checkBox4 };
            if (new_method_radioButton.Checked && Check_question_panel_is_filled(textBoxes, text)
                && Check_right_answers_number(checkBoxes, question_type_comboBox.SelectedIndex)
                    && !database.Check_question_exist(question_textBox.Text, question_type_comboBox.SelectedIndex + 1, user_id)
                    && textbox.Check_invalid_characters(textBoxes, true))
            {
                Save_test_question(textBoxes, checkBoxes);
                ++question_counter;
                radiobutton.Clear_selection(new_method_radioButton);
            }
            else if (select_existing_method_radioButton.Checked && Check_select_existing_question_panel_filled() &&
                    !database.Check_question_exist(select_existing_question_comboBox.SelectedItem.ToString(), version_id))
            {
                database.Create_version_question_connection(Convert.ToInt32(id_textBox.Text), version_id);
                ++question_counter;
                radiobutton.Clear_selection(select_existing_method_radioButton);
            }
        }
        //***************переход на следующий вариант****************
        private void Create_next_version_Click(object sender, EventArgs e)
        {
            string[] text = { "Введите текст вопроса", "Введите ответ №1", "Введите ответ №2", "Введите ответ №3", "Введите ответ №4" };
            TextBox[] textBoxes = { question_textBox, answer_textBox1, answer_textBox2, answer_textBox3, answer_textBox4 };
            CheckBox[] checkBoxes = { checkBox1, checkBox2, checkBox3, checkBox4 };
            if (new_method_radioButton.Checked && Check_question_panel_is_filled(textBoxes, text)
                && Check_right_answers_number(checkBoxes, question_type_comboBox.SelectedIndex)
                    && !database.Check_question_exist(question_textBox.Text, question_type_comboBox.SelectedIndex + 1, user_id)
                    && textbox.Check_invalid_characters(textBoxes, true))
            {
                Save_test_question(textBoxes, checkBoxes);
                ++version_counter;
                question_counter = 1;
                version_id = database.Create_new_test_version(test_id, version_counter);
                radiobutton.Clear_selection(new_method_radioButton);
            }
            else if (select_existing_method_radioButton.Checked && Check_select_existing_question_panel_filled() &&
                    !database.Check_question_exist(select_existing_question_comboBox.SelectedItem.ToString(), version_id))
            {
                database.Create_version_question_connection(Convert.ToInt32(id_textBox.Text), version_id);
                ++version_counter;
                question_counter = 1;
                version_id = database.Create_new_test_version(test_id, version_counter);
                radiobutton.Clear_selection(select_existing_method_radioButton);
            }
        }
        //****************завершение создания теста**********************
        private void Finish_creating_test_Click(object sender, EventArgs e)
        {
            string[] text = { "Введите текст вопроса", "Введите ответ №1", "Введите ответ №2", "Введите ответ №3", "Введите ответ №4" };
            TextBox[] textBoxes = { question_textBox, answer_textBox1, answer_textBox2, answer_textBox3, answer_textBox4 };
            CheckBox[] checkBoxes = { checkBox1, checkBox2, checkBox3, checkBox4 };
            if (new_method_radioButton.Checked && Check_question_panel_is_filled(textBoxes, text) &&
                Check_right_answers_number(checkBoxes, question_type_comboBox.SelectedIndex) &&
                !database.Check_question_exist(question_textBox.Text, question_type_comboBox.SelectedIndex + 1, user_id)
                && textbox.Check_invalid_characters(textBoxes, true))
            {
                Save_test_question(textBoxes, checkBoxes);
                panel.Change_visible(create_new_question_panel, false);
                panel.Change_visible(new GroupBox[] { create_version_questions_groupBox, create_test_groupBox }, new bool[] { false, true });
                data.Reset_teacher_test_table(database, user_id, teacher_available_tests_table);
                data.Reset_teacher_task_table(database, user_id, available_teacher_tasks_table);
            }
            else if (select_existing_method_radioButton.Checked && Check_select_existing_question_panel_filled() &&
                    !database.Check_question_exist(select_existing_question_comboBox.SelectedItem.ToString(), version_id))
            {
                database.Create_version_question_connection(Convert.ToInt32(id_textBox.Text), version_id);
                panel.Change_visible(select_existing_question_panel, false);
                panel.Change_visible(new GroupBox[] { create_test_groupBox, create_version_questions_groupBox }, new bool[] { true, false });
                data.Reset_teacher_test_table(database, user_id, teacher_available_tests_table);//
                data.Reset_teacher_task_table(database, user_id, available_teacher_tasks_table);
            }
        }
        //******************сохранение тестового задания****************
        private void Save_test_question(TextBox[] textBoxes, CheckBox[] checkBoxes)
        {
            int answer_id;
            int question_id = database.Create_question_text(question_type_comboBox.SelectedIndex + 1, question_textBox.Text);
            database.Create_version_question_connection(question_id, version_id);
            for (int i = 1; i < textBoxes.Length; ++i)
            {
                answer_id = database.Create_answer_text(textBoxes[i].Text);
                database.Create_question_answer_connection(question_id, answer_id, checkBoxes[i - 1].Checked);
            }
        }
        //*******проверка заполнения панелей и на соответствие количества правильных ответов типу вопроса****************
        private bool Check_question_panel_is_filled(TextBox[] textBoxes, string[] text)
        {
            if (combobox.Check_selected_index_changed(question_type_comboBox) && textbox.Check_text_changed(textBoxes, text))
                return true;
            MessageBox.Show("Заполните обязательные поля!");
            return false;
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
            if (type == 0 && counter == 1 || type == 1 && counter > 1)
                return true;
            MessageBox.Show("Тип вопроса не совпадает с количеством выбранных правильных ответов");
            return false;
        }

        private bool Check_select_existing_question_panel_filled()
        {
            TextBox[] textBoxes = { existing_question_type_textBox, existing_answer_textBox1, existing_answer_textBox2, existing_answer_textBox3, existing_answer_textBox4 };
            CheckBox[] checkBoxes = { checkBox5, checkBox6, checkBox7, checkBox8 };
            if (combobox.Check_selected_index_changed(select_existing_question_comboBox) ||
                !textbox.Check_text_changed(textBoxes))
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
        //**************открытие панелей создания тестовых заданий*******************
        private void New_method_radioButton_CheckedChanged(object sender, EventArgs e)
        {
            bool value = false;
            if (new_method_radioButton.Checked)
                value = true;
            panel.Change_visible(create_new_question_panel, value);
        }

        private void Select_existing_method_radioButton_CheckedChanged(object sender, EventArgs e)
        {
            bool value = false;

            if (select_existing_method_radioButton.Checked)
                value = true;
            panel.Change_visible(select_existing_question_panel, value);
        }
        //*****************очищение/ заполнение панелей создания*****************
        private void Create_version_questions_groupBox_VisibleChanged(object sender, EventArgs e)
        {
            if (create_version_questions_groupBox.Visible)
            {
                create_version_questions_groupBox.Text = "Вопрос " + question_counter;
                if (edit_mode)
                {
                    finish_creating_test_button.Visible = false;
                    create_next_version.Visible = false;
                    finish_editing_button.Visible = true;
                    if (edit_method == 2)
                    {
                        finish_editing_button.Location = new Point(513, 432);
                        add_next_question_button.Visible = false;
                    }
                }
            }
            else
            {
                radiobutton.Clear_selection(new RadioButton[] { new_method_radioButton, select_existing_method_radioButton });
                test_id = 0;
                version_counter = 0;
                question_counter = 1;
                if (edit_mode)
                {
                    finish_creating_test_button.Visible = true;
                    create_next_version.Visible = true;
                    finish_editing_button.Visible = false;
                    finish_editing_button.Location = new Point(261, 432);
                    tabControl2.SelectedIndex = 2;
                }
            }
        }

        private void Select_existing_question_comboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string text = select_existing_question_comboBox.SelectedItem.ToString();
            int id = database.Get_teacher_question_version_id(user_id, text);
            textbox.Fill(id_textBox, database.Get_question_id(id, text).ToString());
            TextBox[] textBoxes = { existing_answer_textBox1, existing_answer_textBox2, existing_answer_textBox3, existing_answer_textBox4 };
            CheckBox[] checkBoxes = { checkBox5, checkBox6, checkBox7, checkBox8 };
            int type = database.Get_question_type(id, text);
            string type_name = "Вопрос с несколькими правильными вариантами ответа";
            if (type == 1)
                type_name = "Вопрос с одним правильным вариантом ответа";
            textbox.Fill(existing_question_type_textBox, type_name);
            List<string> existing_answers = database.Get_answers_text(id, text);
            textbox.Fill(textBoxes, existing_answers.ToArray());
            List<string> existing_right_answers = database.Get_right_answers(id, text);
            int counter = 0;
            checkbox.Clear(checkBoxes);
            for (int i = 0; i < textBoxes.Length; ++i)
            {
                if (i - counter >= existing_right_answers.Count)
                    break;
                if (textBoxes[i].Text == existing_right_answers[i - counter])
                    checkBoxes[i].Checked = true;
                else ++counter;
            }
            existing_answers.Clear();
            existing_right_answers.Clear();
        }

        private void Create_test_groupBox_VisibleChanged(object sender, EventArgs e)
        {
            if (!create_test_groupBox.Visible)
            {
                textbox.Clear(new TextBox[] { new_test_name_textBox, comment_textBox, new_timer_textBox });
                textbox.Return_original_text(new_test_name_textBox, "Введите название теста");
                textbox.Return_original_text(comment_textBox, "Комментарий к тесту (необязательно)");
            }
        }

        private void Create_new_question_panel_VisibleChanged(object sender, EventArgs e)
        {
            if (!create_new_question_panel.Visible)
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
                textbox.Fill(textboxes, text);
                create_version_questions_groupBox.Text = "Вопрос " + question_counter;
            }
        }

        private void Select_existing_question_panel_VisibleChanged(object sender, EventArgs e)
        {
            if (!select_existing_question_panel.Visible)
            {
                TextBox[] textBoxes = { existing_question_type_textBox, id_textBox, existing_answer_textBox1, existing_answer_textBox2, existing_answer_textBox3, existing_answer_textBox4 };
                CheckBox[] checkBoxes = { checkBox5, checkBox6, checkBox7, checkBox8 };
                textbox.Clear(textBoxes);
                checkbox.Clear(checkBoxes);
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
        //***********************вкладка редактирования теста**************
        //********************редактирование*********************
        private void Delete_test(string test)
        {
            dialogResult = MessageBox.Show("Вы уверены, что хотите безвозвратно удалить выбранный тест?", "Предупреждение", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
                database.Delete_test(user_id, database.Get_test_id(user_id, test));
        }

        private void Delete_version(string test, int version)
        {
            bool delete_test = false;
            string message = "Вы уверены, что хотите безвозвратно удалить выбранный вариант?";
            if (!database.Check_test_have_another_version(test, version, user_id))
            {
                message = "Так как в данном тесте только один вариант с единственным тестовым заданием, " +
                    "при удалении задания, тест так же будет удалён. Продолжить удаление? ";
                delete_test = true;
            }
            dialogResult = MessageBox.Show(message, "Предупреждение", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                database.Delete_version(user_id, version, test);
                if (delete_test)
                    database.Delete_test(user_id, database.Get_test_id(user_id, test));
            }
        }

        private void Delete_task(string test, int version, string task)
        {
            bool delete_version = false;
            bool delete_test = false;
            string message = "Вы уверены, что хотите удалить выбранное тестовое задание?";
            if (!database.Check_version_have_another_tasks(task, version, user_id))
            {
                if (!database.Check_test_have_another_version(test, version, user_id))
                {
                    message = "Так как в данном тесте только один вариант с единственным тестовым заданием, " +
                        "при удалении задания, тест так же будет удалён. Продолжить удаление? ";
                    delete_test = true;
                }
                else
                {
                    message = "В данном варианте только одно задание." +
                    " При удалении тестового задания, вариант так же будет удалён. Продолжить удаление? ";
                    delete_version = true;
                }
            }
            dialogResult = MessageBox.Show(message, "Предупреждение", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                database.Delete_task_from_test_version(user_id, version, test, task);
                if (delete_test)
                    database.Delete_test(user_id, database.Get_test_id(user_id, test));
                else if (delete_version)
                    database.Delete_version(user_id, Convert.ToInt32(delete_mode_choose_version_comboBox.SelectedItem), test);
            }
        }

        private void Confirm_change_button_Click(object sender, EventArgs e)
        {
            if (combobox.Check_selected_index_changed(available_teacher_tests_comboBox) || edit_method == 6)
            {
                string test_name = "";
                if (combobox.Check_selected_index_changed(available_teacher_tests_comboBox))
                    test_name = available_teacher_tests_comboBox.SelectedItem.ToString();
                switch (edit_method)
                {
                    case 0:
                        MessageBox.Show("Выберите раздел редактирования");
                        break;
                    case 1:
                        Go_to_create_page(test_name);
                        break;
                    case 2:
                        if (combobox.Check_selected_index_changed(choose_deleted_version_comboBox)) //проверить что панель заполнена
                            Go_to_create_page(test_name);
                        else MessageBox.Show("Заполните обязательные поля!");
                        break;
                    case 3:
                        if (textbox.Check_text_changed(new_name_textBox, "Введите новое название"))
                        {
                            if (!database.Check_test_exist(new_name_textBox.Text, user_id) && textbox.Check_invalid_characters(new_name_textBox, true))
                            {
                                database.Update_test_name(user_id, test_name, new_name_textBox.Text);
                                panel.Change_visible(new_name_panel, false);
                            }
                        }
                        else MessageBox.Show("Заполните обязательные поля!");
                        break;
                    case 4:
                        if (textbox.Check_text_changed(edit_timer_textBox))
                        {
                            database.Update_test_timer(user_id, Convert.ToInt32(edit_timer_textBox.Text), test_name);
                            panel.Change_visible(new_timer_panel, false);

                        }
                        else MessageBox.Show("Заполните обязательные поля!");
                        break;
                    case 5:
                        if (textbox.Check_text_changed(new_comment_textBox, "Введите комментарий к тесту") &&
                            textbox.Check_invalid_characters(new_comment_textBox, true))
                        {
                            database.Update_test_comment(user_id, test_name, new_comment_textBox.Text);
                            panel.Change_visible(new_comment_panel, false);

                        }
                        else MessageBox.Show("Заполните обязательные поля!");
                        break;
                    case 6:
                        if (combobox.Check_selected_index_changed(choose_task_comboBox))
                        {
                            if (!database.Check_question_exist(edit_test_name_textBox.Text, edit_question_type_comboBox.SelectedIndex + 1, user_id,
                                Convert.ToInt32(edit_question_id_textBox.Text)) && Check_right_answers_number(new CheckBox[]
                                { checkBox9, checkBox10, checkBox11, checkBox12 }, edit_question_type_comboBox.SelectedIndex)
                                && textbox.Check_invalid_characters(new TextBox[] { edit_test_name_textBox, textBox1, textBox2, textBox3, textBox4 }, true))
                            {
                                Update_task();
                                panel.Change_visible(edit_task_panel, false);
                                combobox.Change_visible(available_teacher_tests_comboBox, true);
                            }
                        }
                        else MessageBox.Show("Заполните обязательные поля!");
                        break;
                    case 7:
                        if (combobox.Check_selected_index_changed(delete_mode_choose_version_comboBox) && combobox.Check_selected_index_changed(delete_task_comboBox))
                        {
                            Delete_task(test_name, Convert.ToInt32(delete_mode_choose_version_comboBox.SelectedItem), delete_task_comboBox.SelectedItem.ToString());
                            panel.Change_visible(delete_task_panel, false);
                        }
                        else MessageBox.Show("Заполните обязательные поля!");
                        break;
                    case 8:
                        if (combobox.Check_selected_index_changed(choose_deleted_version_comboBox))
                        {
                            Delete_version(test_name, Convert.ToInt32(choose_deleted_version_comboBox.SelectedItem));
                            panel.Change_visible(add_task_or_delete_version_panel, false);
                        }
                        else MessageBox.Show("Заполните обязательные поля!");
                        break;
                    case 9:
                        Delete_test(test_name);
                        break;
                }
                combobox.Delete_collection(available_teacher_tests_comboBox);
                combobox.Return_original_text(available_teacher_tests_comboBox, "Выберите тест");
                database.Get_teacher_available_tests(user_id, available_teacher_tests_comboBox);
                data.Reset_teacher_test_table(database, user_id, teacher_available_tests_table);
                data.Reset_teacher_task_table(database, user_id, available_teacher_tasks_table);
                contextMenuStrip1.Show();
            }
            else MessageBox.Show("Заполните обязательные поля!");
        }

        private void Update_task()
        {
            database.Update_question_name(Convert.ToInt32(edit_question_id_textBox.Text), edit_question_type_comboBox.SelectedIndex + 1, edit_test_name_textBox.Text);
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
        //********************редактирование на панели создания****************
        private void Go_to_create_page(string test)
        {
            edit_mode = true;
            test_id = database.Get_test_id(user_id, test);
            if (edit_method == 1)
            {
                question_counter = 1;
                version_counter = database.Get_last_version_number(user_id, test) + 1;
                version_id = database.Create_new_test_version(test_id, version_counter);
            }
            else
            {
                version_id = database.Get_version_id_by_number(user_id, test_id, Convert.ToInt32(choose_deleted_version_comboBox.SelectedItem));
                question_counter = database.Get_last_question_number(user_id, test_id, version_id) + 1;
                panel.Change_visible(add_task_or_delete_version_panel, false);
            }
            tabControl2.SelectedTab = create_tests_page;
            panel.Change_visible(new GroupBox[] { create_test_groupBox, create_version_questions_groupBox }, new bool[] { false, true });
        }

        private void Finish_editing_button_Click(object sender, EventArgs e)
        {
            string[] text = { "Введите текст вопроса", "Введите ответ №1", "Введите ответ №2", "Введите ответ №3", "Введите ответ №4" };
            TextBox[] textBoxes = { question_textBox, answer_textBox1, answer_textBox2, answer_textBox3, answer_textBox4 };
            CheckBox[] checkBoxes = { checkBox1, checkBox2, checkBox3, checkBox4 };

            if (new_method_radioButton.Checked && Check_question_panel_is_filled(textBoxes, text)
                && Check_right_answers_number(checkBoxes, question_type_comboBox.SelectedIndex)
                    && !database.Check_question_exist(question_textBox.Text, question_type_comboBox.SelectedIndex + 1, user_id)
                    && textbox.Check_invalid_characters(textBoxes, true))
            {
                Save_test_question(textBoxes, checkBoxes);
                panel.Change_visible(create_new_question_panel, false);
                panel.Change_visible(create_version_questions_groupBox, false);
                edit_mode = false;

            }
            else if (select_existing_method_radioButton.Checked && Check_select_existing_question_panel_filled() &&
                    !database.Check_question_exist(select_existing_question_comboBox.SelectedItem.ToString(), version_id))
            {
                database.Create_version_question_connection(Convert.ToInt32(id_textBox.Text), version_id);
                panel.Change_visible(select_existing_question_panel, false);
                panel.Change_visible(create_version_questions_groupBox, false);
                edit_mode = false;
            }
            data.Reset_teacher_test_table(database, user_id, teacher_available_tests_table);
            data.Reset_teacher_task_table(database, user_id, available_teacher_tasks_table);
        }
        //*******************открытие панелей редактирования***********************
        private void Choose_task_comboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            panel.Change_visible(edit_task_panel2, true);
            combobox.Change_visible(choose_task_comboBox, false);
        }

        private void Add_version_ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Panel[] panels = { delete_task_panel, new_name_panel, new_timer_panel, new_comment_panel, edit_task_panel, add_task_or_delete_version_panel };
            edit_method = 1;
            panel.Change_visible(panels, false);
            contextMenuStrip1.Visible = false;
            combobox.Change_visible(available_teacher_tests_comboBox, true);
        }

        private void Add_task_ToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Panel[] panels = { delete_task_panel, new_name_panel, new_timer_panel, new_comment_panel, edit_task_panel, add_task_or_delete_version_panel };
            edit_method = 2;
            panel.Change_visible(panels, false);
            contextMenuStrip1.Visible = false;
            combobox.Change_visible(available_teacher_tests_comboBox, true);
        }

        private void Edit_name_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Panel[] panels = { delete_task_panel, new_timer_panel, new_comment_panel, edit_task_panel, add_task_or_delete_version_panel };
            edit_method = 3;
            panel.Change_visible(panels, false);
            panel.Change_visible(new_name_panel, true);
            contextMenuStrip1.Visible = false;
            combobox.Change_visible(available_teacher_tests_comboBox, true);
        }

        private void Edit_timer_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Panel[] panels = { delete_task_panel, new_name_panel, new_comment_panel, edit_task_panel, add_task_or_delete_version_panel };

            edit_method = 4;
            panel.Change_visible(panels, false);
            panel.Change_visible(new_timer_panel, true);
            contextMenuStrip1.Visible = false;
            combobox.Change_visible(available_teacher_tests_comboBox, true);
        }

        private void Edit_comment_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Panel[] panels = { delete_task_panel, new_name_panel, new_timer_panel, edit_task_panel, add_task_or_delete_version_panel };

            edit_method = 5;
            panel.Change_visible(panels, false);
            panel.Change_visible(new_comment_panel, true);
            contextMenuStrip1.Visible = false;
            combobox.Change_visible(available_teacher_tests_comboBox, true);
        }

        private void Edit_task_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Panel[] panels = { delete_task_panel, new_name_panel, new_timer_panel, new_comment_panel, add_task_or_delete_version_panel };
            combobox.Change_visible(available_teacher_tests_comboBox, false);
            edit_method = 6;
            panel.Change_visible(panels, false);
            panel.Change_visible(edit_task_panel, true);
            contextMenuStrip1.Visible = false;
        }

        private void Delete_task_ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Panel[] panels = { new_name_panel, new_timer_panel, new_comment_panel, edit_task_panel, add_task_or_delete_version_panel };
            edit_method = 7;
            panel.Change_visible(panels, false);
            panel.Change_visible(delete_task_panel, true);
            contextMenuStrip1.Visible = false;
            combobox.Change_visible(available_teacher_tests_comboBox, true);
        }

        private void Delete_version_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Panel[] panels = { delete_task_panel, new_name_panel, new_timer_panel, new_comment_panel, edit_task_panel, add_task_or_delete_version_panel };
            edit_method = 8;
            panel.Change_visible(panels, false);
            contextMenuStrip1.Visible = false;
            combobox.Change_visible(available_teacher_tests_comboBox, true);
        }

        private void Delete_test_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Panel[] panels = { delete_task_panel, new_name_panel, new_timer_panel, new_comment_panel, edit_task_panel, add_task_or_delete_version_panel };
            edit_method = 9;
            panel.Change_visible(panels, false);
            contextMenuStrip1.Visible = false;
            combobox.Change_visible(available_teacher_tests_comboBox, true);
        }
        //*********************кнопка отмены редактирования***********
        private void Cancel_edit_button_Click(object sender, EventArgs e)
        {
            Panel[] panels = { delete_task_panel, new_name_panel, new_timer_panel, new_comment_panel, edit_task_panel, add_task_or_delete_version_panel };
            contextMenuStrip1.Visible = true;
            panel.Change_visible(edit_test_groupBox, true);
            panel.Change_visible(panels, false);
            combobox.Change_visible(available_teacher_tests_comboBox, true);
            edit_method = 0;
        }
        //*********очищение и заполнение панелей для редактирования*******************
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

        private void Available_teacher_tests_comboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (edit_method == 2 || edit_method == 8)
            {
                panel.Change_visible(add_task_or_delete_version_panel, true);
                database.Fill_version_number_comboBox(choose_deleted_version_comboBox, user_id,
                available_teacher_tests_comboBox.SelectedItem.ToString());
            }
            if (edit_method == 7)
            {
                database.Fill_version_number_comboBox(delete_mode_choose_version_comboBox, user_id,
                available_teacher_tests_comboBox.SelectedItem.ToString());
            }
        }

        private void Delete_mode_choose_version_comboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            combobox.Delete_collection(delete_task_comboBox);
            if (edit_method == 7)
                database.Get_teacher_available_tasks(delete_task_comboBox, user_id, available_teacher_tests_comboBox.SelectedItem.ToString(),
                    Convert.ToInt32(delete_mode_choose_version_comboBox.SelectedItem));
        }

        private void Delete_task_panel_VisibleChanged(object sender, EventArgs e)
        {
            if (!delete_task_panel.Visible)
            {
                ComboBox[] comboBoxes = { delete_mode_choose_version_comboBox, delete_task_comboBox };
                combobox.Delete_collection(comboBoxes);
                combobox.Return_original_text(comboBoxes, new string[] { "Выберите вариант", "Выберите вопрос" });
            }
        }

        private void Delete_version_panel_VisibleChanged(object sender, EventArgs e)
        {
            if (!add_task_or_delete_version_panel.Visible)
            {
                combobox.Delete_collection(choose_deleted_version_comboBox);
                combobox.Return_original_text(choose_deleted_version_comboBox, "Выберите вариант");
            }
        }

        private void New_name_panel_VisibleChanged(object sender, EventArgs e)
        {
            if (!new_name_panel.Visible)
            {
                new_name_textBox.Clear();
                textbox.Return_original_text(new_name_textBox, "Введите новое название");
            }
        }

        private void New_timer_panel_VisibleChanged(object sender, EventArgs e)
        {
            if (!new_timer_panel.Visible)
                edit_timer_textBox.Clear();
        }

        private void New_comment_panel_VisibleChanged(object sender, EventArgs e)
        {
            if (!new_comment_panel.Visible)
            {
                new_comment_textBox.Clear();
                textbox.Return_original_text(new_comment_textBox, "Введите комментарий к тесту");
            }
        }

        private void Edit_task_panel2_VisibleChanged(object sender, EventArgs e)
        {
            if (edit_task_panel2.Visible)
            {
                string question = choose_task_comboBox.SelectedItem.ToString();
                int version = database.Get_teacher_question_version_id(user_id, question);
                Fill_edit_task_panel2(question, version,
                    database.Get_answers_text(version, question),
                    database.Get_answers_id(version, question),
                    database.Get_right_answers(version, question));
            }
        }

        private void Edit_task_panel_VisibleChanged(object sender, EventArgs e)
        {
            if (!edit_task_panel.Visible)
            {
                combobox.Delete_collection(choose_task_comboBox);
                combobox.Return_original_text(choose_task_comboBox, "Выберите вопрос для редактирования");
                combobox.Clear_selection(edit_question_type_comboBox);
                textbox.Clear(new TextBox[]
                { edit_test_name_textBox, textBox1, textBox2, textBox3, textBox4,edit_question_id_textBox,
                    edit_answer_id_textBox1,edit_answer_id_textBox2,edit_answer_id_textBox3,edit_answer_id_textBox4 });
                checkbox.Clear(new CheckBox[] { checkBox9, checkBox10, checkBox11, checkBox12 });
            }
            else database.Get_teacher_available_tasks(user_id, choose_task_comboBox);
        }

        private void Edit_test_groupBox_VisibleChanged(object sender, EventArgs e)
        {
            if (available_teacher_tests_comboBox.Visible)
            {
                contextmenustrip1_enabled = true;
                GroupBox groupBox = (GroupBox)sender;
                contextMenuStrip1.Show(groupBox, new Point(135, 0));
                combobox.Delete_collection(available_teacher_tests_comboBox);
                combobox.Return_original_text(available_teacher_tests_comboBox, "Выберите тест");
                database.Get_teacher_available_tests(user_id, available_teacher_tests_comboBox);
            }
            else contextmenustrip1_enabled = false;
        }
        //****************вкладка студенты и группы*********************
        //*************заполнение таблиц и взаимодействия с ними***************************
        private void Choose_group_for_marks_comboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            data.Clear_rows(group_marks_table);
            data.Reset_group_marks_table(database, choose_group_for_marks_comboBox.SelectedItem.ToString(), group_marks_table, user_id);
        }

        private void Show_students_comboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            data.Reset_students_list_table(database, user_id, show_students_comboBox.SelectedItem.ToString(), students_dataGridView);
        }

        private void Students_dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
                return;
            student_id = -1;
            group_id = -1;
            Button[] student = { change_student_group_button, change_student_access_to_test_button, delete_student_profile_button };
            Button[] group = { change_group_name_button, change_group_access_to_test_button, delete_group_button };
            string name = data.Get_column_name(students_dataGridView, students_dataGridView.SelectedCells[0]);
            if (name == "group")
            {
                tip1.Active = true;
                tip2.Active = false;
                for (int i = 0; i < 3; ++i)
                {
                    student[i].Enabled = false;
                    group[i].Enabled = true;
                }
                group_id = database.Get_group_by_name(students_dataGridView.SelectedCells[0].Value.ToString());
            }
            else if (name == "student")
            {
                tip1.Active = false;
                tip2.Active = true;
                for (int i = 0; i < 3; ++i)
                {
                    student[i].Enabled = true;
                    group[i].Enabled = false;
                }
                student_id = data.Get_null_column_hiden_id(students_dataGridView, students_dataGridView.SelectedCells[0]);
            }
        }
        //****************редактирование(кроме доступа к тестам)****************
        private void Create_student_button_Click(object sender, EventArgs e)
        {
            TextBox[] textBoxes = { full_name_textBox1, full_name_textBox2, full_name_textBox3 };
            if (textbox.Check_text_changed(textBoxes, new string[] { "Фамилия", "Имя", "Отчество" })
                && combobox.Check_selected_index_changed(create_student_group_comboBox))
            {
                if (textbox.Check_invalid_characters(textBoxes, true))
                {
                    string name = full_name_textBox1.Text + " " + full_name_textBox2.Text + " " + full_name_textBox3.Text;
                    if (!database.Check_student_exist(name, create_student_group_comboBox.SelectedItem.ToString()))
                    {
                        int id = database.Create_student(name, create_student_group_comboBox.SelectedItem.ToString());
                        Allow_new_student_group_tests(id, create_student_group_comboBox.SelectedItem.ToString());
                        MessageBox.Show("Профиль успешно создан!");
                        panel.Change_visible(create_student_groupBox, false);
                        splitContainer1.Visible = true;
                    }
                    else MessageBox.Show("Аккаунт этого студента уже существует");
                }
            }
            else MessageBox.Show("Заполните обязательные поля!");
        }

        private void Ok_change_student_group_button_button_Click(object sender, EventArgs e)
        {
            if (combobox.Check_selected_index_changed(groups_comboBox))
            {
                database.Update_student_group(groups_comboBox.SelectedItem.ToString(), student_id);
                Allow_new_student_group_tests(student_id, groups_comboBox.SelectedItem.ToString());
                MessageBox.Show("Учебная группа изменена");
                panel.Change_visible(change_student_group_panel, false);
                splitContainer1.Visible = true;
            }
            else MessageBox.Show("Заполните обязательные поля!");
        }

        private void Ok_change_group_name_button_Click(object sender, EventArgs e)
        {
            if (textbox.Check_text_changed(group_name_textBox, "Введите название группы"))
            {
                if (textbox.Check_invalid_characters(group_name_textBox, true))
                {
                    if (!database.Check_group_exist(group_name_textBox.Text))
                    {
                        string message = "Учебная группа создана";
                        if (group_id != -1)
                        {
                            database.Update_group_name(group_id, group_name_textBox.Text);
                            message = "Название учебной группы изменено";
                        }
                        else database.Create_group(group_name_textBox.Text);
                        MessageBox.Show(message);
                        panel.Change_visible(change_group_name_panel, false);
                        splitContainer1.Visible = true;
                    }
                    else MessageBox.Show("Учебная группа с таким названием уже существует");
                }
            }
            else MessageBox.Show("Заполните обязательные поля!");
        }

        private void Delete_student_profile_button_Click(object sender, EventArgs e)
        {
            if (student_id != -1)
            {
                dialogResult = MessageBox.Show("Данное действие необратимо, вы уверены, что хотите удалить этот аккаунт? " +
                    "У студента больше не будет возможности пользоваться своим профилем.", "Предупреждение", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    database.Delete_user(student_id, "student");
                    splitContainer1.Visible = false;
                    MessageBox.Show("Аккаунт удалён");
                    splitContainer1.Visible = true;
                }
            }
        }

        private void Delete_group_button_Click(object sender, EventArgs e)
        {
            if (group_id != -1)
            {
                dialogResult = MessageBox.Show("Данное действие необратимо, вы уверены, что хотите удалить эту учебную группу? " +
                    "Аккаунты всех студентов группы будут удалены.", "Предупреждение", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    database.Delete_group(group_id);
                    splitContainer1.Visible = false;
                    MessageBox.Show("Учебная группа удалена");
                    splitContainer1.Visible = true;
                }
            }
        }
        //**********************открытие вкладок редактирования******************
        private void Change_student_group_button_Click(object sender, EventArgs e)
        {
            if (student_id != -1)
            {

                change_student_group_panel.Visible = true;
                splitContainer1.Visible = false;
            }
        }

        private void Add_new_student_button_Click(object sender, EventArgs e)
        {
            panel.Change_visible(create_student_groupBox, true);
            splitContainer1.Visible = false;
        }

        private void Allow_student_access_to_test_button_Click(object sender, EventArgs e)
        {
            if (student_id != -1)
            {
                panel.Change_visible(test_access_panel, true);
                splitContainer1.Visible = false;
            }
        }

        private void Change_group_name_button_Click(object sender, EventArgs e)
        {
            if (group_id != -1)
            {
                panel.Change_visible(change_group_name_panel, true);
                splitContainer1.Visible = false;
            }
        }

        private void Allow_group_access_to_test_button_Click(object sender, EventArgs e)
        {
            if (group_id != -1)
            {
                panel.Change_visible(test_access_panel, true);
                splitContainer1.Visible = false;
            }
        }

        private void Add_new_group_button_Click(object sender, EventArgs e)
        {
            group_id = -1;
            panel.Change_visible(change_group_name_panel, true);
            splitContainer1.Visible = false;
        }
        //************заполнение/очищение панелей************ 
        private void SplitContainer1_VisibleChanged(object sender, EventArgs e)
        {
            Button[] student = { change_student_group_button, change_student_access_to_test_button, delete_student_profile_button };
            Button[] group = { change_group_name_button, change_group_access_to_test_button, delete_group_button };
            if (splitContainer1.Visible)
            {
                splitContainer1.SplitterDistance = 504;
                database.Get_groups(show_students_comboBox, groups_and_students_page: true);
                tip1.Active = true;
                tip2.Active = true;
                tip1.SetToolTip(panel3, "Выберите студента из списка");
                tip2.SetToolTip(panel4, "Выберите группу из списка");

                for (int i = 0; i < 3; ++i)
                {
                    student[i].Enabled = false;
                    group[i].Enabled = false;
                }
                group_id = -1;
                student_id = -1;
            }
            else
            {
                combobox.Delete_collection(show_students_comboBox);
                combobox.Return_original_text(show_students_comboBox, "Отобразить...");
                students_dataGridView.ClearSelection();
                data.Clear_table(students_dataGridView);
            }
        }

        private void Create_student_groupBox_VisibleChanged(object sender, EventArgs e)
        {
            if (create_student_groupBox.Visible)
                database.Get_groups(create_student_group_comboBox);
            else
            {
                combobox.Delete_collection(create_student_group_comboBox);
                combobox.Return_original_text(create_student_group_comboBox, "Выберите учебную группу");
                textbox.Fill(new TextBox[] { full_name_textBox1, full_name_textBox2, full_name_textBox3 },
                    new string[] { "Фамилия", "Имя", "Отчество" });
            }
        }

        private void Change_student_group_panel_VisibleChanged(object sender, EventArgs e)
        {
            if (change_student_group_panel.Visible)
                database.Get_groups(groups_comboBox);
            else
            {
                combobox.Clear_selection(groups_comboBox);
                combobox.Delete_collection(groups_comboBox);
            }
        }

        private void Change_group_name_panel_VisibleChanged(object sender, EventArgs e)
        {
            if (!change_group_name_panel.Visible)
            {
                group_name_textBox.Clear();
                textbox.Return_original_text(group_name_textBox, "Введите название группы");
            }
        }

        private void Test_access_panel_VisibleChanged(object sender, EventArgs e)
        {
            if (test_access_panel.Visible)
                database.Get_teacher_available_tests(user_id, test_access_comboBox);
            else
            {
                combobox.Clear_selection(test_access_comboBox);
                combobox.Delete_collection(test_access_comboBox);
                radiobutton.Clear_selection(new RadioButton[] { allow_radioButton, prohibit_radioButton });
            }
        }
        //************отмена редактирования**********
        private void Cancel_create_student_button_Click(object sender, EventArgs e)
        {
            panel.Change_visible(create_student_groupBox, false);
            splitContainer1.Visible = true;
        }

        private void Cancel_change_student_group_button_Click(object sender, EventArgs e)
        {
            panel.Change_visible(change_student_group_panel, false);
            splitContainer1.Visible = true;
        }

        private void Cancel_change_group_name_button_Click(object sender, EventArgs e)
        {
            panel.Change_visible(change_group_name_panel, false);
            splitContainer1.Visible = true;
        }

        private void Cancel_test_access_button_Click(object sender, EventArgs e)
        {
            panel.Change_visible(test_access_panel, false);
            splitContainer1.Visible = true;
        }
        //*******************изменение доступа************************
        //****************изменение доступа для нового/переведенного студента**************
        private void Allow_new_student_group_tests(int id, string group)
        {
            database.Delete_student_test_access_status(id);
            List<string> new_tests = new List<string>();
            database.Get_all_tests(user_id, group, new_tests, id, true);
            foreach (string test in new_tests)
                database.Add_student_test_access_status(id, test, "group");
        }
        //************определение типа изменения доступа**************
        private string Determine_group_type_of_access_status_change(int id, string test, ref string exist_status, ref bool connection_without_mark, bool allow)
        {
            bool connection = database.Check_access_connection_exist(id, test);
            if (connection)
            {
                exist_status = database.Get_student_test_access_status(id, test);
                connection_without_mark = database.Check_access_connection_exist(id, test, exist_status);
            }
            else if (allow) return "insert group";
            else return "no connection";
            if (connection_without_mark && allow)
            {
                if (exist_status != "group")
                    return "update group";
                return "have group access";
            }
            if (connection_without_mark && !allow)
            {
                if (exist_status != "closed group" && exist_status != "closed personal")
                    return "update closed";
                return "already closed";
            }
            if (allow && exist_status != "closed group" && exist_status != "closed personal")
                return "update closed + insert group";
            if (allow)
                return "insert group";
            if (exist_status != "closed group" && exist_status != "closed personal")
                return "update closed";
            return "already closed";
        }

        private string Determine_student_type_of_access_status_change(int id, string test, ref string exist_status, ref bool connection_without_mark, bool allow)
        {
            bool connection = database.Check_access_connection_exist(id, test);
            if (connection)
            {
                exist_status = database.Get_student_test_access_status(id, test);
                connection_without_mark = database.Check_access_connection_exist(id, test, exist_status);
            }
            else if (allow) return "insert";
            else return "У этого студента нет доступа к выбранному вами тесту";
            if (connection_without_mark)
            {
                if (allow)
                {
                    switch (exist_status)
                    {
                        case "group":
                            return "Выбранный вами студент состоит в группе, у которой уже есть доступ к этому тесту.\nЧтобы доступ к тесту был только у этого студента, сначала запретите доступ группе, а потом разрешите студенту";
                        case "personal":
                            return "У этого студента уже есть доступ к выбранному вами тесту";
                        case "closed personal":
                            return "update";
                        case "closed group":
                            return "insert";
                    }
                }
                if (exist_status == "group")
                    return "update closed + update personal";
                if (exist_status == "personal")
                    return "update closed";
                return "Доступ к выбранному вами тесту уже закрыт для этого студента";
            }
            if (allow && (exist_status == "group" || exist_status == "personal"))
                return "update closed + insert extra";
            if (allow)
                return "insert";
            if (exist_status == "group")
                return "update closed + update personal";
            if (exist_status == "personal")
                return "update closed";
            return "Доступ к выбранному вами тесту уже закрыт для этого студента";
        }
        //****************изменение доступа*********************
        private string Make_group_access(List<int> students, bool allow, string test)
        {
            int no_group_connection = 0;
            int have_group_access = 0;
            int already_closed = 0;
            foreach (int student in students)
            {
                string exist_status = "";
                bool without_mark = false;
                string type = Determine_group_type_of_access_status_change(student, test, ref exist_status, ref without_mark, allow);
                switch (type)
                {
                    case "insert group":
                        database.Add_student_test_access_status(student, test, "group");
                        break;
                    case "no connection":
                        ++no_group_connection;
                        break;
                    case "update group":
                        database.Update_student_test_access_status(student, test, "group", exist_status, without_mark);
                        break;
                    case "have group access":
                        ++have_group_access;
                        break;
                    case "update closed":
                        database.Update_student_test_access_status(student, test, "closed " + exist_status, exist_status, without_mark);
                        break;
                    case "already closed":
                        ++already_closed;
                        break;
                    case "update closed + insert group":
                        database.Update_student_test_access_status(student, test, "closed " + exist_status, exist_status, without_mark);
                        database.Add_student_test_access_status(student, test, "group");
                        break;
                }
            }
            if (no_group_connection + already_closed == students.Count && already_closed != students.Count)
                return "У выбранной вами группы нет доступа к этому тесту.";
            if (have_group_access == students.Count)
                return "У выбранной вами группы уже есть доступ к этому тесту.";
            if (already_closed == students.Count)
                return "Доступ к этому тесту уже закрыт у выбранной вами группы.";
            return "Успешно.";
        }

        private string Make_personal_access(List<int> students, bool allow, string test)
        {
            string type;
            string exist_status = "";
            bool without_mark = false;
            type = Determine_student_type_of_access_status_change(student_id, test, ref exist_status, ref without_mark, allow);
            switch (type)
            {
                case "insert":
                    database.Add_student_test_access_status(student_id, test, "personal");
                    break;
                case "update":
                    database.Update_student_test_access_status(student_id, test, "personal", exist_status, without_mark);
                    break;
                case "update closed + update personal":
                    database.Update_student_test_access_status(student_id, test, "closed personal", exist_status, without_mark);
                    database.Get_students_id(student_id, students, true);
                    students.Remove(student_id);
                    foreach (int student in students)
                        database.Update_student_test_access_status(student, test, "personal", exist_status, without_mark);
                    break;
                case "update closed":
                    database.Update_student_test_access_status(student_id, test, "closed personal", exist_status, without_mark);
                    break;
                case "update closed + insert extra":
                    database.Update_student_test_access_status(student_id, test, "closed personal", exist_status, false);
                    database.Add_student_test_access_status(student_id, test, exist_status);
                    break;
                default:
                    return type;
            }
            return "Успешно.";
        }

        private void Ok_test_access_button_Click(object sender, EventArgs e)
        {
            if (combobox.Check_selected_index_changed(test_access_comboBox) && (allow_radioButton.Checked || prohibit_radioButton.Checked))
            {
                bool allow = false;
                if (allow_radioButton.Checked)
                    allow = true;
                List<int> students = new List<int>();
                string header = "";
                string message;
                if (group_id != -1)
                {
                    database.Get_students_id(group_id, students, false);
                    message = Make_group_access(students, allow, test_access_comboBox.SelectedItem.ToString());
                    if (message != "Успешно.")
                        header = "Ошибка";
                    MessageBox.Show(message, header);
                }
                else if (student_id != -1)
                {
                    message = Make_personal_access(students, allow, test_access_comboBox.SelectedItem.ToString());
                    if (message != "Успешно.")
                        header = "Ошибка";
                    MessageBox.Show(message, header);
                }
                panel.Change_visible(test_access_panel, false);
                splitContainer1.Visible = true;
            }
            else MessageBox.Show("Заполните обязательные поля!");
        }
        //************вкладка журнал успеваемости*****************
        private void Choose_student_group_for_marks_comboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            combobox.Change_visible(choose_student_for_marks_comboBox, true);
            database.Fill_student_name_collection(choose_student_group_for_marks_comboBox.SelectedItem.ToString(),
                choose_student_for_marks_comboBox, true);
        }

        private void Choose_student_for_marks_comboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            data.Clear_rows(student_marks_table);
            data.Reset_student_marks_table(database, choose_student_group_for_marks_comboBox.SelectedItem.ToString(),
               choose_student_for_marks_comboBox.SelectedItem.ToString(), student_marks_table, user_id);
        }
        //**************управление вкладками**********************
        private void TabControl2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl2.SelectedIndex == 2)
                panel.Change_visible(edit_test_groupBox, true);
            else contextmenustrip1_enabled = false;
            if (tabControl2.SelectedIndex == 1 && !edit_mode)
                panel.Change_visible(create_test_groupBox, true);
            if (tabControl2.SelectedIndex != 1 && (create_mode || edit_mode && edit_method == 1) && test_id != 0)
            {
                dialogResult = MessageBox.Show("Идёт создание теста. Вы уверены, что хотите прервать создание? Введенные данные не сохранятся.", "Предупреждение", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    if (edit_mode)
                        database.Delete_version(user_id, version_id, test_id: test_id);
                    else database.Delete_test(user_id, test_id);
                    test_id = 0;
                    version_id = 0;
                    version_counter = 0;
                    question_counter = 1;
                    create_mode = false;
                    edit_mode = false;
                    edit_method = 0;
                }
                else if (dialogResult == DialogResult.No)
                    tabControl2.SelectedIndex = 1;
            }
        }

        private void TabControl2_Click(object sender, EventArgs e)
        {
            if (tabControl2.SelectedIndex == 2)
                contextMenuStrip1.Show();
        }

        private void TabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 3)
            {
                database.Get_groups(choose_group_for_marks_comboBox, marks_page: true);
                database.Get_groups(choose_student_group_for_marks_comboBox, marks_page: true);
            }
            else
            {
                ComboBox[] comboBoxes = { choose_student_for_marks_comboBox, choose_group_for_marks_comboBox, choose_student_group_for_marks_comboBox };
                string[] text = { "Выберите студента", "Выберите учебную группу", "Выберите учебную группу" };
                for (int i = 0; i < comboBoxes.Length; ++i)
                {
                    combobox.Delete_collection(comboBoxes[i]);
                    combobox.Return_original_text(comboBoxes[i], text[i]);
                }
                data.Clear_rows(student_marks_table);
                data.Clear_rows(group_marks_table);
                data.Hide(group_marks_table);
                data.Hide(student_marks_table);
            }
            if (tabControl1.SelectedIndex != 1 && (create_mode || edit_mode && edit_method == 1) && test_id != 0)
            {
                dialogResult = MessageBox.Show("Идёт создание теста. Вы уверены, что хотите прервать создание? Введенные данные не сохранятся.", "Предупреждение", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    if (edit_mode)
                        database.Delete_version(user_id, version_id, test_id: test_id);
                    else database.Delete_test(user_id, test_id);
                    test_id = 0;
                    version_id = 0;
                    version_counter = 0;
                    question_counter = 1;
                    create_mode = false;
                    edit_mode = false;
                    edit_method = 0;
                }
                else if (dialogResult == DialogResult.No)
                {
                    tabControl1.SelectedIndex = 1;
                    tabControl2.SelectedIndex = 1;
                }
            }
        }
        //****************взаимодействие с обычным TextBox*************************************
        bool comment_changed = false;
        private void Comment_textBox_TextChanged(object sender, EventArgs e)
        {
            comment_changed = textbox.Check_text_changed(comment_textBox, "Комментарий к тесту (необязательно)");
        }

        private void Comment_textBox_Click(object sender, EventArgs e)
        {
            comment_changed = textbox.Check_is_cleared(comment_textBox, "Комментарий к тесту (необязательно)", comment_changed);
        }

        private void Comment_textBox_Leave(object sender, EventArgs e)
        {
            textbox.Return_original_text(comment_textBox, "Комментарий к тесту (необязательно)");
        }

        bool new_test_name_changed = false;
        private void New_test_name_textBox_TextChanged(object sender, EventArgs e)
        {
            new_test_name_changed = textbox.Check_text_changed(new_test_name_textBox, "Введите название теста");
        }

        private void New_test_name_textBox_Click(object sender, EventArgs e)
        {
            new_test_name_changed = textbox.Check_is_cleared(new_test_name_textBox, "Введите название теста", new_test_name_changed);
        }

        private void New_test_name_textBox_Leave(object sender, EventArgs e)
        {
            textbox.Return_original_text(new_test_name_textBox, "Введите название теста");
        }

        bool new_name_textBox_changed = false;
        private void New_name_textBox_Click(object sender, EventArgs e)
        {
            new_name_textBox_changed = textbox.Check_is_cleared(new_name_textBox, "Введите новое название", new_name_textBox_changed);
        }

        private void New_name_textBox_TextChanged(object sender, EventArgs e)
        {
            new_name_textBox_changed = textbox.Check_text_changed(new_name_textBox, "Введите новое название");
        }

        private void New_name_textBox_Leave(object sender, EventArgs e)
        {
            textbox.Return_original_text(new_name_textBox, "Введите новое название");
        }

        bool new_comment_textBox_changed = false;
        private void New_comment_textBox_Click(object sender, EventArgs e)
        {
            new_comment_textBox_changed = textbox.Check_is_cleared(new_comment_textBox, "Введите комментарий", new_comment_textBox_changed);
        }

        private void New_comment_textBox_TextChanged(object sender, EventArgs e)
        {
            new_comment_textBox_changed = textbox.Check_text_changed(new_comment_textBox, "Введите комментарий");
        }

        private void New_comment_textBox_Leave(object sender, EventArgs e)
        {
            textbox.Return_original_text(new_comment_textBox, "Введите комментарий");
        }

        bool group_name_textBox_changed = false;
        private void Group_name_textBox_Click(object sender, EventArgs e)
        {
            group_name_textBox_changed = textbox.Check_is_cleared(group_name_textBox, "Введите название группы", group_name_textBox_changed);
        }

        private void Group_name_textBox_Leave(object sender, EventArgs e)
        {
            textbox.Return_original_text(group_name_textBox, "Введите название группы");
        }

        private void Group_name_textBox_TextChanged(object sender, EventArgs e)
        {
            group_name_textBox_changed = textbox.Check_text_changed(group_name_textBox, "Введите название группы");
        }

        bool full_name_textBox1_changed = false;
        private void Full_name_textBox1_Click(object sender, EventArgs e)
        {
            full_name_textBox1_changed = textbox.Check_is_cleared(full_name_textBox1, "Фамилия", full_name_textBox1_changed);
        }

        private void Full_name_textBox1_TextChanged(object sender, EventArgs e)
        {
            full_name_textBox1_changed = textbox.Check_text_changed(full_name_textBox1, "Фамилия");
        }

        private void Full_name_textBox1_Leave(object sender, EventArgs e)
        {
            textbox.Return_original_text(full_name_textBox1, "Фамилия");
        }

        bool full_name_textBox2_changed = false;
        private void Full_name_textBox2_Click(object sender, EventArgs e)
        {
            full_name_textBox2_changed = textbox.Check_is_cleared(full_name_textBox2, "Имя", full_name_textBox2_changed);
        }

        private void Full_name_textBox2_TextChanged(object sender, EventArgs e)
        {
            full_name_textBox2_changed = textbox.Check_text_changed(full_name_textBox2, "Имя");
        }

        private void Full_name_textBox2_Leave(object sender, EventArgs e)
        {
            textbox.Return_original_text(full_name_textBox2, "Имя");
        }

        bool full_name_textBox3_changed = false;
        private void Full_name_textBox3_Click(object sender, EventArgs e)
        {
            full_name_textBox3_changed = textbox.Check_is_cleared(full_name_textBox3, "Отчество", full_name_textBox3_changed);
        }

        private void Full_name_textBox3_TextChanged(object sender, EventArgs e)
        {
            full_name_textBox3_changed = textbox.Check_text_changed(full_name_textBox3, "Отчество");
        }

        private void Full_name_textBox3_Leave(object sender, EventArgs e)
        {
            textbox.Return_original_text(full_name_textBox3, "Отчество");
        }

        bool question_text_changed = false;
        private void Question_textBox_Click(object sender, EventArgs e)
        {
            question_text_changed = textbox.Check_is_cleared(question_textBox, "Введите текст вопроса", question_text_changed);
        }

        private void Question_textBox_TextChanged(object sender, EventArgs e)
        {
            question_text_changed = textbox.Check_text_changed(question_textBox, "Введите текст вопроса");
        }

        private void Question_textBox_Leave(object sender, EventArgs e)
        {
            textbox.Return_original_text(question_textBox, "Введите текст вопроса");
        }

        bool answer1_text_changed = false;
        private void Answer_textBox1_Click(object sender, EventArgs e)
        {
            answer1_text_changed = textbox.Check_is_cleared(answer_textBox1, "Введите ответ №1", answer1_text_changed);
        }

        private void Answer_textBox1_TextChanged(object sender, EventArgs e)
        {
            answer1_text_changed = textbox.Check_text_changed(answer_textBox1, "Введите ответ №1");
        }

        private void Answer_textBox1_Leave(object sender, EventArgs e)
        {
            textbox.Return_original_text(answer_textBox1, "Введите ответ №1");
        }

        bool answer3_text_changed = false;
        private void Answer_textBox3_Click(object sender, EventArgs e)
        {
            answer3_text_changed = textbox.Check_is_cleared(answer_textBox3, "Введите ответ №3", answer3_text_changed);
        }

        private void Answer_textBox3_TextChanged(object sender, EventArgs e)
        {
            answer3_text_changed = textbox.Check_text_changed(answer_textBox3, "Введите ответ №3");
        }

        private void Answer_textBox3_Leave(object sender, EventArgs e)
        {
            textbox.Return_original_text(answer_textBox3, "Введите ответ №3");
        }

        bool answer2_text_changed = false;
        private void Answer_textBox2_Click(object sender, EventArgs e)
        {
            answer2_text_changed = textbox.Check_is_cleared(answer_textBox2, "Введите ответ №2", answer2_text_changed);
        }

        private void Answer_textBox2_TextChanged(object sender, EventArgs e)
        {
            answer2_text_changed = textbox.Check_text_changed(answer_textBox2, "Введите ответ №2");
        }

        private void Answer_textBox2_Leave(object sender, EventArgs e)
        {
            textbox.Return_original_text(answer_textBox2, "Введите ответ №2");
        }

        bool answer4_text_changed = false;
        private void Answer_textBox4_Click(object sender, EventArgs e)
        {
            answer4_text_changed = textbox.Check_is_cleared(answer_textBox4, "Введите ответ №4", answer4_text_changed);
        }

        private void Answer_textBox4_TextChanged(object sender, EventArgs e)
        {
            answer4_text_changed = textbox.Check_text_changed(answer_textBox4, "Введите ответ №4");
        }

        private void Answer_textBox4_Leave(object sender, EventArgs e)
        {
            textbox.Return_original_text(answer_textBox4, "Введите ответ №4");
        }
        //*************запрет на открытие contextMenuStrip1 на других вкладках**************
        private void ContextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (contextmenustrip1_enabled)
                e.Cancel = false;
            else e.Cancel = true;
        }
        //*******************************?????********************************
        private void Choose_deleted_version_comboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (edit_method != 2)
                e.Handled = true;
        }
        //*************запрет на ввод(кроме цифр и клавиши BackSpace) в TextBox*******************************
        private void Edit_timer_textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!char.IsDigit(number) && number != 8)
                e.Handled = true;
        }

        private void New_timer_textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!char.IsDigit(number) && number != 8)
                e.Handled = true;
        }
        //*************запрет на ввод в comboBox*******************************
        private void Question_type_comboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void ComboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void Create_student_group_comboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void Choose_group_for_marks_comboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void Choose_student_group_for_marks_comboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void Choose_student_for_marks_comboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void Test_access_comboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void Groups_comboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void Open_version_in_test_comboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void Select_existing_question_comboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void Show_students_comboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void Available_teacher_tests_comboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void Choose_task_comboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void Edit_question_type_comboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
        //**************ограничение на нажатие Enter в многострочных TextBox*******************
        private void New_name_textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                e.Handled = true;
        }

        private void New_comment_textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                e.Handled = true;
        }

        private void Edit_test_name_textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                e.Handled = true;
        }

        private void Question_textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                e.Handled = true;
        }

        private void Answer_textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                e.Handled = true;
        }

        private void Answer_textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                e.Handled = true;
        }

        private void Answer_textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                e.Handled = true;
        }

        private void Answer_textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                e.Handled = true;
        }

        private void New_test_name_textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                e.Handled = true;
        }

        private void Comment_textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                e.Handled = true;
        }
    }
}

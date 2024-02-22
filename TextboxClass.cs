using System.Windows.Forms;

namespace testing_program
{
    class TextboxClass
    {
        public void Change_location(TextBox[] arr, int value1, int[] value2)
        {
            for (int i = 0; i < arr.Length; ++i)

                arr[i].Location = new System.Drawing.Point(value1, value2[i]);
        }

        public bool Check_is_cleared(TextBox textbox, string text, bool is_changed)
        {
            if (is_changed == false || textbox.Text == text)
            {
                textbox.Clear();
                return false;
            }
            return true;
        }

        public bool Return_original_text(TextBox textbox, string text)
        {
            if (textbox.Text == "")
            {
                textbox.Text = text;
                return true;
            }
            return false;
        }

        public void Fill(TextBox textBox, string text)
        {
            textBox.Text = text;
        }

        public void Fill(TextBox[] arr, string[] text)
        {
            for (int i = 0; i < arr.Length; ++i)
                arr[i].Text = text[i];
        }

        public bool Check_text_changed(TextBox textbox)
        {
            if (textbox.Text != "")
                return true;
            return false;
        }

        public bool Check_text_changed(TextBox textbox, string text)
        {
            if (textbox.Text != "" && textbox.Text != text)
                return true;
            return false;
        }

        public bool Check_text_changed(TextBox[] tArr)
        {
            for (int i = 0; i < tArr.Length; ++i)
            {
                if (tArr[i].Text == "")
                    return false;
            }
            return true;
        }

        public bool Check_text_changed(TextBox[] tArr, string[] sArr)
        {
            for (int i = 0; i < tArr.Length; ++i)
            {
                if (tArr[i].Text == "" || tArr[i].Text == sArr[i])
                    return false;
            }
            return true;
        }

        public bool Check_passwords_are_matching(string first, string second)
        {
            if (first != second)
            {
                MessageBox.Show("Пароли не совпадают");
                return false;
            }
            return true;
        }

        public void Change_visible(TextBox textBox, bool value)
        {
            textBox.Visible = value;
        }

        public void Change_visible(TextBox[] arr, bool value)
        {
            for (int i = 0; i < arr.Length; ++i)
                arr[i].Visible = value;
        }

        public void Clear(TextBox textBox)
        {
            textBox.Clear();
        }

        public void Clear(TextBox[] arr)
        {
            for (int i = 0; i < arr.Length; ++i)
                arr[i].Clear();
        }

        public bool Check_invalid_characters_with_space(TextBox textbox)
        {
            if (textbox.Text.Contains(" ") || !Check_invalid_characters(textbox, false))
            {
                MessageBox.Show(@"Некорректно заполнено поле. Запрещено использовать пробел и символы '\""", "Ошибка");
                return false;
            }
            return true;
        }

        public bool Check_invalid_characters_with_space(TextBox[] textboxes)
        {
            foreach (TextBox textbox in textboxes)
            {
                if (textbox.Text.Contains(" ") || !Check_invalid_characters(textbox, false))
                {
                    MessageBox.Show(@"Некорректно заполнено поле. Запрещено использовать пробел и символы '\""", "Ошибка");
                    return false;
                }
            }
            return true;
        }

        public bool Check_invalid_characters(TextBox textBox, bool message)
        {
            if (textBox.Text.Contains("'") ||  textBox.Text.Contains(@"\") || textBox.Text.Contains(@""""))
            {
                if(message)
                    MessageBox.Show(@"Некорректно заполнено поле. Запрещено использовать символы '\""", "Ошибка");
                return false;
            }
            return true;
        }

        public bool Check_invalid_characters(TextBox[] textBoxes, bool message)
        {
            foreach(TextBox box in textBoxes)
            {
                if (box.Text.Contains("'") || box.Text.Contains(@"\") || box.Text.Contains(@"""") || box.Text.Contains(";"))
                {
                    if (message)
                        MessageBox.Show(@"Некорректно заполнено поле. Запрещено использовать символы '\"";", "Ошибка");
                    return false;
                }
            }
            return true;
        }
    }
}

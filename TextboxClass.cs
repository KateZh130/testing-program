using System.Windows.Forms;

namespace testing_program
{
    class TextboxClass
    {


        public bool Check_text_is_changed(TextBox textbox, string text)
        {
            if (textbox.Text != "" && textbox.Text != text)
                return true;
            return false;
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
        public void Fill_textboxes(TextBox[] arr, string[] text)
        {
            for (int i = 0; i < arr.Length; ++i)
            {
                arr[i].Text = text[i];
            }
        }

        public bool Check_textboxes_text_are_changed(string[] sArr, TextBox[] tArr)
        {

            for (int i = 0; i < tArr.Length; ++i)
            {
                if (tArr[i].Text == "" || tArr[i].Text == sArr[i])
                {
                    return false;
                }
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
        public void Change_visible(TextBox[] arr, bool value)
        {
            for (int i = 0; i < arr.Length; ++i)
            {
                arr[i].Visible = value;
            }
        }
        public void Clear_textboxes(TextBox[] arr)
        {

            for (int i = 0; i < arr.Length; ++i)
            {
                arr[i].Clear();
            }
        }


    }
}

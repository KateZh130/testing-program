using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace testing_program
{
    class TextboxClass
    {
        

        public bool Check_textBox_text_is_changed(TextBox textbox, string text)
        {
            if (textbox.Text != "" && textbox.Text != text)
                return true;
            return false;
        }

        public bool Check_textBox_text_is_cleared(TextBox textbox, string text, bool is_changed)
        {
            if (is_changed == false || textbox.Text == text)
            {
                textbox.Clear();
                return false;
            }
            return true;
        }

        public void TextBox_return_original_text(TextBox textbox, string text)
        {
            if (textbox.Text == "")
                textbox.Text = text;
        }

        public bool Check_textboxes_are_filled_in(string[] sArr, TextBox[] tArr)
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

    }
}

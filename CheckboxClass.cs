using System.Collections.Generic;
using System.Windows.Forms;

namespace testing_program
{
    class CheckboxClass
    {
        public void Clear(CheckBox checkbox)
        {
            checkbox.Checked = false;
        }

        public void Fill_text(CheckBox[] checkBoxes, List<string> text)
        {
            for (int i = 0; i < checkBoxes.Length; ++i)
                checkBoxes[i].Text = text[i];
        }

        public void Clear_checkboxes(CheckBox[] checkBoxes)
        {
            for (int i = 0; i < checkBoxes.Length; ++i)
                checkBoxes[i].Checked = false;
        }
    }
}

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

        public void Clear(CheckBox[] checkBoxes)
        {
            for (int i = 0; i < checkBoxes.Length; ++i)
                checkBoxes[i].Checked = false;
        }

        public void Fill_text(CheckBox[] checkBoxes, List<string> text)
        {
            for (int i = 0; i < checkBoxes.Length; ++i)
                checkBoxes[i].Text = text[i];
        }

        public void Change_location(CheckBox checkbox, int value1, int value2)
        {
            checkbox.Location = new System.Drawing.Point(value1, value2);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace testing_program
{
    class RadiobuttonClass
    {
        public void Fill_text(RadioButton[] radioButtons, List<string> text)
        {
            for (int i = 0; i < radioButtons.Length; ++i)
            {
                radioButtons[i].Text = text[i];
            }
        }
    }
   
}

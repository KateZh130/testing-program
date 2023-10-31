using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace testing_program
{
    class GroupboxPanelClass
    {
        public void Change_visible(Panel[] arr, bool value)
        {
            for (int i = 0; i < arr.Length; ++i)
                arr[i].Visible = value;
        }
        public void Change_visible(Panel panel, bool value)
        {
            panel.Visible = value;
        }
        public void Change_visible(Panel[] arr, bool[] value)
        {
            for (int i = 0; i < arr.Length; ++i)
                arr[i].Visible = value[i];
        }
        public void Change_visible(GroupBox box, bool value)
        {
            box.Visible = value;
        }
        public void Change_visible(GroupBox[] arr, bool[] value)
        {
            for (int i = 0; i < arr.Length; ++i)
                arr[i].Visible = value[i];
        }
        public void Change_size(Panel panel, int value1, int value2)
        {
            panel.Size = new System.Drawing.Size(value1,value2);
        }
        public void Change_location(Panel panel, int value1, int value2)
        {
            panel.Location = new System.Drawing.Point(value1, value2);
        }
    }
}

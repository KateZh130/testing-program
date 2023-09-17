using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace testing_program
{
    class ComboboxClass
    {
        public void Clear_selection(ComboBox combobox)
        {
            combobox.SelectedIndex = -1;
        }
        public void Delete_collection(ComboBox combobox)
        {
            combobox.Items.Clear();
        }
        public void Add_item(ComboBox combobox, string item)
        {
            combobox.Items.Add(item);
        }
        public void Change_visible(ComboBox combobox, bool value)
        {
            combobox.Visible = value;
        }
    }
}

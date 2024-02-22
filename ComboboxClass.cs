using System.Windows.Forms;


namespace testing_program
{
    class ComboboxClass
    {
        public void Clear_selection(ComboBox combobox)
        {
            combobox.SelectedIndex = -1;
        }

        public void Add_item(ComboBox combobox, string item)
        {
            combobox.Items.Add(item);
        }

        public void Delete_collection(ComboBox combobox)
        {
            combobox.Items.Clear();
        }

        public void Delete_collection(ComboBox[] arr)
        {
            for (int i = 0; i < arr.Length; ++i)
            {
                arr[i].Items.Clear();
            }
        }

        public void Return_original_text(ComboBox combobox, string text)
        {
            Clear_selection(combobox);
            combobox.Text = text;
        }

        public void Return_original_text(ComboBox[] arr, string[] text)
        {
            for (int i = 0; i < arr.Length; ++i)
            {
                Clear_selection(arr[i]);
                arr[i].Text = text[i];
            }    
        }

        public void Change_visible(ComboBox comboBox, bool value)
        {
            comboBox.Visible = value;
        }

        public void Change_visible(ComboBox[] arr, bool value)
        {
            for (int i = 0; i < arr.Length; ++i)
                arr[i].Visible = value;
        }

        public bool Check_selected_index_changed(ComboBox combobox)
        {
            if (combobox.SelectedIndex == -1)
                return false;
            return true;
        }

        public bool Check_selected_index_changed(ComboBox[] arr)
        {
            for (int i = 0; i < arr.Length; ++i)
            {
                if (arr[i].SelectedIndex == -1)
                    return false;
            }
            return true;
        }
    }
}

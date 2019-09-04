using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SyngrafeasApp
{
    /// <summary>
    /// Interaction logic for AddPersonWindow.xaml
    /// </summary>

    public partial class AddPersonWindow : Window
    {
        public AddPersonWindow(List<string> cmbbox, List<string> lstbox)
        {
            InitializeComponent();
            listBox = new List<string>();
            IDstrings = new List<string>();
            listBox = lstbox;
            IDstrings = cmbbox;
            ContentListBox.ItemsSource = listBox;
            ChoosenComboBox.ItemsSource = IDstrings;
        }

        public List<string> Names;//все персонажи
        public List<string> listBox;//содержание ListBox
        public List<string> IDstrings;//содержание ComboBox
        public List<int> IDs;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (ChoosenComboBox.SelectedIndex > -1)
            {
                int index = ChoosenComboBox.SelectedIndex;
                string currName = ChoosenComboBox.Items[index].ToString();
                listBox.Add(currName);
                ContentListBox.Items.Refresh();
                IDs.Add(Names.IndexOf(currName));
                IDstrings.Remove(currName);
                ChoosenComboBox.Items.Refresh();
                ChoosenComboBox.SelectedIndex = -1;
            }
        }

        private void AcceptButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}

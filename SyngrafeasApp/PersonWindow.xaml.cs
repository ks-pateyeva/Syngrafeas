using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// Interaction logic for PersonWindow.xaml
    /// </summary>
    public partial class PersonWindow : Window
    {
        public PersonWindow()
        {
            InitializeComponent();
            InputLanguageManager.Current.CurrentInputLanguage = new CultureInfo("ru-RU");
        }

        public string ImagePath;
        public List<string> currEvents;
        public List<string> personEvents;
        public List<string> currChapters;
        public List<string> personChapters;

        public Person GetPerson()
        {
            List<int> personchapters = new List<int>();
            for (int i = 0; i < personChapters.Count; i++)
            {
                personchapters.Add(currChapters.IndexOf(personChapters[i]));
            }

            List<int> personevents = new List<int>();
            for (int i = 0; i < personevents.Count; i++)
            {
                personevents.Add(currEvents.IndexOf(personEvents[i]));
            }

            Person person = new Person(NameTextBox.Text, NicknameTextBox.Text, DescriptionTextBox.Text, 
                BiographyTextBox.Text, AdditionalTextBox.Text, personchapters, personevents, ImagePath);
            return person;
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.project.persons[MainWindow.selected] = GetPerson();
            Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Графические изображения |*.bmp; *.jpeg; *.png; *.jpg"
            };
            if (openFileDialog.ShowDialog() == true)
            {
                ImagePath = openFileDialog.FileName;
                ImageSource imageSource = new BitmapImage(new Uri(ImagePath));
                PersonImage.Source = imageSource;
            }  
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Owner.IsEnabled = true;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            List<string> ToAddEvents = new List<string>();
            for (int i = 0; i < currEvents.Count; i++)
            {
                if (personEvents.IndexOf(currEvents[i]) == -1)
                {
                    ToAddEvents.Add(currEvents[i]);
                }
            }

            AddPersonWindow addPersonWindow = new AddPersonWindow(ToAddEvents, personEvents);
            addPersonWindow.Owner = this;
            addPersonWindow.IDs = new List<int>();
            addPersonWindow.Names = new List<string>();
            addPersonWindow.Names = currEvents;
            if (addPersonWindow.ShowDialog() == true)
            {

            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            List<string> ToAddChapters = new List<string>();
            for (int i = 0; i < currChapters.Count; i++)
            {
                if (personChapters.IndexOf(currChapters[i]) == -1)
                {
                    ToAddChapters.Add(currChapters[i]);
                }
            }

            AddPersonWindow addPersonWindow = new AddPersonWindow(ToAddChapters, personChapters);
            addPersonWindow.Owner = this;
            addPersonWindow.IDs = new List<int>();
            addPersonWindow.Names = new List<string>();
            addPersonWindow.Names = currChapters;
            if (addPersonWindow.ShowDialog() == true)
            {

            }
        }
    }
}

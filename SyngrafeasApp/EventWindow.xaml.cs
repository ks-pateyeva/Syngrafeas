using Microsoft.Win32;
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
    /// Interaction logic for EventWindow.xaml
    /// </summary>
    public partial class EventWindow : Window
    {
        public EventWindow()
        {
            InitializeComponent();
        }

        public string ImagePath;
        public List<string> currPeople;
        public List<string> eventPeople;
        public List<string> currPlaces;
        public List<string> eventPlaces;
        public List<string> currChapters;
        public List<string> eventChapters;

        public Event GetEvent()
        {
            List<int> eventPersons = new List<int>();
            for(int i = 0; i < eventPeople.Count; i++)
            {
                eventPersons.Add(currPeople.IndexOf(eventPeople[i]));
            }
            List<int> eventplaces = new List<int>();
            for (int i = 0; i < eventPlaces.Count; i++)
            {
                eventplaces.Add(currPlaces.IndexOf(eventPlaces[i]));
            }

            List<int> eventchapters = new List<int>();
            for (int i = 0; i < eventChapters.Count; i++)
            {
                eventchapters.Add(currChapters.IndexOf(eventChapters[i]));
            }

            Event _event = new Event(NameTextBox.Text, TimeTextBox.Text, 
                DescriptionTextBox.Text, ImagePath, eventPersons, eventplaces, eventchapters);
            return _event;
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        { 
            MainWindow.project.events[MainWindow.selected] = GetEvent();
            Close();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Owner.IsEnabled = true;
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
                EventImage.Source = imageSource;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            List<string> ToAddNames = new List<string>();
            for(int i = 0; i < currPeople.Count; i++)
            {
                if (eventPeople.IndexOf(currPeople[i]) == -1)
                {
                    ToAddNames.Add(currPeople[i]);
                }
            }
            
            AddPersonWindow addPersonWindow = new AddPersonWindow(ToAddNames, eventPeople);
            addPersonWindow.Owner = this;
            addPersonWindow.IDs = new List<int>();
            addPersonWindow.Names = new List<string>();
            addPersonWindow.Names = currPeople;
            if(addPersonWindow.ShowDialog() == true)
            {
                
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            List<string> ToAddPlaces = new List<string>();
            for (int i = 0; i < currPlaces.Count; i++)
            {
                if (eventPlaces.IndexOf(currPlaces[i]) == -1)
                {
                    ToAddPlaces.Add(currPlaces[i]);
                }
            }

            AddPersonWindow addPersonWindow = new AddPersonWindow(ToAddPlaces, eventPlaces);
            addPersonWindow.Owner = this;
            addPersonWindow.IDs = new List<int>();
            addPersonWindow.Names = new List<string>();
            addPersonWindow.Names = currPlaces;
            if (addPersonWindow.ShowDialog() == true)
            {

            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            List<string> ToAddChapters = new List<string>();
            for (int i = 0; i < currChapters.Count; i++)
            {
                if (eventPlaces.IndexOf(currChapters[i]) == -1)
                {
                    ToAddChapters.Add(currChapters[i]);
                }
            }

            AddPersonWindow addPersonWindow = new AddPersonWindow(ToAddChapters, eventChapters);
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

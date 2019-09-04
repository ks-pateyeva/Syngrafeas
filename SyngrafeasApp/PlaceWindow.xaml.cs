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
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class PlaceWindow : Window
    {
        public PlaceWindow()
        {
            InitializeComponent();
        }

        public string ImagePath;
        public List<string> currEvents;
        public List<string> placeEvents;
        public List<string> currChapters;
        public List<string> placeChapters;

        public Place GetPlace()
        {
            List<int> personchapters = new List<int>();
            for (int i = 0; i < placeChapters.Count; i++)
            {
                personchapters.Add(currChapters.IndexOf(placeChapters[i]));
            }

            List<int> personevents = new List<int>();
            for (int i = 0; i < personevents.Count; i++)
            {
                personevents.Add(currEvents.IndexOf(placeEvents[i]));
            }

            Place place = new Place(NameTextBox.Text, DescriptionTextBox.Text, ImagePath);
            return place;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Owner.IsEnabled = true;
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.project.places[MainWindow.selected] = GetPlace();
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
                PlaceImage.Source = imageSource;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            List<string> ToAddEvents = new List<string>();
            for (int i = 0; i < currEvents.Count; i++)
            {
                if (placeEvents.IndexOf(currEvents[i]) == -1)
                {
                    ToAddEvents.Add(currEvents[i]);
                }
            }

            AddPersonWindow addPersonWindow = new AddPersonWindow(ToAddEvents, placeEvents);
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
                if (placeChapters.IndexOf(currChapters[i]) == -1)
                {
                    ToAddChapters.Add(currChapters[i]);
                }
            }

            AddPersonWindow addPersonWindow = new AddPersonWindow(ToAddChapters, placeChapters);
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

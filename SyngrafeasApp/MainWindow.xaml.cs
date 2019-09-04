using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using System.Globalization;

namespace SyngrafeasApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            InputLanguageManager.Current.CurrentInputLanguage = new CultureInfo("ru-RU");
        }

        public static ProjectFile project;
        public static int selected = -1;

        private void CreateProject()
        {
            project = new ProjectFile();
        }

        public void RefreshTables()
        {
            List<PersonInfo> personsInfo = new List<PersonInfo>();
            for (int i = 0; i < project.persons.Count; i++)
            {
                personsInfo.Add(new PersonInfo
                {
                    Name = project.persons[i].Nickname,
                    Description = project.persons[i].Description,
                    Chapters = project.persons[i].chapterID.Count,
                    Events = project.persons[i].chapterID.Count
                });
            }
            List<PlaceInfo> placesInfo = new List<PlaceInfo>();
            for (int i = 0; i < project.places.Count; i++)
            {
                placesInfo.Add(new PlaceInfo
                {
                    Name = project.places[i].Name,
                    Description = project.places[i].Description,
                    Chapters = project.places[i].chapterID.Count,
                    Events = project.places[i].eventID.Count
                });
            }
            List<EventInfo> eventsInfo = new List<EventInfo>();
            for (int i = 0; i < project.events.Count; i++)
            {
                eventsInfo.Add(new EventInfo
                {
                    Name = project.events[i].Name,
                    Time = project.events[i].Time,
                    Description = project.events[i].Description,
                    Chapters = project.events[i].chapterID.Count,
                    Persons = project.events[i].personID.Count,
                    Places = project.events[i].placeID.Count
                });
            }
            personsDataGrid.ItemsSource = personsInfo;
            placesDataGrid.ItemsSource = placesInfo;
            eventsDataGrid.ItemsSource = eventsInfo;
        }

        private void OpenProject()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Syngrafeas Project (*.syfs)|*.syfs"
            };
            if (openFileDialog.ShowDialog() == true)
            {
                project = new ProjectFile(openFileDialog.FileName);
                project.LoadProject();
                mainWindow.Title = "Syngrafeas - " + project.ProjectName;
                NotesTextBox.Clear();
                for (int i = 0; i < project.notes.Count; i++)
                {
                    NotesTextBox.Text += project.notes[i] + Environment.NewLine;
                }

                RefreshTables();    
                
                for (int i = 0; i < project.parts.Count; i++)
                {
                    TreeViewItem tvItem = new TreeViewItem
                    {
                        Header = project.parts[i].Name
                    };
                    for (int j = 0; j < project.parts[i].chapters.Count; j++)
                    {
                        TreeViewItem subtvItem = new TreeViewItem
                        {
                            Header = project.parts[i].chapters[j].Name
                        };
                        tvItem.Items.Add(subtvItem);
                    }
                    tvItem.IsExpanded = true;
                    ProjectTreeView.Items.Add(tvItem);
                }
            }
        }

        public class PersonInfo
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public int Chapters { get; set; }
            public int Events {  get; set; }
            
        }

        public class PlaceInfo
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public int Chapters { get; set; }
            public int Events { get; set; }
        }

        public class EventInfo
        {
            public string Name { get; set; }
            public string Time { get; set; }
            public string Description { get; set; }
            public int Chapters { get; set; }
            public int Persons { get; set; }
            public int Places { get; set; }
        }
  
        private void TextBlock_MouseEnter(object sender, MouseEventArgs e)
        {
            TextBlock_ClickToOpen.TextDecorations.Remove(TextDecorations.Underline[0]);
            TextBlock_ClickToOpen.TextDecorations.Add(TextDecorations.Underline[0]);
        }

        private void TextBlock_ClickToOpen_MouseLeave(object sender, MouseEventArgs e)
        {
            TextBlock_ClickToOpen.TextDecorations.Remove(TextDecorations.Underline[0]);
        }

        private void MenuItemOpen_Click(object sender, RoutedEventArgs e)
        {
            OpenProject();
            ProjectTreeView.Visibility = Visibility.Visible;
        }

        private void MenuItemNew_Click(object sender, RoutedEventArgs e)
        {
            BitmapImage bi3 = new BitmapImage();
            bi3.BeginInit();
            bi3.UriSource = new Uri("Resources/plus.png", UriKind.Relative);
            bi3.EndInit();
            ToDoImage.Stretch = Stretch.Fill;
            ToDoImage.Source = bi3;
            BackgroundRectangle.Fill = Brushes.White; 
            TextBlock_ClickToOpen.Text = "Нажмите, чтобы добавить раздел";
            CreateProject();
        }

        private void PersonsDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (personsDataGrid.Items.Count > 0) {
                PersonWindow personWindow = new PersonWindow();
                personWindow.Show();
                personWindow.Owner = this;
                IsEnabled = false;
                selected = personsDataGrid.SelectedIndex;
                if (selected < project.persons.Count)
                {
                    personWindow.NameTextBox.Text = project.persons[selected].FullName;
                    personWindow.NicknameTextBox.Text = project.persons[selected].Nickname;
                    personWindow.AdditionalTextBox.Text = project.persons[selected].Additional;
                    personWindow.DescriptionTextBox.Text = project.persons[selected].Description;
                    personWindow.BiographyTextBox.Text = project.persons[selected].Biography;
                    if (project.persons[selected].ImagePath != "")
                    {
                        ImageSource imageSource = new BitmapImage(new Uri(project.persons[selected].ImagePath));
                        personWindow.PersonImage.Source = imageSource;
                        personWindow.ImagePath = project.persons[selected].ImagePath;
                    }

                    personWindow.currEvents = new List<string>();
                    for (int i = 0; i < project.events.Count; i++)
                    {
                        personWindow.currEvents.Add(project.events[i].Name);
                    }
                    personWindow.personEvents = new List<string>();
                    for (int i = 0; i < project.persons[selected].eventID.Count; i++)
                    {
                        personWindow.personEvents.Add(project.events[project.persons[selected].eventID[i]].Name);
                    }

                    personWindow.currChapters = new List<string>();
                    for (int i = 0; i < project.parts[0].chapters.Count; i++)
                    {
                        personWindow.currChapters.Add(project.parts[0].chapters[i].Name);
                    }
                    personWindow.personChapters = new List<string>();
                    for (int i = 0; i < project.persons[selected].chapterID.Count; i++)
                    {
                        personWindow.personChapters.Add(project.parts[0].chapters[project.persons[selected].chapterID[i]].Name);
                    }

                }
            }

        }

        private void PlacesDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            PlaceWindow placeWindow = new PlaceWindow();
            placeWindow.Show();
            this.IsEnabled = false;
            placeWindow.Owner = this;
            selected = placesDataGrid.SelectedIndex;
            if (selected < project.places.Count)
            {
                placeWindow.NameTextBox.Text = project.places[selected].Name;
                placeWindow.DescriptionTextBox.Text = project.places[selected].Description;
                if (project.places[selected].ImagePath != null)
                {
                    ImageSource imageSource = new BitmapImage(new Uri(project.persons[selected].ImagePath));
                    placeWindow.PlaceImage.Source = imageSource;
                    placeWindow.ImagePath = project.places[selected].ImagePath;
                }

                placeWindow.currEvents = new List<string>();
                for (int i = 0; i < project.events.Count; i++)
                {
                    placeWindow.currEvents.Add(project.events[i].Name);
                }
                placeWindow.placeEvents = new List<string>();
                for (int i = 0; i < project.places[selected].eventID.Count; i++)
                {
                    placeWindow.placeEvents.Add(project.events[project.places[selected].eventID[i]].Name);
                }

                placeWindow.currChapters = new List<string>();
                for (int i = 0; i < project.parts[0].chapters.Count; i++)
                {
                    placeWindow.currChapters.Add(project.parts[0].chapters[i].Name);
                }
                placeWindow.placeChapters = new List<string>();
                for (int i = 0; i < project.places[selected].chapterID.Count; i++)
                {
                    placeWindow.placeChapters.Add(project.parts[0].chapters[project.places[selected].chapterID[i]].Name);
                }
            }
        }

        private void MiSave_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Syngrafeas Project (*.syfs)|*.syfs"
            };
            if ((saveFileDialog.ShowDialog() == true))
            {
                project.SaveProject(saveFileDialog.FileName);
            }
        }

        private void mainWindow_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (selected >= 0)
            {
                RefreshTables();
                selected = -1;
            }
        }

        private void eventsDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            EventWindow eventWindow = new EventWindow();
            eventWindow.Show();
            this.IsEnabled = false;
            eventWindow.Owner = this;
            selected = eventsDataGrid.SelectedIndex;
            if (selected < project.events.Count)
            {
                eventWindow.NameTextBox.Text = project.events[selected].Name;
                eventWindow.DescriptionTextBox.Text = project.events[selected].Description;
                eventWindow.TimeTextBox.Text = project.events[selected].Time;
                if (project.events[selected].ImagePath != null) 
                {
                    ImageSource imageSource = new BitmapImage(new Uri(project.persons[selected].ImagePath));
                    eventWindow.EventImage.Source = imageSource;
                    eventWindow.ImagePath = project.events[selected].ImagePath;
                }
                eventWindow.currPeople = new List<string>();
                for (int i = 0; i < project.persons.Count; i++) {
                    eventWindow.currPeople.Add(project.persons[i].Nickname);
                }
                eventWindow.eventPeople = new List<string>();
                for (int i = 0; i < project.events[selected].personID.Count; i++)
                {
                    eventWindow.eventPeople.Add(project.persons[project.events[selected].personID[i]].Nickname);
                }
                eventWindow.currPlaces = new List<string>();
                for (int i = 0; i < project.places.Count; i++)
                {
                    eventWindow.currPlaces.Add(project.places[i].Name);
                }
                eventWindow.eventPlaces = new List<string>();
                for (int i = 0; i < project.events[selected].placeID.Count; i++)
                {
                    eventWindow.eventPlaces.Add(project.places[project.events[selected].placeID[i]].Name);
                }

                eventWindow.currChapters = new List<string>();
                for (int i = 0; i < project.parts[0].chapters.Count; i++)
                {
                    eventWindow.currChapters.Add(project.parts[0].chapters[i].Name);
                }
                eventWindow.eventChapters = new List<string>();
                for (int i = 0; i < project.events[selected].chapterID.Count; i++)
                {
                    eventWindow.eventChapters.Add(project.parts[0].chapters[project.events[selected].chapterID[i]].Name);
                }
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            AddWindow addWindow = new AddWindow();
            if (addWindow.ShowDialog() == true)
            {
                Part currpart = new Part(addWindow.PartName.Text);
                project.parts.Add(currpart);
                currpart.ID = project.parts.Count;
                TreeViewItem treeViewItem = new TreeViewItem
                {
                    Header = project.parts[project.parts.Count - 1].Name
                };
                treeViewItem.IsExpanded = true;
                ProjectTreeView.Items.Add(treeViewItem);
                ProjectTreeView.Visibility = Visibility.Visible;
                if (project.parts.Count > 0)
                {
                    BackgroundRectangle.Visibility = Visibility.Hidden;
                    ToDoImage.Visibility = Visibility.Hidden;
                    TextBlock_ClickToOpen.Visibility = Visibility.Hidden;
                }
            }
        }

        private void TextBlock_ClickToOpen_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (TextBlock_ClickToOpen.Text == "Нажмите, чтобы открыть проект")
            {
                OpenProject();
                ProjectTreeView.Visibility = Visibility.Visible;
            }
            else
            {
                AddWindow addWindow = new AddWindow();
                if (addWindow.ShowDialog() == true)
                {
                    Part currpart = new Part(addWindow.PartName.Text);
                    project.parts.Add(currpart);
                    TreeViewItem treeViewItem = new TreeViewItem
                    {
                        Header = project.parts[project.parts.Count - 1].Name
                    };
                    treeViewItem.IsExpanded = true;
                    ProjectTreeView.Visibility = Visibility.Visible;
                    ProjectTreeView.Items.Add(treeViewItem);
                    if (project.parts.Count > 0)
                    {
                        BackgroundRectangle.Visibility = Visibility.Hidden;
                        ToDoImage.Visibility = Visibility.Hidden;
                        TextBlock_ClickToOpen.Visibility = Visibility.Hidden;
                    }
                }
            }
        }

        private void mainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

       
                string msg = "Файл не сохранен. Вы уверены, что хотите закрыть приложение?";
                MessageBoxResult result =
                  MessageBox.Show(
                    msg,
                    "Файл не сохранен",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);
                if (result == MessageBoxResult.No)
                {
                    e.Cancel = true;
                }
            
        }
    }
}

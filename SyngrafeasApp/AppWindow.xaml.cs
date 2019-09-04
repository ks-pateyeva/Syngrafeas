using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SyngrafeasApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class ApSyngrafeasApp : Application
    {
    }

    public class Person
    {
        public string FullName { get; set; }
        public string Nickname { get; set; }
        public string Description { get; set; }
        public string Biography { get; set; }
        public string Additional { get; set; }
        public string ImagePath { get; set; }
        public int ID { get; set; }

        public List<int> chapterID = new List<int>();
        public List<int> eventID = new List<int>();

        public void WritePerson(StreamWriter f)
        {
            f.WriteLine("\t</PERSON>");

            f.WriteLine("\t\t</ID>");
            f.Write("\t\t\t");
            f.WriteLine(ID);
            f.WriteLine("\t\t</ID>");
      
            f.WriteLine("\t\t</FULLNAME>");
            f.Write("\t\t\t");
            f.WriteLine(FullName);
            f.WriteLine("\t\t</FULLNAME>");

            f.WriteLine("\t\t</NICKNAME>");
            f.Write("\t\t\t");
            f.WriteLine(Nickname);
            f.WriteLine("\t\t</NICKNAME>");

            f.WriteLine("\t\t</DESCRIPTION>");
            f.Write("\t\t\t");
            f.WriteLine(Description);
            f.WriteLine("\t\t</DESCRIPTION>");

            f.WriteLine("\t\t</BIOGRAPHY>");
            f.Write("\t\t\t");
            f.WriteLine(Biography);
            f.WriteLine("\t\t</BIOGRAPHY>");

            f.WriteLine("\t\t</ADDITIONAL>");
            f.Write("\t\t\t");
            f.WriteLine(Additional);
            f.WriteLine("\t\t</ADDITIONAL>");

            f.WriteLine("\t\t</CHAPTERS>");
            foreach (int chapter in chapterID)
            {
                f.Write("\t\t\t");
                f.WriteLine(chapter);
            }
            f.WriteLine("\t\t</CHAPTERS>");

            f.WriteLine("\t\t</EVENTS>");
            foreach (int Event in eventID)
            {
                f.Write("\t\t\t");
                f.WriteLine(Event);
            }
            f.WriteLine("\t\t</EVENTS>");

            f.WriteLine("\t\t</IMAGE>");
            f.WriteLine(ImagePath);
            f.WriteLine("\t\t</IMAGE>");

            f.WriteLine("\t</PERSON>");
        }

        public Person(string fullname, string nickname, string description, string biography,
            string additional, List<int> chapters, List<int> events, string imagePath)
        {
            FullName = fullname;
            Nickname = nickname;
            Description = description;
            Biography = biography;
            Additional = additional;
            chapterID = chapters;
            eventID = events;
            ImagePath = imagePath;
        }
        public Person(StreamReader f)
        {
            string currLine, beginLine;
            while ((beginLine = f.ReadLine().Replace("\t", String.Empty)) != "</PERSON>")
            {
                while ((currLine = f.ReadLine().Replace("\t", String.Empty)) != beginLine)
                {
                    switch (beginLine)
                    {
                        case "</ID>":
                            ID = int.Parse(currLine);
                            break;
                        case "</FULLNAME>":
                            FullName += currLine;
                            break;
                        case "</NICKNAME>":
                            Nickname += currLine;
                            break;
                        case "</DESCRIPTION>":
                            Description += currLine;
                            break;
                        case "</BIOGRAPHY>":
                            Biography += currLine;
                            break;
                        case "</ADDITIONAL>":
                            Additional += currLine;
                            break;
                        case "</EVENTS>":
                            eventID.Add(int.Parse(currLine));
                            break;
                        case "</CHAPTERS>":
                            chapterID.Add(int.Parse(currLine));
                            break;
                        case "</IMAGE>":
                            ImagePath = currLine;
                            break;
                    }
                }
            } 
        }
    }

    public class Place
    {
        public string Name;
        public string Description;
        public int ID;
        public string ImagePath;

        public List<int> chapterID = new List<int>();
        public List<int> eventID = new List<int>();

        public void WritePlace(StreamWriter f)
        {
            f.WriteLine("\t</PLACE>");

            f.WriteLine("\t\t</ID>");
            f.Write("\t\t\t");
            f.WriteLine(ID);
            f.WriteLine("\t\t</ID>");

            f.WriteLine("\t\t</NAME>");
            f.Write("\t\t\t");
            f.WriteLine(Name);
            f.WriteLine("\t\t</NAME>");

            f.WriteLine("\t\t</DESCRIPTION>");
            f.Write("\t\t\t");
            f.WriteLine(Description);
            f.WriteLine("\t\t</DESCRIPTION>");

            f.WriteLine("\t\t</CHAPTERS>");
            foreach (int chapter in chapterID)
            {
                f.Write("\t\t\t");
                f.WriteLine(chapter);
            }
            f.WriteLine("\t\t</CHAPTERS>");

            f.WriteLine("\t\t</EVENTS>");
            foreach (int Event in eventID)
            {
                f.Write("\t\t\t");
                f.WriteLine(Event);
            }
            f.WriteLine("\t\t</EVENTS>");

            f.WriteLine("\t\t</IMAGE>");
            f.WriteLine(ImagePath);
            f.WriteLine("\t\t</IMAGE>");

            f.WriteLine("\t</PLACE>");
        }

        public Place(string name, string description, string imagePath)
        {
            Name = name;
            Description = description;
            ImagePath = imagePath;
        }
        public Place(StreamReader f)
        {
            string currLine, beginLine;
            while ((beginLine = f.ReadLine().Replace("\t", String.Empty)) != "</PLACE>")
            {
                while ((currLine = f.ReadLine().Replace("\t", String.Empty)) != beginLine)
                {
                    switch (beginLine)
                    {
                        case "</ID>":
                            ID = int.Parse(currLine);
                            break;
                        case "</NAME>":
                            Name += currLine;
                            break;
                        case "</DESCRIPTION>":
                            Description += currLine;
                            break;
                        case "</EVENTS>":
                            eventID.Add(int.Parse(currLine));
                            break;
                        case "</CHAPTERS>":
                            chapterID.Add(int.Parse(currLine));
                            break;
                        case "</IMAGE>":
                            ImagePath = currLine;
                            break;
                    }
                }
            }
        }
    }

    public class Chapter
    {
        public List<int> personID = new List<int>();
        public List<int> placeID = new List<int>();
        public List<int> eventID = new List<int>();

        public string filePath;
        public string Name;

        public void WriteChapter(StreamWriter f)
        {
            f.WriteLine("\t\t\t</CHAPTER>");

            f.WriteLine("\t\t\t\t</NAME>");
            f.Write("\t\t\t\t\t");
            f.WriteLine(Name);
            f.WriteLine("\t\t\t\t</NAME>");

            f.WriteLine("\t\t\t\t</FILE>");
            f.Write("\t\t\t\t\t");
            f.WriteLine(filePath);
            f.WriteLine("\t\t\t\t</FILE>");

            f.WriteLine("\t\t\t\t</EVENTS>");
            foreach (int Event in eventID)
            {
                f.Write("\t\t\t\t\t");
                f.WriteLine(Event);
            }
            f.WriteLine("\t\t\t\t</EVENTS>");

            f.WriteLine("\t\t\t</CHAPTER>");
        }

        public Chapter(StreamReader f)
        {
            string currLine, beginLine;
            while ((beginLine = f.ReadLine().Replace("\t", String.Empty)) != "</CHAPTER>")
            {
                while ((currLine = f.ReadLine().Replace("\t", String.Empty)) != beginLine)
                {
                    switch (beginLine)
                    {
                        case "</NAME>":
                            Name += currLine;
                            break;
                        case "</PERSONS>":
                            personID.Add(int.Parse(currLine));
                            break;
                        case "</PLACES>":
                            placeID.Add(int.Parse(currLine));
                            break;
                        case "</EVENTS>":
                            eventID.Add(int.Parse(currLine));
                            break;
                        case "</FILE>":
                            filePath = currLine;
                            break;
                    }
                }
            }
        }
    }

    public class Part
    {
        public string Name;
        public int ID;
        public List<Chapter> chapters = new List<Chapter>();

        public void WritePart(StreamWriter f)
        {
            f.WriteLine("\t</PART>");

            f.WriteLine("\t\t</ID>");
            f.Write("\t\t\t");
            f.WriteLine(ID);
            f.WriteLine("\t\t</ID>");

            f.WriteLine("\t\t</NAME>");
            f.Write("\t\t\t");
            f.WriteLine(Name);
            f.WriteLine("\t\t</NAME>");

            f.WriteLine("\t\t</CHAPTERS>");        
            foreach (Chapter chapter in chapters)
            {
                chapter.WriteChapter(f);
            }
            f.WriteLine("\t\t</CHAPTERS>");

            f.WriteLine("\t</PART>");
        }

        public Part(string name)
        {
            Name = name;
        }

        public Part(StreamReader f)
        {
            string currLine, beginLine;
            while ((beginLine = f.ReadLine().Replace("\t", String.Empty)) != "</PART>")
            {
                while ((currLine = f.ReadLine().Replace("\t", String.Empty)) != beginLine)
                {
                    switch (beginLine)
                    {
                        case "</ID>":
                            ID = int.Parse(currLine);
                            break;
                        case "</NAME>":
                            Name += currLine;
                            break;
                        case "</CHAPTERS>":
                            Chapter currChapter = new Chapter(f);
                            chapters.Add(currChapter);
                            break;

                    }
                }
            }
        }
    }

    public class Event
    {
        public string Name;
        public string Time;
        public string Description;
        public int ID;
        public string ImagePath;

        public List<int> personID = new List<int>();
        public List<int> placeID = new List<int>();
        public List<int> chapterID = new List<int>();

        public void WriteEvent(StreamWriter f)
        {
            f.WriteLine("\t</EVENT>");

            f.WriteLine("\t\t</ID>");
            f.Write("\t\t\t");
            f.WriteLine(ID);
            f.WriteLine("\t\t</ID>");

            f.WriteLine("\t\t</NAME>");
            f.Write("\t\t\t");
            f.WriteLine(Name);
            f.WriteLine("\t\t</NAME>");

            f.WriteLine("\t\t</TIME>");
            f.Write("\t\t\t");
            f.WriteLine(Time);
            f.WriteLine("\t\t</TIME>");

            f.WriteLine("\t\t</DESCRIPTION>");
            f.Write("\t\t\t");
            f.WriteLine(Description);
            f.WriteLine("\t\t</DESCRIPTION>");

            f.WriteLine("\t\t</PERSONS>");
            foreach (int person in personID)
            {
                f.Write("\t\t\t");
                f.WriteLine(person);
            }
            f.WriteLine("\t\t</PERSONS>");

            f.WriteLine("\t\t</PLACES>");
            foreach (int place in placeID)
            {
                f.Write("\t\t\t");
                f.WriteLine(place);
            }
            f.WriteLine("\t\t</PLACES>");

            f.WriteLine("\t\t</CHAPTERS>");
            foreach (int chapter in chapterID)
            {
                f.Write("\t\t\t");
                f.WriteLine(chapter);
            }
            f.WriteLine("\t\t</CHAPTERS>");

            f.WriteLine("\t\t</IMAGE>");
            f.WriteLine(ImagePath);
            f.WriteLine("\t\t</IMAGE>");

            f.WriteLine("\t</EVENT>");
        }

        public Event(string name, string time, string description,
            string imagePath, List<int> persons, List<int> places, List<int> chapters)
        {
            Name = name;
            Time = time;
            Description = description;
            ImagePath = imagePath;
            personID = persons;
            placeID = places;
            chapterID = chapters; 
        }
        public Event(StreamReader f)
        {
            string currLine, beginLine;
            while ((beginLine = f.ReadLine().Replace("\t", String.Empty)) != "</EVENT>")
            {
                while ((currLine = f.ReadLine().Replace("\t", String.Empty)) != beginLine)
                {
                    switch (beginLine)
                    {
                        case "</ID>":
                            ID = int.Parse(currLine);
                            break;
                        case "</NAME>":
                            Name += currLine;
                            break;
                        case "</TIME>":
                            Time += currLine;
                            break;
                        case "</DESCRIPTION>":
                            Description += currLine;
                            break;
                        case "</PERSONS>":
                            personID.Add(int.Parse(currLine));
                            break;
                        case "</PLACES>":
                            placeID.Add(int.Parse(currLine));
                            break;
                        case "</CHAPTERS>":
                            chapterID.Add(int.Parse(currLine));
                            break;
                        case "</IMAGE>":
                            ImagePath = currLine;
                            break;
                    }
                }
            }
        }
    }


    public class ProjectFile
    {
        private string projectName;
        private StreamReader file;
        public bool IsSaved;

        public ProjectFile(string path)
        {
            file = File.OpenText(path);
            projectName = Path.GetFileName(path);
        }

        public ProjectFile() {

        }
         
        public string ProjectName
        {
            get
            {
                return projectName;
            }
        }

        public List<string> notes = new List<string>();
        public List<Person> persons = new List<Person>();
        public List<Place> places = new List<Place>();
        public List<Event> events = new List<Event>();
        public List<Part> parts = new List<Part>();

        public void LoadProject()
        {
            string beginLine;
            string currLine;
            while ((beginLine = file.ReadLine()) != null)
            {
                while((currLine = file.ReadLine()) != beginLine)
                {
                    switch (beginLine)
                    {
                        case "</NOTES>":
                            notes.Add(currLine);
                         break;
                        case "</PERSONS>":
                            Person currPerson = new Person(file);
                            persons.Add(currPerson);
                            break;
                        case "</PLACES>":
                            Place currPlace = new Place(file);
                            places.Add(currPlace);
                            break;
                        case "</EVENTS>":
                            Event currEvent = new Event(file);
                            events.Add(currEvent);
                            break;
                        case "</PARTS>":
                            Part currPart = new Part(file);
                            parts.Add(currPart);
                            break;
                    }
                }
            }
            file.Close();
            IsSaved = true;
        }

        public void SaveProject(string path)
        {
            using (StreamWriter f = new StreamWriter(path, false, System.Text.Encoding.Unicode))
            {
                f.WriteLine("</NOTES>");
                foreach (string note in notes)
                {
                    f.WriteLine(note);
                }
                f.WriteLine("</NOTES>");
                f.WriteLine("</PERSONS>");
                foreach (Person person in persons)
                {
                    person.WritePerson(f);
                }
                f.WriteLine("</PERSONS>");

                f.WriteLine("</PLACES>");
                foreach(Place place in places)
                {
                    place.WritePlace(f);
                }
                f.WriteLine("</PLACES>");

                f.WriteLine("</EVENTS>");
                foreach (Event Event in events)
                {
                    Event.WriteEvent(f);
                }
                f.WriteLine("</EVENTS>");

                f.WriteLine("</PARTS>");
                foreach (Part part in parts)
                {
                    part.WritePart(f);
                }
                f.WriteLine("</PARTS>");

                f.Close();
            }  
        }

        public void CreateProject()
        {
            IsSaved = false;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Globalization;

namespace internship_3_oop_intro
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var data = new Dictionary<Event, List<Person>>()
            {
                {new Event("Jutarnja kava", 0, new DateTime(2020, 12, 16, 7, 00, 00), new DateTime(2020, 12, 16, 9, 00, 00)), new List<Person>()
                {
                    new Person("Marko", "Markic", 183568, 0984532788),
                    new Person("Mirko", "Mirkic", 879554, 0982549908),
                    new Person("Marica", "Maric", 183568, 0987654321)
                }},
                {new Event("Vecernji koncert", (Event.EventType)2, new DateTime(2020, 12, 17, 20, 30, 00), new DateTime(2020, 12, 17, 23, 00, 00)), new List<Person>()
                {
                    new Person("Lada", "Ladic", 569801, 0919870439),
                    new Person("Lino", "Linic", 903426, 0919889543),
                    new Person("Lana", "Lanic", 908786, 0918987952)
                }},
                {new Event("Popodnevno predavanje", (Event.EventType)1, new DateTime(2020, 12, 20, 14, 15, 00), new DateTime(2020, 12, 20, 19, 05, 00)), new List<Person>()
                {
                    new Person("Zvone", "Zvonic", 468956, 0956666666),
                    new Person("Zvonko", "Zvonkic", 5890674, 0958767000),
                    new Person("Zvonka", "Zvonkic", 875567, 0952468654)
                }}
            };

            AddEvent(data);
            DeleteEvent(data);
            EditEvent(data);
            AddPersonToEvent(data);
            DeletePerson(data);
            Menu(data);
        }

        static void AddEvent(Dictionary<Event, List<Person>> dictionary)
        {
            Console.WriteLine("Unesite ime novog dogadaja.");
            var addName = Console.ReadLine();
            AddNameCheck(addName, dictionary);

            Console.WriteLine("Unesite tip dogadaja.  Coffe/Lecture/Concert/StudySession");
            var addType = TypeCheck(Console.ReadLine());

            Console.WriteLine("Unesite vrijeme pocetka dogadaja u obliku mjesec/dan/godina sat:minuta:sekunda");
            var addStartTime = TimeCheck(Console.ReadLine());

            Console.WriteLine("Unesite vrijeme kraja dogadaja u obliku mjesec/dan/godina sat:minuta:sekunda");
            var addEndTime = TimeCheck(Console.ReadLine());

            dictionary.Add(new Event(addName, (Event.EventType)addType, addStartTime, addEndTime), new List<Person>());
        }

        static void DeleteEvent(Dictionary<Event, List<Person>> dictionary)
        {
            Console.WriteLine("Unesite ime dogadaja koji zelite izbrisati.");
            var deleteName = Console.ReadLine();
            NameCheck(deleteName, dictionary);
        }

        static void EditEvent(Dictionary<Event, List<Person>> dictionary)
        {
            Console.WriteLine("Sto zelite promijeniti u dogadaju?   ime/tip/pocetak/kraj");
            var edit = Console.ReadLine();
            EditCheck(edit, dictionary);
        }

        static void AddPersonToEvent(Dictionary<Event, List<Person>> dictionary)
        {
            Console.WriteLine("Na koji dogadaj zelite dodati osobu?");
            var eventName = Console.ReadLine();
            int counter = 0;
            while (counter < 1)
            {
                foreach (var pair in dictionary)
                {
                    bool compare = pair.Key.Name.Equals(eventName, StringComparison.OrdinalIgnoreCase);
                    while (compare)
                    {
                        Console.WriteLine("Unesite ime osobe koju zelite dodati na dogadaj.");
                        var firstName = Console.ReadLine();
                        Console.WriteLine("Unesite prezime osobe koju zelite dodati na dogadaj.");
                        var lastName = Console.ReadLine();
                        Console.WriteLine("Unesite OIB osobe koju zelite dodati na dogadaj.");
                        int oib = int.Parse(Console.ReadLine());
                        OibCheck(oib, eventName, dictionary);
                        Console.WriteLine("Unesite broj telefona osobe koju zelite dodati na dogadaj.");
                        int phoneNumber = int.Parse(Console.ReadLine());
                        pair.Value.Add(new Person(firstName, lastName, oib, phoneNumber));
                    }
                }
                counter = 1;
            }
        }

        static void DeletePerson(Dictionary<Event, List<Person>> dictionary)
        {
            Console.WriteLine("Unesite ime eventa s kojeg zelite ukloniti osobu:");
            string eventName = Console.ReadLine();
            Console.WriteLine("Unesite oib osobe koju želite ukloniti: ");
            int oib = int.Parse(Console.ReadLine());
            int counter = 0;
            while (counter < 1)
            {
                foreach (var pair in dictionary)
                {
                    bool compare = pair.Key.Name.Equals(eventName, StringComparison.OrdinalIgnoreCase);
                    while (compare)
                    {
                        for (int i = 0; i < pair.Value.Count; i++)
                        {
                            if (pair.Value[i].OIB == oib)
                            {
                                pair.Value.Remove(pair.Value[i]);
                            }
                        }
                        counter = 0;
                    }
                }
                counter = 1;
            }
        }

        static void AddNameCheck(string name, Dictionary<Event, List<Person>> dictionary)
        {
            int counter = 0;
            while (counter < 1)
            {
                foreach (var pair in dictionary)
                {
                    bool compare = pair.Key.Name.Equals(name, StringComparison.OrdinalIgnoreCase);
                    while (compare)
                    {
                        Console.WriteLine("Tako imenovan dogadaj vec postoji");
                        name = Console.ReadLine();
                        compare = pair.Key.Name.Equals(name, StringComparison.OrdinalIgnoreCase);
                        counter = 0;
                    }
                }
                counter = 1;
            }
        }

        static DateTime TimeCheck(string time)
        {
            CultureInfo hrHR = new CultureInfo("hr-HR");
            DateTime dateValue;
            if (DateTime.TryParseExact(time, "M/dd/yyyy HH:mm:ss", hrHR, DateTimeStyles.None, out dateValue))
            {
                Console.WriteLine("Unijeli ste datum {0}.", dateValue);
            }
            else
            {
                Console.WriteLine("'{0}' nije prihvatljiv zapis datuma.\nUnesite vrijeme pocetka dogadaja u obliku mjesec/dan/godina sat:minuta:sekunda", time);
                TimeCheck(time);
            }
            return dateValue;
        }

        static int TypeCheck(string type)
        {
            if (type.ToLower() == "coffee")
            {
                return 0;
            }
            else if (type.ToLower() == "lecture")
            {
                return 1;
            }
            else if (type.ToLower() == "concert")
            {
                return 2;
            }
            else if (type.ToLower() == "studysession")
            {
                return 3;
            }
            else
            {
                Console.WriteLine("Pogresno ste unijeli tip.\nUnesite tip dogadaja.  Coffe/Lecture/Concert/StudySession");
                return TypeCheck(Console.ReadLine());
            }
        }

        static object NameCheck(string name, Dictionary<Event, List<Person>> dictionary)
        {
            int counter = 0;
            while (counter < 1)
            {
                foreach (var pair in dictionary)
                {
                    bool compare = pair.Key.Name.Equals(name, StringComparison.OrdinalIgnoreCase);
                    while (compare)
                    {
                        dictionary.Remove(pair.Key);
                    }
                }
                counter = 1;
            }
        }

        static object EditCheck(string edit, Dictionary<Event, List<Person>> dictionary)
        {
            if (edit.ToLower() == "ime")
            {
                Console.WriteLine("Ime kojeg dogadaja zelite promijeniti?");
                var nameChanger = Console.ReadLine();
                int counter = 0;
                while (counter < 1)
                {
                    foreach (var pair in dictionary)
                    {
                        bool compare = pair.Key.Name.Equals(nameChanger, StringComparison.OrdinalIgnoreCase);
                        while (compare)
                        {
                            Console.WriteLine("Unesite novo ime dogadaja.");
                            pair.Key.Name = Console.ReadLine();
                            break;
                        }
                    }
                    counter = 1;
                }
                Console.WriteLine("Unijeli ste neispravno ime dogadaja.");
                return NameCheck(Console.ReadLine(), dictionary);
            }
            else if (edit.ToLower() == "tip")
            {
                Console.WriteLine("Tip kojeg dogadaja zelite promijeniti?");
                var typeChanger = Console.ReadLine();
                int counter = 0;
                while (counter < 1)
                {
                    foreach (var pair in dictionary)
                    {
                        bool compare = pair.Key.Name.Equals(typeChanger, StringComparison.OrdinalIgnoreCase);
                        while (compare)
                        {
                            Console.WriteLine("Unesite tip dogadaja.  Coffe/Lecture/Concert/StudySession");
                            pair.Key.EventType0 = (Event.EventType)TypeCheck(Console.ReadLine());
                            break;
                        }
                    }
                    counter = 1;
                }
                Console.WriteLine("Unijeli ste neispravno ime dogadaja.");
                return NameCheck(Console.ReadLine(), dictionary);
            }
            else if (edit.ToLower() == "pocetak")
            {
                Console.WriteLine("Pocetak kojeg dogadaja zelite promijeniti?");
                var startTimeChanger = Console.ReadLine();
                int counter = 0;
                while (counter < 1)
                {
                    foreach (var pair in dictionary)
                    {
                        bool compare = pair.Key.Name.Equals(startTimeChanger, StringComparison.OrdinalIgnoreCase);
                        while (compare)
                        {
                            Console.WriteLine("Unesite novi pocetak dogadaja u obliku mjesec/dan/godina sat:minuta:sekunda.");
                            pair.Key.StartTime = TimeCheck(Console.ReadLine());
                            break;
                        }
                    }
                    counter = 1;
                }
                Console.WriteLine("Unijeli ste neispravno ime dogadaja.");
                return NameCheck(Console.ReadLine(), dictionary);
            }
            else if (edit.ToLower() == "kraj")
            {
                Console.WriteLine("Kraj kojeg dogadaja zelite promijeniti?");
                var endTimeChanger = Console.ReadLine();
                int counter = 0;
                while (counter < 1)
                {
                    foreach (var pair in dictionary)
                    {
                        bool compare = pair.Key.Name.Equals(endTimeChanger, StringComparison.OrdinalIgnoreCase);
                        while (compare)
                        {
                            Console.WriteLine("Unesite novi pocetak dogadaja u obliku mjesec/dan/godina sat:minuta:sekunda.");
                            pair.Key.EndTime = TimeCheck(Console.ReadLine());
                            break;
                        }
                    }
                    counter = 1;
                }
                Console.WriteLine("Unijeli ste neispravno ime dogadaja.");
                return NameCheck(Console.ReadLine(), dictionary);
            }
            else
            {
                Console.WriteLine("Izgleda da ste unijeli pogresno.\nSto zelite promijeniti u dogadaju?   ime/tip/pocetak/kraj");
                return EditCheck(Console.ReadLine(), dictionary);
            }
        }

        private static int OibCheck(int oib, string eventName, Dictionary<Event, List<Person>> dictionary)
        {
            var list = new List<int>();
            int counter = 0;
            while (counter < 1)
            {
                foreach (var pair in dictionary)
                {
                    bool compare = pair.Key.Name.Equals(eventName, StringComparison.OrdinalIgnoreCase);
                    while (compare)
                    {
                        for (int i = 0; i < pair.Value.Count; i++)
                        {
                            list.Add(pair.Value[i].OIB);
                        }
                        if (!list.Contains(oib))
                        {
                            return oib;
                        }
                    }
                }
                counter = 1;
            }
        }

        static void Menu(Dictionary<Event, List<Person>> dictionary)
        {
            Console.WriteLine("Unesite opciju ispisa detalja eventa\n1 - Ispis detalja eventa\n2 - Ispis svih osoba na eventu\n3 - Ispis svih detalja\n4 - Izlazak iz podmenija (->povratak u glavni menu)\n");
            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    Console.WriteLine("Ispis detalja dogadaja:");
                    EventDetails(dictionary);
                    break;

                case 2:
                    Console.WriteLine("Ispis svih osoba na dogadaju:");
                    EventPeople(dictionary);
                    break;

                case 3:
                    Console.WriteLine("Ispis detalja dogadaja:");
                    EventDetails(dictionary);

                    Console.WriteLine("Ispis svih osoba na dogadaju:");
                    EventPeople(dictionary);
                    break;

                case 4:
                    break;
            }
        }

        static void EventDetails(Dictionary<Event, List<Person>> dictionary)
        {
            Console.WriteLine("Unesite ime eventa o kojem zelite znati detalje:");
            var eventInfo = Console.ReadLine().ToLower();

            var a = false;
            foreach (var pair in dictionary)
            {
                if (pair.Key.Name.ToLower() == eventInfo)
                {
                    TimeSpan span = pair.Key.EndTime - pair.Key.StartTime;
                    Console.WriteLine("{0} - {1} - {2} - {3} - {4} - {5}", pair.Key.Name, pair.Key.EventType0, pair.Key.StartTime, pair.Key.EndTime, span, pair.Value.Count);
                }
            }
            if (a == false)
                Console.WriteLine("Ne postoji dogadaj s tim imenom.");
        }

        static void EventPeople(Dictionary<Event, List<Person>> dictionary)
        {
            Console.WriteLine("Osobe na dogadaju:");
            var i = 0;
            foreach (var pair in dictionary)
            {
                Console.WriteLine("{0} - {1} - {2} - {3}", i, pair.Value[i].FirstName, pair.Value[i].LastName, pair.Value[i].PhoneNumber);
                i++;
            }
        }
    }
}
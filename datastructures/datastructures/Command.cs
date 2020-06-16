using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Threading;

namespace datastructuresproject
{
    public class Command
    {
        #region LoadFile
        public static void LoadFile(string file)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew(); //calculate execution time
            Console.Clear(); 
            ListOfHotels.hotels.Clear(); 
            StreamReader sr = new StreamReader(file, Encoding.UTF8);

            int numberofhotels = int.Parse(sr.ReadLine().Split(';')[0]); 

            while (!sr.EndOfStream){

                List<Reservation> res = new List<Reservation>(); 
                string line = sr.ReadLine();
                int numberofres = (line.Split(';').Length - 4) / 3; 
                for (int i = 0; i < numberofres; i++){
                    if (!string.IsNullOrEmpty(line.Split(';')[4 + (3 * i)])){
                        res.Add(new Reservation{   
                            name = line.Split(';')[4 + (3 * i)],
                            checkinDate = DateTime.Parse(line.Split(';')[5 + (3 * i)]),
                            stayDurationDays = int.Parse(line.Split(';')[6 + (3 * i)])
                        });
                    }
                }

                ListOfHotels.hotels.Add(new Hotel{   
                    id = int.Parse(line.Split(';')[0]),
                    name = line.Split(';')[1],
                    stars = int.Parse(line.Split(';')[2]),
                    numberOfRooms = int.Parse(line.Split(';')[3]),
                    reservations = res                    
                });

            }

            sr.Close(); 
            Console.WriteLine("\nFile Loaded.");
            watch.Stop(); //calculate execution time
            Console.WriteLine("Time taken: {0}ms", watch.Elapsed.TotalMilliseconds);
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
        #endregion

        #region AddHotel
        public static int AddHotel()
        {
            Console.Clear(); 
            bool exist = true;
            Hotel hotel = new Hotel(); 

            while (exist){
                Console.WriteLine("Enter ID of the Hotel:");

                try {
                    hotel.id = int.Parse(Console.ReadLine());
                }
                catch(FormatException e){
                    Console.WriteLine("{0}, Invalid ID \n Press any key to continue...", e);
                    Console.ReadKey();
                    return 0;
                }

                if (!ListOfHotels.hotels.Any(a => a.id == hotel.id)){
                    exist = false;
                }
                else{
                    Console.WriteLine("This hotel already exists.");
                    Console.WriteLine("Do you want to add another hotel? (Y/N)");
                    ConsoleKeyInfo key = Console.ReadKey();
                    if (key.KeyChar == 'N' || key.KeyChar == 'n') {
                        return 0;
                    }
                }
            }

            Console.WriteLine("\nEnter the name of the hotel:");
            try {
                hotel.name = Console.ReadLine();
            }
            catch (IOException e){
                Console.WriteLine("{0}, Invalid input \n Press any key to continue...", e);
                Console.ReadKey();
                return 0;
            }

            hotel.stars = -1;
            try{
                while (hotel.stars < 1 || hotel.stars > 5){
                    Console.WriteLine("Enter the number of stars of the hotel:");
                    hotel.stars = int.Parse(Console.ReadLine());
                }
            }
            catch (FormatException e){
                Console.WriteLine("{0}, Invalid number \n Press any key to continue...", e);
                Console.ReadKey();
                return 0;
            }
            hotel.numberOfRooms = 0;

            try{
                while (hotel.numberOfRooms < 1){
                    Console.WriteLine("Enter the number of rooms of the hotel:");
                    hotel.numberOfRooms = int.Parse(Console.ReadLine());
                }
            }
            catch(FormatException e){
                Console.WriteLine("{0}, Invalid number \n Press any key to continue...", e);
                Console.ReadKey();
                return 0;
            }

            bool entry = true;
            while (entry){
                Console.WriteLine("Do you wish to enter a reservation? (Y/N or y/n");
                ConsoleKeyInfo key = Console.ReadKey();

                if (key.KeyChar == 'Y' || key.KeyChar == 'y'){
                    Reservation r = new Reservation();

                    try {
                        Console.WriteLine("\n Enter surname:");
                    }
                    catch(IOException e){
                        Console.WriteLine("{0}, Invalid input \n Press any key to continue...", e);
                        Console.ReadKey();
                        return 0;
                    }

                    r.name = Console.ReadLine();
                    Console.WriteLine("Enter check-in date:");

                    try {
                        r.checkinDate = DateTime.Parse(Console.ReadLine());
                    }
                    catch(FormatException e){
                        Console.WriteLine("{0}, Invalid input \n Press any key to continue...", e);
                        Console.ReadKey();
                        return 0;
                    }

                    r.stayDurationDays = 0;

                    try{
                        while (r.stayDurationDays < 1){
                            Console.WriteLine("Enter number of stay duration:");
                            r.stayDurationDays = int.Parse(Console.ReadLine());
                        }
                    }
                    catch(FormatException e){
                        Console.WriteLine("{0}, Invalid number \n Press any key to continue...", e);
                        Console.ReadKey();
                        return 0;
                    }
                    hotel.reservations.Add(r); 
                }
                else{
                    ListOfHotels.hotels.Add(hotel); 
                    entry = false;
                }
                Console.WriteLine("Hotel added in a temporary list. To save it, press 2 on the main menu.");
            }
            return 0;
        }
        #endregion

        #region SearchbySurname
        public static void SearchbySurname(string surname)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew(); //calculate execution time
            Console.Clear();

            bool exist = false;
            foreach (Hotel hotel in ListOfHotels.hotels){

                foreach (Reservation r in hotel.reservations){

                    if (r.name == surname){
                        Console.WriteLine("A reservation in this name is in {0} at {1} for {2} days.", hotel.name, r.checkinDate, r.stayDurationDays);
                        watch.Stop(); //calculate execution time
                        Console.WriteLine("Time taken: {0}ms", watch.Elapsed.TotalMilliseconds);
                        exist = true;
                    }
                }
            }

             if (!exist) {
                Console.WriteLine("\n Surname not found.");
                watch.Stop(); //calculate execution time
                Console.WriteLine("Time taken: {0}ms", watch.Elapsed.TotalMilliseconds);
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
        #endregion

        #region SearchbyID
        public static void SearchbyID(int search)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew(); //calculate execution time
            bool exist = false;
            foreach (Hotel hotel in ListOfHotels.hotels){

                if (hotel.id == search) {
                    Console.WriteLine("\n The name of the hotel is {0}", hotel.name);
                    watch.Stop(); //calculate execution time
                    Console.WriteLine("Time taken: {0}ms", watch.Elapsed.TotalMilliseconds);
                    exist = true;
                    break; 
                }
            }
            
            if (!exist) {
                Console.WriteLine("\n ID not found.");
                watch.Stop(); //calculate execution time
                Console.WriteLine("Time taken: {0}ms", watch.Elapsed.TotalMilliseconds);
            } 

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
        #endregion

        #region SaveFile
        public static void SaveFile(string file)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew(); //calculate execution time
            Console.Clear();
            StreamWriter sw = new StreamWriter(file, false, Encoding.UTF8);
            sw.WriteLine(ListOfHotels.hotels.Count() + ";");
            foreach (Hotel hotel in ListOfHotels.hotels){

                string line = hotel.id.ToString() + ";" + hotel.name + ';' + hotel.stars + ';' + hotel.numberOfRooms;

                foreach (Reservation r in hotel.reservations){
                    line += ';'  + r.name + ';' + r.checkinDate.ToShortDateString() + ';' + r.stayDurationDays;
                }
                sw.WriteLine(line);
            }

            sw.Close();
            Console.WriteLine("\nFile Saved");
            watch.Stop(); //calculate execution time
            Console.WriteLine("Time taken: {0}ms", watch.Elapsed.TotalMilliseconds);
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
        #endregion
    }
}
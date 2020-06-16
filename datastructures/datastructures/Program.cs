using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace datastructuresproject
{
    class Program
    {
        #region Main
        static void Main(string[] args)
        {
            string file = System.IO.Directory.GetCurrentDirectory() + @"\data.csv"; 
            if (args.Length != 0) {
                file = args[0];
            } 
            showMenu();
            option(file);        
            Console.Write("");
        }
        #endregion

        #region showMenu
        private static void showMenu()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Black; 
            Console.BackgroundColor = ConsoleColor.DarkYellow;  
            Console.WriteLine("Hotel Menu.\n");
            Console.ResetColor();
            Console.WriteLine("Press the number of your option.");
            Console.WriteLine("1. Load Hotels and Reservations from file");
            Console.WriteLine("2. Save Hotels and Reservations to file");
            Console.WriteLine("3. Add a Hotel (and its reservations)");
            Console.WriteLine("4. Search and Display a Hotel by id");
            Console.WriteLine("5. Display Reservations by surname search");
            Console.WriteLine("6. Exit");
        }
        #endregion

        #region option
        private static void option(string file)
        {
            while (true){

                ConsoleKeyInfo key = Console.ReadKey();

                switch (key.KeyChar){
                    case '1':
                        Command.LoadFile(file);
                        //AVL.root = null;
                        //AVL.CallInsert();
                        Trie.root = new NodeTrie(" ");
                        Trie.CallInsert();
                        break;
                    case '2':
                        Command.SaveFile(file);
                        //AVL.root = null;
                        //AVL.CallInsert();
                        Trie.root = new NodeTrie(" ");
                        Trie.CallInsert();
                        break;
                    case '3':
                        Command.AddHotel();
                        break;
                    case '4':
                        Console.Clear();
                        int j=0;

                        try{
                            while (j < 1){
                                Console.WriteLine("\nEnter ID.");
                                j = int.Parse(Console.ReadLine());
                            }
                        }
                        catch (FormatException e){
                            Console.WriteLine("{0}\nInvalid input\r\nPress any key to continue...", e);
                            Console.ReadKey();
                            break;
                        }

                        Console.WriteLine("\n Choose searching type:");
                        Console.WriteLine(" 1. Linear Search\n 2. Binary Search\n 3. Interpolation Search\n 4. AVL Tree");
                        ConsoleKeyInfo i = Console.ReadKey();

                        switch (i.KeyChar) {
                            case '1':
                                Command.SearchbyID(j);
                                break;
                            case '2':
                                Searching.BinarySearch(j);
                                break;
                            case '3':
                                Searching.Interpolation(j);
                                break;
                            //case '4':
                            //    AVL.FindItem(j);
                            //    break;
                            default:
                                Console.WriteLine("\nPlease try again.");
                                Console.ReadKey();
                                break;
                        }

                        break;
                    case '5':
                        Console.Clear();
                        Console.WriteLine("\nEnter Surname.");
                        string sn = Console.ReadLine();
                        Console.WriteLine("\nChoose searching type:");
                        Console.WriteLine("1. Linear Search\r\n2. Trie");
                        ConsoleKeyInfo l = Console.ReadKey();

                        switch (l.KeyChar)
                        {
                            case '1':
                                Command.SearchbySurname(sn);
                                break;
                            case '2':
                                Trie.Find(sn);
                                break;
                            default:
                                Console.WriteLine("\nPlease try again.");
                                Console.ReadKey();
                                break;
                        }

                        break;
                    case '6':
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("\nPlease enter again a valid number.");
                        option(file);
                        break;
                }
                showMenu();
            }
        }
        #endregion
    }
}
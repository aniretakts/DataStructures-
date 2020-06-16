using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading;

namespace datastructuresproject
{
    public class Searching
    {
        public static IOrderedEnumerable<Hotel> Sorted()
        {
            return ListOfHotels.hotels.OrderBy(a => a.id);
        }

        #region Interpolation
        public static void Interpolation(int key)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew(); //calculate execution time
            var temp=Sorted().ToArray();
            if (temp==null){
                throw new ArgumentNullException("The list is empty.");
            }

            int min = 0;
            int max = temp.Count() - 1;

            while (temp.ElementAt(max).id >= key && temp.ElementAt(min).id < key){
                int rise=max - min;
                int run=temp.ElementAt(max).id - temp.ElementAt(min).id;
                int x=key - temp.ElementAt(min).id;
                int index=(rise / run) * x + min;

                if (key < temp.ElementAt(index).id) {
                    max=index - 1;
                }
                else if (key > temp.ElementAt(index).id) {
                    min=index + 1;
                }
                else {
                    min=index;
                }
            }

            if (temp.ElementAt(min).id==key){
                Console.WriteLine("\nThe name of the hotel is {0}", temp.ElementAt(min).name);
                watch.Stop(); //calculate execution time
                Console.WriteLine("Time taken: {0}ms", watch.Elapsed.TotalMilliseconds);
            }
            else{
                Console.WriteLine("\n ID not found.");
                watch.Stop(); //calculate execution time
                Console.WriteLine("Time taken: {0}ms", watch.Elapsed.TotalMilliseconds);
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
        #endregion

        #region BinarySearch
        public static void BinarySearch(int key)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew(); //calculate execution time
            var temp=Sorted().ToArray();

            if (temp==null){
                throw new ArgumentNullException("The list is empty.");
            }

            int min=0;
            int max=temp.Count() - 1;
            int mid;
            string _name="";

            while (min <= max){
                mid=(min + max) / 2;
                if (key == temp.ElementAt(mid).id){
                    _name=temp.ElementAt(mid).name;
                    break;
                }
                else if (key < temp.ElementAt(mid).id) {
                    max=mid - 1;
                }
                else {
                    min=mid + 1;
                }
            }

            if (_name == ""){
                Console.WriteLine("\nID not found.");
                watch.Stop(); //calculate execution time
                Console.WriteLine("Time taken: {0}ms", watch.Elapsed.TotalMilliseconds);
            }
            else {
                Console.WriteLine("\nThe name of the hotel is {0}", _name);
                watch.Stop(); //calculate execution time
                Console.WriteLine("Time taken: {0}ms", watch.Elapsed.TotalMilliseconds);
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
        #endregion

        
    }
}
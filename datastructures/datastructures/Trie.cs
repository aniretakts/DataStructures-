using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading;

namespace datastructuresproject
{
    public class NodeTrie
    {
        public string key; 
        public int value; 
        public List<NodeTrie> children = new List<NodeTrie>(); 

        public NodeTrie(string c) {
            key = c; value = 0;
        }
    }

    public class Trie
    {
        public static int clicks = 0;
        public static double time = 0.0;

        public static NodeTrie root = new NodeTrie(" ");

        #region CallInsert
        public static void CallInsert()
        {
            var watch = System.Diagnostics.Stopwatch.StartNew(); //calculate execution time
            foreach (Hotel hotel in ListOfHotels.hotels){
                foreach (Reservation r in hotel.reservations){ 
                    Insert(r.name, hotel.name, r.stayDurationDays, r.checkinDate);
                }
            }
            Console.WriteLine("\nTrie is ready.");
            watch.Stop(); //calculate execution time
            Console.WriteLine("Time taken: {0}ms", watch.Elapsed.TotalMilliseconds);
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
        #endregion

        #region Insert
        public static void Insert(string n, string h, int s, DateTime c)
        {
            NodeTrie x = root; 
            if (n == null) { return; } 
            if (x.children.Count() == 0) {
                for (int i = 0; i < n.Length; i++) {
                    NodeTrie y = new NodeTrie(n.ElementAt(i).ToString()); 
                    if (i==n.Length - 1) {
                        y.value = 1;
                    } 
                    x.children.Add(y); 
                    x=y; 
                }
            }
            else{
                bool e=false;
                int j=0;
                for (int i=0; i<n.Length; i++) 
                {
                    foreach (NodeTrie nt in x.children) 
                    {
                        e=true;
                        if (n.ElementAt(i).ToString()==nt.key) 
                        {
                            x=nt;
                            e=false;
                            break;
                        }
                    }
                    if (e)
                    {
                        j=i;
                        break;
                    }
                }
                if (e) {//for the letters that don't exist in the trie in the level wanted
                    for (int i=j; i<n.Length; i++){
                        NodeTrie y=new NodeTrie(n.ElementAt(i).ToString()); //new node for every letter
                        if (i==n.Length - 1) {
                            y.value=1;
                        } //in the last letter I set value=1
                        x.children.Add(y); //added to the children list of the parent
                        x=y; //continue to the next level
                    }
                }
            }
            x.children.Add(new NodeTrie(h+" for "+ s.ToString()+" days at "+c.ToString()));
        }
        #endregion

        #region Find
        public static void Find(string n)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew(); //calculate execution time

            NodeTrie x = root;
            bool ex = true;
            int tot = 0;
            for (int i = 0; i < n.Length; i++) {
                foreach (NodeTrie nt in x.children){
                    tot++;
                    ex=false;
                    if (n.ElementAt(i).ToString()==nt.key) {
                        ex=true;
                        x=nt;
                        break;
                    }
                }
                if(!ex) {
                    break; }
            }
          
            if (ex){
                foreach (NodeTrie nt in x.children) {
                    Console.WriteLine("\r\n There is a reservation this name is for {0}", nt.key);
                    watch.Stop(); //calculate execution time
                    Console.WriteLine("Time taken: {0}ms", watch.Elapsed.TotalMilliseconds);
                }
            }
            else {
                Console.WriteLine("There is no reservation in this name.");
                watch.Stop(); //calculate execution time
                Console.WriteLine("Time taken: {0}ms", watch.Elapsed.TotalMilliseconds);
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
        #endregion
    }
}
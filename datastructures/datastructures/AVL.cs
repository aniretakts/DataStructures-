using datastructuresproject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace datastructures
{

    public class Node
    {
        public Node left;
        public Node right;
        public Node parent;
        public int _id;
        public string _name;

        public Node(int data, string name)
        {
            _id = data;
            _name = name;
        }

    }

    class AVL
    {
        public static Node root = null;

        #region CallInsert
        public static void CallInsert()
        {
            var watch = System.Diagnostics.Stopwatch.StartNew(); //calculate execution time
            foreach (Hotel hotel in ListOfHotels.hotels)
            {
                InsertItem(hotel.id, hotel.name);
            }

            Console.WriteLine("\r\nAVL is ready");
            watch.Stop(); //calculate execution time
            Console.WriteLine("Time taken: {0}ms", watch.Elapsed.TotalMilliseconds);
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        public static void InsertItem(int id, string name)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region InsertItem
        public void Insert(int _id, string name)
        {
            Node newItem = new Node(_id, name);
            if (root == null)
            {
                root = newItem;
            }
            else
            {
                root = RecursiveInsert(root, newItem);
            }
        }
        private Node RecursiveInsert(Node current, Node n)
        {
            if (current == null)
            {
                current = n;
                return current;
            }
            else if (n._id < current._id)
            {
                current.left = RecursiveInsert(current.left, n);
                current = balance_tree(current);
            }
            else if (n._id > current._id)
            {
                current.right = RecursiveInsert(current.right, n);
                current = balance_tree(current);
            }
            return current;
        }
        #endregion


        #region Balance

        private int max(int l, int r)
        {
            return l > r ? l : r;
        }

        private int getHeight(Node current)
        {
            int height = 0;
            if (current != null)
            {
                int l = getHeight(current.left);
                int r = getHeight(current.right);
                int m = max(l, r);
                height = m + 1;
            }
            return height;
        }

        private int balance_factor(Node current)
        {
            int l = getHeight(current.left);
            int r = getHeight(current.right);
            int b_factor = l - r;
            return b_factor;
        }

        private Node RotateRR(Node parent)
        {
            Node pivot = parent.right;
            parent.right = pivot.left;
            pivot.left = parent;
            return pivot;
        }

        internal static void Find(int j)
        {
            throw new NotImplementedException();
        }

        private Node RotateLL(Node parent)
        {
            Node pivot = parent.left;
            parent.left = pivot.right;
            pivot.right = parent;
            return pivot;
        }

        private Node RotateLR(Node parent)
        {
            Node pivot = parent.left;
            parent.left = RotateRR(pivot);
            return RotateLL(parent);
        }
        private Node RotateRL(Node parent)
        {
            Node pivot = parent.right;
            parent.right = RotateLL(pivot);
            return RotateRR(parent);
        }

        private Node balance_tree(Node current)
        {
            int b_factor = balance_factor(current);
            if (b_factor > 1)
            {
                if (balance_factor(current.left) > 0)
                {
                    current = RotateLL(current);
                }
                else
                {
                    current = RotateLR(current);
                }
            }
            else if (b_factor < -1)
            {
                if (balance_factor(current.right) > 0)
                {
                    current = RotateRL(current);
                }
                else
                {
                    current = RotateRR(current);
                }
            }
            return current;
        }
        #endregion

        #region FindItem
        public void FindItem(int key)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew(); //calculate execution time
            if (Find(key, root)._id == key)
            {
                Console.WriteLine("The id {0} was found!", key);
                watch.Stop(); //calculate execution time
                Console.WriteLine("Time taken: {0}ms", watch.Elapsed.TotalMilliseconds);
            }
            else
            {
                Console.WriteLine("\nID not found.");
                watch.Stop(); //calculate execution time
                Console.WriteLine("Time taken: {0}ms", watch.Elapsed.TotalMilliseconds);
            }
        }
        private Node Find(int target, Node current)
        {

            if (target < current._id)
            {
                if (target == current._id)
                {
                    return current;
                }
                else
                    return Find(target, current.left);
            }
            else
            {
                if (target == current._id)
                {
                    return current;
                }
                else
                    return Find(target, current.right);
            }

        }
        #endregion

    }
}

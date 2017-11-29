using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoublyLinkedList
{
    class Program
    {
        static void Main(string[] args)
        {
            DoublyLinkedList<int> doublyLinkedList = new DoublyLinkedList<int>();
            doublyLinkedList.Add(1);
            doublyLinkedList.Add(2);
            doublyLinkedList.Add(3);

            Console.WriteLine("Doubly linked link has size: {0}", doublyLinkedList.Size());

            doublyLinkedList.Remove(2);
            Console.WriteLine("Doubly linked link has size: {0}", doublyLinkedList.Size());
            Console.WriteLine("Element \"3\" exists {0}", doublyLinkedList.ElementExists(3));
            Console.WriteLine("Element \"2\" exists {0}", doublyLinkedList.ElementExists(2));

            DoublyLinkedList<string> doublyStrLinkedList = new DoublyLinkedList<string>();
            doublyStrLinkedList.Add("Hello");
            doublyStrLinkedList.Add("My");
            doublyStrLinkedList.Add("World");
            doublyStrLinkedList.Add("Big");

            Console.WriteLine("Doubly linked link has size: {0}", doublyStrLinkedList.Size());

            doublyStrLinkedList.Remove("Big");
            Console.WriteLine("Doubly linked link has size: {0}", doublyStrLinkedList.Size());
            Console.WriteLine("Element \"World\" exists {0}", doublyStrLinkedList.ElementExists("World"));
            Console.WriteLine("Element \"Big\" exists {0}", doublyStrLinkedList.ElementExists("Big"));

            doublyStrLinkedList.Remove("Hello");
            Console.WriteLine("Doubly linked link has size: {0}", doublyStrLinkedList.Size());

            Console.ReadKey();
        }
    }
}

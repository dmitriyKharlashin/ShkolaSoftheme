using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoublyLinkedList
{
    class DoublyLinkedList<T>
    {
        private int _length = 0;
        Node<T> head = null;
        Node<T> tail = null;

        public DoublyLinkedList(){
            Console.WriteLine("New doubly linked list was created");
        }

        public void Add(T element)
        {
            Node<T> node = new Node<T>(element);
            if (head == null)
            {
                head = node;
                tail = node;
            } else
            {
                Node<T> currentNode = head;

                while (currentNode.next != null)
                {
                    currentNode = currentNode.next;
                }

                currentNode.next = node;
                node.previous = currentNode;

                tail = node;
            }

            Console.WriteLine("New element was added");
            Console.WriteLine(element);
            _length++;
        }

        public void Remove(T element)
        {
            Node<T> currentNode = head;
            Node<T> previousNode = null;

            if (element.Equals(currentNode.Data) == true)
            {
                head = currentNode.next;
                currentNode.next.previous = null;
            }
            else
            {
                while (element.Equals(currentNode.Data) == false)
                {
                    previousNode = currentNode;
                    currentNode = currentNode.next;
                }
                previousNode.next = currentNode.next;
                if (currentNode.next == null)
                {
                    tail = currentNode.previous;
                } else
                {
                    currentNode.next.previous = previousNode;
                }
            }

            Console.WriteLine("The element was removed");
            Console.WriteLine(element);
            _length--;
        }

        public int Size()
        {
            return _length;
        }

        public T[] ToArray()
        {
            T[] nodesArray = new T[Size()];

            if (head != null)
            {
                Node<T> currentNode = head;
                nodesArray[0] = currentNode.Data;

                int index = 1;
                while (currentNode.next != null)
                {
                    currentNode = currentNode.next;
                    nodesArray[index] = currentNode.Data;
                }
            }

            return nodesArray;
        }

        public bool ElementExists(T element)
        {
            return ToArray().Contains(element);
        }
    }
}

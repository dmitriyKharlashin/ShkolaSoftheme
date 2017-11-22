using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoublyLinkedList
{
    class DoublyLinkedList<T>
    {
        int Length = 0;
        Node<T> Head = null;
        Node<T> Tail = null;

        public void Add(T element)
        {
            Node<T> node = new Node<T>(element);
            if (Head == null)
            {
                Head = node;
                Tail = node;
            } else
            {
                Node<T> currentNode = Head;

                while (currentNode.Next != null)
                {
                    currentNode = currentNode.Next;
                }

                currentNode.Next = node;
                node.Next = currentNode;

                Tail = node;
            }

            Length++;
        }

        public void Remove(T element)
        {
            Node<T> currentNode = Head;
            Node<T> previousNode = null;

            if (currentNode.Data === element)
            {
                Head = currentNode.Next;
                currentNode.Next.Previous = null;
            }
            else
            {
                while (currentNode.Data !== element)
                {
                    previousNode = currentNode;
                    currentNode = currentNode.Next;
                }
                previousNode.Next = currentNode.Next;
                if (currentNode.Next == null)
                {
                    Tail = currentNode.Previous;
                } else
                {
                    currentNode.Next.Previous = previousNode;
                }
            }

            Length--;
        }

        public int Size()
        {
            return Length;
        }
    }
}

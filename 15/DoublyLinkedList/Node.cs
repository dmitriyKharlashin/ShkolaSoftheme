using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoublyLinkedList
{
    class Node<T>
    {
        public Node<T> previous = null;
        public Node<T> next = null;
        public T Data { get; private set;}

        public Node(T element)
        {
            Data = element;
        }
    }
}

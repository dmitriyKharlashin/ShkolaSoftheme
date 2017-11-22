using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoublyLinkedList
{
    class Node<T>
    {
        public Node<T> Previous = null;
        public Node<T> Next = null;
        public T Data;

        public Node(T data)
        {
            T Data = data;
        }
    }
}

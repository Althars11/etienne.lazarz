using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace TP7
{
    public class List<T>
    {
        private Node<T> _head;
        private Node<T> _tail;
        private int _size;
        
        
        public List()
        {
            _head = null;
            _tail = null;
            _size = 0;
        }

        public List(T[] data)
        {
            if (data.Length == 0)
            {
                _head = null;
                _tail = null;
                _size = 0;
            }
            else
            {
                _head = new Node<T>(data[0]);
                _tail = new Node<T>(data[0]);
                
                for (int i = 1; i < data.Length; i++)
                {
                    Node<T> new_head = new Node<T>(data[i]);

                    
                    new_head.prev = _head;
                    new_head.next = _tail;

                    _head = new_head;
                }

                _size = data.Length;
            }
        }

        public void AddFirst(T data)
        {
            Node<T> new_head = new Node<T>(data);
            new_head.next = _head;
            new_head.prev = _tail;
            _head = new_head;
            _size ++;
        }

        public void AddLast(T data)
        {
            Node<T> new_tail = new Node<T>(data);
            new_tail.next = _head;
            new_tail.prev = _tail;
            _tail = new_tail;
            _size ++;
        }

        public void Add(int index, T data)
        {
            try
            {
                Node<T> new_node = new Node<T>(data);

                //new_node.next = ;
                //new_node.prev = ;
                _size++;
            }
            catch (ArgumentOutOfRangeException out_of_range)
            {
                Console.WriteLine("List.Add_error: {0}", out_of_range);
                throw;
            }
        }

        public void RemoveFirst()
        {
            try
            {
                Node<T> new_head = _head.next;
                new_head.prev = _tail;
                new_head.next = _head.next;
                _head = new_head;
                _size--;
            }
            catch (ArgumentOutOfRangeException out_of_range)
            {
                Console.WriteLine("List.Pop_error: {0}", out_of_range);
                throw;
            }
        }

        public void RemoveLast()
        {
            try
            {
                Node<T> new_tail = _head.next;
                new_tail.prev = _tail;
                new_tail.next = _head;
                _tail = new_tail;
                _size--;
            }
            catch (ArgumentOutOfRangeException out_of_range)
            {
                Console.WriteLine("List.Pop_error: {0}", out_of_range);
                throw;
            }
        }

        public void Remove(int index)
        {
            //Node<T> removed_one
        }

        public int Count()
        {
            return _size;
        }
    }
}
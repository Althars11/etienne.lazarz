using System;
using System.Security.Cryptography.X509Certificates;

namespace TP7
{
    public class Stack<T>
    {
        private Node<T> _head;
        private int _size;

        public Stack()
        {
            _head = null;
            _size = 0;
        }
        
        public Stack(T[] data)
        {
            if (data.Length == 0)
            {
                _head = null;
                _size = 0;
            }
            else
            {
                _head = new Node<T>(data[0]);
                for (int i = 1; i < data.Length; i++)
                {
                    Node<T> new_head = new Node<T>(data[i]);
                    new_head.prev = _head;
                    _head = new_head;
                }

                _size = data.Length;
            }
        }

        public void Push(T data)
        {
            Node<T> new_head = new Node<T>(data);
            new_head.prev = _head;
            _head = new_head;
            _size ++;
        }

        public T Pop()
        {
            if (_size >= 1)
            {
                if (_size == 1)
                {
                    _size--;
                    T headdata = _head.data;
                    _head = null;
                    return headdata;
                }
                else
                {
                    T headdata = _head.data;
                    _head = _head.prev;
                    _size--;
                    return headdata;
                }
            }
            else
            {
                throw new ArgumentOutOfRangeException("Stack.Pop_error");                
            }
        }

        public T Peek()
        {
            if (_size>=1)
            {
                return _head.data;
            }
            throw new ArgumentOutOfRangeException("Stack.Peek_error");
            
        }

        public int Count()
        {
            return _size;
        }
    }
}
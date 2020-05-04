using System;
using System.Collections;

namespace TP7
{
    public class Queue<T>
    {
        private Node<T> _head;
        private Node<T> _tail;
        private int _size;
        
        public Queue()
        {
            _head = null;
            _tail = null;
            _size = 0;
        }
        
        public Queue(T[] data)
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
                    Node<T> new_tail = new Node<T>(data[i]);
                    new_tail.next = _tail;
                    _tail = new_tail;
                }

                _size = data.Length;
            }
        }

        public void Push(T data)
        {
            Node<T> new_tail = new Node<T>(data);
            new_tail.prev = _tail;
            _tail = new_tail;
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
                throw new ArgumentOutOfRangeException("Queue.Pop_error");                
            }
        }

        public T Peek()
        {
            if (_size>=1)
            {
                return _tail.data;
            }
            throw new ArgumentOutOfRangeException("Queue.Peek_error");
            
        }

        public int Count()
        {
            return _size;
        }
    }
}
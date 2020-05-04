namespace TP7
{
    public class Node<T>
    {
        public T data;
        public Node<T> next;
        public Node<T> prev;
        
        public Node(T data)
        {
            this.data = data;
        }
    }
}
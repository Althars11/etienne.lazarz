using System;
using System.Collections;

namespace TP7
{
    public class TestQueue
    {
        public static int[] array_tmp = new[] {0, 1, 2, 3, 4, 5, 6, 7, 8, 9};
        public static int[] array_zero = new[] {0};
        public static Queue<int> queue = new Queue<int>(array_tmp);
        public static Queue<int> queuebis = new Queue<int>(array_zero);

        public static void execute_my_queue(int[] array)
        {
            Console.WriteLine(queuebis.Pop());
            //erreur déclenchée
            //Console.WriteLine(queuebis.Peek());
            
            Console.WriteLine(queue.Count());
            queue.Push(20);
            Console.WriteLine(queue.Peek());
            queue.Pop();
            Console.WriteLine(queue.Peek());
            queue.Pop();
            Console.WriteLine(queue.Peek());

            //Console.WriteLine(stackbis.Pop());
            //stackbis.Peek();
        }
    }
}
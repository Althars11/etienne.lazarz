using System;

namespace TP7
{
    public class TestStack
    {
        public static int[] array_tmp = new[] {0, 1, 2, 3, 4, 5, 6, 7, 8, 9};
        public static int[] array_zero = new[] {0};
        public static Stack<int> stack = new Stack<int>(array_tmp);
        public static Stack<int> stackbis = new Stack<int>(array_zero);

        public static void print_my_stack_index(Stack<int> teststack)
        {
            for (int i = 0; i <  teststack.Count(); i++)
            {
                Console.Write("{0} ", i);
            }
            Console.WriteLine();
        }
        
        public static void execute_my_stack(int[] array)
        {
            Console.WriteLine(stack.Count());
            Console.WriteLine(stack.Pop());
            stack.Push(11);
            stackbis.Push(1);
            Console.WriteLine(stackbis.Peek());
            Console.WriteLine(stackbis.Pop());
            Console.WriteLine(stackbis.Pop());
            //erreurs déclenchées via ArgumentException
            //Console.WriteLine(stackbis.Pop());
            //stackbis.Peek();
        }
    }
}
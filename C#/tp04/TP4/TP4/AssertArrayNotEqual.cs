using System;

namespace TP4
{
    public class AssertArrayNotEqual<T>: Assert<T[]>, IAssert
    {
        protected int _size;

        public AssertArrayNotEqual(string name, T[] a, T[] b, int size) : base(name, a, b)
        {
            _size = size;
        }

        protected override bool Test()
        {
            return !Equals(_a, _b);
        }

        protected override void Exception()
        {
            Console.WriteLine("Test' <{0}>' fail: {1} is equal to {2}.",_name,_a,_b);
        }

        public new void Run()
        {
            base.Run();
        } 
    }
}
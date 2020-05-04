using System;

namespace TP4
{
    public class AssertNotEqual<T>: Assert<T>, IAssert
    {
        public AssertNotEqual(String name, T a, T b) : base(name, a, b)
        {
        }

        protected override bool Test()
        {
            return !Equals(_a, _b);
        }

        protected override void Exception()
        {
            Console.WriteLine("Test '<{0}>' fail: <{1}> is equal to <{2}>.", _name, _a, _b);
        }

        public new void Run()
        {
            base.Run();
        }
    }
}
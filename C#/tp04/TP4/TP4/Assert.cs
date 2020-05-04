using System;
using System.IO;

namespace TP4
{
    public interface IAssert
    {
        void Run();
    }
    public class Assert<T>
    {
        protected String _name;
        protected T _a;
        protected T _b;

        public Assert(String name, T a, T b)
        {
            _name = name;
            _a = a;
            _b = b;
        }

        public bool Run()
        {
            if (Test())
            {
                Success();
            }
            else
            {
                Exception();
            }
            return Test();
        }

        protected virtual bool Test()
        {
            return false;
        }

        protected virtual void Success()
        {
            Console.WriteLine("Test '<{0}>' succes.", _name);
        }

        protected virtual void Exception()
        {
            Console.WriteLine("Test '<{0}>' fail.", _name);
        }
    }
}
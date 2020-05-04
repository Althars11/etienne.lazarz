using System;
using System.Collections.Generic;

namespace TP4
{
    public class Test
    {
        private string _name;

        public Test(string name)
        {
            _name = name;
        }

        private List<IAssert> _asserts = new List<IAssert>();

        public void AddAssert(IAssert assert)
        {
            _asserts.Add(assert);
        }

        public void Run()
        {
            Console.WriteLine("Start test suite: {0}",_name);
            for (int i = 0; i < _asserts.Count; i++)
            {
                Console.WriteLine("Start test number: {0}",i);
                _asserts[i].Run();
            }
        }
    }
}
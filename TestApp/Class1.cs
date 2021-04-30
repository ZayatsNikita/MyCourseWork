using System;
using System.Collections.Generic;
using System.Text;

namespace TestApp
{
    public class Class1
    {
        public static void Main()
        {
            (int, int) a = (10, 12);
            a.Item1 = 1;
            Console.WriteLine(a.Item1);
        }
    }
}

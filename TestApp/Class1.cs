using System;
using System.Collections.Generic;
using System.Text;

namespace TestApp
{

    public interface IA
    {
        public int Get(int a);
    }
    public class A : IA
    {
        public int Get(int a = 5) => a;
    }


    public class Class1
    {
        public static void Main()
        {
            IA a = new A();
            Console.WriteLine(a.Get(3));
        }
    }
}

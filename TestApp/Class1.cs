using System;
using System.Collections.Generic;
using System.Text;

namespace TestApp
{
    public class Class1
    {
        public static void Main()
        {
            string a = "asd";
            string b = "asd";
           
            if (a == b)
            {
                Console.WriteLine(Object.ReferenceEquals(a,b));
                
            }
        }
    }
}

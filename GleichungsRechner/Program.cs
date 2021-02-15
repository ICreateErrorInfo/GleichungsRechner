using System;
using System.Collections.Generic;
using System.Linq;

namespace GleichungsRechner
{
    class Program
    {
        static void Main(string[] args)
        {
            string a = Console.ReadLine(); //input
            Berechne.parser(a);
            Console.WriteLine(Berechne.Term(a));
        }

        

        
       
    }
}

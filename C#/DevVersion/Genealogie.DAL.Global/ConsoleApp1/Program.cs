using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            coucou c = new coucou { unString="cou"};

            Console.WriteLine($"{c.unString} {c.id}");

            Console.ReadKey();
        }

        public class coucou
        {
            public int id { get; set; }
            public string unString { get; set; }
        }

    }
}

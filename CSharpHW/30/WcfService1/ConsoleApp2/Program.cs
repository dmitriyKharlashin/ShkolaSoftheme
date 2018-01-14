using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp2.ServiceReference1;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            var service = new Service1Client();

            string result = service.GetData(1111);

            Console.WriteLine(result);
            Console.ReadKey();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottery
{
    class Program
    {
        static void Main(string[] args)
        {
            AbstractDraw draw = new Draw();
            draw.PopulateTicket();
            draw.CheckDrawResult();
            draw.DisplayDrawResults();
            Console.ReadKey();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Classes
{
    public class Chip : Product
    {
        public Chip(string slot, string name, double price, int quantity) : base(slot, name , price, quantity)
        {
            
        }

        public override void Consume()
        {
            Console.WriteLine("Crunch Crunch, Yumm!");
        }
    }
}

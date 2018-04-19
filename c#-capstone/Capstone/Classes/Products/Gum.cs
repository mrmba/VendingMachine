using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Classes.Products
{
    class Gum : Product
    {
        public Gum(string slot, string name, double price, int quantity) : base(slot, name, price, quantity)
        {

        }

        public override void Consume()
        {
            Console.WriteLine("Chew Chew, Yumm!");
        }

    }
}

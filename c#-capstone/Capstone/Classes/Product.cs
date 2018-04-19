using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Classes
{   
    //[Serializable]
    public class Product
    {
        public string Slot { get; }
        public string Name { get; }
        public double Price  { get; }
        public int Quantity  { get; set; }
        public bool SoldOut { get { return Quantity == 0; } }

        public Product(string slot, string name, double price, int quantity)
        {
            Slot = slot;
            Name = name;
            Price = price;
            Quantity = quantity;
        }

        public virtual void Consume()
        {
            Console.WriteLine("MMM mmm mm Gooood");
        }

    }
}

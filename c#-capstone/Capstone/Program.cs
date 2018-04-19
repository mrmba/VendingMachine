using Capstone.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Capstone
{
    class Program
    {
        static void Main(string[] args)
        {
            VendoMachine newVendo = new VendoMachine();

            bool stillShopping = true;

            string userChoice = newVendo.DisplayMainMenu();

            while (stillShopping)
            {

                switch (userChoice)
                {
                    case "1":
                        userChoice = newVendo.DisplayItemMenu();

                        if (userChoice == "1")
                        {
                            userChoice = "2";
                        }
                        break;
                    case "2":
                        newVendo.DisplayPurchaseMenu();

                        bool validChoice = false;
                        while (!validChoice)
                        {
                            Console.WriteLine();
                            Console.WriteLine("Make another purchase?");
                            Console.WriteLine("(1) Yes");
                            Console.WriteLine("(2) No");
                            userChoice = Console.ReadLine();
                            validChoice = userChoice == "1" || userChoice == "2";
                        }

                        if (userChoice == "2")
                        {
                            Console.Clear();
                            Console.WriteLine("Vendo-Matic 500");
                            Console.WriteLine();
                            Console.WriteLine("Vendo-500 Thanks You!!!!");
                            stillShopping = false;
                        }
                        break;
                    default:
                        userChoice = newVendo.DisplayMainMenu();
                        break;
                }

            }
            Console.ReadKey();
        }
    }
}
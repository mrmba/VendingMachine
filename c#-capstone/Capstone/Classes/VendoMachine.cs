using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Capstone.Classes
{
    public class VendoMachine
    {
        static StockService stockService = new StockService();
        Transaction transaction = new Transaction();
        List<Product> currentStock = stockService.Products;

        public string DisplayMainMenu()
        {
            string userInput = "";
            bool correctInput = userInput == "1" || userInput == "2";

            while (!correctInput)
            {
                Console.Clear();
                Console.WriteLine("Vendo-Matic 500");
                Console.WriteLine();
                Console.WriteLine("(1) Display Vending Machine Items");
                Console.WriteLine("(2) Purchase");
                Console.WriteLine();
                Console.Write("Choose An Option: ");
                userInput = Console.ReadLine();
                correctInput = userInput == "1" || userInput == "2";
            }

            return userInput;
        }

        public string DisplayItemMenu()
        {
            Console.Clear();
            Console.WriteLine("Vendo-Matic 500");
            Console.WriteLine();

            DisplayProducts();

            string userInput = "";
            bool correctInput = userInput == "1" || userInput == "2";

            while (!correctInput)
            {

                Console.WriteLine();
                Console.Write("(1) Purchase: ");
                Console.Write("(2) Main Menu: ");
                userInput = Console.ReadLine();
                correctInput = userInput == "1" || userInput == "2";
            }
            return userInput;
        }

        public void DisplayPurchaseMenu()
        {
            bool isStillSelecting = true;

            while (isStillSelecting)
            {
                string userInput = "";
                bool correctInput = userInput == "1" || userInput == "2";
                
                while (!correctInput)
                {
                    Console.Clear();
                    Console.WriteLine("Vendo-Matic 500");
                    Console.WriteLine();
                    Console.WriteLine("(1) Feed Money");
                    Console.WriteLine("(2) Select Product");
                    Console.WriteLine("(3) Finish Transaction");
                    Console.WriteLine($"\nTotal Wallet: {transaction.TotalMoneyInserted:c}");
                    Console.Write("\nChoose An Option: ");
                    userInput = Console.ReadLine();
                    correctInput = userInput == "1" || userInput == "2" || userInput == "3";
                }

                switch (userInput)
                {
                    case "1":
                        //Add Money
                        Console.Clear();
                        Console.Write("Insert money ($1, $2, $5, $10): ");
                        userInput = Console.ReadLine();
                        transaction.AcceptCash(userInput);

                        Console.WriteLine();

                        for (int i = 0; i < 7; i++)
                        {
                            Console.Write("===");
                            Thread.Sleep(200);
                        }

                        Console.WriteLine();
                        Console.WriteLine("Cash Accepted!");
                        Thread.Sleep(2000);

                        break;
                    case "2":
                        Console.Clear();
                        Console.WriteLine("Vendo-Matic 500");
                        Console.WriteLine();

                        DisplayProducts();

                        Console.WriteLine($"\nTotal Wallet: {transaction.TotalMoneyInserted:c}");
                        Console.Write("\nChoose an option: ");
                        userInput = Console.ReadLine();
                        transaction.SelectItem(userInput.ToLower(), stockService.Products);


                        Console.WriteLine("\nPress any key to continue");
                        Console.ReadKey();

                        break;
                    case "3":
                        Console.Clear();
                        Console.WriteLine("Vendo-Matic 500");
                        Console.WriteLine();
                        transaction.ReturnChange();
                        SalesReport.UpdateSalesReport(stockService.Products);
                        //SalesReport.UpdateSalesReport(transaction.AllItemsPurchased);
                        isStillSelecting = false;
                        break;
                    default:
                        break;
                }
            }
        }

        private void DisplayProducts()
        {
            Console.WriteLine();

            foreach (var product in currentStock)
            {
                //Set product price to the price, unless its sold out
                string price = String.Format($"{product.Price:c}");
                if (product.Quantity == 0)
                {
                    price = "SOLD OUT";
                }

                Console.WriteLine($"{product.Slot} {product.Name} {price}");
                Thread.Sleep(100);
            }
            Console.WriteLine("==============================================================");
            Console.WriteLine();
        }
    }
}

using Capstone.Classes.Products;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Classes
{
    public class StockService
    {
        // Alll Products with quantity
        public List<Product> Products = new List<Product>();

        public StockService()
        {
            string productFile = @"../../../etc/vendingmachine.csv";
            bool productFileExists = File.Exists(productFile);

            if (productFileExists)
            {
                try
                {
                    List<string> productStreamList = new List<string>();

                    // Get products from a stream and save them to productStreamList
                    using (StreamReader sR = new StreamReader(productFile))
                    {
                        while (!sR.EndOfStream)
                        {
                            
                            string productStream = sR.ReadLine();
                            productStreamList.Add(productStream);
                        }
                    }

                    //Go thru productStreamList and creat products
                    foreach (var productStreamLine in productStreamList)
                    {
                        List<string> productProps = productStreamLine.Split('|').ToList();
                        Product newProduct = null;

                        switch (productProps[0].Substring(0,1))
                        {
                            case "A":
                                newProduct = new Chip(productProps[0], productProps[1], double.Parse(productProps[2]), 5);
                                break;
                            case "B":
                                newProduct = new Candy(productProps[0], productProps[1], double.Parse(productProps[2]), 5);
                                break;
                            case "C":
                                newProduct = new Drinks(productProps[0], productProps[1], double.Parse(productProps[2]), 5);
                                break;
                            case "D":
                                newProduct = new Gum(productProps[0], productProps[1], double.Parse(productProps[2]), 5);
                                break;
                        }
                        
                        Products.Add(newProduct);


                    }
                }
                catch (Exception e)
                {

                    Console.WriteLine("Sorry...Vendo is currently experienceing some technical diffculties:");
                    Console.WriteLine(e.Message);
                }
            }
            else
            {
                Console.WriteLine("Vendo-Matic 500 down for Maintenance");
            }
        }
    }
}

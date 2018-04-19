using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Classes
{
    public class SalesReport
    {

        //Dictionary<string, int> productsInReport = new Dictionary<string, int>();

        public static void UpdateSalesReport(List<Product> itemsPurchased)
        {
            string destinationFolder = @"..\..\..\etc";
            bool directroyExists = Directory.Exists(destinationFolder);
            StockService productService = new StockService();

            if (directroyExists)
            {
                try
                {
                    using (FileStream fS = new FileStream(Path.Combine(destinationFolder ,"SalesReport.txt"), 
                        FileMode.Create, FileAccess.Write ))
                    {
                        using (StreamWriter sW = new StreamWriter(fS))
                        {
                            decimal totalPurchased = 0.0m;
                            int beginningStockAmnt = 5;

                            foreach (var product in itemsPurchased)
                            {
                                int amountSold = beginningStockAmnt - product.Quantity;
                                sW.WriteLine($"{product.Name,-21} | {amountSold}");
                                totalPurchased = (decimal)(amountSold * product.Price);
                                Console.WriteLine();
                            }
                            Console.WriteLine();
                            sW.WriteLine($"Total Purchased: {totalPurchased:c}");
                        }
                    }
                    
                }
                catch (Exception)
                {

                    throw new Exception("Error writing log file.");
                }

            }
        }
    }
}

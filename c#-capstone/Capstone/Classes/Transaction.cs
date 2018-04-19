using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Capstone.Classes
{
    public class Transaction
    {
        //Properties
        public decimal TotalMoneyInserted { get; private set; } //TotalMoneyInserted
        public decimal CustomerChangeDue { get; private set; }
        public decimal TotalSales { get; private set; }

        //Dictionary of arrays that include name, price, and quanity purchased for each item
        public Dictionary<string, int> AllItemsPurchased = new Dictionary<string, int>();
        //String of bills and coins to return
        private Dictionary<string, int> CustomerChangeList = new Dictionary<string, int>()
        {
            {"Five(s)",0 },
            {"One(s)",0 },
            {"Quarter(s)",0 },
            {"Dime(s)",0 },
            {"Nickel(s)",0 }
        };
        

        #region Methods

        /// Method compares inserted money to acceptable denominations, if acceptable, set property
        public void AcceptCash(string moneyStringInserted)
        {
            bool isDouble = double.TryParse(moneyStringInserted, out double moneyInserted);
            bool isCorrectDenomination = moneyInserted == 1 || moneyInserted == 2 || moneyInserted == 5 || moneyInserted == 10;

            if (isDouble && isCorrectDenomination)
            {
                //Change insertedMoney Variable
                //stillChecking = false;
                TotalMoneyInserted += (int)moneyInserted;
                //MachineBalance += (int)moneyInserted;
                Log.UpdateLog($"Feed Money {moneyInserted:c} {TotalMoneyInserted:c} ");/*Total Money Inserted*/

            }
            else
            {
                Console.WriteLine("Money returned......");
                Console.WriteLine("Please insert a whole dollar amount in the form of $1, $2, $5, or $10.");
            }
        }

        //Gives customer their item
        public void SelectItem(string userChoice, List<Product> products)
        { 
            foreach (var product in products)
            {
                if (product.Slot.ToLower() == userChoice.ToLower() && product.Quantity > 0)
                {
                    if ((decimal)product.Price < TotalMoneyInserted)
                    {
                        TotalSales += (decimal)product.Price;
                        Log.UpdateLog($"{product.Name} {product.Slot}: {TotalMoneyInserted:c} {(TotalMoneyInserted - (decimal)product.Price):c}");
                        TotalMoneyInserted -= (decimal)product.Price;

                        Console.WriteLine();
                        product.Consume();
                        
                        //Check Dictionary to See if holds a current quantity for product and add to it.
                        // Dictionary to update log
                        AllItemsPurchased.TryGetValue(product.Name, out int currentTotal);
                        AllItemsPurchased[product.Name] = currentTotal + 1;
                    }
                    else
                    {
                        // IF inserted cash isn't enough to dispense item
                        Console.Clear();
                        Console.WriteLine("Vendo-Matic 500");
                        Console.WriteLine();
                        Console.WriteLine("Insert More Money");
                        Console.ReadKey();
                    }

                    product.Quantity--;
                }
                else if (product.Slot == userChoice && product.Quantity == 0)
                {
                    Console.WriteLine("Sold Out!");
                }

            }
        }

        /// Method compares InsertedMoney to cartTotal, returns customer change.
        public decimal ReturnChange()
        {
            decimal changeGiven = 0;
            // if inserted money is more than cartTotal
            if (TotalMoneyInserted > 0)
            {
                CustomerChangeDue = TotalMoneyInserted;
                TotalMoneyInserted = 0;
                    
                GetLeastAmountOfChange(CustomerChangeDue);

                Console.WriteLine();
                Console.WriteLine($"Total change: {CustomerChangeDue:c}");
                foreach (KeyValuePair<string, int> cash in CustomerChangeList)
                {
                    Console.WriteLine($"{cash.Value} {cash.Key}");
                }
                changeGiven = CustomerChangeDue;
                CustomerChangeDue = 0;
            }
            return changeGiven;
        }

        /// Returns the least amount of change to user
        private void GetLeastAmountOfChange(decimal customerChange)
        {
            //Check why double has a penny

            while (customerChange > 0)
            {
                if (customerChange >= 5)
                {
                    CustomerChangeList["Five(s)"] += 1;
                    customerChange -= 5;
                }
                else if (customerChange >= 1)
                {
                    CustomerChangeList["One(s)"] += 1;
                    customerChange -= 1;
                }
                else if (customerChange >= .25m)
                {
                    CustomerChangeList["Quarter(s)"] += 1;
                    customerChange -= .25m;
                }
                else if (customerChange >= .10m)
                {
                    CustomerChangeList["Dime(s)"] += 1;
                    customerChange -= .10m;
                }
                else if (customerChange >= .05m)
                {
                    CustomerChangeList["Nickel(s)"] += 1;
                    customerChange -= .05m;
                }
            }
            //Update Log
            Log.UpdateLog($"GIVE CHANGE {CustomerChangeDue:C} {customerChange:c}");
        }


        #endregion
    }
}

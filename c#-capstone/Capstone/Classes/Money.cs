using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Classes
{
    public class Money : ILog
    {
        //Properties
        public int InsertedMoney { get; private set; }

        //Methods

        /// <summary>
        /// Method compares inserted money to acceptable denominations, if acceptable, set property
        /// </summary>
        /// <param name="moneyStringInserted"></param>
        public void DenominationCheck(string moneyStringInserted)
        {
            double moneyInserted = 0;
            bool denomAccepted = false;

            while (!denomAccepted)
            {
                if (double.TryParse(moneyStringInserted, out moneyInserted))
                {
                    if (moneyInserted == 1 || moneyInserted == 2 || moneyInserted == 5 || moneyInserted == 10)
                    {
                        denomAccepted = true;
                    }
                }
                else
                {
                    denomAccepted = false;
                    Console.WriteLine("Please insert a whole dollar amount in the form of $1, $2, $5, or $10.");
                }
            }
            InsertedMoney = (int)moneyInserted;
        }

        /// <summary>
        /// Method compares InsertedMoney to cartTotal, returns customer change. 
        /// </summary>
        /// <param name="cartTotal"></param>
        /// <returns></returns>
        public double CreateChange(double cartTotal)
        {
            bool canMakeChange = false;
            double customerChange = 0;
            while(!canMakeChange)
            {
                if (InsertedMoney >= cartTotal)
                {
                    canMakeChange = true;
                    customerChange = InsertedMoney - cartTotal;
                }
                else
                {
                    canMakeChange = false;
                    Console.WriteLine("Please insert more money to complete your purchase.");
                }               
            }
            return customerChange;
        }
    }
}
 
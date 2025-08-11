using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace banktask2
{
    internal class SavingAccount: bank_account
    {
        decimal InterestRate;
        public override decimal CalculateInterest() { 
           return Balance* InterestRate / 100;
                }
        public override void ShowAccountDetails()
        {
            base.ShowAccountDetails();
            Console.WriteLine($"Interest Rate is :{InterestRate}");

        }
    }
}

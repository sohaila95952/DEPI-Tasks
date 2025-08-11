using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace banktask2
{
    internal class CurrentAccount: bank_account
    {
        decimal OverdraftLimit;
        public override decimal CalculateInterest(){ return 0; }
        public override void ShowAccountDetails() { 
            base.ShowAccountDetails();
            Console.WriteLine($"Over draft Limit is :{OverdraftLimit}");

        }
    }
}

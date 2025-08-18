using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace session4
{
    public class SavingsAccount : Account
    {
        //private decimal interest_rate;
        public decimal InterestRate { get; private set; }
        public SavingsAccount(string acount_number, decimal initial_balance, decimal interest_rate):base(acount_number, initial_balance)
        {
            if (interest_rate < 0) 
            { 
                throw new ArgumentOutOfRangeException(nameof(interest_rate),"Interest Rate cant be Negative");
            }
            this.InterestRate=interest_rate;
        }
        public void ApplyInterest() {
            var interestAmount = this.CurrentBalance * this.InterestRate / 100;
            if (interestAmount > 0)
            {
                Deposite(interestAmount);
                Console.WriteLine($"Interst {interestAmount} added to account {AccountNumber}");
            }
        }

        public override void WithDraw(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentOutOfRangeException(nameof(amount), "Amount should be positive");
            if (this.CurrentBalance<amount)
                throw new InvalidOperationException("Insufficient for WithDrawal");
            this.CurrentBalance -= amount;
            var withdraw_transaction = new Transaction(amount, DateTime.Now, "WithDraw");
            AddTransaction(withdraw_transaction);
        }
    }
}

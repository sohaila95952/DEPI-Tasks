using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace session4
{
    public abstract class Account
    {
       
        //fields,properties
        //private static string acount_number = "0";
        //private decimal current_balance;
        //private DateTime date_opened;
        
        public string AccountNumber { get; private set; }
        public decimal CurrentBalance {  get; protected set; }   
        public DateTime DateOpened { get; private set; } 
        //account has a trnasaction =>relationship
        private readonly List<Transaction> transaction_history = new List<Transaction>(); //private field
        public IReadOnlyList<Transaction> TransactionHistory => transaction_history.AsReadOnly();//public property to prevent outside edit
        public Account(string acount_number,decimal initial_balance)
        {
            if (string.IsNullOrWhiteSpace(acount_number))
                throw new ArgumentException("Account Number cant be empty");
           if (initial_balance <0)
                throw new ArgumentOutOfRangeException(nameof(initial_balance),"Initial Balance cant be negative");

            AccountNumber =acount_number;
            CurrentBalance = initial_balance;
            DateOpened= DateTime.Now;
            if (initial_balance>0)
            {
                var initial_transaction = new Transaction(initial_balance,this.DateOpened, "Initial Balance");
                transaction_history.Add(initial_transaction);
            }
        }
        public abstract void WithDraw(decimal amount);
        public virtual void Deposite(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentOutOfRangeException(nameof(amount), " you cant Deposit negative Amount");

            this.CurrentBalance += amount;
            var deposit_transaction = new Transaction(amount, DateTime.Now, "Deposit");
            transaction_history.Add(deposit_transaction);
            
        }
        public string GetAccountHistory()
        {
            var report = new StringBuilder();
            report.AppendLine($"Transactions For Account Number {this.AccountNumber} is :");
            report.AppendLine($"Date\t\t\tAmount\t\t\tDescription");
            foreach (var transaction in transaction_history)
            { report.AppendLine($"{transaction.Date.ToShortDateString()}\t\t\t{transaction.Amount:C}\t\t\t{transaction.Description}"); }
            return report.ToString();
        }
        protected void AddTransaction(Transaction transaction)
        {
          this.transaction_history.Add(transaction);
        }
    
    }
}

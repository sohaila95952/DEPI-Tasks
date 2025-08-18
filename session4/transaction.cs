using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace session4
{
    public class Transaction
    {
        //fields,properties
        //private decimal amount;
        //private DateTime date;
        //private string description;
        public decimal Amount { get; protected set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        //constructor (ctor)
        public Transaction(decimal amount ,DateTime date,string description)
        {
            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(Amount),"Invalid Amount");
            }
            if (string.IsNullOrWhiteSpace(description)) 
            {
                throw new ArgumentException("Description cant be Null or White Space");
            }
            this.Amount = amount;
            this.Date = date;
            this.Description = description;
            
        }

    }
}

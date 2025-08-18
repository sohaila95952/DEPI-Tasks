using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace session4
{
    public class CurrentAccount:Account
    {
        
        public decimal OverDraftLimit { get;private set; }
        public CurrentAccount(string acount_number, decimal initial_balance, decimal over_draft_limit) : base(acount_number, initial_balance)
        {
            if (over_draft_limit < 0)
                throw new ArgumentOutOfRangeException(nameof(over_draft_limit), "Over Draft Limit cant be negative");
            this.OverDraftLimit = over_draft_limit;
        }
        public override void WithDraw(decimal amount)
        {
            if (OverDraftLimit + CurrentBalance < amount)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "WithDraw Amount cant be exceeded ");

            }
            else
            {
                CurrentBalance -= amount;

                var withdraw_transaction = new Transaction(amount, DateTime.Now, "Over Draft WithDraw");
                AddTransaction(withdraw_transaction);
            }

        }
        
        }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace session4
{
    public class Customer
    {
        //private static int id = 0;
        //private int customer_id;
        //private string full_name;
        //private string national_id;
        //private DateTime date_of_birth;
        public int CustomerId { private set; get; }
        public string FullName { private set; get; }
        public string NationalId { private set; get; }
        public DateTime DateOfBirth { private set; get; } 
        //private field
        //customer (has a) account
        private readonly List<Account> accounts = new List<Account>();
        //public property to prevent outside edit
        public IReadOnlyList<Account> Accounts => accounts.AsReadOnly();
        public Customer(int customer_id,string full_name, string national_id, DateTime date_of_birth) {
            if (customer_id <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(customer_id), "Customer Id not valid ");

            }
            if (string.IsNullOrWhiteSpace(full_name))
                throw new ArgumentException("Full Name cant be empty");
            if (string.IsNullOrWhiteSpace(national_id))
                throw new ArgumentException("National Id cant be empty");
            this.CustomerId = customer_id;
            this.FullName = full_name;
            this.NationalId = national_id;
            this.DateOfBirth = date_of_birth;
        }
        void update_details(string new_name,DateTime new_date)
        {
            if (!string.IsNullOrEmpty(new_name))
            {
                this.FullName = new_name;
            }
            this.DateOfBirth = new_date;
        }
        internal void AddAccount(Account account)
        {
            if (account == null) 
            {  
                throw new ArgumentNullException(nameof(account)); 
            }
            accounts.Add(account);

        }
        public decimal ShowTotalBalance() 
        {
            decimal totalBalance = 0;
            foreach (Account account in Accounts)
            {
                totalBalance += account.CurrentBalance;
            }
            return totalBalance;
        }
        
    }
}

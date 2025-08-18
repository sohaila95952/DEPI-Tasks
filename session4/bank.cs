using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace session4
{
    public class bank
    {
        //private string name;
        //private string branch_code;
        public string Name { private set; get; }
        public string BranchCode { private set; get; }
        //bank has customers
        private readonly List<Customer> customers = new List<Customer>();
        //public property to prevent outside edit
        public IReadOnlyList<Customer> Customers => customers.AsReadOnly();

        private int customer_id=0;
        private int account_id=0;
        public bank(string name,string branch_code)
        {
            Name = name;
            BranchCode = branch_code;
        }
        public Customer CreateCustomer(string full_name, string national_id, DateTime date_of_birth)
        {
            int new_customer_id=++customer_id;
            Customer NewCustomer = new Customer(new_customer_id, full_name, national_id, date_of_birth);
            customers.Add(NewCustomer);
            return NewCustomer;
        }
        public Account CreateAccount(int customer_id,decimal initial_balance,string account_type,decimal interest_or_limit) 
        {
            Customer owner=FindCustomerById(customer_id);
            if (owner == null)
            {
                throw new InvalidOperationException("Customer Not Found");
            }
            account_id++;
            string new_account_number= account_id.ToString("D8");
            Account NewAccount;
            if (account_type.Equals("Savings",StringComparison.OrdinalIgnoreCase))
            {
                decimal interest_rate=interest_or_limit;
                NewAccount = new SavingsAccount(new_account_number, initial_balance, interest_rate);
            }
            else if (account_type.Equals("Current", StringComparison.OrdinalIgnoreCase))
            {
                decimal over_draft_limit = interest_or_limit;
                NewAccount = new CurrentAccount(new_account_number, initial_balance, over_draft_limit);
            }
            else
            {
                throw new ArgumentException("Invalid Account Type");
            }
            owner.AddAccount(NewAccount);
            return NewAccount;
        }
        public Customer? FindCustomerById(int customer_id) 
        {
            foreach (Customer Customer in customers)
            {
                if (Customer.CustomerId == customer_id)
                    return Customer;
            }
            return null;
        }
        public Customer? FindCustomerByNationalId(string national_id)
        {
            foreach (Customer Customer in customers)
            {
                if (Customer.NationalId == national_id)
                    return Customer;
            }
            return null;
        }
        public Customer? FindCustomerByName(string name)
        {
            foreach (Customer Customer in customers)
            {
                if (Customer.FullName == name)
                    return Customer;
            }
            return null;
        }
        public Account? FindAccountByNumber(string account_id)
        {
            foreach (Customer Customer in customers)
            {
                foreach (Account Account in Customer.Accounts)
                {
                    if (Account.AccountNumber == account_id)
                        return Account;
                }
                
            }return null;
        }
        public void GenerateBankReport()
        {
            Console.WriteLine($"Bank Report for : {this.Name} Branch : {this.BranchCode}");
            Console.WriteLine("----------------------------------------");
            foreach (Customer Customer in customers)
            {
                Console.WriteLine($"Customer Name : {Customer.FullName} ID : {Customer.CustomerId} NationalId : {Customer.NationalId}");
                if (Customer.Accounts.Count == 0)
                    Console.WriteLine("This Customer has NO Accounts");
                else
                {
                    foreach (Account Account in Customer.Accounts)
                    {
                        Console.WriteLine($"->Account Number : {Account.AccountNumber} , Type : {Account.GetType().Name} , Balance : {Account.CurrentBalance}");
                        Console.WriteLine("----------------------------------------");
                    }
                }
                Console.WriteLine("====================================");
            }
            
        }
        public bool RemoveCustomer(int customer_id) 
        { 
            Customer customer_to_remove=FindCustomerById(customer_id);
            if (customer_to_remove == null) 
            {
                return false;
            }
            else if (customer_to_remove.ShowTotalBalance() == 0)
            {
                customers.Remove(customer_to_remove);
                return true;
            }
            else
                //account has money
                return false;
        }
        public void TransferMoney(string fromAccountNumber, string toAccountNumber,decimal amount)
        {
            Account fromAccount = FindAccountByNumber(fromAccountNumber);
            Account toAccount = FindAccountByNumber(toAccountNumber);
            if (fromAccount == null || toAccount == null) 
            {
                throw new InvalidOperationException("Account Not Found"); 
            }
            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "Amount should be Positive");
            }
            fromAccount.WithDraw(amount);
            toAccount.Deposite(amount);
        }
    }
}

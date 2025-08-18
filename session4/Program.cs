namespace session4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting Bank System....");
            bank testbank = new bank("Bank1","Bank001");
            while (true) 
            {
                ShowMainMenu();
                Console.Write("Please enter your choice: ");
                string choice = Console.ReadLine();
                Console.Clear();
                HandleUserChoice(choice, testbank);
                Console.WriteLine("Press any key to return to the main menu...");
                Console.ReadKey();
                Console.Clear();
            }
        }
      
            static void ShowMainMenu()
            {
                Console.WriteLine("===== Bank Main Menu =====");
                Console.WriteLine("1. Create New Customer");
                Console.WriteLine("2. Create New Account for Existing Customer");
                Console.WriteLine("3. Deposit Money");
                Console.WriteLine("4. Withdraw Money");
                Console.WriteLine("5. Transfer Money");
                Console.WriteLine("6. Show Customer Details & Total Balance");
                Console.WriteLine("7. Show Account Transaction History");
                Console.WriteLine("8. Generate Full Bank Report");
                Console.WriteLine("9. Remove Customer");
                Console.WriteLine("10. Exit");
                Console.WriteLine("==========================");
            }
        static void HandleUserChoice(string choice,bank bank)
        {
            try 
            {
                switch (choice) {
                    case "1":
                        CreateNewCustomer(bank);
                        break;
                    case "2":
                        CreateNewAccount(bank);
                        break;
                    case "3":
                        DepositMoney(bank);
                        break; 
                    case "4":
                        WithDrawMoney(bank);
                        break;
                    case "5":
                        TransferMoney(bank);
                        break;  
                    case "6":
                        ShowCustomerDetails(bank);
                        break;
                    case "7":
                        ShowAccountTransactionHistory(bank);
                        break;
                    case "8":
                        bank.GenerateBankReport();  
                        break;
                    case "9":
                        RemoveCustomer(bank);
                        break;
                    case "10":
                        Console.WriteLine("Cloasing Bank System....");
                        break;
                    default:
                        Console.WriteLine("Invalid Choice");
                        break;

                }
            }
            catch (Exception e) {
            Console.WriteLine($"An Error Occured \n{e.Message}");
            }
        }

        static void CreateNewCustomer(bank bank) 
        {
            Console.WriteLine("-------Create New Customer-------");
            Console.WriteLine("Enter Full Name : ");
            string name=Console.ReadLine();
            Console.WriteLine("Enter National Id : ");
            string nationalid=Console.ReadLine();
            Console.WriteLine("Enter Date Of Birth (YYYY-MM-DD) : ; ");
            DateTime dateofbirth = DateTime.Parse(Console.ReadLine());
            Customer newcustomer = bank.CreateCustomer(name, nationalid, dateofbirth);
            Console.WriteLine("Customer Added Successfully !");
                
        }

         static void CreateNewAccount(bank bank)
        {
            Console.WriteLine("-------Create New Account-------");
            Console.WriteLine("First, let's find the customer:");
            Console.WriteLine("1. By National ID");
            Console.WriteLine("2. By Full Name");
            Console.Write("Enter your choice: ");
            string searchChoice = Console.ReadLine();
            Customer customer= null;
            switch (searchChoice)
            {
                case "1":
                    {
                        Console.Write("Enter National ID : ");
                        string national_id = Console.ReadLine();
                        customer = bank.FindCustomerByNationalId(national_id);
                        break;
                    }
                case "2":
                    {
                        Console.Write("Enter Full Name : ");
                        string full_name = Console.ReadLine();
                        customer = bank.FindCustomerByName(full_name);
                    break;
                    }
                default:
                    {
                        Console.WriteLine("Invalid Choice");
                        return;
                    }
            }
             if(searchChoice== null)
                {
                    throw new InvalidOperationException("Customer Not Found");
                }

                //int id = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter Initial Balance : ");
            decimal initialBalance = decimal.Parse(Console.ReadLine());
            Console.Write("Enter Account Type ('Savings' or 'Current'): ");
            string type=Console.ReadLine();
            decimal rateorlimit = 0;
            if (type.Equals("Savings", StringComparison.OrdinalIgnoreCase))
            {
                Console.Write("Enter Interest Rate (e.g. 5 for 5%): ");
                rateorlimit = decimal.Parse(Console.ReadLine());
            }
            else if (type.Equals("Current", StringComparison.OrdinalIgnoreCase))
            {
                Console.Write("Enter Overdraft Limit: ");
                rateorlimit = decimal.Parse(Console.ReadLine());
            }
            Account newaccount=bank.CreateAccount(customer.CustomerId,initialBalance,type,rateorlimit);
            Console.WriteLine("Account Created Successfully !");
            Console.WriteLine($"  >> IMPORTANT: The new Account Number is: {newaccount.AccountNumber} <<");

        }
         static void DepositMoney(bank bank)
        {
            Console.WriteLine("-------Deposit Money-------");
            Console.WriteLine("Enter Account Number : ");
            string account_number=Console.ReadLine();
            Account account=bank.FindAccountByNumber(account_number);
            if (account == null)
            {
                throw new InvalidOperationException("Account Not Found");
            }
            Console.WriteLine($"Enter Amount to Deposit (Current Balance is :{account.CurrentBalance}): ");
            decimal amount=decimal.Parse(Console.ReadLine());
            account.Deposite(amount);
            Console.WriteLine($"Success! New Balance Is : {account.CurrentBalance:C}");
        }

        static void WithDrawMoney(bank bank)
        {
            Console.WriteLine("-------WithDraw Money-------");
            Console.WriteLine("Enter Account Number : ");
            string account_number = Console.ReadLine();
            Account account = bank.FindAccountByNumber(account_number);
            if (account == null)
            {
                throw new InvalidOperationException("Account Not Found");
            }
            Console.WriteLine($"Enter Amount to WithDraw (Current Balance is :{account.CurrentBalance}): ");
            decimal amount = decimal.Parse(Console.ReadLine());
            account.WithDraw(amount);
            Console.WriteLine($"Success! New Balance Is : {account.CurrentBalance:C}");
        }
        static void TransferMoney(bank bank) 
        {
            Console.WriteLine("-------Transfer Money-------");
            Console.WriteLine("Enter Account Number to Tranfer From: ");
            string from_account_number = Console.ReadLine();
            Console.WriteLine("Enter Account Number to Tranfer To: ");
            string to_account_number = Console.ReadLine();
            Console.WriteLine("Enter Amount to Tranfer: ");
            decimal amount = decimal.Parse(Console.ReadLine());
            bank.TransferMoney(from_account_number, to_account_number, amount);
            Console.WriteLine("Success! Transfer completed.");

        }
        static void ShowCustomerDetails(bank bank)
        {
            Console.WriteLine("------- Show Customer Details -------");
            Console.Write("Enter National ID to find customer : ");
            string natinal_id= Console.ReadLine();
            Customer customer=bank.FindCustomerByNationalId(natinal_id);
            if (customer == null)
            {
                throw new InvalidOperationException("Customer not found.");
            }
            Console.WriteLine($"Details For Customer :  {customer.FullName} , Id : {customer.CustomerId}");
            Console.WriteLine($"Total Balance : {customer.ShowTotalBalance:C}");
            Console.WriteLine("Accounts:");
            foreach (var account in customer.Accounts)
            {
                Console.WriteLine($"  -> Account Number : {account.AccountNumber}, Type: {account.GetType().Name}, Balance: {account.CurrentBalance:C}");

            }

        }
        static void ShowAccountTransactionHistory(bank bank)
        {
            Console.WriteLine("------ Account Transaction History ------");
            Console.Write("Enter Account Number : ");
            string account_number = Console.ReadLine();
            Account account = bank.FindAccountByNumber(account_number);
            if (account == null)
            {
                throw new InvalidOperationException("Account not found.");
            }
            //return string
            Console.WriteLine(account.GetAccountHistory());
        }
        static void RemoveCustomer(bank bank)
        {
            Console.WriteLine("------ Remove Customer ------");
            Console.Write("Enter the National ID of the customer you want to remove: ");
            string nationalId = Console.ReadLine();
            Customer customerToRemove = bank.FindCustomerByNationalId(nationalId);
            if (customerToRemove == null)
            {
                throw new InvalidOperationException("Customer not found with the provided National ID.");
            }
            Console.WriteLine($"Found Customer: {customerToRemove.FullName} (ID: {customerToRemove.CustomerId})");
            Console.WriteLine($"Total Balance: {customerToRemove.ShowTotalBalance():C}");
            Console.Write("Are you sure you want to remove this customer? (yes/no): ");
            string confirmation = Console.ReadLine();
            if (confirmation.Equals("yes", StringComparison.OrdinalIgnoreCase))
            {
                bool wasRemoved = bank.RemoveCustomer(customerToRemove.CustomerId);
                if (wasRemoved)
                {
                    Console.WriteLine("Success! Customer has been removed.");
                }
                else
                {
                    throw new InvalidOperationException("Action failed: Customer cannot be removed because their total balance is not zero.");
                }

            }
            else
            {
                Console.WriteLine("Action cancelled.");
            }
            }
        }
    }


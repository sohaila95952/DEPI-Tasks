using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bank
{
    internal class bank_account
    {
        const string BankCode = "BNK001";
        readonly DateTime CreatedDate;
        private int _accountNumber;
        private string _fullName ;
        private string _nationalID;
        private string _phoneNumber;
        private string _address ;
        private decimal _balance;
        public string FullName
        {
            get { return _fullName;  }

            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException("Full name cannot be null or empty.");
                else
                    _fullName = value;
                
                //else {
                //    Console.WriteLine("enter valid name");
                //    //break;
                //}
               
            }
        }
        public string NationalID
        {
            get { return _nationalID; }

            set
            {
                if (value.Length == 14)
                    _nationalID = value;
                    
               else 
                    throw new ArgumentException("National ID must be exactly 14 digits.");
                //
                //{
                //    Console.WriteLine("enter valid NationalID");

                //}
            }
        }
        public string PhoneNumber
        {
            get { return _phoneNumber; }

            set
            {
                if (value.Length == 11 && value.StartsWith("01"))
                    _phoneNumber = value;
                    else
                    throw new ArgumentException("Phone number must be 11 digits and start with '01'.");

                
                //else
                //{
                //    Console.WriteLine("enter valid PhoneNumber");

                //}
            }
        }
        public decimal Balance
        {
            get { return _balance; }

            set
            {
                if (value > 0)
                    _balance = value;
                else
                {
                    Console.WriteLine("enter valid Balance");

                }
            }
        }
        public string Address { set; get; }

        public bank_account()
        {
            _fullName = "";
            _nationalID = "";
            _phoneNumber = "";
            _balance = 0;
            CreatedDate = DateTime.Now;
        }
        public bank_account(string fullname, string nationalid, string phonenumber, string address, decimal balance)
        {
            _fullName = fullname;
            _nationalID = nationalid;
            _phoneNumber = phonenumber;
            _balance = balance;
            _address = address;
            CreatedDate = DateTime.Now;
        }
        public bank_account(string fullname, string nationalid, string phonenumber, string address) :
                this(fullname, nationalid, phonenumber, address, 0)
        { }
        public void ShowAccountDetails()
        {
            Console.WriteLine("--------------------------");
            Console.WriteLine($"full name is :{_fullName}");
            Console.WriteLine($"National ID is :{_nationalID}");
            Console.WriteLine($"Phone Number is :{_phoneNumber}");
            Console.WriteLine($"Address is :{_address}");
            Console.WriteLine($"Balance is :{_balance}");
            Console.WriteLine($"Bank Code is :{BankCode}");
            Console.WriteLine($"Created Date is :{CreatedDate}");

        }
        public bool IsValidNationalID(string _nationalID)
        {
            return (_nationalID.Length == 14);
        }
        public bool IsValidPhoneNumber(string _phoneNumber)
        {
            return (_phoneNumber.Length == 11 && _phoneNumber.StartsWith("01"));
        }
    }
}

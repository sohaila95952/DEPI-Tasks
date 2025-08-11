using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace banktask2
{
    
        internal class bank_account
        {
            const string BankCode = "BNK001";
            readonly DateTime CreatedDate;
            private int _accountNumber;

            private string _fullName;
            private string _nationalID;
            private string _phoneNumber;
            private string _address;
            private decimal _balance;
            public string FullName
            {
                get { return _fullName; }

                set
                {

                    if (string.IsNullOrEmpty(_fullName))
                    {
                        throw new ArgumentException("enter valid name");
                        //Console.WriteLine("enter valid name");

                    }
                    _fullName = value;

                }
            }
            public string NationalID
            {
                get { return _nationalID; }

                set
                {
                    if (value == null || value.Length != 14)
                    {
                        throw new ArgumentException("enter valid NationalID");
                    }

                    _nationalID = value;
                }
            }
            public string PhoneNumber
            {
                get { return _phoneNumber; }

                set
                {
                    if (value == null || value.Length != 11 && !value.StartsWith("01"))
                        throw new ArgumentException("Phone number must be 11 digits and start with '01'.");
                    _phoneNumber = value;

                }
            }
            public decimal Balance
            {
                get { return _balance; }

                set
                {
                    if (value < 0)
                        throw new ArgumentException("enter valid Balance");
                    _balance = value;

                }
            }
            public string Address { set; get; }
            public int AccountNumber { set; get; }


            public bank_account()
            {
                _fullName = "Default";
                _nationalID = "00000000000000";
                _phoneNumber = "01000000000";
                _address = "Default";
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
                _accountNumber = 0;
                CreatedDate = DateTime.Now;
            }
            public bank_account(string fullname, string nationalid, string phonenumber, string address) :
                    this(fullname, nationalid, phonenumber, address, 0)
            { }
            public virtual void ShowAccountDetails()
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
            public virtual decimal CalculateInterest() => 0;
    }
    }



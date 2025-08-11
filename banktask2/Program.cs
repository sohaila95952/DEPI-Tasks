namespace banktask2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SavingAccount s1= new SavingAccount();
            CurrentAccount c1 = new CurrentAccount();
            List <bank_account> l1= new List<bank_account> { s1,c1 };
            foreach (var l in l1)
            {
                l.ShowAccountDetails();
                l.CalculateInterest();
            }
        }
    }
}

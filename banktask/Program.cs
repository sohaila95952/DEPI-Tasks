namespace bank
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bank_account bank1 = new bank_account("sohaila", "30405091300162", "01113350725", "sharkia", 10000);
            bank_account bank2 = new bank_account("x", "12345678912345", "01012345678", "cairo", 10000);
            bank1.ShowAccountDetails();
            bank2.ShowAccountDetails();
        }
    }
}

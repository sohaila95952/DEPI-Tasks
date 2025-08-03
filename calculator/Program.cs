namespace calculator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            Console.WriteLine("Hello!");
            Console.WriteLine("Input the first number:");
            int x= Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Input the second number:");
            int y = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("What do you want to do with those numbers?");
            Console.WriteLine("[A]dd");
            Console.WriteLine("[S]ubtract");
            Console.WriteLine("[M]ultiply");
            char ch =Convert.ToChar(Console.ReadLine());
            switch (ch) {
                case 'a':
                case 'A':
                    Console.WriteLine($"The Result Of Addition is :\n{x}+{y} = {x+y}");
                    break;
                case 's':
                case 'S':
                    Console.WriteLine($"The Result Of Subtraction is :\n{x}-{y} = {x-y}");
                    break;
                case 'm':
                case 'M':
                    Console.WriteLine($"The Result Of Multiplication is :\n{x}*{y} = {x * y}");
                    break;
                default:
                    Console.WriteLine("Invalid option");
                    break;
            }
            Console.WriteLine("Press any key to close");
            Console.ReadKey(true);
        }
    }
}

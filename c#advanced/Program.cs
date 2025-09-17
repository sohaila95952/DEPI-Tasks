namespace c_advanced
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            #region 1
            //PhoneBook person1=new PhoneBook("sohaila",01113350725);
            //Console.WriteLine(person1["sohaila"]);
            #endregion
            #region 2
            //WeeklySchedule Schedule = new WeeklySchedule();
            //Schedule["saturday"].Add("session1");
            //Schedule["saturday"].Add("session2");
            //Schedule.print("saturday");
            ////Console.WriteLine(Schedule["saturday"]);
            //Console.WriteLine(string.Join(", ", Schedule["saturday"]));
            #endregion
            #region 3
            //int[,] matrix = new int[,] {
            //    {1, 2, 3,4 }, 
            //    {5, 6, 7, 8 }, 
            //    {9, 10, 11, 12 },
            //    {13, 14, 15, 16 }
            //};
            //Matrix matrix1= new Matrix(matrix);
            //Console.WriteLine($"{matrix1[1, 1]} + {matrix1[2, 2]} = {matrix1[1,1]+ matrix1[2, 2]}");
            #endregion
            #region 4
            //MyStack<int> stack1 = new MyStack<int>();
            //stack1.Push(1);
            //stack1.Push(2);
            //stack1.Push(3);
            //Console.WriteLine(stack1.Pop());
            //Console.WriteLine(stack1.Peek());
            //MyStack<string> stack2 = new MyStack<string>();
            //stack2.Push("hello");
            //stack2.Push("to");
            //stack2.Push("my stack");
            //Console.WriteLine(stack2.Pop());
            //Console.WriteLine(stack2.Peek());
            #endregion
            #region 5
            //Pair<string, int> pair = new Pair<string, int>("sohaila", 1);
            //Console.WriteLine(pair.ToString());
            ////pair<T> =>takes one type parameter only
            ////Pair<string> pair1 = new Pair<string>("sohaila"," 1");
            #endregion
            #region 6
            //Cache<int, string> cache = new Cache<int, string>(1, "sohaila", DateTime.Now.AddHours(1));
            //cache.Add(2, "salma", DateTime.Now.AddSeconds(5));
            //Console.WriteLine("Key 1: " + cache.GetValue(1)); 
            //Console.WriteLine("Key 2: " + cache.GetValue(2));
            //System.Threading.Thread.Sleep(6000);
            //Console.WriteLine("==============");
            //Console.WriteLine("Key 1: " + cache.GetValue(1)); 
            //Console.WriteLine("Key 2: " + cache.GetValue(2)); 
            #endregion
            #region 7
            //List <int> numbers = new List <int>() { 1, 2, 3 };
            ////function takes list ,function of conversion=>anonymos function using lambda expression
            ////converter function takes each item in numbers and return it converted to string
            //List<string> strings = ConvertList(numbers, x => x.ToString());
            //foreach (var s in strings) { 
            //Console.WriteLine(s);
            //}
            #endregion
            #region 8
            //Repository<student> repository = new Repository<student>();
            //repository.Add(new student(1,"sohaila"));

            //var student1= repository.Read(1);
            //Console.WriteLine($"{student1.Id} , {student1.Name}");

            //student student2 = new student(1, "updated");
            //repository.Update(1,student2);
            //var student3 = repository.Read(1);
            //Console.WriteLine($"{student3.Id} , {student3.Name}");

            //repository.Delete(1);
            #endregion
            #region 9
            //Dictionary<string,string> contact_manager=new Dictionary<string,string>();
            ////add
            //contact_manager.Add("sohaila1", "01113350725");
            //contact_manager.Add("sohaila2", "01015769614");
            //Console.WriteLine("after adding:");
            //foreach (var item in contact_manager)
            //{
            //    Console.WriteLine($"key : {item.Key} , Value : {item.Value}");
            //}
            ////remove
            //contact_manager.Remove("sohaila2");
            //Console.WriteLine("after removing:");
            //foreach (var item in contact_manager)
            //{
            //    Console.WriteLine($"key : {item.Key} , Value : {item.Value}");
            //}
            ////search
            //Console.WriteLine("search by key:");
            //if (contact_manager.ContainsKey("sohaila1"))
            //{
            //    Console.WriteLine($"key : sohaila1 , Value : {contact_manager["sohaila1"]}");
            //}
            //else
            //{
            //    Console.WriteLine("not found");
            //    return;
            //}
            #endregion
            #region 10
            //shoppingcart shoppingcart = new shoppingcart();
            //shoppingcart.addItem("apple", 10);
            //shoppingcart.addDiscount("apple");
            //shoppingcart.showCart();
            //shoppingcart.removeItem("apple");
            //shoppingcart.showCart();
            #endregion
            #region 11
            //int?[] arr0 = { 0,0,0 };
            //int?[] arr1 = {1,2,3};
            //int?[] arr2 = { null, null };
            //Console.WriteLine( Avg(arr0));
            //Console.WriteLine(Avg(arr1));
            //Console.WriteLine(Avg(arr2));
            #endregion
            #region 12
            //Person person1 = new Person("sohaila", new DateTime( 2004,5,9));
            //Console.WriteLine( person1.ToString());
            //Person person2 = new Person(null, null);
            //Console.WriteLine(person2.ToString());
            #endregion
            #region 13
            //int a = 2;
            //int b = 3;
            //Console.WriteLine(a.IsEven());
            //Console.WriteLine(b.IsOdd());
            //int c = 4;
            //int d= 5;
            //Console.WriteLine(c.IsPrime());
            //Console.WriteLine(d.IsPrime());
            //Console.WriteLine(d.Factorial());
            #endregion
            #region 14
            //DateTime date = DateTime.Now;
            //Console.WriteLine( date.StartOfWeek());
            //Console.WriteLine( date.EndOfWeek());
            //Console.WriteLine( date.StartOfMonth() );
            //Console.WriteLine( date.EndOfMonth() );
            //Console.WriteLine(date.StartOfYear());
            //Console.WriteLine( date.EndOfYear() );
            //DateTime birthdate = new DateTime(2004,09,05);
            //Console.WriteLine( birthdate.CalculateAge() );
            //Console.WriteLine( date.isBusinessDay() );
            #endregion
            #region 16

            //int x = 10;
            //int y = 2;
            //MathOperation operation = Add;
            //Console.WriteLine(operation(x, y));
            //operation=Subtract;
            //    Console.WriteLine(operation(x, y));
            //    operation = Multiply;
            //    Console.WriteLine(operation(x, y));
            //    operation=Divide;
            //    Console.WriteLine(operation(x, y));

            #endregion
            #region 17
            //Notification notification = sendEmail;
            //notification += sendSms;
            //notification("Hello in my delegate");

            #endregion
            #region 19
            //    List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6 };
            //    //Define filter
            //    Filteration<int> isEven = x => x % 2 == 0;
            //    //Define transformation
            //    Transformation<int, int> square = x => x * x;
            ////Apply filter
            //List<int> evenNumbers = Filter(numbers, isEven);
            //Console.WriteLine("Even numbers: " + string.Join(", ", evenNumbers));

            //    // 5. Apply transformations
            //    var squaredNumbers = Transform(numbers, square);
            //    Console.WriteLine("Squared: " + string.Join(", ", squaredNumbers));
            #endregion
            #region 20
            //List<int> grades = new List<int>() { 45, 50, 60, 75, 80, 95 };
            //List<int> passed=grades.Where(g => g >= 60).ToList();
            //Console.WriteLine("Passed students: " + string.Join(", ", passed));
            //List<string> letters = grades.Select(g => g >= 90 ? "A" :g >= 80 ? "B" :g >= 70 ? "C" :g >= 60 ? "D" : "F").ToList();
            //Console.WriteLine(string.Join(", ", letters));
            //double average = grades.Average();
            //Console.WriteLine("Average grade: " + average);
            #endregion
            #region 22
            //var timer = new SimpleTimer(5);
            //var monitor = new TimerMonitor();
            //monitor.Subscribe(timer);
            //timer.Start();
            //Console.ReadLine();
            #endregion
            #region 23
            ////->main :static async Task Main(string[] args)
            //string file = "test.txt";
            //string content = "hello from my file";
            //await WriteFile(file, content);
            //string read = await ReadFile(file);
            //if (read != null)
            //{
            //    Console.WriteLine(read);
            //}

            #endregion
            #region 24
            #endregion
        }






        //7
        public static List<TTarget> ConvertList<TSource, TTarget>(List<TSource> source, Func<TSource, TTarget> converter/*delegate جاهزة*/)
        {

            List<TTarget> list2 = new List<TTarget>();
            foreach (TSource item in source)
            {
                TTarget target1 = converter(item);
                list2.Add(target1);
            }
            return list2;
        }
        //11
        public static double? Avg(int?[] arr)
        {
            int sum = 0;
            int count = 0;

            foreach (int? item in arr)
            {
                if (item.HasValue)
                {
                    sum += item.Value;
                    count++;
                }
            }
            if (count == 0)
            {
                Console.WriteLine("no average for null");
                return null;
            }
            else
            {

                return sum / arr.Count();
            }
        }
        //16
        public delegate int MathOperation(int x, int y);
        public static int Add(int x, int y) => x + y;
        public static int Subtract(int x, int y) => x - y;
        public static int Multiply(int x, int y) => x * y;
        public static int Divide(int x, int y) => x / y;
        //17
        public delegate void Notification(string message);
        public static void sendEmail(string message)
        {
            Console.WriteLine($"Email :{message}");
        }
        public static void sendSms(string message)
        {
            Console.WriteLine($"SMS :{message}");


        }
        //19
        public delegate TOutput Transformation<TInput, TOutput>(TInput input);
        public delegate bool Filteration<T>(T item);
        public static List<T> Filter<T>(List<T> data, Filteration<T> filter)
        {
            List<T> filteredData = new List<T>();
            foreach (T item in data)
            {
                if (filter(item))
                {
                    filteredData.Add(item);
                }
            }
            return filteredData;
        }
     
        public static List<TOutput> Transform<TOutput,T>(List<T> data, Transformation<T, TOutput> transform)
        {
            List<TOutput> transformedData = new List<TOutput>();
            foreach (T item in data)
            {
                transformedData.Add(transform(item));
            }
            return transformedData;
        }
        //23
        public static async Task WriteFile(string path, string content)
        {
            try
            {
                await File.WriteAllTextAsync(path, content);
                Console.WriteLine($"Successfully wrote");
            }
            catch (Exception ex)
            {
                Console.WriteLine("An unexpected error occurred while Writing ");
            }
        }
        public static async Task<string> ReadFile(string path)
        {
            try
            {
                if (!File.Exists(path))
                {
                    Console.WriteLine("File does not exist.");
                    return null;
                }
                string content = await File.ReadAllTextAsync(path);
                Console.WriteLine("Successfully read");
                return content;
            }
            catch (Exception ex) {
                Console.WriteLine("An unexpected error occurred while reading ");
                return null;
            }

        }
    }
}
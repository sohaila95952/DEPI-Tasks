using LibraryManagementSystem;
using LINQ_DATA;
using static System.Reflection.Metadata.BlobBuilder;
using System.ComponentModel;
using System.Globalization;

namespace linq
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //1
            var availableBooks = LibraryData.Books
            .Where(book => book.IsAvailable == true);
            foreach (var book in availableBooks)
            {
                Console.WriteLine($"{book.Title} => {book.IsAvailable}");

                ////2
                //var titles = LibraryData.Books.Select(book => book.Title).ToList();
                //foreach (var title in titles)
                //{
                //    Console.WriteLine(title);
                //}

                ////3
                //var programmingBooks = LibraryData.Books.Where(book => book.Genre == "Programming");
                //foreach (var book in programmingBooks) {
                //Console.WriteLine($"{book.Title} => {book.Genre}");
                //}

                ////4
                //var sorted=LibraryData.Books.OrderBy(book=>book.Title).ToList();
                //foreach (var book in sorted)
                //{
                //    Console.WriteLine($"{book.Title}");
                //}

                ////5
                //var expensive = LibraryData.Books.Where(book => book.Price > 30);
                //foreach (var book in expensive) { 
                //Console.WriteLine($"{book.Title} => {book.Price}");
                //}

                ////6
                //var genres=LibraryData.Books.DistinctBy(book=>book.Genre);
                //foreach (var book in genres) {
                //    Console.WriteLine(book.Genre);
                //}

                ////7
                //var genregroups=LibraryData.Books.GroupBy(book=>book.Genre)
                //    .Select(genre=>new
                //    {
                //        genre=genre.Key,
                //        count=genre.Count()
                //    })
                //    ;
                //genregroups.ToConsoleTable();

                ////8
                //var recent = LibraryData.Books
                //    .Where(book => book.PublishedYear > 2010)
                //    .OrderBy(book=>book.PublishedYear);
                //recent.ToConsoleTable();

                ////9
                //var first5 = LibraryData.Books.Take(5);
                //first5.ToConsoleTable();

                ////10
                //var result = LibraryData.Books.Any(book => book.Price > 50);
                //Console.WriteLine(result);

                ////11
                //var book_author = LibraryData.Books
                //    .Join(LibraryData.Authors, book => book.AuthorId, author => author.Id, (book, author)
                //    => new
                //    {
                //        book.Title,
                //        book.Genre,
                //        author.Name
                //    });
                //    book_author.ToConsoleTable();

                ////12
                //var average_genre = LibraryData.Books
                //    .GroupBy(x => x.Genre)
                //    .Select(genre => new
                //    {
                //        Genre = genre.Key,
                //        average = genre.Average(x => x.Price)
                //    }
                //    );
                //average_genre.ToConsoleTable();

                ////13
                //var expensive=LibraryData.Books.OrderByDescending(x=>x.Price).Take(1);
                //expensive.ToConsoleTable();

                ////14
                //var puplished_decade = LibraryData.Books
                //    .GroupBy(x => (x.PublishedYear / 10) *10)
                //    .Select(x => new
                //    {
                //        decade=x.Key,
                //        books =string.Join(" ,",x.Select(b=>b.Title))

                //    } ).ToList();
                //puplished_decade.ToConsoleTable();

                ////15
                //var active_loan = LibraryData.Loans
                //    .Where(l => l.ReturnDate == null)
                //    .Select(l => new { l.MemberId, l.ReturnDate });
                //active_loan.ToConsoleTable();

                ////16
                //var borrowed_more_than_once = LibraryData.Loans
                //    .GroupBy(x => x.BookId)
                //    .Where(x => x.Count() > 1)
                //    .Select(x => new
                //    {
                //        BookId = x.Key,
                //        Count = x.Count()
                //    });
                //borrowed_more_than_once.ToConsoleTable();

                ////17
                //var overdue = LibraryData.Loans
                //    .Where(x => x.ReturnDate == null && x.DueDate < DateTime.Now);
                //overdue.ToConsoleTable();

                ////18
                //var Author_Book_Counts = LibraryData.Books
                //    .GroupBy(x => x.AuthorId)
                //    .Select(x => new
                //    {
                //        author = x.Key,
                //        count = x.Count()
                //    });
                //Author_Book_Counts.ToConsoleTable();

                ////19
                //var categories = LibraryData.Books
                //    .GroupBy(b => b.Price < 20 ? "cheap" :
                //    b.Price <= 40 ? "medium" :
                //    "expensive")
                //    .Select(x =>
                //    new
                //    {
                //        Category = x.Key,
                //        Count = x.Count()
                //    });
                //categories.ToConsoleTable();

                ////20
                //var statistics = LibraryData.Loans
                //    .GroupBy(x => x.MemberId)
                //    .Select(x =>
                //    new
                //    {
                //        member_id = x.Key,
                //        total_loans = x.Count(),
                //        active_loans = x.Count(x => x.ReturnDate == null),
                //        average_days = x.Where(l => l.ReturnDate != null)
                //        .Select(x=>(x.ReturnDate.Value-x.LoanDate).TotalDays)
                //        .Average()
                //    });
                //statistics.ToConsoleTable();   

            }
    }
}
    

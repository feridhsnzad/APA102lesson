using System;

namespace LibrarySystem.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int Year { get; set; }
        public int PageCount { get; set; }

        public Book(int id, string title, string author, int year, int pageCount)
        {
            Id = id;
            Title = title;
            Author = author;
            Year = year;
            PageCount = pageCount;
        }

        public void DisplayInfo()
        {
            Console.WriteLine($"[{Id}] {Title} - {Author} ({Year}) - {PageCount} səhifə");
        }
    }
}

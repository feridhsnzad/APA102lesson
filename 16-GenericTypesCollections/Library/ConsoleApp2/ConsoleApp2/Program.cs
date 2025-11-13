using LibrarySystem.Models;
using LibrarySystem.Managers;
using LibrarySystem.Generics;
using System;
using System.Collections.Generic;

namespace LibrarySystem
{
    class Program
    {
        static void Main()
        {
            var book1 = new Book(1, "Martin Eden", "Jack London", 1909, 400);
            var book2 = new Book(2, "1984", "George Orwell", 1949, 328);
            var book3 = new Book(3, "Animal Farm", "George Orwell", 1945, 112);
            var book4 = new Book(4, "Ağ Gəmi", "Cingiz Aytmatov", 1970, 200);
            var book5 = new Book(5, "Qırıq Budaq", "Elçin", 1998, 350);

            book1.DisplayInfo();
            book2.DisplayInfo();
            book3.DisplayInfo();
            book4.DisplayInfo();
            book5.DisplayInfo();

            Console.WriteLine("\n=== Generic Library Test ===");
            var lib = new Library<Book>("Milli Kitabxana");
            lib.Add(book1);
            lib.Add(book2);
            lib.Add(book3);
            lib.Add(book4);
            lib.Add(book5);

            Console.WriteLine($"Kitab sayı: {lib.Count()}");
            lib.FindByIndex(0).DisplayInfo();
            lib.FindByIndex(2).DisplayInfo();

            Console.WriteLine("Bütün kitablar:");
            foreach (var b in lib.GetAll())
                b.DisplayInfo();

            Console.WriteLine("\n=== Üzvlər və Borrow Test ===");
            var members = new List<Member>()
            {
                new Member(1, "Ali Məmmədov", "ali@mail.com"),
                new Member(2, "Leyla Həsənova", "leyla@mail.com"),
                new Member(3, "Vüqar Əliyev", "vuqar@mail.com")
            };

            var member = members[0];
            member.BorrowBook(book1);
            member.BorrowBook(book2);
            member.DisplayBorrowedBooks();
            member.ReturnBook(1);
            member.DisplayBorrowedBooks();
            member.BorrowBook(book3);
            member.BorrowBook(book4);
            member.BorrowBook(book5);
            member.BorrowBook(book1); 

            Console.WriteLine("\n=== Dictionary Test ===");
            var manager = new BookManager();
            manager.AddBook(book1);
            manager.AddBook(book2);
            manager.AddBook(book3);
            manager.AddBook(book4);
            manager.AddBook(book5);

            void ShowBooks(string author)
            {
                var list = manager.GetBooksByAuthor(author);
                Console.WriteLine($"\n{author} üçün tapılan kitablar: {list.Count}");
                foreach (var b in list) b.DisplayInfo();
            }

            ShowBooks("George Orwell");
            ShowBooks("Cingiz Aytmatov");
            ShowBooks("Jack London");
            ShowBooks("Dostoyevski");

            Console.WriteLine("\n=== Queue Test ===");
            manager.AddToWaitingQueue("Nigar");
            manager.AddToWaitingQueue("Rəşad");
            manager.AddToWaitingQueue("Səbinə");

            Console.WriteLine($"Növbədə: {manager.WaitingQueue.Count}");
            var served = manager.ServeNextInQueue();
            Console.WriteLine($"Xidmət edilir: {served}");
            Console.WriteLine($"Qalan növbə: {manager.WaitingQueue.Count}");
            manager.ServeNextInQueue();
            manager.ServeNextInQueue();
            Console.WriteLine($"Növbə boşdumu? {manager.WaitingQueue.Count == 0}");

            Console.WriteLine("\n=== Stack Test ===");
            manager.ReturnBook(book1);
            manager.ReturnBook(book2);
            manager.ReturnBook(book3);
            Console.WriteLine($"Stack-də kitab sayı: {manager.RecentlyReturned.Count}");
            var last = manager.GetLastReturnedBook();
            Console.WriteLine($"Son qaytarılan: {last.Title}");
            manager.RecentlyReturned.Pop();
            Console.WriteLine($"Yeni stack sayı: {manager.RecentlyReturned.Count}");
            Console.WriteLine($"İndi son qaytarılan: {manager.GetLastReturnedBook().Title}");

            Console.WriteLine("\n=== Axtarış Test ===");
            var found = manager.SearchByTitle("1984");
            Console.WriteLine(found != null ? $"Tapıldı: {found.Title}" : "Tapılmadı");
            var notFound = manager.SearchByTitle("Harry Potter");
            Console.WriteLine(notFound == null ? "Tapılmadı (null)" : "Tapıldı");

            Console.WriteLine("\n=== Statistika ===");
            Console.WriteLine($"Ümumi kitab: {manager.Books.Count}");
            Console.WriteLine($"Ümumi üzv: {members.Count}");
            Console.WriteLine($"Növbədə: {manager.WaitingQueue.Count}");
            Console.WriteLine($"Stack-də: {manager.RecentlyReturned.Count}");

            int minYear = int.MaxValue, maxYear = int.MinValue;
            foreach (var b in manager.Books)
            {
                if (b.Year < minYear) minYear = b.Year;
                if (b.Year > maxYear) maxYear = b.Year;
            }
            Console.WriteLine($"Ən köhnə kitab ili: {minYear}");
            Console.WriteLine($"Ən yeni kitab ili: {maxYear}");
        }
    }
}

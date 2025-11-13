using System;
using CafeOrderSystem.Enums;
using CafeOrderSystem.Models;

namespace CafeOrderSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            var order1 = new DrinkOrder(101, "Ali", DrinkType.Coffee, DrinkSize.Medium);
            var order2 = new DrinkOrder(102, "Leyla", DrinkType.Tea, DrinkSize.Large);
            var order3 = new DrinkOrder(103, "Vüqar", DrinkType.Juice, DrinkSize.Small);

            order1.DisplayOrder();
            order1.UpdateStatus(OrderStatus.Preparing);
            order1.UpdateStatus(OrderStatus.Ready);
            order1.UpdateStatus(OrderStatus.Delivered);

            order2.DisplayOrder();
            order2.UpdateStatus(OrderStatus.Ready);

            order3.DisplayOrder();

            Console.WriteLine("\nBütün DrinkType dəyərləri:");
            foreach (var drink in Enum.GetValues(typeof(DrinkType)))
                Console.WriteLine($"- {drink}");

            Console.WriteLine("\nBütün DrinkSize dəyərləri:");
            foreach (var size in Enum.GetValues(typeof(DrinkSize)))
                Console.WriteLine($"- {size}");

            Console.WriteLine("\nBütün OrderStatus dəyərləri:");
            foreach (var status in Enum.GetValues(typeof(OrderStatus)))
                Console.WriteLine($"- {status}");

            Console.WriteLine($"\nToString nümunəsi: {DrinkType.Coffee.ToString()}, {DrinkSize.Large.ToString()}");

            var parsedDrink = (DrinkType)Enum.Parse(typeof(DrinkType), "Tea");
            var parsedSize = (DrinkSize)Enum.Parse(typeof(DrinkSize), "Medium");
            Console.WriteLine($"Parse nümunəsi: Drink={parsedDrink}, Size={parsedSize}");

            Console.WriteLine("\n=== Sifariş Statistikası ===");
            Console.WriteLine($"Ümumi sifariş: 3");
            Console.WriteLine($"1-ci sifariş qiyməti: {order1.Price} AZN");
            Console.WriteLine($"2-ci sifariş qiyməti: {order2.Price} AZN");
            Console.WriteLine($"3-cü sifariş qiyməti: {order3.Price} AZN");
            Console.WriteLine($"Ümumi məbləğ: {order1.Price + order2.Price + order3.Price} AZN");
        }
    }
}

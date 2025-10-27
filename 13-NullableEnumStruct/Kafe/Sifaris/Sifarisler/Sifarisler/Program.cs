using System;

namespace CafeOrderSystem
{
    // İçki növü enum
    enum DrinkType
    {
        Coffee = 0,
        Tea = 1,
        Juice = 2,
        Water = 3
    }

    // Ölçü enum
    enum DrinkSize
    {
        Small = 0,
        Medium = 1,
        Large = 2
    }

    // Sifariş statusu enum
    enum OrderStatus
    {
        New = 0,
        Preparing = 1,
        Ready = 2,
        Delivered = 3
    }

    // DrinkOrder sinfi
    class DrinkOrder
    {
        public int OrderNumber { get; set; }
        public string CustomerName { get; set; }
        public DrinkType Drink { get; set; }
        public DrinkSize Size { get; set; }
        public OrderStatus Status { get; set; }
        public decimal Price { get; set; }

        // Constructor
        public DrinkOrder(int orderNumber, string customerName, DrinkType drink, DrinkSize size)
        {
            OrderNumber = orderNumber;
            CustomerName = customerName;
            Drink = drink;
            Size = size;
            Status = OrderStatus.New;
            Price = CalculatePrice();
        }

        // Qiyməti hesablayan metod
        public decimal CalculatePrice()
        {
            decimal price = 0;
            switch (Drink)
            {
                case DrinkType.Coffee:
                    switch (Size)
                    {
                        case DrinkSize.Small: price = 3m; break;
                        case DrinkSize.Medium: price = 4m; break;
                        case DrinkSize.Large: price = 5m; break;
                    }
                    break;
                case DrinkType.Tea:
                    switch (Size)
                    {
                        case DrinkSize.Small: price = 2m; break;
                        case DrinkSize.Medium: price = 3m; break;
                        case DrinkSize.Large: price = 4m; break;
                    }
                    break;
                case DrinkType.Juice:
                    switch (Size)
                    {
                        case DrinkSize.Small: price = 4m; break;
                        case DrinkSize.Medium: price = 5m; break;
                        case DrinkSize.Large: price = 6m; break;
                    }
                    break;
                case DrinkType.Water:
                    switch (Size)
                    {
                        case DrinkSize.Small: price = 1m; break;
                        case DrinkSize.Medium: price = 1.5m; break;
                        case DrinkSize.Large: price = 2m; break;
                    }
                    break;
            }
            return price;
        }

        // Status yeniləyən metod
        public void UpdateStatus(OrderStatus newStatus)
        {
            Status = newStatus;
            Console.WriteLine($"Sifariş #{OrderNumber} statusu: {newStatus}");
        }

        // Sifarişi göstərən metod
        public void DisplayOrder()
        {
            Console.WriteLine($"\n--- Sifariş Melumatı ---");
            Console.WriteLine($"Sifariş №: {OrderNumber}");
            Console.WriteLine($"Müşteri: {CustomerName}");
            Console.WriteLine($"İçki: {Drink}");
            Console.WriteLine($"Olçu: {Size}");
            Console.WriteLine($"Status: {Status}");
            Console.WriteLine($"Qiymet: {Price} AZN");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // 1️⃣ Sifarişlər
            var order1 = new DrinkOrder(101, "Ali", DrinkType.Coffee, DrinkSize.Medium);
            order1.DisplayOrder();
            order1.UpdateStatus(OrderStatus.Preparing);
            order1.UpdateStatus(OrderStatus.Ready);
            order1.UpdateStatus(OrderStatus.Delivered);

            var order2 = new DrinkOrder(102, "Leyla", DrinkType.Tea, DrinkSize.Large);
            order2.DisplayOrder();
            order2.UpdateStatus(OrderStatus.Ready);

            var order3 = new DrinkOrder(103, "Vüqar", DrinkType.Juice, DrinkSize.Small);
            order3.DisplayOrder();

            // 2️⃣ Enum metodları
            Console.WriteLine("\n--- DrinkType Deyerleri ---");
            foreach (var val in Enum.GetValues(typeof(DrinkType)))
                Console.WriteLine(val);

            Console.WriteLine("\n--- DrinkSize Deyerleri ---");
            foreach (var val in Enum.GetValues(typeof(DrinkSize)))
                Console.WriteLine(val);

            Console.WriteLine("\n--- OrderStatus Deyerleri ---");
            foreach (var val in Enum.GetValues(typeof(OrderStatus)))
                Console.WriteLine(val);

            Console.WriteLine("\n--- ToString() və Parse() Numuneleri ---");
            Console.WriteLine($"DrinkType.Coffee.ToString() → {DrinkType.Coffee.ToString()}");
            Console.WriteLine($"DrinkSize.Large.ToString() → {DrinkSize.Large.ToString()}");

            DrinkType parsedDrink = (DrinkType)Enum.Parse(typeof(DrinkType), "Tea");
            DrinkSize parsedSize = (DrinkSize)Enum.Parse(typeof(DrinkSize), "Medium");
            Console.WriteLine($"Parsed Drink: {parsedDrink}");
            Console.WriteLine($"Parsed Size: {parsedSize}");

            // 3️⃣ Sadə statistika
            Console.WriteLine("\n--- Statistik Melumat ---");
            Console.WriteLine($"Umumi sifariş sayı: 3");
            Console.WriteLine($"1-ci sifarişin qiymeti: {order1.Price} AZN");
            Console.WriteLine($"2-ci sifarişin qiymeti: {order2.Price} AZN");
            Console.WriteLine($"3-cü sifarişin qiymeti: {order3.Price} AZN");
            decimal total = order1.Price + order2.Price + order3.Price;
            Console.WriteLine($"Umumi mebleğ: {total} AZN");
        }
    }
}

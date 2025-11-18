<<<<<<< HEAD
﻿using System;
using CafeOrderSystem.Enums;

namespace CafeOrderSystem.Models
{
    public class DrinkOrder
    {
        public int OrderNumber { get; private set; }
        public string CustomerName { get; private set; }
        public DrinkType Drink { get; private set; }
        public DrinkSize Size { get; private set; }
        public OrderStatus Status { get; private set; }
        public decimal Price { get; private set; }

        public DrinkOrder(int orderNumber, string customerName, DrinkType drink, DrinkSize size)
        {
            OrderNumber = orderNumber;
            CustomerName = customerName;
            Drink = drink;
            Size = size;
            Status = OrderStatus.New;
            Price = CalculatePrice();
        }

        private decimal CalculatePrice()
        {
            decimal price = 0;

            switch (Drink)
            {
                case DrinkType.Coffee:
                    price = Size switch
                    {
                        DrinkSize.Small => 3m,
                        DrinkSize.Medium => 4m,
                        DrinkSize.Large => 5m,
                        _ => 0
                    };
                    break;

                case DrinkType.Tea:
                    price = Size switch
                    {
                        DrinkSize.Small => 2m,
                        DrinkSize.Medium => 3m,
                        DrinkSize.Large => 4m,
                        _ => 0
                    };
                    break;

                case DrinkType.Juice:
                    price = Size switch
                    {
                        DrinkSize.Small => 4m,
                        DrinkSize.Medium => 5m,
                        DrinkSize.Large => 6m,
                        _ => 0
                    };
                    break;

                case DrinkType.Water:
                    price = Size switch
                    {
                        DrinkSize.Small => 1m,
                        DrinkSize.Medium => 1.5m,
                        DrinkSize.Large => 2m,
                        _ => 0
                    };
                    break;
            }

            return price;
        }

        public void UpdateStatus(OrderStatus newStatus)
        {
            Status = newStatus;
            Console.WriteLine($"Sifariş #{OrderNumber} statusu: {newStatus}");
        }

        public void DisplayOrder()
        {
            Console.WriteLine("-----------------------------");
            Console.WriteLine($"Sifariş №: {OrderNumber}");
            Console.WriteLine($"Müştəri: {CustomerName}");
            Console.WriteLine($"İçki: {Drink}");
            Console.WriteLine($"Ölçü: {Size}");
            Console.WriteLine($"Qiymət: {Price} AZN");
            Console.WriteLine($"Status: {Status}");
            Console.WriteLine("-----------------------------\n");
        }
    }
}
=======
﻿using System;
using CafeOrderSystem.Enums;

namespace CafeOrderSystem.Models
{
    public class DrinkOrder
    {
        public int OrderNumber { get; private set; }
        public string CustomerName { get; private set; }
        public DrinkType Drink { get; private set; }
        public DrinkSize Size { get; private set; }
        public OrderStatus Status { get; private set; }
        public decimal Price { get; private set; }

        public DrinkOrder(int orderNumber, string customerName, DrinkType drink, DrinkSize size)
        {
            OrderNumber = orderNumber;
            CustomerName = customerName;
            Drink = drink;
            Size = size;
            Status = OrderStatus.New;
            Price = CalculatePrice();
        }

        private decimal CalculatePrice()
        {
            decimal price = 0;

            switch (Drink)
            {
                case DrinkType.Coffee:
                    price = Size switch
                    {
                        DrinkSize.Small => 3m,
                        DrinkSize.Medium => 4m,
                        DrinkSize.Large => 5m,
                        _ => 0
                    };
                    break;

                case DrinkType.Tea:
                    price = Size switch
                    {
                        DrinkSize.Small => 2m,
                        DrinkSize.Medium => 3m,
                        DrinkSize.Large => 4m,
                        _ => 0
                    };
                    break;

                case DrinkType.Juice:
                    price = Size switch
                    {
                        DrinkSize.Small => 4m,
                        DrinkSize.Medium => 5m,
                        DrinkSize.Large => 6m,
                        _ => 0
                    };
                    break;

                case DrinkType.Water:
                    price = Size switch
                    {
                        DrinkSize.Small => 1m,
                        DrinkSize.Medium => 1.5m,
                        DrinkSize.Large => 2m,
                        _ => 0
                    };
                    break;
            }

            return price;
        }

        public void UpdateStatus(OrderStatus newStatus)
        {
            Status = newStatus;
            Console.WriteLine($"Sifariş #{OrderNumber} statusu: {newStatus}");
        }

        public void DisplayOrder()
        {
            Console.WriteLine("-----------------------------");
            Console.WriteLine($"Sifariş №: {OrderNumber}");
            Console.WriteLine($"Müştəri: {CustomerName}");
            Console.WriteLine($"İçki: {Drink}");
            Console.WriteLine($"Ölçü: {Size}");
            Console.WriteLine($"Qiymət: {Price} AZN");
            Console.WriteLine($"Status: {Status}");
            Console.WriteLine("-----------------------------\n");
        }
    }
}
>>>>>>> b1db9bcdde6daf2d3436510a1a8a844309dfc47b

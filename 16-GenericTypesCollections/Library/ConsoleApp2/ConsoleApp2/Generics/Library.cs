using System;
using System.Collections.Generic;

namespace LibrarySystem.Generics
{
    public class Library<T>
    {
        public List<T> Items { get; set; }
        public string Name { get; set; }

        public Library(string name)
        {
            Name = name;
            Items = new List<T>();
        }

        public void Add(T item)
        {
            Items.Add(item);
            Console.WriteLine("Əlavə edildi");
        }

        public void Remove(T item)
        {
            Items.Remove(item);
            Console.WriteLine("Silindi");
        }

        public List<T> GetAll() => Items;

        public int Count() => Items.Count;

        public T FindByIndex(int index)
        {
            if (index >= 0 && index < Items.Count)
                return Items[index];

            Console.WriteLine("İndeks yanlışdır!");
            return default!;
        }
    }
}

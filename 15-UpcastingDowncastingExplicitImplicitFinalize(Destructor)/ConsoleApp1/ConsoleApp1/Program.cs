using ConsoleApp1.Models;

namespace ConsoleApp1
{
    internal class Proqram
    {
        static void Main(string args)
        {
            //Dog dog = new Dog() { AvgLifeTime = 20, Breed = "kangal", Gender = "male", Name = "Hatiko" };
            //Eagle eagle = new Eagle() { AvgLifeTime = 300, FlySpeed = 120, Gender = "female" };


            //int a = 5;
            //object b = a; // boxing


            //int c = (int)b; // unboxing

            //Test test= new Test();

            //object d = test; // boxing
            //ITestable testable= test; // boxing










            //// Implicit - Upcasting
            //Animal animal = dog;
            //Animal animal1 = eagle;





            //Dog dog1 = (Dog)animal; // Explicit - Downcasting


            //Eagle eagle1 = (Eagle)animal1; // Explicit - Downcasting

            //Animal[] animals = { eagle,dog};
            //foreach (Animal animal in animals)
            {
                //Eagle eagle1 = (Eagle)animal;
                //eagle.Fly();
                //Eagle eagle2 = animal as Eagle;
                //if (eagle2 != null)
                //{
                //    eagle2.Fly();

                //}
                //if (animal is Eagle eagle3)
                //{
                //    Eagle eagle1 = (Eagle)animal;
                //    Console.WriteLine(eagle1.Fly); 
                
            }
        }
    }

    public struct Test: ITestable 
    {
        public int x { get; set; }
        public int y { get; set ; }
    }
    public interface ITestable
    {
        public int y { get; set; }
    }
}
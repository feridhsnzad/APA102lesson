using Colculate;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== kalkulator ===");
        Console.WriteLine("Emeliyyatlar: +  -  *  /");

        Console.Write("Birinci ededi daxil edin: ");
        double a = Convert.ToDouble(Console.ReadLine());

        Console.Write("Emeliyyatı daxil edin (+, -, *, /): ");
        string op = Console.ReadLine();

        Console.Write("İkinci ededi daxil edin: ");
        double b = Convert.ToDouble(Console.ReadLine());

        // 🔹 Calculation class-ını çağırırıq (ICalculation interfeysi ilə)
        ICalculation calculator = new Calculation();

        double result = calculator.Calculate(a, b, op);

        if (!double.IsNaN(result))
            Console.WriteLine($"\nNetice: {a} {op} {b} = {result}");

        Console.WriteLine("\nProqram bitdi.");
    }
}

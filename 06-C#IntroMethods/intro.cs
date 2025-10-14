using System.Runtime.Serialization.Formatters;

namespace ConsoleAppFRD;

public class Methodss
{
    #region  Main

    public static void Main()
    {
        Console.WriteLine("Tek ve cut ededleri tap -1 , Riyazi hesablamalar et -2 , 5-e ve 4-e bolunen ededlerin cemi -3 , Cumleden character sayma -4");
        int math = int.Parse(Console.ReadLine());
        switch (math)
        {
            case 1:
                TekOrCut();
                int tek_cut = TekOrCut2();
                Console.WriteLine(tek_cut);
                break;
            case 2:
                Operations();
                break;
            case 3:
                BeseBolunme();
                int result6 = BeseBolunme2(0);
                Console.WriteLine(result6);
                break;
            case 4:
                FindSymbol();
                int result7 = FindSymbol2();
                Console.WriteLine(result7);
                break;
                
        }

        ;
        {
            
        }
        
    }

    #endregion
    #region operations
    
    public static void Operations()
    {
        Console.WriteLine("---Bir operasiya secin--- \nToplama - 1 , Cixma - 2 , Vurma - 3, Bolme - 4");
        int operations = int.Parse(Console.ReadLine());

        switch (operations)
        {
            case 1:
                Sum();
                double result = Sum2();
                Console.WriteLine("Cem :"+" "+ result);
                break;
            case 2:
                Minus();
                double result3 = Minus2();
                Console.WriteLine("Cixma"+" "+ result3);
                break;
            case 3:
                Multiply();
                double result2= Multiply2();
                Console.WriteLine("Vurma"+" "+result2);
                break;
            case 4:
                Divide();
                double result4 = Divide2();
                Console.WriteLine("Cixma"+" "+result4);
                break;
        }
        

    }
    #endregion

    #region toplama
    public static void Sum()
    { 
        
            Console.Write("Birinci ededi daxil edin :");
            double a = int.Parse(Console.ReadLine());
            Console.Write("Ikinci ededi daxil edin :");
            double b = int.Parse(Console.ReadLine());
            Console.Write("Cem:");
            double sum = a + b;
            Console.WriteLine(sum);
        

    }
    public static double Sum2()
    { 
        
        Console.Write("Birinci ededi daxil edin :");
        double a = int.Parse(Console.ReadLine());
        Console.Write("Ikinci ededi daxil edin :");
        double b = int.Parse(Console.ReadLine());
        double sum = a + b;
        return sum;
        

    }
    #endregion

    #region Vurma
    public static void Multiply()
    {
        Console.Write("Birinci ededi daxil edin :");
        double a = int.Parse(Console.ReadLine());
        Console.Write("Ikinci ededi daxil edin :");
        double b = int.Parse(Console.ReadLine());
        double mlt = a * b;
        Console.WriteLine("Vurma"+" "+mlt);
        
    }
    public static double Multiply2()
    {
        Console.Write("Birinci ededi daxil edin :");
        double a = int.Parse(Console.ReadLine());
        Console.Write("Ikinci ededi daxil edin :");
        double b = int.Parse(Console.ReadLine());
        double mlt = a * b;
        return mlt;
        
    }
    #endregion
    #region Bolme
    public static void Divide()
    {
        Console.Write("Birinci ededi daxil edin :");
        double a = int.Parse(Console.ReadLine());
        Console.Write("Ikinci ededi daxil edin :");
        double b = int.Parse(Console.ReadLine());
        double div= a / b;
        Console.WriteLine("Bolme"+" "+div);
    }
    
    public static double Divide2()
    {
        Console.Write("Birinci ededi daxil edin :");
        double a = int.Parse(Console.ReadLine());
        Console.Write("Ikinci ededi daxil edin :");
        double b = int.Parse(Console.ReadLine());
        double div= a / b;
        return div;
    }
    #endregion

    #region Cixma

    public static void Minus()
    {
        
        Console.Write("Birinci ededi daxil edin :");
        double a = int.Parse(Console.ReadLine());
        Console.Write("Ikinci ededi daxil edin :");
        double b = int.Parse(Console.ReadLine());
        double min= a - b;
        Console.WriteLine("Bolme"+" "+min);
        
    }
    public static double Minus2()
    {
        
            Console.Write("Birinci ededi daxil edin :");
            double a = int.Parse(Console.ReadLine());
            Console.Write("Ikinci ededi daxil edin :");
            double b = int.Parse(Console.ReadLine());
            double min= a - b;
            return min;
        
    }
    #endregion

    #region Tek_or_Cut

    public static void TekOrCut()
    {
        int i = 0;
            
        int[] array = [14,20,35,40,57,60,100];
        while (i<array.Length)
        {
            i++;
            if (array[i] % 2 == 0)
            {
                int cut = array[i];
                Console.WriteLine("Cut eded:"+" "+cut);
            }
            else
            {
                int tek = array[i];
                Console.WriteLine("Tek eded :"+" "+tek);
            }
            
            
        }
    }

    #endregion

    #region TekOrCut2

    public static int TekOrCut2()
    {
        int i = 0;
        int tek = 0;   
        int cut = 0;   
    
        int[] array = [14, 20, 35, 40, 57, 60, 100];  

        while (i < array.Length)
        {
            if (array[i] % 2 == 0)
            {
                cut = array[i];
                Console.WriteLine("Cut eded:" + " " + cut);
            }
            else
            {
                tek = array[i];
                Console.WriteLine("Tek eded :" + " " + tek);
            }
            i++;
        }

        return tek;   //en sonuncu tek ededi gonderir
    }
    #endregion

    #region Bese_bolunme_elameti

    public static void BeseBolunme()
    {
        int [] arr=[14,20,35,40,57,60,100];
        int sumArr5 = 0;
        int sumArr4 = 0;
        for (int i = 0; i < arr.Length; i++)
        {
            if (arr[i] % 5 == 0)
            {
                sumArr5 += arr[i];
            }
            if (arr[i] % 4 == 0)
            {
                sumArr4 += arr[i];
            }

        }
        Console.WriteLine("4 e bolunen cemi :"+" "+sumArr4);
        Console.WriteLine("5 e bolunen cemi :"+" "+sumArr5);
    }
    public static int BeseBolunme2(int c)
    {
        int [] arr=[14,20,35,40,57,60,100];
        int sumarr5 = 0;
        int sumarr4 = 0;
        for (int i = 0; i < arr.Length; i++)
        {
            if (arr[i] % 5 == 0)
            {
                sumarr5 += arr[i];
            }
            if (arr[i] % 4 == 0)
            {
                sumarr4 += arr[i];
            }
            
        }

        Console.WriteLine("4 e bolunen cemi :"+" "+sumarr4);
        Console.WriteLine("5 e bolunen cemi :"+" "+sumarr5);
        c=sumarr5+sumarr4; // 5 e ve 4 e bolunen ededlerin cemini gonderir
        return c;
    }

    #endregion

    #region FindSymbol

    public static void FindSymbol()
    {
        Console.WriteLine("Herfi daxil edin :");
        char symbol = Console.ReadLine();
        Console.WriteLine("Cumleni daxil edin :");
        string cumle =  Console.ReadLine();
        int times = 0;
        for (int i = 0; i < cumle.Length; i++)
        {
            if (cumle[i] == symbol)
            {
                times++;
            }
        }

        Console.WriteLine(times);
    }
    #endregion

    #region FindSymbol2
    
    public static int FindSymbol2()
    {
        Console.WriteLine("Herfi daxil edin :");
        char symbol = Console.ReadLine()[0];
        Console.WriteLine("Cumleni daxil edin :");
        string cumle =  Console.ReadLine();
        int times = 0;
        for (int i = 0; i < cumle.Length; i++)
        {
            if (cumle[i] == symbol)
            {
                times++;
            }
        }

        return times;
    }
#endregion


    
}
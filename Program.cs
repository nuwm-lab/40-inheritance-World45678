using System;

// 1
public class Fraction
{
    
    protected double a;

    public Fraction()
    {
        a = 1.0; 
    }

    // 2
    public virtual void SetCoefficients()
    {
        Console.WriteLine("--- Налаштування простого дробу ---");
        Console.Write("Введіть коефіцієнт 'a' для дробу виду 1/(a*x): ");
        while (!double.TryParse(Console.ReadLine(), out a))
        {
            Console.WriteLine("Помилка. Будь ласка, введіть коректне число.");
            Console.Write("Введіть коефіцієнт 'a': ");
        }
    }

    // 3
    public virtual void DisplayCoefficients()
    {
        Console.WriteLine("\n--- Інформація про простий дріб ---");
        Console.WriteLine("Тип: 'дріб'");
        Console.WriteLine($"Формула: 1 / ({a} * x)");
        Console.WriteLine($"Коефіцієнт a = {a}");
    }

    // 4
    public virtual double Calculate(double x)
    {
        double denominator = a * x;
        if (denominator == 0)
        {
            // 5
            throw new DivideByZeroException("Знаменник (a*x) не може дорівнювати нулю.");
        }
        return 1.0 / denominator;
    }
}

// 6
public class ComplexFraction : Fraction
{
    private double a1, a2, a3;

    // 7
    private double ReadCoefficient(string name)
    {
        double value;
        while (true)
        {
            Console.Write($"Введіть коефіцієнт '{name}' (не може дорівнювати 3): ");
            if (double.TryParse(Console.ReadLine(), out value))
            {
                if (value == 3)
                {
                    Console.WriteLine("Помилка: коефіцієнт не може дорівнювати 3. Спробуйте ще раз.");
                }
                else
                {
                    return value;
                }
            }
            else
            {
                Console.WriteLine("Помилка. Будь ласка, введіть коректне число.");
            }
        }
    }

   
    public override void SetCoefficients()
    {
        Console.WriteLine("\n--- Налаштування тригонометричного підхідного дробу ---");
        a1 = ReadCoefficient("a1");
        a2 = ReadCoefficient("a2");
        a3 = ReadCoefficient("a3");
    }

    
    public override void DisplayCoefficients()
    {
        Console.WriteLine("\n--- Інформація про тригонометричний підхідний дріб ---");
        Console.WriteLine("Тип: 'тригонометричний підхідний дріб'");
        Console.WriteLine("Формула: 1 / (a1*x + 1 / (a2*x + 1 / a3*x))");
        Console.WriteLine($"Коефіцієнти: a1 = {a1}, a2 = {a2}, a3 = {a3}");
    }

    
    public override double Calculate(double x)
    {
        // 8
        double innerDenominator = a3 * x;
        if (innerDenominator == 0)
        {
            throw new DivideByZeroException("Внутрішній знаменник (a3*x) дорівнює нулю.");
        }

        double middleDenominator = a2 * x + (1.0 / innerDenominator);
        if (middleDenominator == 0)
        {
            throw new DivideByZeroException("Середній знаменник дорівнює нулю.");
        }

        double outerDenominator = a1 * x + (1.0 / middleDenominator);
        if (outerDenominator == 0)
        {
            throw new DivideByZeroException("Зовнішній знаменник дорівнює нулю.");
        }

        return 1.0 / outerDenominator;
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        // 9
        Fraction simpleFraction = new Fraction();
        ComplexFraction complexFraction = new ComplexFraction();

        // 10
        simpleFraction.SetCoefficients();
        simpleFraction.DisplayCoefficients();

        complexFraction.SetCoefficients();
        complexFraction.DisplayCoefficients();

        
        Console.WriteLine("\n-------------------------------------------");
        Console.Write("Введіть значення 'x' для обчислення дробів: ");
        double x;
        while (!double.TryParse(Console.ReadLine(), out x))
        {
            Console.WriteLine("Помилка. Будь ласка, введіть число для 'x'.");
            Console.Write("Введіть значення 'x': ");
        }

        
        try
        {
            double result1 = simpleFraction.Calculate(x);
            Console.WriteLine($"\n✅ Результат для простого дробу при x = {x}: {result1:F4}");
        }
        catch (DivideByZeroException ex)
        {
            Console.WriteLine($"\n❌ Помилка обчислення простого дробу: {ex.Message}");
        }

       
        try
        {
            double result2 = complexFraction.Calculate(x);
            Console.WriteLine($"✅ Результат для тригонометричного підхідного дробу при x = {x}: {result2:F4}");
        }
        catch (DivideByZeroException ex)
        {
            Console.WriteLine($"\n❌ Помилка обчислення тригонометричного підхідного дробу: {ex.Message}");
        }
    }
}

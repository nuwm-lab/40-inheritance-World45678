using System;

// ✅ Клас простого дробу
public class Fraction
{
    // FIX: зроблено приватним полем із властивістю замість protected
    private double _a;

    // NEW: властивість із простою валідацією
    public double A
    {
        get => _a;
        set
        {
            if (Math.Abs(value) < 1e-12)
                Console.WriteLine("⚠️ Попередження: коефіцієнт 'a' дуже малий, можливі помилки при діленні.");
            _a = value;
        }
    }

    public Fraction()
    {
        _a = 1.0;
    }

    // Ввід коефіцієнта
    public virtual void SetCoefficients()
    {
        Console.WriteLine("--- Налаштування простого дробу ---");
        Console.Write("Введіть коефіцієнт 'a' для дробу виду 1/(a*x): ");

        double value;
        while (!double.TryParse(Console.ReadLine(), out value))
        {
            Console.WriteLine("Помилка. Будь ласка, введіть коректне число.");
            Console.Write("Введіть коефіцієнт 'a': ");
        }
        A = value;
    }

    // Вивід коефіцієнтів
    public virtual void DisplayCoefficients()
    {
        Console.WriteLine("\n--- Інформація про простий дріб ---");
        Console.WriteLine("Тип: 'дріб'");
        Console.WriteLine($"Формула: 1 / ({_a} * x)");
        Console.WriteLine($"Коефіцієнт a = {_a}");
    }

    // Обчислення 1/(a*x)
    public virtual double Calculate(double x)
    {
        double denominator = _a * x;

        // FIX: порівняння через епсілон
        const double eps = 1e-12;
        if (Math.Abs(denominator) < eps)
        {
            throw new DivideByZeroException("Знаменник (a*x) занадто малий або дорівнює нулю.");
        }
        return 1.0 / denominator;
    }

    // NEW: додатковий метод для зручного задання параметра без Console
    public void SetCoefficients(double a) => A = a;
}


// ✅ Клас складного (тригонометричного) дробу
public class ComplexFraction : Fraction
{
    // FIX: приватні поля замість захищених
    private double _a1, _a2, _a3;

    // NEW: властивості для зручності та перевірки
    public double A1
    {
        get => _a1;
        set
        {
            if (Math.Abs(value - 3.0) < 1e-12)
                throw new ArgumentException("Коефіцієнт a1 не може дорівнювати 3.");
            _a1 = value;
        }
    }
    public double A2
    {
        get => _a2;
        set
        {
            if (Math.Abs(value - 3.0) < 1e-12)
                throw new ArgumentException("Коефіцієнт a2 не може дорівнювати 3.");
            _a2 = value;
        }
    }
    public double A3
    {
        get => _a3;
        set
        {
            if (Math.Abs(value - 3.0) < 1e-12)
                throw new ArgumentException("Коефіцієнт a3 не може дорівнювати 3.");
            _a3 = value;
        }
    }

    private double ReadCoefficient(string name)
    {
        double value;
        while (true)
        {
            Console.Write($"Введіть коефіцієнт '{name}' (не може дорівнювати 3): ");
            if (double.TryParse(Console.ReadLine(), out value))
            {
                if (Math.Abs(value - 3.0) < 1e-12)
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

    // Ввід коефіцієнтів
    public override void SetCoefficients()
    {
        Console.WriteLine("\n--- Налаштування тригонометричного підхідного дробу ---");
        A1 = ReadCoefficient("a1");
        A2 = ReadCoefficient("a2");
        A3 = ReadCoefficient("a3");
    }

    // Вивід інформації
    public override void DisplayCoefficients()
    {
        Console.WriteLine("\n--- Інформація про тригонометричний підхідний дріб ---");
        Console.WriteLine("Тип: 'тригонометричний підхідний дріб'");
        // FIX: виправлено формулу з дужками
        Console.WriteLine("Формула: 1 / (a1*x + 1 / (a2*x + 1 / (a3*x)))");
        Console.WriteLine($"Коефіцієнти: a1 = {A1}, a2 = {A2}, a3 = {A3}");
    }

    // Обчислення складного дробу
    public override double Calculate(double x)
    {
        const double eps = 1e-12;

        double innerDenominator = A3 * x;
        if (Math.Abs(innerDenominator) < eps)
            throw new DivideByZeroException("Внутрішній знаменник (a3*x) занадто малий або дорівнює нулю.");

        double middleDenominator = A2 * x + (1.0 / innerDenominator);
        if (Math.Abs(middleDenominator) < eps)
            throw new DivideByZeroException("Середній знаменник занадто малий або дорівнює нулю.");

        double outerDenominator = A1 * x + (1.0 / middleDenominator);
        if (Math.Abs(outerDenominator) < eps)
            throw new DivideByZeroException("Зовнішній знаменник занадто малий або дорівнює нулю.");

        return 1.0 / outerDenominator;
    }

    // NEW: перегрузка для зручного тестування без вводу
    public void SetCoefficients(double a1, double a2, double a3)
    {
        A1 = a1;
        A2 = a2;
        A3 = a3;
    }
}


// ✅ Головна програма
public class Program
{
    public static void Main(string[] args)
    {
        Fraction simpleFraction = new Fraction();
        ComplexFraction complexFraction = new ComplexFraction();

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

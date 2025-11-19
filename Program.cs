using System;

namespace VirtualMethodsLab
{
    // Базовий клас: Система двох векторів на площині (2D)
    class TwoVectorSystem
    {
        // Координати вектора A (x, y)
        protected double ax, ay;
        // Координати вектора B (x, y)
        protected double bx, by;

        // Віртуальний метод для введення даних
        public virtual void Input()
        {
            Console.WriteLine("\n--- Введення системи 2-х векторiв (2D) ---");
            Console.WriteLine("Введiть координати вектора A:");
            Console.Write("ax: "); ax = Convert.ToDouble(Console.ReadLine());
            Console.Write("ay: "); ay = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine("Введiть координати вектора B:");
            Console.Write("bx: "); bx = Convert.ToDouble(Console.ReadLine());
            Console.Write("by: "); by = Convert.ToDouble(Console.ReadLine());
        }

        // Віртуальний метод для виведення даних
        public virtual void Show()
        {
            Console.WriteLine("\n--- Система 2-х векторiв ---");
            Console.WriteLine($"Вектор A = ({ax}, {ay})");
            Console.WriteLine($"Вектор B = ({bx}, {by})");
        }

        // Віртуальний метод перевірки лінійної незалежності
        public virtual bool IsLinearlyIndependent()
        {
            // Для двох векторів на площині: визначник | ax ay |
            //                                       | bx by |
            // Det = ax*by - ay*bx
            double det = ax * by - ay * bx;
            
            Console.Write($"Визначник системи 2x2 = {det:F2}. ");
            // Якщо визначник не дорівнює 0, вектори лінійно незалежні
            return Math.Abs(det) > 1e-9; 
        }
    }

    // Похідний клас: Система трьох векторів у просторі (3D)
    class ThreeVectorSystem : TwoVectorSystem
    {
        // Додаткові Z-координати для A і B
        private double az, bz;
        // Координати нового вектора C
        private double cx, cy, cz;

        // Перевизначення (override) методу введення
        public override void Input()
        {
            Console.WriteLine("\n--- Введення системи 3-х векторiв (3D) ---");
            Console.WriteLine("Введiть координати вектора A:");
            Console.Write("ax: "); ax = Convert.ToDouble(Console.ReadLine());
            Console.Write("ay: "); ay = Convert.ToDouble(Console.ReadLine());
            Console.Write("az: "); az = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine("Введiть координати вектора B:");
            Console.Write("bx: "); bx = Convert.ToDouble(Console.ReadLine());
            Console.Write("by: "); by = Convert.ToDouble(Console.ReadLine());
            Console.Write("bz: "); bz = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine("Введiть координати вектора C:");
            Console.Write("cx: "); cx = Convert.ToDouble(Console.ReadLine());
            Console.Write("cy: "); cy = Convert.ToDouble(Console.ReadLine());
            Console.Write("cz: "); cz = Convert.ToDouble(Console.ReadLine());
        }

        // Перевизначення методу виведення
        public override void Show()
        {
            Console.WriteLine("\n--- Система 3-х векторiв ---");
            Console.WriteLine($"Вектор A = ({ax}, {ay}, {az})");
            Console.WriteLine($"Вектор B = ({bx}, {by}, {bz})");
            Console.WriteLine($"Вектор C = ({cx}, {cy}, {cz})");
        }

        // Перевизначення методу перевірки лінійної незалежності
        public override bool IsLinearlyIndependent()
        {
            // Визначник матриці 3x3 за правилом трикутника (Саррюса)
            // | ax ay az |
            // | bx by bz |
            // | cx cy cz |
            double det = ax * by * cz + ay * bz * cx + az * bx * cy 
                       - az * by * cx - ay * bx * cz - ax * bz * cy;

            Console.Write($"Визначник системи 3x3 = {det:F2}. ");
            return Math.Abs(det) > 1e-9;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Вказівник (посилання) на базовий клас
            TwoVectorSystem systemProcessor;

            while (true)
            {
                Console.WriteLine("\n============================================");
                Console.WriteLine("Оберiть режим роботи:");
                Console.WriteLine("1 - Робота з системою 2-х векторiв (2D)");
                Console.WriteLine("2 - Робота з системою 3-х векторiв (3D)");
                Console.WriteLine("0 - Вихiд");
                Console.Write("Ваш вибiр: ");
                
                string choice = Console.ReadLine();

                if (choice == "0") break;

                // Поліморфізм: створюємо об'єкт залежно від вибору,
                // але зберігаємо посилання у змінній базового типу.
                if (choice == "1")
                {
                    systemProcessor = new TwoVectorSystem();
                }
                else if (choice == "2")
                {
                    systemProcessor = new ThreeVectorSystem();
                }
                else
                {
                    Console.WriteLine("Некоректний ввiд, спробуйте ще раз.");
                    continue;
                }

                // Виклик віртуальних методів
                // Програма сама вирішить, яку версію методу викликати (Base чи Derived)
                // на етапі виконання (Runtime).
                systemProcessor.Input();
                systemProcessor.Show();

                bool isIndependent = systemProcessor.IsLinearlyIndependent();
                
                if (isIndependent)
                {
                    Console.WriteLine("Результат: Система векторiв ЛIНIЙНО НЕЗАЛЕЖНА.");
                }
                else
                {
                    Console.WriteLine("Результат: Система векторiв ЛIНIЙНО ЗАЛЕЖНА.");
                }
            }
        }
    }
}

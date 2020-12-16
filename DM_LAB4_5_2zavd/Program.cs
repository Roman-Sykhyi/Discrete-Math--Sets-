using System;
using System.Collections.Generic;
using System.Linq;

namespace DM_LAB4_5_2zavd
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;

            List<double> universum = new List<double>();
            Console.WriteLine("Введіть елементи універсума. (дробову частину виділяйте знаком \",\", елементи розділяйте пробілом)");
            bool flag = false;
            do
            {
                string universum_string = Console.ReadLine();
                string[] universum_stringArray = universum_string.Split(' ');
                universum = Array.ConvertAll(universum_stringArray, s => double.TryParse(s, out var x) ? x : -1).ToList();

                if (universum.Contains(-1))
                {
                    flag = true;
                    Console.WriteLine("Неправильно введено елементи множини! (дробову частину виділяйте знаком \",\", елементи розділяйте пробілом)");
                }
                else
                {
                    flag = false;
                }
            } while (flag);

            Console.WriteLine("\nВведіть елементи множини.");
            List<double> set = new List<double>();
            flag = false;
            do
            {
                string set_string = Console.ReadLine();
                string[] set_stringArray = set_string.Split(' ');
                set = Array.ConvertAll(set_stringArray, s => double.TryParse(s, out var x) ? x : -1).ToList();

                if (universum.Contains(-1) || !IsInUniversum(set, universum))
                {
                    flag = true;
                    Console.WriteLine("Неправильно введено елементи множини або один з елементів не входить в універсум! (дробову частину виділяйте знаком \",\", елементи розділяйте пробілом)");
                }
                else
                {
                    flag = false;
                }
            } while (flag);

            List<double> complement = GetComplement(set, universum);
            Console.WriteLine("\nЕлементи доповнення:");
            foreach(double x in complement)
            {
                Console.Write(x + " ");
            }

            Console.WriteLine("\n\nХарактеристичний вектор множини:");
            WriteCharacteristicVector(set, universum);
            Console.WriteLine("\nХарактеристичний вектор доповнення:");
            WriteCharacteristicVector(complement, universum);

            Console.WriteLine($"\n\nПотужність множини: {set.Count}");
            Console.WriteLine($"Потужність доповнення: {complement.Count}");
            Console.WriteLine($"Потужність універсума: {universum.Count}");

            Console.WriteLine("\nБулеан доповнення:");
            WritePowerSet(complement);

            Console.WriteLine($"Потужність булеана: {Math.Pow(2, complement.Count)}");
            

            Console.ReadKey();
        }

        private static void WritePowerSet(List<double> set)
        {
            string result = "P = { ";

            int n = (int)(Math.Pow(2, set.Count));

            for (int i = 0; i < n; i++)
            {
                result += "{";
                for (int j = 0; j < set.Count; j++)
                {
                    if ((i & (int)Math.Pow(2, j)) != 0)
                        result += set[j] + "; ";
                }
                result = result.Remove(result.Length - 2, 2);
                result += "}; ";
            }

            result = result.Remove(result.Length - 2, 2);
            result += "}";
            result = result.Insert(4, "{");

            Console.WriteLine(result);
        }

        private static bool IsInUniversum(List<double> set, List<double> universum)
        {
            foreach (double x in set)
            {
                if (!universum.Contains(x))
                    return false;
            }
            return true;
        }

        private static List<double> GetComplement(List<double> set, List<double> universum)
        {
            List<double> result = new List<double>();
            foreach (double x in universum)
            {
                if (!set.Contains(x))
                    result.Add(x);
            }
            return result;
        }

        public static void WriteCharacteristicVector(List<double> set, List<double> universum)
        {
            List<bool> result = new List<bool>();

            foreach (double x in universum)
            {
                if (set.Contains(x))
                {
                    result.Add(true);
                }
                else
                {
                    result.Add(false);
                }
            }

            foreach (bool x in result)
            {
                Console.Write(Convert.ToInt16(x));
            }
        }
    }
}

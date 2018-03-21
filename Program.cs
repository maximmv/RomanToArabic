/*
 Использовались следующие принципы римской системы счисления:
 I = 1; V = 5; X = 10; L = 50; C = 100; D = 500; M = 1000. 
Все целые числа от 1 до 3999 записываются с помощью приведенных выше цифр. При этом:
если большая цифра стоит перед меньшей, они складываются:
VI = 5 + 1 = 6; XV = 10 + 5 = 15; LX = 50 + 10 = 60; CL = 100 + 50 = 150; 
если меньшая цифра стоит перед большей (в этом случае она не может повторяться), 
то меньшая вычитается из большей; вычитаться могут только цифры, обозначающие 1 или степени 10;
уменьшаемым может быть только цифра, ближайшая в числовом ряду к вычитаемой(IX,IV,XL,XC,CD,CM)
IV = 5 - 1 = 4; IX = 10 - 1 = 9; XL = 50 - 10 = 40; XC = 100 - 10 = 90; 
цифры V, L, D не могут повторяться; цифры I, X, C, M могут повторяться не более трех раз подряд:
VIII = 8; LXXX = 80; DCCC = 800; MMMD = 3500.  
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LightIt
{
    class Program
    {
        static int RomanToArab(string r_num)
        {
            Dictionary<char, int> Roman = new Dictionary<char, int>()
            {
                {'I', 1},
                {'V', 5},
                {'X', 10},
                {'L', 50},
                {'C', 100},
                {'D', 500},
                {'M',1000}
            };
            if (r_num == "")
                throw new Exception("Roman number is empty!!!");
            //Проверка на наличие правильных символов в строке
            for (int i = 0; i < r_num.Length; i++)
            {
                bool flag = false;
                foreach (KeyValuePair<char, int> kvp in Roman)
                {
                    if (r_num[i] == kvp.Key)
                        flag = true;
                }
                if (!flag)
                    throw new Exception("Roman number includes invalid characters!!!");
            }

            //Регулярное выражение для определения корректной записи римского числа
            string pattern = @"^(M{0,3})((C[DM])|(D?C{0,3}))?((X[LC])|(L?X{0,3}))?((I[XV])|(V?I{0,3}))?$";

            if (!Regex.IsMatch(r_num, pattern))
            {
                throw new Exception("Incorrect arrangement of characters in the number");
            }
            int res = 0;
            for (int i = 0; i < r_num.Length - 1; i++)
            {

                if (Roman[r_num[i]] < Roman[r_num[i + 1]])
                    res -= Roman[r_num[i]];
                else
                    res += Roman[r_num[i]];
            }
            res += Roman[r_num[r_num.Length - 1]];
            return res;
        }
        static string arabToRoman(int num)
        {
            if (num < 1 || num > 3999)
                throw new Exception("Out of range 1-3999");
            Dictionary<int, string> arabic = new Dictionary<int, string>()
                {
                    { 1000 , "M" },
                    { 900  , "CM"},
                    { 500  , "D" },
                    { 400  , "CD"},
                    { 100  , "IX"},
                    { 90   , "XC"},
                    { 50   , "L" },
                    { 40   , "XL"},
                    { 10   , "X" },
                    { 9    , "IX"},
                    { 5    , "V" },
                    { 4    , "IV"},
                    { 1    , "I" }
                };
            string res = "";
            foreach (KeyValuePair<int, string> kvp in arabic)
            {
                while (num >= kvp.Key)
                {
                    num -= kvp.Key;
                    res += kvp.Value;
                }

            }
            return res;
        }
        static void Main(string[] args)
        {
            string menu = "Convert Roman to Arabic press \"1\" | Convert Arabic to Roman press \"2\"| Exit press \"3\"\n"+
                "--------------------------------------------------------------------------------------";
            while (true)
            {
                Console.Clear();
                Console.WriteLine(menu);         
                string choice = "";
                choice = Console.ReadLine();
                while ((choice != "1") && (choice != "2") && (choice != "3"))
                {
                    Console.Clear();
                    Console.WriteLine(menu);               
                    Console.WriteLine("Invalid input. Please enter a choice(1-3): ");
                    choice = Console.ReadLine();
                }               
                
                switch (choice)

                {
                    case "1":

                        {   while (true)
                            {
                                Console.Clear();
                                Console.WriteLine("Input ROMAN number in range I - MMMCMXCIX (1 - 3999) : ");
                                string number = Console.ReadLine();
                                try
                                {
                                    Console.WriteLine("Исходное число: {0}, результат:{1}", number, RomanToArab(number));
                                    Console.WriteLine("Press any key to continue");
                                    Console.ReadKey();
                                    break;
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine("Exception: {0}!!!", e.Message);
                                    Console.WriteLine("Press any key to continue");
                                    Console.ReadKey();
                                }
                            }
                            
                            break;
                        }

                    case "2":                        

                        {
                            while (true)
                            {
                                Console.Clear();
                                Console.WriteLine("Input ARABIC number in range 1 - 3999 : ");
                                try
                                {
                                    int number = Convert.ToInt32(Console.ReadLine());
                                    Console.WriteLine("Исходное число: {0}, результат:{1}", number, arabToRoman(number));
                                    Console.WriteLine("Press any key to continue");
                                    Console.ReadKey();
                                    break;
                                }

                                catch (FormatException e)
                                {
                                    Console.WriteLine("Exception: {0}!!!", e.Message);
                                    Console.WriteLine("Press any key to continue");
                                    Console.ReadKey();
                                }                                
                                catch (Exception e)
                                {
                                    Console.WriteLine("Exception: {0}!!!", e.Message);
                                    Console.WriteLine("Press any key to continue");
                                    Console.ReadKey();
                                }
                            }
                            break;
                        }



                    case "3":

                        {
                            return;                           
                        }
                    default:
                        {
                            break;
                        }
                } 
            }
        }
    }
}

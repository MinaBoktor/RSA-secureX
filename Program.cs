using System;
using System.Numerics;
using securex;
using System.Collections.Generic;



namespace securex
{
    class Program
    {
        static void Main(string[] args)
        {
            int c = 0;
            Console.WriteLine("1. Encrypt");
            Console.WriteLine("2. Decrypt");
            Console.WriteLine("3. String Decrypt");
            Console.WriteLine("4. Generate Key");
            Console.WriteLine("5. Run Test Cases");
            Console.Write("Your Choice: ");
            c = int.Parse((Console.ReadLine()).Trim());

            Bigint n, key, m;

            switch (c)
            {
                case 1:
                    (n, key, m) = get_data();
                    Console.WriteLine($"Encryption: {RSA.Encrypt(m, key, n)}");
                    break;
                case 2:
                    (n, key, m) = get_data();
                    Console.WriteLine($"Decryption: {RSA.Encrypt(m, key, n)}");
                    break;

                case 3:
                    (n, key, m) = get_data();
                    Console.WriteLine($"Decryption: {Ascii.convert(RSA.Decrypt(m, key, n))}");
                    break;
                case 4:
                    int t;
                    Console.Write("Number of digits: ");
                    t = int.Parse((Console.ReadLine()).Trim());
                    RSA.Generate_Public(t);
                    break;
                case 5:
                    // Testing Bigint Operations

                    string path = @"C:\Users\RexoL\source\repos\RSA-secureX";
                    int passed, total;
                    string[] filenames = ["AddTestCases", "MultiplyTestCases", "SubtractTestCases"];
                    char[] operations = ['+', '*', '-'];

                    for (int i = 0; i < filenames.Length; i++)
                    {
                        (passed, total) = Test.Operations($@"{path}\Tests\sample cases for RSA Operations ( add , sub , mult)\{filenames[i]}.txt", @$"{path}\Tests\sample cases for RSA Operations ( add , sub , mult)\{filenames[i]}_Output.txt", $@"{path}\Results\sample cases for RSA Operations ( add , sub , mult)\{filenames[i]}.txt", operations[i]);
                        Console.WriteLine($"{filenames[i]} Passed: {passed} / {total}\n");

                    }

                    // Testing RSA SecureX
                    filenames = ["SampleRSA.txt", @"Complete Test\TestRSA.txt"];

                    for (int i = 0; i < filenames.Length; i++)
                    {
                        (passed, total) = Test.SecureX(@$"{path}\Tests\{filenames[i]}", $@"{path}\Results\{filenames[i]}");
                        Console.WriteLine($"{filenames[i]} Passed: {passed} / {total}\n");
                    }

                    filenames = ["StringRSA.txt"];

                    for (int i = 0; i < filenames.Length; i++)
                    {
                        (passed, total) = Test.String(@$"{path}\Tests\{filenames[i]}", $@"{path}\Results\{filenames[i]}");
                        Console.WriteLine($"{filenames[i]} Passed: {passed} / {total}\n");
                    }
                    break;

            }

            

            



        }
        public static (Bigint, Bigint, Bigint) get_data()
        {
            Bigint n, key, m;

            Console.Write("N: ");
            n = (Console.ReadLine()).Trim();
            Console.Write("Key: ");
            key = (Console.ReadLine()).Trim();
            Console.Write("Message: ");
            m = (Console.ReadLine()).Trim();

            return (n, key, m);
        }
    }
}



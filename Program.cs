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
            // Testing Bigint Operations
            int passed, total;
            string[] filenames = ["AddTestCases", "MultiplyTestCases", "SubtractTestCases"];
            char[] operations = ['+', '*', '-'];

            for (int i = 0; i < filenames.Length; i++)
            {
                (passed, total) = Test.Operations($@"C:\Users\RexoL\source\repos\RSA-secureX\Tests\sample cases for RSA Operations ( add , sub , mult)\{filenames[i]}.txt", @$"C:\Users\RexoL\source\repos\RSA-secureX\Tests\sample cases for RSA Operations ( add , sub , mult)\{filenames[i]}_Output.txt", $@"C:\Users\RexoL\source\repos\RSA-secureX\Results\sample cases for RSA Operations ( add , sub , mult)\{filenames[i]}.txt", operations[i]);
                Console.WriteLine($"{filenames[i]} Passed: {passed} / {total}\n");
            }

            // Testing RSA SecureX
            filenames = ["SampleRSA.txt", @"Complete Test\TestRSA.txt"];
            
            for (int i = 0; i < filenames.Length; i++)
            {
                (passed, total) = Test.SecureX(@$"C:\Users\RexoL\source\repos\RSA-secureX\Tests\{filenames[i]}", $@"C:\Users\RexoL\source\repos\RSA-secureX\Results\{filenames[i]}");
                Console.WriteLine($"{filenames[i]} Passed: {passed} / {total}\n");
            }

            filenames = ["StringRSA.txt"];

            for (int i = 0; i < filenames.Length; i++)
            {
                (passed, total) = Test.String(@$"C:\Users\RexoL\source\repos\RSA-secureX\Tests\{filenames[i]}", $@"C:\Users\RexoL\source\repos\RSA-secureX\Results\{filenames[i]}");
                Console.WriteLine($"{filenames[i]} Passed: {passed} / {total}\n");
            }

        }
    }
}



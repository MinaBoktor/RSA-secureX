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
            string line;
            Queue<string> lines = new Queue<string>();
            StreamReader sr = new StreamReader(@"C:\Users\RexoL\source\repos\RSA-secureX\SampleRSA.txt");
            line = sr.ReadLine();
            
            while (line != null)
            {
                lines.Enqueue(line);
                line = sr.ReadLine();
            }

            sr.Close();

            using (StreamWriter sw = File.CreateText(@"C:\Users\RexoL\source\repos\RSA-secureX\Result.txt"))
            {

                while (lines.Count > 0)
                {
                    int t1 = System.Environment.TickCount;

                    Bigint n = lines.Dequeue();
                    Bigint key = lines.Dequeue();
                    string message = lines.Dequeue();
                    bool operation = (lines.Dequeue() == "0") ? false: true;

                    sw.WriteLine($"N: {n}");
                    sw.WriteLine($"Key: {key}");
                    sw.WriteLine($"Message: {message}");
                    sw.WriteLine($"Operation: {operation}");

                    if (!operation)
                    {
                        sw.WriteLine($"The Encryption: {Encrypt(message, key, n)}");
                    }
                    else
                    {
                        sw.WriteLine($"The Decryption: {Decrypt(message, key, n)}");
                    }
                    int t2 = System.Environment.TickCount;
                    sw.WriteLine($"Execution Time: {t2 - t1} Milliseconds\n");
                }
            }

        }

        public static Bigint Encrypt(Bigint m, Bigint e, Bigint n)
        {
            // Using Modular Exponential
            Bigint result = "1";

            m = m % n;

            while (e > 0)
            {
                if (!e.Is_even())
                {
                    result = (result * m) % n;
                }

                m = (m ^ 2) % n;
                e = e / 2;
            }

            return result;
        }

        public static Bigint Decrypt(Bigint em, Bigint d, Bigint n)
        {
            // Using Modular Exponential
            Bigint result = "1";

            em = em % n;

            while (d > 0)
            {
                if (!d.Is_even())
                {
                    result = (result * em) % n;
                }

                em = (em ^ 2) % n;
                d = d / 2;
            }

            return result;
        }
    }
}



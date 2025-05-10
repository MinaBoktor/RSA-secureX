using System;
using securex;



namespace securex
{
    class Program
    {
        static void Main(string[] args)
        {
            int t1 = System.Environment.TickCount;


            //Test.simple();



            Bigint n = "";
            Bigint key = "";
            Bigint message = "";
            bool operation = false;

            if (!operation)
            {
                Console.WriteLine(Encrypt(message, key, n));
            }
            else
            {
                Console.WriteLine(Decrypt(message, key, n));
            }

            int t2 = System.Environment.TickCount;
            Console.WriteLine($"Execution Time: {t2 - t1} Milliseconds");
        }

        public static Bigint Encrypt(Bigint m, Bigint e, Bigint n)
        {
            return (m^e)%n;
        }

        public static Bigint Decrypt(Bigint em, Bigint d, Bigint n)
        {
            return (em^d)%n;
        }
    }
}


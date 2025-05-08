using System;
using securex;



namespace securex
{
    class Program
    {
        static void Main(string[] args)
        {
            int t1 = System.Environment.TickCount;

            //Testing();

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

        public static void Testing() 
        {
            Bigint p = "1232145542523548967863514354897456132165456132153614564798432513256497898";
            Bigint q = "3";
            
            Console.WriteLine(p);
            Console.WriteLine(q);
            Bigint a = p * q;
            Console.WriteLine(a);
            Console.WriteLine($"Number of Digits: {a.ToString().Length}");

            string answer = "3696436627570646903590543064692368396496368396460843694395297539769493694";
            if (a.ToString() == answer)
            {
                Console.WriteLine("Sucess");
            }

        }

    }
}


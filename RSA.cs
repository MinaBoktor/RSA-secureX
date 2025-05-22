using System;
using System.Security.Cryptography;


namespace securex
{
    public class RSA
    {
        public static string Encrypt(Bigint m, Bigint e, Bigint n)
        {
            // Using Modular Exponential
            Bigint result = Modular_exponential(m, e, n);

            return result.get_value();
        }

        public static string String_Encrypt(string m, string e, string n)
        {
            string temp = "";
            string temp2 = "0";
            for (int i = 0; i < m.Length; i++)
            {
                temp2 = Encrypt(m[i].ToString(), e, n);
                
                while (temp2.Length != 10) { temp2 = "0" + temp2; }
                temp = temp + temp2;
            }

            return temp;
        }

        public static string String_Decrypt(string m, string d, string n)
        {
            string temp = "";
            string temp2 = "0";
            for (int i = 0; i < m.Length; i += 10)
            {
                temp2 = Decrypt(m.Substring(i, 10), d, n);
                temp += Ascii.Deconvert(temp2);
            }
            return temp;
        }

        public static string Decrypt(Bigint em, Bigint d, Bigint n)
        {
            // Using Modular Exponential
            Bigint result = Modular_exponential(em, d, n);

            return result.get_value();
        }

        public static Bigint Modular_exponential(Bigint bV, Bigint exp, Bigint mod)
        {
            Bigint result = "1";

            bV = bV % mod;

            while (exp > 0)
            {
                if (!exp.Is_even())
                {
                    result = (result * bV) % mod;
                }

                bV = (bV ^ 2) % mod;
                exp = exp / 2;
            }

            return result;
        }

        public static Bigint Modular_exponential_Inverse(Bigint a, Bigint m)
        {
            Bigint m0 = m, y = 0, x = 1;

            while (a > 1)
            {
                Bigint q = a / m;
                Bigint t = m;

                m = a % m;
                a = t;
                t = y;

                y = x - q * y;
                x = t;
            }

            if (x < 0)
                x += m0;

            return x;
        }

        public static (Bigint, Bigint, Bigint) Generate_Public(int digits)
        {
            Bigint n, e, d;
            (n, e, d) = CreateKeys(GetPrime(digits), GetPrime(digits));

            Console.WriteLine($"N: {n}");
            Console.WriteLine($"E: {e}");
            Console.WriteLine($"D: {d}");

            return (n, e, d);
        }

        public static Bigint GetGCD(Bigint n1, Bigint n2)
        {
            Bigint temp;

            while (!n2.Equals(0))
            {
                temp = n2;
                n2 = n1 % n2;
                n1 = temp;
            }
            return n1;
        }

        public static (Bigint n, Bigint enc, Bigint dec) CreateKeys(Bigint p, Bigint q)
        {
            Bigint n, totient, enc, dec;

            n = p * q;
            totient = (p - 1) * (q - 1); // Euler's totient

            enc = 3;
            while (enc < totient)
            {
                if ((GetGCD(enc, totient)).Equals(1))
                    break;
                enc = enc + 1;
            }

            Console.WriteLine($"e = {enc}, totient = {totient}");
            dec = Modular_exponential_Inverse(enc, totient);

            return (n, enc, dec);

        }


        public static Bigint GetPrime(int digits)
        {
            Random rnd = new Random();
            Bigint min = "10" ^ (Bigint.ToBigint(digits.ToString()) - "1");
            Bigint max = ("10" ^ Bigint.ToBigint(digits.ToString())) - "1";


            Bigint guess;
            do
            {
                guess = NextLong(rnd, min, max);
            } while (!IsPrime(guess));

            return guess;
        }

        // Miller-Rabin Prime Number Test O((N^1.585) * Log N)
        public static bool IsPrime(Bigint num, int rounds=5)
        {
            Random rnd = new Random();
            if (num < 2)
                return false;
            if (num.Equals(2) || num.Equals(3))
                return true;
            if ((num % 2).Equals(0))
                return false;

            // num - 1 as 2^s * d
            Bigint d = num - 1;
            Bigint s = 0;
            while ((d % 2).Equals(0))
            {
                d /= 2;
                s += 1;
            }

            
            for (int i = 0; i < rounds; i++)
            {
                Bigint a = NextLong(rnd, 2, num - 2);
                Bigint x = Modular_exponential(a, d, num);

                if (x.Equals(1) || x == num - 1)
                    continue;

                bool continueOuter = false;
                for (int r = 1; r < s; r++)
                {
                    x = Modular_exponential(x, new Bigint(2), num);
                    if (x == num - 1)
                    {
                        continueOuter = true;
                        break;
                    }
                }

                if (!continueOuter)
                    return false;
            }

            return true;
        }



        public static Bigint NextLong(Random rnd, Bigint min, Bigint max)
        {
            byte[] buf = new byte[8];
            rnd.NextBytes(buf);
            Bigint val = (BitConverter.ToInt64(buf, 0) & 0x7FFFFFFFFFFFFFFF).ToString(); // from 0 to 255;
            return (val % (max - min + 1)) + min;
        }

    }
}


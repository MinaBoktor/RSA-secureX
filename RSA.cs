using System;


namespace securex
{
    public class RSA
    {
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


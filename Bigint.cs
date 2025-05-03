using System;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Numerics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace securex
{
    public class Bigint
    {

        private string value;
        public Bigint(string bigint)
        {
            value = bigint;
        }

        public Bigint(int bigint)
        {
            value = bigint.ToString();
        }

        // The addition property
        public static Bigint operator +(Bigint first, Bigint second)
        {
            int[] f = first.Split();
            int[] s = second.Split();

            
            int tf = f.Length;
            int ts = s.Length;

            int max = tf > ts ? tf : ts;

            Stack<int> answer = new Stack<int>();

            int sum = -1;
            int r = 0;
            int indexf, indexs;

            for (int i = 0; i < max; i++)
            {
                indexf = tf - i - 1;
                indexs = ts - i - 1;

                if (indexf < 0)
                {
                    if (indexs >= 0)
                    {
                        (sum, r) = Add(r, s[indexs]);
                    }
                }
                else if (indexs < 0) {
                    (sum, r) = Add(f[indexf], r);
                }
                else
                {
                    (sum, r) = Add(f[indexf], s[indexs] + r);
                }

                answer.Push(sum);


            }

            if (r == 1)
            {
                answer.Push(r);
            }
            return string.Join("", answer);
        }

        public static (int, int) Add(int f, int s)
        {
            int n = f + s;

            if (n.ToString().Length == 1)
            {
                return (n, 0);
            }
            else
            {
                return (n-10, 1);
            }
        }

        // The subtraction property
        public static Bigint operator -(Bigint first, Bigint second)
        {
            int[] f = first.Split();
            int[] s = second.Split();


            int tf = f.Length;
            int ts = s.Length;

            int max = tf > ts ? tf : ts;

            Stack<int> answer = new Stack<int>();

            int diff = -1;
            int r = 0;
            int indexf, indexs;

            for (int i = 0; i < max; i++)
            {
                indexf = tf - i - 1;
                indexs = ts - i - 1;

                if (indexf < 0)
                {
                    if (indexs >= 0)
                    {
                        (diff, r) = Sub(0, s[indexs] - r);
                    }
                }
                else if (indexs < 0)
                {
                    (diff, r) = Sub(f[indexf], r);
                }
                else
                {
                    
                    (diff, r) = Sub(f[indexf], s[indexs] - r);
                }

                answer.Push(diff);


            }

            if (answer.Peek() == 0)
            {
                answer.Pop();
            }

            if (r == 1)
            {
                return "0";
            }
            return string.Join("", answer);

        }

        public static (int, int) Sub(int f, int s)
        {
            int n = f - s;

            if (n >= 0)
            {
                return (n, 0);
            }
            else
            {
                return (n + 10, 1);
            }
        }

        // The multiplication property
        public static Bigint operator *(Bigint first, Bigint second)
        {
            // Delete the following line and Write your code
            throw new NotImplementedException();
        }

        // The division property
        public static Bigint operator /(Bigint first, Bigint second)
        {
            // Delete the following line and Write your code
            throw new NotImplementedException();
        }

        // The Modulus property
        public static Bigint operator %(Bigint first, Bigint second)
        {
            // Delete the following line and Write your code
            throw new NotImplementedException();
        }

        // The Power property
        public static Bigint operator ^(Bigint first, Bigint second)
        {
            // Delete the following line and Write your code
            throw new NotImplementedException();
        }

        public int[] Split()
        {
            return value.ToCharArray().Select(c => int.Parse(c.ToString())).ToArray();
        }

        // To represent Bigint as String
        public override string ToString()
        {
            return value;
        }

        // To make Bigint accept the assignation of a string
        public static implicit operator Bigint(string number)
        {
            return new Bigint(number);
        }

        // To make Bigint accept the assignation of an int
        public static implicit operator Bigint(int number)
        {
            return new Bigint(number);
        }
    }
}



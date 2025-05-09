using System;
using System.Collections.Specialized;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Numerics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace securex
{
    public class Bigint
    {

        private string value;
        public bool positive;
        public Bigint(string bigint)
        {
            if (bigint.Length == 0)
            {
                value = "0";
                positive = true;
            }
            else
            {
                positive = bigint[0] == '-'? false : true;

                Set_value(bigint);

            }

        }

        public Bigint(int bigint)
        {
            value = bigint.ToString();
            positive = true;
        }

        public void Set_value(string bigint)
        {
            for (int i = 0; i < bigint.Length; i++)
            {
                try
                {
                    if (!int.TryParse(bigint[i].ToString(), out int value))
                    {
                        bigint = bigint.Remove(i, 1);
                        i--;
                    }
                }
                catch (Exception ex)
                {
                    continue;
                }

            }

            while (bigint[0] == '0' && bigint.Length > 1)
            {
                bigint = bigint.Substring(1);
            }


            value = bigint;
        }

        // The addition property
        public static Bigint operator +(Bigint first, Bigint second)
        {
            if (first.positive && !second.positive) 
            { 
                second.positive = true;
                return first - second; 
            }
            if (!first.positive && second.positive)
            {
                first.positive = true;
                return second - first; 
            }
            if (!first.positive && !second.positive) {
                first.positive = true;
                second.positive = true;
                Bigint ans = first + second;
                ans.positive = false;
                return ans;
            }

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
            Bigint ans;
            if (first.positive && second.positive && second > first)
            {
                ans = second - first;
                ans.positive = false;
                return ans;
            }

            if (!first.positive && second.positive) 
            {
                first.positive = true;
                ans = first + second;
                ans.positive = false;
                return ans;
            }

            if (first.positive && !second.positive)
            {
                second.positive = true;
                return first + second;
            }

            if (!first.positive && !second.positive)
            {
                first.positive = true;
                second.positive = true;
                return second - first;
            }


            int[] f = first.Split();
            int[] s = second.Split();


            int tf = f.Length;
            int ts = s.Length;

            int max = tf > ts ? tf : ts;

            Stack<int> answer = new Stack<int>();

            int diff = 0;
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
                        (diff, r) = Sub(0, s[indexs], r);
                    }
                }
                else if (indexs < 0)
                {
                    (diff, r) = Sub(f[indexf], 0, r);
                }
                else
                {

                    (diff, r) = Sub(f[indexf], s[indexs], r);
                }


                answer.Push(diff);


            }


            while (answer.Count > 1 && answer.Peek() == 0)
            {
                answer.Pop();
            }

            if (r == 1)
            {
                return "0";
            }

            return string.Join("", answer);

        }

        public static (int, int) Sub(int f, int s, int r=0)
        {
            int n = f - s - r;

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
            // Sign Determination
            Bigint final = "";
            if (first.positive && second.positive) { final.positive = true; }
            else if (!first.positive && second.positive)
            {
                first.positive = true;
                final.positive = false;
            }
            else if (first.positive && !second.positive)
            {
                second.positive = true;
                final.positive = false;
            }
            else if (!first.positive && !second.positive)
            {
                first.positive = true;
                second.positive = true;
                final.positive = true;
            }

            string x = first.value;
            string y = second.value;

            if (x == "0" || y == "0")
            {
                return "0";
            }

            int n = Math.Max(x.Length, y.Length);

            // Base case for recursion
            if (n < 9)
            {
                final.Set_value((long.Parse(x) * long.Parse(y)).ToString());
                return final;

            }


            // Make lengths equal by padding with leading zeros
            while (x.Length < n) x = "0" + x;
            while (y.Length < n) y = "0" + y;

            // Split numbers into halves
            int m = n / 2;
            string a = x.Substring(0, x.Length - m);
            string b = x.Substring(x.Length - m);
            string c = y.Substring(0, y.Length - m);
            string d = y.Substring(y.Length - m);

            // Intializing Bigintergers
            Bigint biga = a;
            Bigint bigb = b;
            Bigint bigc = c;
            Bigint bigd = d;

            // Recursive steps
            Bigint ac = biga * bigc;
            Bigint bd = bigb * bigd;

            // (a+b)*(c+d)
            Bigint temp = (biga + bigb) * (bigc + bigd);  

            // Combine: (a+b)*(c+d) - ac - bd
            Bigint middle = temp - ac - bd;

            // Adding the weight to each number according to its position
            ac = ac.ToString() + new string('0', 2*m);
            middle = middle.ToString() + new string('0', m);

            final.value = (ac + middle + bd).ToString();
            return final;

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
            if (!positive)
                return $"-{value}";
            else
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

        public static bool operator <(Bigint a, Bigint b)
        {
            return a.CompareTo(b) < 0;
        }

        public static bool operator >(Bigint a, Bigint b)
        {
            return a.CompareTo(b) > 0;
        }

        public bool Equals(Bigint other)
        {
            if (this.value == other.value)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int CompareTo(Bigint other)
        {
            if (!this.positive &&  other.positive) { return -1; }
            if (this.positive && !other.positive) { return 1; }
            if (this.positive && other.positive)
            {
                if (this.value.Length > other.value.Length) { return 1; }
                if (this.value.Length < other.value.Length) { return -1; }
                for (int i = 0; i < this.value.Length; i++)
                {
                    if (this.value[i] > other.value[i]) { return 1; }
                    if (this.value[i] < other.value[i]) { return -1; }
                }
                return 0;
            }
            else
            {
                if (this.value.Length < other.value.Length) { return 1; }
                if (this.value.Length > other.value.Length) { return -1; }
                for (int i = 0; i < this.value.Length; i++)
                {
                    if (this.value[i] > other.value[i]) { return -1; }
                    if (this.value[i] < other.value[i]) { return 1; }
                }
                return 0;
            }
        }
    }
}



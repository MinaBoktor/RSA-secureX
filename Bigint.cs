using System;
using System.Diagnostics.CodeAnalysis;
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

        public static Bigint operator +(Bigint first, Bigint second)
        {
            // Delete the following line and Write your code
            throw new NotImplementedException();
        }

        public static Bigint operator -(Bigint first, Bigint second)
        {
            // Delete the following line and Write your code
            throw new NotImplementedException();

        }

        public static Bigint operator *(Bigint first, Bigint second)
        {
            // Delete the following line and Write your code
            throw new NotImplementedException();
        }

        public static Bigint operator /(Bigint first, Bigint second)
        {
            // Delete the following line and Write your code
            throw new NotImplementedException();
        }

        public static Bigint operator %(Bigint first, Bigint second)
        {
            // Delete the following line and Write your code
            throw new NotImplementedException();
        }

        public static Bigint operator ^(Bigint first, Bigint second)
        {
            // Delete the following line and Write your code
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            // Write your code here
            return value;
        }

        public static implicit operator Bigint(string name)
        {
            return new Bigint(name);
        }
    }
}



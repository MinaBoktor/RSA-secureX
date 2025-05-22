using System;
using System.Collections.Generic;

namespace securex
{
    public class Ascii
    {
        public Ascii()
        {

        }

        public static string convert(string str)
        {
            string result = "";
            int temp;
            for (int i = 0; i < str.Length; i++)
            {
                temp = str[i];
                if (temp < 10) { result = $"{result}00{temp}"; }
                else if (temp < 100) { result = $"{result}0{temp}"; }
                else { result = $"{result}{temp}"; }
            }
            return result;
        }

        public static string Deconvert(string number)
        {
            string result = "";
            char temp;

            
            while (number.Length%10 != 0) { number = 0 + number; }

            for (int i = 0; i < number.Length; i+=10)
            {
                temp = (char) Int32.Parse(number.Substring(i, 10));
                result += temp;
            }
            return result;
        }
    }
}


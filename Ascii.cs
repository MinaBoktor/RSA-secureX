using System;

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

        public static string convert(Bigint number)
        {
            string result = "";
            char temp;
            string value = number.get_value();
            
            while (value.Length%3 != 0) { value = 0 + value; }

            for (int i = 0; i < value.Length; i+=3)
            {
                temp = (char) Int32.Parse($"{value[i]}{value[i + 1]}{value[i + 2]}");
                result += temp;
            }
            return result;
        }
    }
}


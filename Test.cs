using System;
using System.Collections.Generic;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;

namespace securex
{
    public class Test
    {
        public Test() {}

        public static List<string> Read(string path)
        {
            string line;
            List<string> lines = new List<string>();
            StreamReader sr = new StreamReader(path);
            line = sr.ReadLine();

            while (line != null)
            {
                lines.Add(line);
                line = sr.ReadLine();
            }

            sr.Close();

            return lines;
        }

        public static (int, int) SecureX(string path, string output)
        {
            Bigint result;
            List<string> lines = Read(path);
            int total = int.Parse(lines[0]);
            int passed = 0;

            using (StreamWriter sw = File.CreateText(output))
            {
                

                for (int i = 1; i < lines.Count; i+=4)
                {
                    int t1 = System.Environment.TickCount;

                    Bigint n = lines[i];
                    Bigint key = lines[i + 1];
                    string message = lines[i + 2];
                    bool operation = (lines[i + 3] == "0") ? false : true;

                    sw.WriteLine($"N: {n}");
                    sw.WriteLine($"Key: {key}");
                    sw.WriteLine($"Message: {message}");
                    sw.WriteLine($"Operation: {operation}");

                    if (!operation)
                    {
                        result = RSA.Encrypt(message, key, n);
                        sw.WriteLine($"The Encryption: {result}");

                        if (result.ToString() == lines[i + 6])
                        {
                            sw.WriteLine($"Status: Passed");
                            passed++;
                        }
                        else
                        {
                            sw.WriteLine($"Status: Failed");
                        }
                    }

                    else
                    {
                        result = RSA.Decrypt(message, key, n).ToString();

                        sw.WriteLine($"The Decryption: {result}");

                        if (result.ToString() == lines[i - 2]) 
                        {
                            sw.WriteLine($"Status: Passed");
                            passed++; 
                        }
                        else
                        {
                            sw.WriteLine($"Status: Failed");
                        }
                    }
                    int t2 = System.Environment.TickCount;
                    sw.WriteLine($"Execution Time: {t2 - t1} Milliseconds\n");
                }
            }

            return (passed, total);
        }

        public static (int, int) String(string path, string output)
        {
            string final;
            List<string> lines = Read(path);
            int total = int.Parse(lines[0]);
            int passed = 0;

            using (StreamWriter sw = File.CreateText(output))
            {
                for (int i = 1; i < lines.Count; i += 4)
                {
                    int t1 = System.Environment.TickCount;

                    string n = lines[i];
                    string key = lines[i + 1];
                    string message = lines[i + 2];
                    bool operation = (lines[i + 3] == "0") ? false : true;

                    sw.WriteLine($"N: {n}");
                    sw.WriteLine($"Key: {key}");
                    sw.WriteLine($"Message: {message}");
                    sw.WriteLine($"Operation: {operation}");

                    if (!operation)
                    {
                        final = RSA.String_Encrypt(message, key, n);
                        sw.WriteLine($"The Encryption: {final}");

                        if (final == lines[i + 6])
                        {
                            sw.WriteLine($"Status: Passed");
                            passed++;
                        }
                        else
                        {
                            sw.WriteLine($"Status: Failed");
                        }
                    }
                    else
                    {
                        final = RSA.String_Decrypt(message, key, n);

                        sw.WriteLine($"The Decryption: {final}");

                        if (final == lines[i - 2])
                        {
                            sw.WriteLine($"Status: Passed");
                            passed++;
                        }
                        else
                        {
                            sw.WriteLine($"Status: Failed");
                        }
                    }
                    int t2 = System.Environment.TickCount;
                    sw.WriteLine($"Execution Time: {t2 - t1} Milliseconds\n");
                }
            }

            return (passed, total);
        }



        public static (int, int) Operations(string path, string answers_path , string output, char operation)
        {
            
            Queue<string> lines = new Queue<string>(Read(path));
            Queue<string> answers = new Queue<string>(Read(answers_path));

            int total = int.Parse(lines.Dequeue());
            int passed = 0;
            Bigint result;

            using (StreamWriter sw = File.CreateText(output))
            {
                while (lines.Count > 2)
                {
                    int t1 = System.Environment.TickCount;

                    lines.Dequeue();
                    Bigint first = lines.Dequeue();
                    Bigint second = lines.Dequeue();
                    sw.WriteLine($"First Number: {first}");
                    sw.WriteLine($"Second Number: {second}");

                    if (operation == '+')
                    {
                        result = first + second;
                    }
                    else if (operation == '-')
                    {
                        result = first - second;
                    }
                    else if (operation == '*')
                    {
                        result = first * second;
                    }
                    else
                    {
                        return (-1, -1);
                    }

                    sw.WriteLine($"Result: {result}");

                    if (result.ToString() == answers.Dequeue())
                    {
                        passed++;
                        sw.WriteLine($"Status: Passed");
                    }
                    else
                    {
                        sw.WriteLine($"Status: Failed");
                    }

                    if (answers.Count > 0)
                    {
                        answers.Dequeue();
                    }
                    

                    int t2 = System.Environment.TickCount;
                    sw.WriteLine($"Execution Time: {t2 - t1} Milliseconds\n");
                }
            }

            return (passed, total);
        }

    }
}

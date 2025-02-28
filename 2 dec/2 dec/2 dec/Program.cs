﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2_dec
{
    internal class Program
    {
        static List<string> allLines = new List<string>();
        static void Main(string[] args)
        {
            setup();
            //int safe = puzzle1();
            int safe = puzzle2();
            Console.WriteLine(safe);
            //Console.ReadLine();

        }

        static int puzzle1()
        {
            int safe = 0;
            foreach (var line in allLines)
            {
                bool isSafe = true;
                var numbers = line.Split(' ')?.Select(Int32.Parse).ToList();

                isSafe = checkSafety(numbers);

                if (isSafe)
                {
                    safe++;
                }
            }
            return safe;
        }

        static int puzzle2()
        {
            int safe = 0;
            foreach (var line in allLines)
            {
                bool isSafe;
                List<int> numbers = line.Split(' ')?.Select(Int32.Parse).ToList();

                isSafe = checkSafety(numbers);

                if (!isSafe)
                {
                    for (int i = 0; i < numbers.Count; i++)
                    {
                        List<int> numberVariation = numbers.ToList();
                        numberVariation.RemoveAt(i);

                        isSafe = checkSafety(numberVariation);

                        if (isSafe)
                        {
                            safe++;
                            break;
                        }
                    }
                }
                else
                {
                    safe++;
                }
            }
            return safe;
        }

        static bool checkSafety(List<int> numbers)
        {
            bool isSafe = true;
            bool isAsc = false;
            if (numbers[0] < numbers[1])
            {
                isAsc = true;
            }
            else
            {
                isAsc = false;
            }
            for (int i = 0; i < numbers.Count - 1; i++)
            {
                if (Math.Abs(numbers[i] - numbers[i + 1]) < 1 || Math.Abs(numbers[i] - numbers[i + 1]) > 3)
                {
                    isSafe = false;
                    break;
                }
                else if (isAsc == true && numbers[i] > numbers[i + 1] || isAsc == false && numbers[i] < numbers[i + 1])
                {
                    isSafe = false;
                    break;
                }
                else
                {
                    isSafe = true;
                }
            }
            return isSafe;
        }

        public static void setup()
        {
            String line;
            try
            {
                //Pass the file path and file name to the StreamReader constructor
                StreamReader sr = new StreamReader("C:\\Users\\Robin\\Documents\\GitHub\\AdventOfCode2024\\2 dec\\2 dec\\input.txt");
                //Read the first line of text
                line = sr.ReadLine();
                //Continue to read until you reach end of file
                while (line != null)
                {
                    //write the line to console window
                    allLines.Add(line);
                    //Read the next line
                    line = sr.ReadLine();
                }
                //close the file
                sr.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
        }
        
    }
}

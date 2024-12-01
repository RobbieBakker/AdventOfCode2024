using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace _1_dec
{
    internal class Program
    {
        static List<string> allLines = new List<string>();
        static List<int> leftList = new List<int>();
        static List<int> rightList = new List<int>();
        static void Main(string[] args)
        {
            setup();
            //puzzle1();
            puzzle2();
        }

        static void setup()
        {
            readFile();
            splitLists();
        }

        static void puzzle1()
        {
            // Sorting the lists so they are in ascending order, making comparing pairs easier
            leftList.Sort();
            rightList.Sort();
            Console.WriteLine(checkDifferences());
        }

        static void puzzle2()
        {
            Console.WriteLine(checkSimilarity());
        }

        static void splitLists()
        {
            // Looping through every line, splitting them on the white-spacing and adding them to left and right lists.
            foreach (string line in allLines)
            {
                string[] numbers = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                leftList.Add(int.Parse(numbers[0]));
                rightList.Add(int.Parse(numbers[1]));
            }
        }

        static int checkDifferences()
        {
            int difference = 0;
            // looping through the left list
            while (leftList.Count > 0)
            {
                // Calculating the differences in the ascending sorted lists and deleting the compared values.
                difference += Math.Abs(leftList[0] - rightList[0]);
                leftList.RemoveAt(0);
                rightList.RemoveAt(0);
            }
            return difference;
        }

        static int checkSimilarity()
        {
            int similarity = 0;
            // looping through the left list
            foreach (int item in leftList)
            {
                // Checking if value exists in the right list and counting the occurences.
                int occurence = rightList.Where(x => x.Equals(item)).Count();
                similarity += item * occurence;
            }
            return similarity;
        }

        static void readFile()
        {
            String line;
            try
            {
                //Pass the file path and file name to the StreamReader constructor
                StreamReader sr = new StreamReader("C:\\Users\\Robin\\Documents\\GitHub\\AdventOfCode2024\\1 dec\\input.txt");
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

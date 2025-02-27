using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5_dec
{
    internal class Program
    {
        static int[,] order = new int[1176, 2];
        static List<List<int>> updates = new List<List<int>>();
        static void Main(string[] args)
        {
            var watch = Stopwatch.StartNew(); // Set stopwatch for tracking execution time in ms

            setup();
            //Console.WriteLine(puzzle1());
            Console.WriteLine(puzzle2());

            watch.Stop(); // Stop stopwatch and print
            Console.WriteLine("Execution time: " + watch.ElapsedMilliseconds);
        }

        // Puzzle 1 asks for the correct updates and return the sum of the middle values.
        static int puzzle1()
        {
            return getCorrectUpdates();
            //return sumMiddleItems(correctUpdates);
        }

        // Puzzle 2 asks for the wrong updates, sorting them into the correct order and return the sum of the middle values.
        static int puzzle2()
        {
            List<List<int>> wrongUpdates = getWrongUpdates();
            return orderList(wrongUpdates);
            //return sumMiddleItems(wrongUpdates);
        }

        // Returns the sum of the middle elements of the given list
        static int sumMiddleItems(List<List<int>> list)
        {
            int sum = 0;
            foreach (List<int> correctedUpdate in list)
            {
                sum += correctedUpdate[(correctedUpdate.Count / 2)];
            }
            return sum;
        }

        // Takes the total list 'updates', puts that into the 'correctUpdates' list and removes all incorrect updates.
        // Then is returns the list with only the correct updates.
        static int getCorrectUpdates()
        {
            List<List<int>> correctUpdates = new List<List<int>>(updates);
            int sum = 0;
            for (int i = 0; i < correctUpdates.Count; i++)
            {
                for (int j = 0; j < order.GetLength(0) ; j++)
                {
                    if (correctUpdates[i].Contains(order[j, 0]) && correctUpdates[i].Contains(order[j, 1]))
                    {
                        // If wrong order then delete & correct counter i--
                        if (correctUpdates[i].IndexOf(order[j, 0]) > correctUpdates[i].IndexOf(order[j, 1]))
                        {
                            correctUpdates.RemoveAt(i);
                            i--;
                            break;
                        }
                    }
                }
            }
            foreach (List<int> row in correctUpdates)
            {

                sum += row[row.Count / 2];
            }
            return sum;
        }

        // Takes the 'updates' lists, check when a rule fails and then adds that update to the incorrect list.
        // Then it returns the list with only the incorrect updates.
        static List<List<int>> getWrongUpdates()
        {
            List<List<int>> wrongUpdates = new List<List<int>>();
            for (int i = 0; i < updates.Count; i++)
            {
                for (int j = 0; j < order.GetLength(0); j++)
                {
                    int leftOrder = order[j, 0];
                    int rightOrder = order[j, 1];
                    if (updates[i].Contains(leftOrder) && updates[i].Contains(rightOrder))
                    {
                        if (updates[i].IndexOf(leftOrder) > updates[i].IndexOf(rightOrder))
                        {
                            wrongUpdates.Add(updates[i]);
                            break;
                        }
                    }
                }
            }
            return wrongUpdates;
        }

        // Takes a list and swaps numbers according to the order-list.
        // It keeps swapping until all order-rules are happy and one iteration is performed without swaps.
        // Then it returns the now-ordered list.
        static int orderList(List<List<int>> unorderedList)
        {
            int sum = 0;
            for (int i = 0; i < unorderedList.Count; i++)
            {
                bool correctOrder = false;
                while (!correctOrder)
                {
                    bool swaps = false;
                    for (int j = 0; j < order.GetLength(0); j++)
                    {
                        if (unorderedList[i].Contains(order[j, 0]) && unorderedList[i].Contains(order[j, 1]))
                        {
                            int leftIndex = unorderedList[i].IndexOf(order[j, 0]);
                            int rightIndex = unorderedList[i].IndexOf(order[j, 1]);
                            // Check order
                            if (leftIndex > rightIndex) // swap numbers  
                            {
                                unorderedList[i][rightIndex] = order[j, 0];
                                unorderedList[i][leftIndex] = order[j, 1];
                                swaps = true;
                            }
                        }
                    }
                    if (!swaps)
                    {
                        correctOrder = true;
                        sum += unorderedList[i][unorderedList[i].Count / 2];
                        break;
                    }
                }
            }
            return sum;
        }

        // Reads the input files and puts the input order into an int[,] and the updates into a List<List<int>>
        static void setup()
        {
            String line;
            try
            {
                //Pass the file path and file name to the StreamReader constructor
                StreamReader sr = new StreamReader("C:\\Users\\Robin\\Documents\\GitHub\\AdventOfCode2024\\5 dec\\input.txt");
                bool parsingRules = true;
                int i = 0;
                //Read the first line of text
                line = sr.ReadLine();
                //Continue to read until you reach end of file
                while (line != null)
                {
                    if (parsingRules && string.IsNullOrWhiteSpace(line))
                    {
                        parsingRules = false;
                        line = sr.ReadLine();
                    }
                    else if (parsingRules)
                    {
                        order[i, 0] = int.Parse(line.Split('|')[0]);
                        order[i, 1] = int.Parse(line.Split('|')[1]);
                        i++;
                    }
                    else
                    {
                        updates.Add(line.Split(',').Select(int.Parse).ToList());
                    }
                    //Read the next line
                    line = sr.ReadLine();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
        }
    }
}

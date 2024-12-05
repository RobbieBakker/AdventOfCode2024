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
        static int[,] order;
        static List<List<int>> updates = new List<List<int>>();
        static void Main(string[] args)
        {
            setup();
            var watch = Stopwatch.StartNew(); // Set stopwatch for tracking execution time in ms

            //Console.WriteLine(puzzle1());
            Console.WriteLine(puzzle2());

            watch.Stop(); // Stop stopwatch and print
            Console.WriteLine("Execution time: " + watch.ElapsedMilliseconds);
        }

        // Puzzle 1 asks for the correct updates and return the sum of the middle values.
        static int puzzle1()
        {
            List<List<int>> correctUpdates = new List<List<int>>();
            correctUpdates = getCorrectUpdates();
            return sumMiddleItems(correctUpdates);
        }

        // Puzzle 2 asks for the wrong updates, sorting them into the correct order and return the sum of the middle values.
        static int puzzle2()
        {
            List<List<int>> wrongUpdates = getWrongUpdates();
            wrongUpdates = orderList(wrongUpdates);
            return sumMiddleItems(wrongUpdates);
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
        static List<List<int>> getCorrectUpdates()
        {
            List<List<int>> correctUpdates = updates;
            for (int i = 0; i < correctUpdates.Count; i++)
            {
                for (int j = 0; j < order.GetLength(0); j++)
                {
                    if (correctUpdates[i].Contains(order[j, 0]) && correctUpdates[i].Contains(order[j, 1]))
                    {
                        // If wrong order then delete & correct counter i--
                        if (correctUpdates[i].IndexOf(order[j, 0]) > correctUpdates[i].IndexOf(order[j, 1]))
                        {
                            correctUpdates.RemoveAt(i);
                            i--;
                        }
                    }
                }
            }
            return correctUpdates;
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
                    if (updates[i].Contains(order[j, 0]) && updates[i].Contains(order[j, 1]))
                    {
                        if (updates[i].IndexOf(order[j, 0]) > updates[i].IndexOf(order[j, 1]))
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
        static List<List<int>> orderList(List<List<int>> unorderedList)
        {
            for (int i = 0; i < unorderedList.Count; i++)
            {
                bool correctOrder = false;
                while (!correctOrder)
                {
                    int swaps = 0;
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
                                swaps++;
                            }
                            else // 
                            {
                            }
                        }
                    }
                    if (swaps == 0)
                    {
                        correctOrder = true;
                    }
                }
            }
            return unorderedList; // Now it is ordered though.
        }

        // Reads the input files and puts the input order into an int[,] and the updates into a List<List<int>>
        static void setup()
        {
            String line;
            try
            {
                List<string> lines = new List<string>();
                //Pass the file path and file name to the StreamReader constructor
                StreamReader sr = new StreamReader("C:\\Users\\Robin\\Documents\\GitHub\\AdventOfCode2024\\5 dec\\order.txt");
                //Read the first line of text
                line = sr.ReadLine();
                //Continue to read until you reach end of file
                while (line != null)
                {
                    lines.Add(line);
                    //Read the next line
                    line = sr.ReadLine();
                }
                //close the file
                sr.Close();
                order = new int[lines.Count, 2];
                for (int i = 0; i < lines.Count; i++)
                {
                    order[i, 0] = int.Parse(lines[i].Split('|')[0]);
                    order[i, 1] = int.Parse(lines[i].Split('|')[1]);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            try
            {
                List<string> lines = new List<string>();
                //Pass the file path and file name to the StreamReader constructor
                StreamReader sr = new StreamReader("C:\\Users\\Robin\\Documents\\GitHub\\AdventOfCode2024\\5 dec\\update.txt");
                //Read the first line of text
                line = sr.ReadLine();
                //Continue to read until you reach end of file
                while (line != null)
                {
                    //write the line to console window
                    lines.Add(line);
                    //Read the next line
                    line = sr.ReadLine();
                }
                //close the file
                sr.Close();
                for (int i = 0; i < lines.Count; i++)
                {
                    string[] tmp = lines[i].Split(',');
                    updates.Add(new List<int>());
                    foreach (string letter in tmp)
                    {
                        updates[i].Add(int.Parse(letter));
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
        }
    }
}

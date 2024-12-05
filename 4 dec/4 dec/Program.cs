using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace _4_dec
{
    internal class Program
    {
        static List<List<string>> matrix = new List<List<string>>();
        static int counter = 0;
        static int[,] directions =
            { 
                { -1 , 0},
                { -1, 1 },
                { 0, 1 },
                { 1, 1 },
                { 1, 0 },
                { 1, -1 },
                { 0, -1 },
                { -1, -1 },
            };

        static void Main(string[] args)
        {
            setup();
            //puzzle1();
            //puzzle2();
            newPuzzle1();
            Console.WriteLine(counter);

        }

        static void newPuzzle1()
        {
            Console.WriteLine(directions[0,0] * 2 + "," + directions[0,1] * 2);
        }



        static void puzzle1()
        {
            for(int i = 0; i < matrix.Count; i++)
            {
                for(int j = 0; j < matrix[i].Count; j++)
                {
                    try
                    {
                        Console.Write("\n" + matrix[i][j] + matrix[i][j + 1]+ matrix[i][j + 2] + matrix[i][j + 3]);
                        if (matrix[i][j].Equals("X") && matrix[i][j+1].Equals("M") && matrix[i][j+2].Equals("A") && matrix[i][j+3].Equals("S")) // -->
                        {
                            counter++;
                            Console.Write("++++");
                        }
                        else if (matrix[i][j].Equals("S") && matrix[i][j + 1].Equals("A") && matrix[i][j + 2].Equals("M") && matrix[i][j + 3].Equals("X")) // <--
                        {
                            counter++;
                            Console.Write("++++");
                        }
                    }
                    catch
                    {
                        Console.WriteLine("Out of bounds");
                    }
                    try
                    {

                        Console.Write("\n" + matrix[i][j] + matrix[i + 1][j] + matrix[i + 2][j] + matrix[i + 3][j]);
                        if (matrix[i][j].Equals("X") && matrix[i + 1][j].Equals("M") && matrix[i + 2][j].Equals("A") && matrix[i + 3][j].Equals("S")) // ^
                        {
                            counter++;
                            Console.Write("++++");
                        }
                        else if (matrix[i][j].Equals("S") && matrix[i + 1][j].Equals("A") && matrix[i + 2][j].Equals("M") && matrix[i + 3][j].Equals("X")) // !
                        {
                            counter++;
                            Console.Write("++++");
                        }
                    }
                    catch
                    {
                        Console.WriteLine("Out of bounds");
                    }
                    try
                    {

                        Console.Write("\n" + matrix[i][j] + matrix[i+1][j + 1] + matrix[i + 2][j + 2] + matrix[i+3][j + 3]);
                        if (matrix[i][j].Equals("X") && matrix[i + 1][j + 1].Equals("M") && matrix[i + 2][j + 2].Equals("A") && matrix[i + 3][j + 3].Equals("S")) // naar rechtsonder
                        {
                            counter++;
                            Console.Write("++++");
                        }
                        else if (matrix[i][j].Equals("S") && matrix[i + 1][j + 1].Equals("A") && matrix[i + 2][j + 2].Equals("M") && matrix[i + 3][j + 3].Equals("X")) // naar linksboven
                        {
                            counter++;
                            Console.Write("++++");
                        }
                    }
                    catch
                    {
                        Console.WriteLine("Out of bounds");
                    }
                    try
                    {

                        Console.Write("\n" + matrix[i][j] + matrix[i +1 ][j - 1] + matrix[i + 1][j - 2] + matrix[i + 1][j - 3]);
                        if (matrix[i][j].Equals("X") && matrix[i + 1][j - 1].Equals("M") && matrix[i + 2][j - 2].Equals("A") && matrix[i + 3][j - 3].Equals("S")) // naar linksonder
                        {
                            counter++;
                            Console.Write("++++");
                        }
                        else if (matrix[i][j].Equals("S") && matrix[i + 1][j - 1].Equals("A") && matrix[i + 2][j - 2].Equals("M") && matrix[i + 3][j - 3].Equals("X")) // naar rechtsboven
                        {
                            counter++;
                            Console.Write("++++");
                        }
                    }
                    catch
                    {
                        Console.WriteLine("Out of bounds");
                    }

                }
            }

        }

        static void puzzle2()
        {
            for (int i = 0; i < matrix.Count; i++)
            {
                for (int j = 0; j < matrix[i].Count; j++)
                {
                    try
                    {
                        if (matrix[i][j].Equals("M") && matrix[i+1][j + 1].Equals("A") && matrix[i+2][j + 2].Equals("S") && matrix[i][j + 2].Equals("M") && matrix[i + 2][j].Equals("S")) // -->
                        {
                            counter++;
                            Console.Write("++++");
                        }
                        else if (matrix[i][j].Equals("M") && matrix[i + 1][j + 1].Equals("A") && matrix[i + 2][j + 2].Equals("S") && matrix[i][j + 2].Equals("S") && matrix[i + 2][j].Equals("M")) // -->
                        {
                            counter++;
                            Console.Write("++++");
                        }
                        else if (matrix[i][j].Equals("S") && matrix[i + 1][j + 1].Equals("A") && matrix[i + 2][j + 2].Equals("M") && matrix[i][j + 2].Equals("M") && matrix[i + 2][j].Equals("S")) // -->
                        {
                            counter++;
                            Console.Write("++++");
                        }
                        else if (matrix[i][j].Equals("S") && matrix[i + 1][j + 1].Equals("A") && matrix[i + 2][j + 2].Equals("M") && matrix[i][j + 2].Equals("S") && matrix[i + 2][j].Equals("M")) // -->
                        {
                            counter++;
                            Console.Write("++++");
                        }
                    }
                    catch
                    {
                        Console.WriteLine("Out of bounds");
                    }
                }
            }

        }

        static void setup()
        {
            List<string> input = new List<string>();
            String line;
            try
            {
                //Pass the file path and file name to the StreamReader constructor
                StreamReader sr = new StreamReader("C:\\Users\\Robin\\Documents\\GitHub\\AdventOfCode2024\\4 dec\\input.txt");
                //Read the first line of text
                line = sr.ReadLine();
                //Continue to read until you reach end of file
                while (line != null)
                {
                    //write the line to console window
                    input.Add(line);
                    //Read the next line
                    line = sr.ReadLine();
                }
                //close the file
                sr.Close();
                for (int i = 0; i < input.Count; i++)
                {
                    char[] tmp = input[i].ToCharArray();
                    matrix.Add(new List<string>());
                    foreach (char letter in tmp)
                    {
                        matrix[i].Add(letter.ToString());
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

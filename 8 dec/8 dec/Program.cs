using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8_dec
{
    internal class Program
    {
        static char[,] matrixArray;
        //static int antinodes = 0;
        static HashSet<(int, int)> antinodes = new HashSet<(int, int)>();

        static void Main(string[] args)
        {
            var watch = Stopwatch.StartNew(); // Set stopwatch for tracking execution time in ms
            setup();

            puzzle1();
            Console.WriteLine(antinodes.Count);


            watch.Stop(); // Stop stopwatch and print
            Console.WriteLine("Execution time: " + watch.ElapsedMilliseconds);
        }

        static void puzzle1()
        {
            for (byte y = 0; y < matrixArray.GetLength(0); y++)
            {
                for(byte x = 0; x < matrixArray.GetLength(1); x++)
                {
                    // looping through the matrix to find antennas
                    if (!matrixArray[y,x].Equals('.') && !matrixArray[y,x].Equals('#')) // find all but neglect empty positions and antinodes
                    {
                        char currentFrequency = matrixArray[y, x]; // get the current frequency letter
                        byte[] baseCoord = { y, x }; // find the current antenna coordinate
                        findOtherAntennas(baseCoord, currentFrequency);
                    }
                }
            }
        }

        static void findOtherAntennas(byte[] baseCoord, char currentFrequency)
        {
            for (byte y = baseCoord[0]; y < matrixArray.GetLength(0); y++) // loop through all rows, but neglect rows before the original antenna
            {
                for (byte x = 0; x < matrixArray.GetLength(1); x++) // loop through all positions on row
                {
                    if (y == baseCoord[0] && x == 0 && baseCoord[1]+1 < matrixArray.GetLength(1)) // We can ignore all positions on row before antenna, so if on same row and X is before antenna X
                    {
                        x = (byte)(baseCoord[1] +1); // set to check X to X after antenna X
                    }
                    if (matrixArray[y, x] == currentFrequency) // Found other antenna
                    {
                        int diffY = y - baseCoord[0]; // calculate difference in rows/Y
                        int diffX = x - baseCoord[1]; // calculate difference in positions/X
                        int[] node1 = {y + diffY, x + diffX }; // Calculate the coordinates of one other antinode
                        int[] node2 = { baseCoord[0] - diffY, baseCoord[1] - diffX }; // Calculate the coordinates of one other antinode
                        try // try-catch because when out of bounds we want to ignore
                        {
                            //antinodes.Add((node1[0], node1[1]));
                            if (matrixArray[node1[0], node1[1]].Equals('.')) // When found antinode is still empty
                            {
                                matrixArray[node1[0], node1[1]] = '#';
                                antinodes.Add((node1[0], node1[1]));
                            }
                            else // When found antinode is not empty (when another antenna, add it to antinodes. When other antinode it doesn't add (hashmap)
                            {
                                antinodes.Add((node1[0], node1[1]));
                            }
                        }
                        catch // when out of bounds
                        {
                        }
                        try // try-catch because when out of bounds we want to ignore
                        {
                            //antinodes.Add((node2[0], node2[1]));
                            if (matrixArray[node2[0], node2[1]].Equals('.')) // When found antinode is still empty
                            {
                                matrixArray[node2[0], node2[1]] = '#';
                                antinodes.Add((node2[0], node2[1]));
                            }
                            else
                            {
                                antinodes.Add((node2[0], node2[1]));
                            }
                        }
                        catch // When out of bounds
                        {
                        }
                    }
                }
            }
        }


        static void setup()
        {
            string[] lines = File.ReadAllLines("C:\\Users\\Robin\\Documents\\GitHub\\AdventOfCode2024\\8 dec\\input.txt");
            int rows = lines.Length;
            int cols = lines[0].Length;
            matrixArray = new char[rows, cols];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    matrixArray[i, j] = lines[i][j];
                }
            }
        }
    }
}

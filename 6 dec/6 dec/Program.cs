using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace _6_dec
{
    internal class Program
    {
        static char[,] matrixArray;
        static byte[] coord = new byte[2];
        static byte[] startingPosition = new byte[2];
        static byte directionIndex = 0;
        static sbyte[] direction = new sbyte[2];
        static int obstacleCount = 0;
        static HashSet<((byte, byte), byte)> visitedPostions = new HashSet<((byte, byte), byte)>();
        static void Main(string[] args)
        {
            var watch = Stopwatch.StartNew(); // Set stopwatch for tracking execution time in ms
            setup();

            getStartingPos();
            //puzzle1();
            puzzle2();


            watch.Stop(); // Stop stopwatch and print
            Console.WriteLine("Execution time: " + watch.ElapsedMilliseconds);
        }

        static void puzzle1()
        {
            coord = startingPosition;
            move();
            Console.WriteLine(visitedPostions.Count + 1);
        }

        static void puzzle2()
        {
            for (byte y=0; y< matrixArray.GetLength(0); y++)
            {
                for(byte x=0; x< matrixArray.GetLength(1); x++)
                {
                    if (!matrixArray[y, x].Equals('#') && (y != coord[0] || x != coord[1]))
                    {
                        matrixArray[y, x] = '#';

                        //Console.WriteLine("Amended coord: " + y + ", " + x);

                        if (move2())
                        {
                            obstacleCount++;
                        }
                        matrixArray[y, x] = '.';
                        matrixArray[coord[0], coord[1]] = '.';
                        visitedPostions.Clear();
                        coord = startingPosition.ToArray();
                        directionIndex = 0;
                        direction = new sbyte[2] { -1, 0 };
                        matrixArray[coord[0], coord[1]] = '^';
                    }
                }
            }
            Console.WriteLine(obstacleCount);
            //Console.WriteLine("Loops occurred: " + obstacleCount);
        }
        static void move()
        {
            while (true)
            {
                // Check next position
                byte oldY = coord[0];
                byte oldX = coord[1];
                byte nextY = (byte)(coord[0] + direction[0]);
                byte nextX = (byte)(coord[1] + direction[1]);
                try
                {
                    if (!matrixArray[nextY, nextX].Equals('#'))
                    {
                        if (matrixArray[nextY, nextX].Equals('.'))
                        {
                            visitedPostions.Add(((oldY, oldX), directionIndex));
                        }
                        matrixArray[oldY, oldX] = 'X';
                        matrixArray[nextY, nextX] = '^';
                        coord[0] = nextY;
                        coord[1] = nextX;
                    }
                    else
                    {
                        changeDirection();
                    }
                    //return true;
                }
                catch
                {
                    break;
                }

            }
        }

        static bool move2()
        {
            while (true)
            {
                // Check next position
                byte oldY = coord[0];
                byte oldX = coord[1];
                byte nextY = (byte)(coord[0] + direction[0]);
                byte nextX = (byte)(coord[1] + direction[1]);
                try
                {
                    if (!matrixArray[nextY, nextX].Equals('#'))
                    {
                        if (!visitedPostions.Contains(((oldY, oldX), directionIndex)))
                        {
                            //beenThere.Add(new List<int> { oldY, oldX, directionIndex });
                            visitedPostions.Add(((oldY, oldX), directionIndex));
                            matrixArray[oldY, oldX] = '.';
                            matrixArray[nextY, nextX] = '^';
                            coord[0] = nextY;
                            coord[1] = nextX;
                        }
                        else // HIER GAAT HET DUS MIS... HIJ KOMT NOOIT BIJ DE ELSE OMDAT HIJ ZEGT DAT DIE NIET BESTAAT TERWIJL DIE ER WEL IN ZIT...
                        {
                            //Console.WriteLine("Found a loop on " + coord[0] + ", " + coord[1]);
                            return true;
                        }

                        //Console.WriteLine("Walks 1 step");
                    }
                    else
                    {
                        //Console.WriteLine("Turns");
                        changeDirection();
                    }
                    //return true;
                }
                catch
                {
                    visitedPostions.Add(((oldY, oldX), directionIndex));
                    //beenThere.Add(new List<int> { coord[0], coord[1], directionIndex });
                    //Console.WriteLine("Walked out of the field");
                    return false;
                }

            }
        }

        static void getStartingPos()
        {
            direction = new sbyte[2] { -1, 0 };
            directionIndex = 0;
            for (byte y = 0; y < matrixArray.GetLength(0); y++)
            {
                for (byte x = 0; x < matrixArray.GetLength(1); x++)
                {
                    if (matrixArray[y, x].Equals('^'))
                    {
                        startingPosition = new byte[] { y,x };
                        return;
                    }
                }
            }
        }

        static void changeDirection()
        {
            sbyte[,] directions = { { -1, 0 }, { 0, 1 }, { 1, 0 }, { 0, -1 } };
            if (directionIndex < 3)
            {
                directionIndex += 1;
            }
            else
            {
                directionIndex = 0;
            }
            direction[0] = directions[directionIndex, 0];
            direction[1] = directions[directionIndex, 1];
        }

        static void setup()
        {
            string[] lines = File.ReadAllLines("C:\\Users\\Robin\\Documents\\GitHub\\AdventOfCode2024\\6 dec\\input.txt");
            int rows = lines.Length;
            int cols = lines[0].Length;
            matrixArray = new char[rows,cols];
            for (int i = 0; i < rows; i++)
            {
                for(int j = 0; j < cols; j++)
                {
                    matrixArray[i, j] = lines[i][j];
                }
            }
        }
    }
}

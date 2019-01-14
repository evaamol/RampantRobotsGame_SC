using System;
namespace RampantRobots
{
    public class Mechanic
    {
        //Set the automatic properties
        public int x { get; set; }
        public int y { get; set; }

        //Set the constructor
        public Mechanic()
        {
            //Initialise the mechanic in the upperleft corner
            this.x = 1;
            this.y = 1;

        }

        //Method ReplaceMechanic
        public static void ReplaceMechanic(string userInput, Mechanic mechanic, int width, int height)
        {
            //The User Input is given in WASD
            //W: UP
            //S: DOWN
            //A: LEFT
            //D: RIGHT

            //Translate wasd to up down, left or right
            //Define nine unique cases: the corner points, the edges and the interior points
            for (int i = 0;  i < userInput.Length; i++) 
            {
                //When a movement from the corner of edge, mechanic stays on the same point.
                if (mechanic.x == 1)
                {
                    if (mechanic.y == 1)
                    {
                        if (userInput[i].ToString().ToLower() == "d") { mechanic.x = mechanic.x + 1; }
                        if (userInput[i].ToString().ToLower() == "s") { mechanic.y = mechanic.y + 1; }
                        else
                        {
                            mechanic.x = mechanic.x; //a not possible
                            mechanic.y = mechanic.y; //w not possible
                            Console.WriteLine("The corner was reached, use d or s to move away from it!");
                        }
                    }
                    else if (mechanic.y == height)
                    {
                        if (userInput[i].ToString().ToLower() == "d") { mechanic.x = mechanic.x + 1; }
                        if (userInput[i].ToString().ToLower() == "w") { mechanic.y = mechanic.y - 1; }
                        else
                        {
                            mechanic.x = mechanic.x; //a not possible
                            mechanic.y = mechanic.y; //s not possible
                            Console.WriteLine("The corner was reached, use d or w to move away from it!");
                        }
                    }

                    else
                    {
                        if (userInput[i].ToString().ToLower() == "d") { mechanic.x = mechanic.x + 1; }
                        if (userInput[i].ToString().ToLower() == "s") { mechanic.y = mechanic.y + 1; }
                        if (userInput[i].ToString().ToLower() == "w") { mechanic.y = mechanic.y - 1; }
                        else
                        {
                            mechanic.x = mechanic.x;//a not possible
                            Console.WriteLine("The edge was reached, use w,s,d, to move away from it.");
                        }
                    }
                }

                else if (mechanic.x == width)
                {
                    if (mechanic.y == height)
                    {
                        if (userInput[i].ToString().ToLower() == "a") { mechanic.x = mechanic.x - 1; }
                        if (userInput[i].ToString().ToLower() == "w") { mechanic.y = mechanic.y - 1; }
                        else
                        {
                            mechanic.x = mechanic.x;//d not possible
                            mechanic.y = mechanic.y;//s not possible
                            Console.WriteLine("The corner was reached, use a or w to move away from it!");
                        }
                    }
                    else if (mechanic.y == 1)
                    {
                        if (userInput[i].ToString().ToLower() == "a") { mechanic.x = mechanic.x - 1; }
                        if (userInput[i].ToString().ToLower() == "s") { mechanic.y = mechanic.y + 1; }
                        else
                        {
                            mechanic.x = mechanic.x;//d not possible
                            mechanic.y = mechanic.y;//w not possible
                            Console.WriteLine("The corner was reached, use a or s to move away from it!");
                        }
                    }
                    else
                    {
                        if (userInput[i].ToString().ToLower() == "a") { mechanic.x = mechanic.x - 1; }
                        if (userInput[i].ToString().ToLower() == "w") { mechanic.y = mechanic.y - 1; }
                        if (userInput[i].ToString().ToLower() == "s") { mechanic.y = mechanic.y + 1; }
                        else
                        {
                            mechanic.x = mechanic.x;//d not possible
                            mechanic.y = mechanic.y;//s not possible
                            Console.WriteLine("The edge was reached, use a, s or w to move away from it!");
                        }
                    }
                }
                else if (mechanic.y == 1)
                {
                    if (userInput[i].ToString().ToLower() == "a") { mechanic.x = mechanic.x - 1; }
                    if (userInput[i].ToString().ToLower() == "d") { mechanic.x = mechanic.x + 1; }
                    if (userInput[i].ToString().ToLower() == "s") { mechanic.y = mechanic.y + 1; }
                    else
                    {
                        mechanic.x = mechanic.x;//d not possible
                        mechanic.y = mechanic.y;//s not possible
                        Console.WriteLine("The edge was reached, use a, s or d to move away from it!");
                    }
                }
                else if (mechanic.y == height)
                {
                    if (userInput[i].ToString().ToLower() == "a") { mechanic.x = mechanic.x - 1; }
                    if (userInput[i].ToString().ToLower() == "d") { mechanic.x = mechanic.x + 1; }
                    if (userInput[i].ToString().ToLower() == "w") { mechanic.y = mechanic.y + 1; }
                    else
                    {
                        mechanic.x = mechanic.x;//d not possible
                        mechanic.y = mechanic.y;//s not possible
                        Console.WriteLine("The edge was reached, use a, d or w to move away from it!");
                    }

                }

                else
                {
                    //If movement from and to an interior point, wsad is possible
                    if (userInput[i].ToString().ToLower() == "w") { mechanic.y = mechanic.y - 1; }
                    else if (userInput[i].ToString().ToLower() == "s") { mechanic.y = mechanic.y + 1; }
                    else if (userInput[i].ToString().ToLower() == "a") { mechanic.x = mechanic.x - 1; }
                    else if (userInput[i].ToString().ToLower() == "d") { mechanic.x = mechanic.x + 1; }
                    //If enter is hit, the mechanic stays on the same place
                    else if (Console.ReadKey(true).Key == ConsoleKey.Enter)
                    {
                        mechanic.x = mechanic.x;
                        mechanic.y = mechanic.y;

                    }
                    //If input is not enter, or wasd, ask for different input
                    //(Costs a turn!)
                    else
                    {
                        mechanic.x = mechanic.x;
                        mechanic.y = mechanic.y;
                        Console.WriteLine("Input not valid, use w,a,s or d to navigate the factory!");

                    }
                }
            }

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


namespace RampantRobots
{
    class Factory
    {   //Set the automatic properties
        private int Width { get; set; }
        private int Height { get; set; }
        private int NrOfRobots { get; set; }
        private int Turns { get; set; }
        private bool RobotsMove { get; set; }

        //Set the list of robots (Field)
        private List<Robot> robotsPlacement;

        //Set the constructor
        public Factory(int width, int height, int nrRobots, int turns, bool robotsMove)
        {

            this.Width = width;
            this.Height = height;
            this.RobotsMove = robotsMove;
            this.Turns = turns;
            this.NrOfRobots = nrRobots;
          
        }

        //Method InitialiseEmptyFactoryGrid 
        public string InitialiseEmptyFactoryGrid()
        {
            //This method initialises the empty factory, based on Width and Height
            string factoryEmpty = null;
            for (int i = 0; i < Height; i++)
            {   
                StringBuilder partialString = new StringBuilder(Width);
                partialString.Insert(0, ".", Width);// fill each place with a .
                partialString.Append(Environment.NewLine);
                factoryEmpty += partialString.ToString(); //Add each line to factory, done i times
            }

            return factoryEmpty;

        }

        //Method ShowMovementFactory
        public string ShowMovementFactory(string factoryGridInitial, List<Robot> robotsPlacement, Mechanic mechanic)
        {
            //This method shows the Mechanic and Robots on the factory grid.
            StringBuilder factoryRM = new StringBuilder(factoryGridInitial);//Use empty grid, to place the mechanic and robots

            //Place the robots
            for (int r = 0; r < robotsPlacement.Count; r++)
            {
                Robot placeRobot = robotsPlacement[r];

                int positionx = placeRobot.x;
                int positiony = placeRobot.y;
                factoryRM[(positionx - 1) + (Width + 1) * (positiony - 1)] = 'R';
            }

            //Place Mechanic
            factoryRM[(mechanic.x - 1) + (Width + 1) * (mechanic.y - 1)] = 'M';

            return factoryRM.ToString();
        }

        //Method WhenMechanicAndRobotsTouch
        public void WhenMechanicAndRobotsTouch(Mechanic mechanic, List<Robot> robotsPlacement)
        {
            //This method removes the robot from the list when the mechanic is on the same place.
            for(int i = 0; i < robotsPlacement.Count; i++)
            {
                //On the same place when the x- and y-coordinates are the same
                if((mechanic.x == robotsPlacement[i].x) & (mechanic.y == robotsPlacement[i].y))
                {
                    //Remove the robot at index i
                    robotsPlacement.RemoveAt(i);
                    Console.WriteLine("Yay! A robot was lubricated and it left the factory.");
                    if(robotsPlacement.Count != 0)
                    {
                        Console.WriteLine("Still got " + robotsPlacement.Count + " to go!");
                    }

                }
            }
        }

        //Method GameRulesExplanation
        public void GameRulesExplanation()
        {
            //This method shows the rules of the game
            Console.WriteLine("----------------------------------------------------------------------------");
            Console.WriteLine("Hello Gamer,\nThis game is called Rampant Robots\nThe intention of this game is to move the mechanic (M)\ntrough the factory, so the robots (R) will be lubricated.");
            Console.WriteLine("A robot is lubricated when the robot and mechanic are positioned\non the same place");
            Console.WriteLine("When a robot is lubricated, it will leave the factory.\nYour goal is to lubricate all the robots, in a maximum of " + Turns + " turns.");
            Console.WriteLine("Navigate the factory with the controlbuttons:\nw (up), a (left), s (down) and d (right).");
            Console.WriteLine("Happy Hunting :)!");
            Console.WriteLine("P.S.: The robots move in a random order. Each robot can make a step\nin the x- or y-direction, for each step you take.");
            Console.WriteLine("P.P.S.: Written by: Eva Mol");
            Console.WriteLine("----------------------------------------------------------------------------");
        }

        //Method Run
        public void Run()
        {
            //In this method, all the methods from factory, robot and mechanic are used to run the application
            GameRulesExplanation();
            //Initialise the mechanic, and the list of robots
            Mechanic mechanic = new Mechanic();
            robotsPlacement = Robot.PlaceRobots(Width, Height, NrOfRobots);
            List<Robot> robotListNew = null;
            string factoryEmpty = InitialiseEmptyFactoryGrid(); //Make the empty factory
            string factoryGrid = ShowMovementFactory(factoryEmpty, robotsPlacement, mechanic); //Make the initial factory
            Console.WriteLine(factoryGrid); //Show in console
            int initialTurn = Turns; //Keep track of initial turns

            do //while (Turns > 0 & robotsPlacement.Count > 0)
            {
                //Show turns and get movement mechanic
                string NumberOfTurns = Turns.ToString();
                Console.WriteLine("Number of turns:" + NumberOfTurns);
                Console.Write("Give movement of the mechanic (w,a,s,d) ... ");
                string inputDirection = Console.ReadLine();

                //Replace the mechanic according to movement
                Mechanic.ReplaceMechanic(inputDirection, mechanic, Width, Height);

                //Als de boolean RobotsMove == true, dan bewegen de robots
                if (RobotsMove)
                {
                    for(int m = 0; m < inputDirection.Length; m++)
                    {
                        robotListNew = Robot.MoveAllRobots(NrOfRobots, robotsPlacement, Width, Height);
                    }

                }

                //Overwrite robotsPlacement with the new coordinates for the robots
                robotsPlacement = robotListNew;
                //If the mechanic and robot are on the same coordinate, remove the robot
                WhenMechanicAndRobotsTouch(mechanic, robotsPlacement);
                //Show the new grid
                string updatedGrid = ShowMovementFactory(factoryEmpty, robotsPlacement, mechanic);
                Console.WriteLine(updatedGrid);
                //Decrease turns with one
                Turns--;



            } while (Turns > 0 & robotsPlacement.Count > 0);

            //Messages for when the game is won or lost.
            if (Turns > 0 & robotsPlacement.Count == 0)
            {
                Console.WriteLine("Congratulations, you won the game in " + (initialTurn - Turns) + " turns");
                Console.ReadLine();
            }
            else if (Turns == 0 & robotsPlacement.Count > 0)
            {
                Console.WriteLine("Unfortunately, you lost the game after " + (initialTurn) + " turns");
                Console.ReadLine();
            }

        }

    }
}

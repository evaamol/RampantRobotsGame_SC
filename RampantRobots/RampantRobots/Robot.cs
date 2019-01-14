using System;
using System.Collections.Generic;

namespace RampantRobots
{
    public class Robot
    {
        //Set the automatic properties
        public int x { get; set; }
        public int y { get; set; }


        //Set the constructor
        public Robot(int width, int height)
        {
            Random getPlace = new Random();

            this.x = getPlace.Next(1, width);
            this.y = getPlace.Next(1, height);
        }


        //Method PlaceRobots
        public static List<Robot> PlaceRobots(int width, int height, int nrRobots)
        {
            //This method is used for the initialisation of the robots
            List<Robot> robotList = new List<Robot>(nrRobots);


            for (int i = 0; i < nrRobots; i++)
            {
                //Get random location Robot
                Robot robot = new Robot(width, height);

                if (i >= 1)
                {
                    if (CheckDuplicates(robotList, robot))
                    {
                        i--; //When robot already in the list, create a new robot (previous i)

                    }
                    else if (robot.x == 1 & robot.y == 1) //Place of the mechanic after initialisation
                    {
                        i--; //One step back (in i) when the robot is initialised on the mechanic's place
                    }
                    else
                    {
                        robotList.Add(robot); //Add a robot when it's not a duplicate or initialised on the mechanic's place
                    }
                }
                else
                {
                    if (robotList.Count == 0)
                    {
                        if (robot.x == 1 & robot.y == 1) //Place of the mechanic after initialisation
                        {
                            //When i = 0, and the list is not filled with minimal 1 entry,
                            //The robot's place is slightly changed 
                            robot.x = robot.x + 1;
                            robot.y = robot.y + 1;

                        }

                        robotList.Add(robot); //Add only at i = 0, when there are no entries in the list
                    }

                }

            }

            return robotList;


        }

        //Method ReplaceRobot
        public static void ReplaceRobot(Robot robot, int width, int height)
        {
            //Initialise a randomizer for random changes in the x- and y-coordinates
            Random newPlace = new Random();

            //Save the current x- and y coordinate
            int xPosition = robot.x;
            int yPosition = robot.y;

            //Save a cloned version of the robot, because changes can be permanent if not.
            Robot robotCase = (Robot)robot.MemberwiseClone();

            //Get a random integer between 1 and 5 (5 tops) to determine the direction of x and y
            int direction = newPlace.Next(1, 5);

            //Switch per case of the direction
            switch (direction)
            {
                case 1:
                    xPosition = (xPosition) - 1;
                    if (xPosition <= 0 | xPosition >= width - 1)
                    {
                        xPosition = robotCase.x;
                    }
                    break;
                case 2:
                    xPosition = (xPosition) + 1;
                    if (xPosition <= 0 | xPosition >= width - 1)
                    {
                        xPosition = robotCase.x;
                    }
                    break;

                case 3:
                    yPosition = (yPosition) - 1;
                    if (yPosition <= 0 | yPosition >= height - 1)
                    {
                        yPosition = robotCase.y;
                    }
                    break;
                case 4:
                    yPosition = (yPosition) + 1;
                    if (yPosition <= 0 | yPosition >= height - 1)
                    {
                        yPosition = robotCase.y;
                    }
                    break;

            }

            //Set the new x- and y-coordinates to the determined coordinates
            robot.x = xPosition;
            robot.y = yPosition;
            

        }


        //Method CheckDuplicates
        public static bool CheckDuplicates(List<Robot> robotList, Robot robotToCheck)
        {
            //Set the boolean to false als initialisation
            bool duplicatesInList = false;

            //Check for duplicates between a robot and a list of robots.
            for (int i = 0; i < robotList.Count; i++)
            {
                if ((robotToCheck.x == robotList[i].x) & (robotToCheck.y == robotList[i].y))
                {
                    //When a duplicate, return the boolean true
                    duplicatesInList = true;
                    return duplicatesInList;
                }
            }

            //Return also when false
            return duplicatesInList;
        }

        // Method MoveAllRobots
        public static List<Robot> MoveAllRobots(int nrRobotsMoving, List<Robot> robotsPlacement, int width, int height)
        {
            //Initialise a robotlist
            List<Robot> robotListNew = new List<Robot>(nrRobotsMoving);

            //Replace the robots, according to the new placeement
            for (int i = 0; i < robotsPlacement.Count; i++)
            {

                //Method used to replace one robot
                ReplaceRobot(robotsPlacement[i], width, height);

                if (i >= 1)
                {
                    //Check if the new robot is in the new list with robots
                    if (CheckDuplicates(robotListNew, robotsPlacement[i]))
                    {
                        //If so, et i one step back
                        i--;
                    }
                    else
                    {
                        //Else, add the robot to the new list
                        robotListNew.Add(robotsPlacement[i]);
                    }
                }
                else
                {
                    if (robotListNew.Count == 0)
                    {
                        //Add when there is nothing in the new list.
                        robotListNew.Add(robotsPlacement[i]);
                    }
                }
            }

            return robotListNew;
        }
    }
}

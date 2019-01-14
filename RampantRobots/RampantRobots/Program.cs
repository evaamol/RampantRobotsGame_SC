using System;
using System.Collections.Generic;

namespace RampantRobots
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            //Make the factory
            Factory factory = new Factory(16, 5, 3, 10, true);

            //Run the application
            factory.Run();
        }
    }
}

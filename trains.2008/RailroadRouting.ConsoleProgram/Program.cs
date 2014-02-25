using System;

namespace RailroadRouting.ConsoleProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            bool exit;
            string input;

            Console.WriteLine("Please enter a command (help for list): ");
            do
            {
                Console.Write("> ");
                input = Console.ReadLine();

                ExecuteCommand(input, out exit);
            }
            while (!exit);

        }

        #region Methods

        /// <summary>
        /// Execute user input
        /// </summary>
        /// <param name="input">user inoput</param>
        /// <param name="exit">true: program will exit, false: program will continue</param>
        private static void ExecuteCommand(string input, out bool exit)
        {
            string[] command = input.Split(' ');


            switch (command[0])
            {
                case "help":
                    PrintHelp();
                    break;
                case "e":
                case "excersize":
                    RunExcersize(command.Length > 1 && command[1].Equals("v"));
                    break;
                case "exit":
                    exit = true;
                    return;
                default:
                    PrintInvalidInput();
                    break;
            }
            exit = false;
        }

        /// <summary>
        /// Run the excersize
        /// </summary>
        /// <param name="verbose">Verbose mode on (true) or off (false)</param>
        private static void RunExcersize(bool verbose)
        {
            Console.WriteLine(string.Empty);

            QuestionOutput iqo = new QuestionOutput(verbose);

            //-----------------------------------------------------------------
            //1.  The distance of the route A-B-C.
            //-----------------------------------------------------------------
            iqo.CalcDistance(1, "A", "B", "C");

            //-----------------------------------------------------------------
            //2.  The distance of the route A-D.
            //-----------------------------------------------------------------
            iqo.CalcDistance(2, "A", "D");

            //-----------------------------------------------------------------
            //3.  The distance of the route A-D-C.
            //-----------------------------------------------------------------
            iqo.CalcDistance(3, "A", "D", "C");

            //-----------------------------------------------------------------
            //4.  The distance of the route A-E-B-C-D.
            //-----------------------------------------------------------------
            iqo.CalcDistance(4, "A", "E", "B", "C", "D");

            //-----------------------------------------------------------------
            //5.  The distance of the route A-E-D.
            //-----------------------------------------------------------------
            iqo.CalcDistance(5, "A", "E", "D");

            //-----------------------------------------------------------------
            //6.  The number of trips starting at C and ending at C with a 
            //    maximum of 3 stops. In the sample data below, there are two 
            //    such trips: C-D-C (2 stops). and C-E-B-C (3 stops).
            //-----------------------------------------------------------------
            iqo.FindPathsByDepthLimit(6, "C", "C", 4, false);

            //-----------------------------------------------------------------
            //7.  The number of trips starting at A and ending at C with exactly
            //    4 stops. In the sample data below, there are three such trips:
            //    A to C (via B,C,D); A to C (via D,C,D); and A to C (via D,E,B).
            //-----------------------------------------------------------------
            iqo.FindPathsByDepthLimit(7, "A", "C", 5, true);

            //-----------------------------------------------------------------
            //8.  The length of the shortest route (in terms of distance to 
            //    travel) from A to C.
            //-----------------------------------------------------------------
            iqo.FindShortestPath(8, "A", "C");
            iqo.FindShortestPathWithEdges(8, "A", "C");

            //-----------------------------------------------------------------
            //9.  The length of the shortest route (in terms of distance to 
            //    travel) from B to B.
            //-----------------------------------------------------------------
            iqo.FindShortestPath(9, "B", "B");
            iqo.FindShortestPathWithEdges(9, "B", "B");

            //-----------------------------------------------------------------
            //10. The number of different routes from C to C with a distance of
            //    less than 30. In the sample data, the trips are: CDC, CEBC, 
            //    CEBCDC, CDCEBC, CDEBC, CEBCEBC, CEBCEBCEBC.
            //-----------------------------------------------------------------
            iqo.FindPathsByWeightLimit(10, "C", "C", 30);
        }

        /// <summary>
        /// Print invalid input error message.
        /// </summary>
        private static void PrintInvalidInput()
        {
            Console.WriteLine(string.Empty);
            Console.WriteLine("Invalid input.");
            Console.WriteLine(string.Empty);
        }

        /// <summary>
        /// Print help list
        /// </summary>
        private static void PrintHelp()
        {
            Console.WriteLine(string.Empty);
            Console.WriteLine("help          : command list");
            Console.WriteLine("exit          : exit program");
            Console.WriteLine("excersize [v] : outputs of excersize");
            Console.WriteLine("                v = verbose");
            Console.WriteLine(string.Empty);
        }

        #endregion
    }


}


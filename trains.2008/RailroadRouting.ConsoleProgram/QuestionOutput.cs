using System.Collections.Generic;
using System.Collections.ObjectModel;
using Common.DataStructures.Graph;

namespace RailroadRouting.ConsoleProgram
{
    internal class QuestionOutput
    {
        #region Field
        Graph<string> graph;
        bool verbose;
        #endregion

        #region Properties
        public Graph<string> Graph
        {
            get { return graph; }
            set { graph = value; }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="verbose">Indicator to switch on verbose mode</param>
        internal QuestionOutput(bool verbose)
        {
            this.verbose = verbose;
            graph = new Graph<string>();

            graph.AddNode("A");
            graph.AddNode("B");
            graph.AddNode("C");
            graph.AddNode("D");
            graph.AddNode("E");

            graph.AddEdge("A", "B", 5);
            graph.AddEdge("B", "C", 4);
            graph.AddEdge("C", "D", 8);
            graph.AddEdge("D", "C", 8);
            graph.AddEdge("D", "E", 6);
            graph.AddEdge("A", "D", 5);
            graph.AddEdge("C", "E", 2);
            graph.AddEdge("E", "B", 3);
            graph.AddEdge("A", "E", 7);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Calculate the distance of a specific path
        /// </summary>
        /// <param name="questionNo">coding problem question number</param>
        /// <param name="nodes">Path to calculate</param>
        internal void CalcDistance(int questionNo, params string[] nodes)
        {
            uint totalDistance = 0;
            uint[] weights = new uint[nodes.Length];
            bool noRoute = false;

            GraphNode<string> from, to;

            for (int i = 0; i < nodes.Length - 1; i++)
            {
                from = graph[nodes[i]];
                to = graph[nodes[i + 1]];

                if (from.Edges.Contains(to))
                {
                    weights[i] = from.Edges[to].Weight;
                    totalDistance += weights[i];
                }
                else
                    noRoute = true;
            }
            if (noRoute)
                System.Console.WriteLine(string.Format("  Output #{0}: NO SUCH ROUTE", questionNo));
            else
                System.Console.WriteLine(string.Format("  Output #{0}: {1}", questionNo, totalDistance));

            if (verbose)
            {
                for (int i = 0; i < nodes.Length - 1; i++)
                {
                    from = graph[nodes[i]];
                    to = graph[nodes[i + 1]];

                    if (from.Edges.Contains(to))
                    {
                        System.Console.WriteLine(string.Format("             {0} to {1} (Distance = {2})", from.Value, to.Value, from.Edges[to].Weight));
                    }
                    else
                        System.Console.WriteLine(string.Format("             {0} to {1} (NO ROUTE)", from.Value, to.Value));
                }
                System.Console.WriteLine(string.Empty);
            }
        }

        /// <summary>
        /// Find and ouput all paths between two nodes with a specific depth limit
        /// </summary>
        /// <param name="questionNo">coding problem question number</param>
        /// <param name="nodes">Path to calculate</param>
        /// <param name="from">Starting point</param>
        /// <param name="to">Destination</param>
        /// <param name="depth">Depth to which the search will be limited</param>
        /// <param name="exact">If true only match at the specified depth will be returned.
        /// If false all matches inlcude those at the specified depth are returned.</param>
        internal void FindPathsByDepthLimit(int questionNo, string from, string to, int maxDepth, bool exact)
        {
            ReadOnlyCollection<GraphNode<string>[]> paths = graph.FindPathsByDepthLimit(from, to, maxDepth, exact);

            System.Console.WriteLine(string.Format("  Output #{0}: {1} paths found.", questionNo, paths.Count));

            if (verbose)
            {
                foreach (GraphNode<string>[] path in paths)
                {
                    System.Console.Write("             Path: ");

                    foreach (GraphNode<string> node in path)
                        System.Console.Write(string.Format(" {0} ", node.Value));

                    System.Console.WriteLine(string.Empty);
                }
                System.Console.WriteLine(string.Empty);
            }

        }

        /// <summary>
        /// Find and ouput all paths between two nodes with a specific weight limit
        /// </summary>
        /// <param name="questionNo">coding problem question number</param>
        /// <param name="nodes">Path to calculate</param>
        /// <param name="from">Starting point</param>
        /// <param name="to">Destination</param>
        /// <param name="maxWeight">Total edge Weight to which the search will be limited</param>
        internal void FindPathsByWeightLimit(int questionNo, string from, string to, uint maxWeight)
        {
            ReadOnlyCollection<GraphNode<string>[]> paths = graph.FindPathsByWeightLimit(from, to, maxWeight);

            System.Console.WriteLine(string.Format("  Output #{0}: {1} paths found.", questionNo, paths.Count));

            foreach (GraphNode<string>[] path in paths)
            {
                System.Console.Write("             Path: ");

                foreach (GraphNode<string> node in path)
                    System.Console.Write(string.Format("{0} ", node.Value));

                System.Console.WriteLine(string.Empty);
            }

        }

        /// <summary>
        /// Find and ouput the shortest paths between two nodes
        /// </summary>
        /// <param name="questionNo">coding problem question number</param>
        /// <param name="nodes">Path to calculate</param>
        /// <param name="from">Starting point</param>
        /// <param name="to">Destination</param>
        internal void FindShortestPath(int questionNo, string from, string to)
        {
            uint totalDistance;
            Stack<string> path = graph.FindShortestPath(from, to, out totalDistance);

            System.Console.WriteLine(string.Format("  Output #{0}: {1} is the shortest path potentially excluding edges.", questionNo, totalDistance));

            if (verbose)
            {
                string current;
                string next;

                current = path.Pop();

                while (path.Count > 0)
                {
                    next = path.Pop();
                    System.Console.WriteLine(string.Format("             {0} to {1} (Distance = {2})", current, next, graph[current].Edges[graph[next]].Weight));
                    current = next;
                }
                System.Console.WriteLine(string.Empty);
            }
        }

        /// <summary>
        /// Find and ouput the shortest paths (with edges in the path) between two nodes
        /// </summary>
        /// <param name="questionNo">coding problem question number</param>
        /// <param name="nodes">Path to calculate</param>
        /// <param name="from">Starting point</param>
        /// <param name="to">Destination</param>
        internal void FindShortestPathWithEdges(int questionNo, string from, string to)
        {
            uint totalDistance;
            Stack<string> path = graph.FindShortestPathWithEdges(from, to, out totalDistance);

            System.Console.WriteLine(string.Format("  Output #{0}: {1} is the shortest path including edges.", questionNo, totalDistance));

            if (verbose)
            {
                string current;
                string next;

                current = path.Pop();

                while (path.Count > 0)
                {
                    next = path.Pop();
                    System.Console.WriteLine(string.Format("             {0} to {1} (Distance = {2})", current, next, graph[current].Edges[graph[next]].Weight));
                    current = next;
                }
                System.Console.WriteLine(string.Empty);
            }
        }
        #endregion
    }
}

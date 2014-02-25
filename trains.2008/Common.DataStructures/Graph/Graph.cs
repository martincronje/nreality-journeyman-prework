using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Common.DataStructures.Graph
{
    /// <summary>
    /// Representation of a Directed Weighed Graph. Nodes and edges are
    /// structured using Adjacency Lists.
    /// </summary>
    /// <typeparam name="T">Type used for storage in the graph</typeparam>
    public class Graph<T> : IEnumerable<T>
    {
        #region Fields
        List<GraphNode<T>> nodes;
        #endregion

        #region Properties

        /// <summary>
        /// Gets the number of elements actually contained in the Graph. 
        /// </summary>
        public int Count
        {
            get { return nodes.Count; }
        }
        /// <summary>
        /// Gets or sets the total number of elements the internal data structure can hold without resizing.  
        /// </summary>
        public int Capacity
        {
            get { return nodes.Capacity; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Graph class that is empty and 
        /// has the default initial capacity.
        /// </summary>
        public Graph() : this(0) { }

        /// <summary>
        /// Initializes a new instance of the Graph class that is empty and 
        /// has the specified initial capacity. 
        /// </summary>
        /// <param name="capacity">The number of elements that the new list 
        /// can initially store.</param>
        public Graph(int capacity)
        {
            this.nodes = new List<GraphNode<T>>(capacity);
        }
        #endregion

        #region Methods

        #region Add

        /// <summary>
        /// Add a GraphNode to this Graph.
        /// </summary>
        /// <param name="t">Node that will be added</param>
        /// 
        public void AddNode(T t)
        {
            if (!this.Contains(t))
                this.nodes.Add(new GraphNode<T>(t));
        }

        /// <summary>
        /// Add a GraphNode to this Graph.
        /// </summary>
        /// <param name="node">GraphNode that will be added</param>
        public void AddNode(GraphNode<T> node)
        {
            if (!this.Contains(node))
                this.nodes.Add(node);
        }

        /// <summary>
        /// Add a GraphEdge to this Graph.
        /// </summary>
        /// <param name="from">node which is the starting point of GraphEdge</param>
        /// <param name="to">node which is the end point of GraphEdge</param>
        /// <param name="weight">Weight of the GraphEdge</param>
        public void AddEdge(T from, T to, uint weight)
        {
            AddEdge(this[from], this[to], weight);
        }

        /// <summary>
        /// Add a GraphEdge to this Graph.
        /// </summary>
        /// <param name="from">GraphNode which is the starting point of GraphEdge</param>
        /// <param name="to">GraphNode which is the end point of GraphEdge</param>
        /// <param name="weight">Weight of the GraphEdge</param>
        public void AddEdge(GraphNode<T> from, GraphNode<T> to, uint weight)
        {
            from.Edges.Add(to, weight);
        }
        #endregion

        #region Remove

        /// <summary>
        /// Remove a GraphNode from this Graph.
        /// </summary>
        /// <param name="t">The node that will be removed</param>
        public void RemoveNode(T t)
        {
            RemoveNode(this[t]);
        }

        /// <summary>
        /// Remove a GraphNode from this Graph.
        /// </summary>
        /// <param name="GraphNode">The GraphNode that will be removed</param>
        public void RemoveNode(GraphNode<T> node)
        {
            foreach (GraphNode<T> otherNodes in nodes)
                otherNodes.Edges.Remove(node);

            this.nodes.Remove(node);
        }

        /// <summary>
        /// Remove a GraphEdge from this Graph.
        /// </summary>
        /// <param name="from">Node which is the starting point of GraphEdge</param>
        /// <param name="to">Node which is the end point of GraphEdge</param>
        public void RemoveEdge(T from, T to)
        {
            RemoveEdge(this[from], this[to]);
        }

        /// <summary>
        /// Remove a GraphEdge from this Graph.
        /// </summary>
        /// <param name="from">GraphNode which is the starting point of GraphEdge</param>
        /// <param name="to">GraphNode which is the end point of GraphEdge</param>
        public void RemoveEdge(GraphNode<T> from, GraphNode<T> to)
        {
            from.Edges.Remove(to);
        }

        #endregion

        #region Contains

        /// <summary>
        /// Determines whether an element is in the Graph. 
        /// </summary>
        /// <param name="node">The GraphNode to locate in the Graph. </param>
        /// <returns>true if item is found in the List; otherwise, false</returns>
        public bool Contains(GraphNode<T> node)
        {
            return nodes.Contains(node);
        }

        /// <summary>
        /// Determines whether an element is in the Graph. 
        /// </summary>
        /// <param name="t">The node to locate in the Graph. </param>
        /// <returns>true if item is found in the List; otherwise, false</returns>
        public bool Contains(T t)
        {
            return this[t] != null;
        }

        #endregion

        #region Indexer

        /// <summary>
        /// Indexer to give the iteration of the Graph matrix like quailities 
        /// when used in context of the nodes and their edges.
        /// </summary>
        /// <param name="node">GraphNode that will be found</param>
        /// <returns>GraphEdge representing an type searched</returns>
        public GraphNode<T> this[T t]
        {
            get { return this.nodes.Find(new GraphNodeMatch<T>(t).Match); }
        }

        #endregion

        #region IEnumerable

        /// <summary>
        /// Returns an enumerator that iterates through a collection. 
        /// </summary>
        /// <returns>An IEnumerator object that can be used to iterate through
        /// the collection.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection. 
        /// </summary>
        /// <returns>An IEnumerator object that can be used to iterate through
        /// the collection.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            foreach (GraphNode<T> node in nodes)
                yield return node.Value;
        }

        #endregion

        #region FindShortestPath...

        #region FindShortestPath

        /// <summary>
        /// Find the shorted path within the graph from one point to another.
        /// This method makes used of Dijkstra's algorithm to find the path.
        /// </summary>
        /// <param name="from">Node representing the starting point in the Graph</param>
        /// <param name="to">Node representing the end point in the Graph</param>
        /// <param name="totalWeight">The total weight of the path returned</param>
        /// <returns>The shortest path between two points.</returns>
        public Stack<T> FindShortestPath(T from, T to, out uint totalWeight)
        {
            return FindShortestPath(this[from], this[to], out totalWeight);
        }

        /// <summary>
        /// Find the shorted path within the graph from one point to another.
        /// This method makes used of Dijkstra's algorithm to find the path.
        /// </summary>
        /// <param name="from">GraphNode representing the starting point in the Graph</param>
        /// <param name="to">GraphNode representing the end point in the Graph</param>
        /// <param name="totalWeight">The total weight of the path returned</param>
        /// <returns>The shortest path between two points.</returns>
        public Stack<T> FindShortestPath(GraphNode<T> from, GraphNode<T> to, out uint totalWeight)
        {
            Dictionary<GraphNode<T>, uint?> weightList = new Dictionary<GraphNode<T>, uint?>();
            Dictionary<GraphNode<T>, GraphNode<T>> pathList = new Dictionary<GraphNode<T>, GraphNode<T>>();
            List<GraphNode<T>> q = new List<GraphNode<T>>();
            GraphNode<T> u;

            //-----------------------------------------------------------------
            // Initializations
            //-----------------------------------------------------------------
            Dijkstra_Initialize(weightList, pathList, q);

            //-----------------------------------------------------------------
            // Distance from source to source
            //-----------------------------------------------------------------
            weightList[from] = 0;

            //-----------------------------------------------------------------
            // The main loop
            //-----------------------------------------------------------------
            while (q.Count > 0)
            {
                //-------------------------------------------------------------
                // Remove and return best vertex from nodes in two given nodes 
                // we would use a path finding algorithm on the new graph, such
                // as depth-first search.
                //-------------------------------------------------------------
                u = Dijkstra_GetMinWeight(weightList, q);

                q.Remove(u);

                //-------------------------------------------------------------
                // Where v has not yet been removed from Q.
                //-------------------------------------------------------------
                Dijkstra_OptimizeEdges(weightList, pathList, q, u);
            }

            //-----------------------------------------------------------------
            // Set total weight of shortest path
            //-----------------------------------------------------------------
            totalWeight = weightList[to].GetValueOrDefault();

            //-----------------------------------------------------------------
            // Read the shortest path from source to target by iteration
            //-----------------------------------------------------------------
            Stack<T> path = new Stack<T>();
            GraphNode<T> current = to;

            path.Push(current.Value);

            while (!from.Equals(current))
            {
                current = pathList[current];
                path.Push(current.Value);
            }
            return path;
        }
        #endregion

        #region FindShortestPathWithEdges

        /// <summary>
        /// Find the shorted path within the graph from one point to another.
        /// This method makes used of Dijkstra's algorithm to find the path. 
        /// This alternative to the FindShortestPath method contains a modified
        /// version of Dijkstra's algorothm that will ensure edges are returned 
        /// even when calulating the shorted distance between the same point
        /// (e.g. B to B in A,B,C )
        /// </summary>
        /// <param name="from">Node representing the starting point in the Graph</param>
        /// <param name="to">Node representing the end point in the Graph</param>
        /// <param name="totalWeight">The total weight of the path returned</param>
        /// <returns>The shortest path between two points.</returns>
        public Stack<T> FindShortestPathWithEdges(T from, T to, out uint totalWeight)
        {
            return FindShortestPathWithEdges(this[from], this[to], out totalWeight);
        }

        /// <summary>
        /// Find the shorted path within the graph from one point to another.
        /// This method makes used of Dijkstra's algorithm to find the path. 
        /// This alternative to the FindShortestPath method contains a modified
        /// version of Dijkstra's algorothm that will ensure edges are returned 
        /// even when calulating the shorted distance between the same point
        /// (e.g. B to B in A,B,C )
        /// </summary>
        /// <param name="from">GraphNode representing the starting point in the Graph</param>
        /// <param name="to">GraphNode representing the end point in the Graph</param>
        /// <param name="totalWeight">The total weight of the path returned</param>
        /// <returns>The shortest path between two points.</returns>
        public Stack<T> FindShortestPathWithEdges(GraphNode<T> from, GraphNode<T> to, out uint totalWeight)
        {
            Dictionary<GraphNode<T>, uint?> weightList = new Dictionary<GraphNode<T>, uint?>();
            Dictionary<GraphNode<T>, GraphNode<T>> pathList = new Dictionary<GraphNode<T>, GraphNode<T>>();
            List<GraphNode<T>> q = new List<GraphNode<T>>();
            GraphNode<T> u;
            bool selfPathHandled = false;

            //-----------------------------------------------------------------
            // Initializations
            //-----------------------------------------------------------------
            Dijkstra_Initialize(weightList, pathList, q);

            //-----------------------------------------------------------------
            // Distance from source to source
            //-----------------------------------------------------------------
            weightList[from] = 0;

            //-----------------------------------------------------------------
            // The main loop
            //-----------------------------------------------------------------
            while (q.Count > 0)
            {
                //-------------------------------------------------------------
                // Remove and return best vertex from nodes in two given nodes 
                // we would use a path finding algorithm on the new graph, such
                // as depth-first search.
                //-------------------------------------------------------------
                u = Dijkstra_GetMinWeight(weightList, q);

                q.Remove(u);

                //-------------------------------------------------------------
                // Where v has not yet been removed from Q.
                //-------------------------------------------------------------
                Dijkstra_OptimizeEdges(weightList, pathList, q, u);

                //-------------------------------------------------------------
                // Modifcation to Dijkstra's algorithm. This check allows the
                // algorithm to calculate the shortest path from v1 to v1
                // while still returning a path that is not direct to itself.
                //-------------------------------------------------------------
                if (to.Equals(from) && !selfPathHandled)
                {
                    selfPathHandled = true;
                    q.Add(to);
                    weightList[to] = null;
                }
            }

            //-----------------------------------------------------------------
            // Set total weight of shortest path
            //-----------------------------------------------------------------
            totalWeight = weightList[to].GetValueOrDefault();

            //-----------------------------------------------------------------
            // Read the shortest path from source to target by iteration
            //-----------------------------------------------------------------
            Stack<T> path = new Stack<T>();
            GraphNode<T> current = to;

            path.Push(current.Value);

            do
            {
                current = pathList[current];
                path.Push(current.Value);
            }
            while (pathList[current] != null && !path.Contains(pathList[current].Value));

            //-------------------------------------------------------------
            // Modifcation to Dijkstra's algorithm. This check allows the
            // algorithm to calculate the shortest path from v1 to v1
            // while still returning a path that is not direct to itself.
            //-------------------------------------------------------------
            if (to.Equals(from)) path.Push(to.Value);

            return path;
        }
        #endregion

        #region Dijkstra_GetMinWeight
        /// <summary>
        /// Get the remaining items with the lowest weight in q
        /// </summary>
        /// <param name="weightList">Used to dertmine lowest weight</param>
        /// <param name="q">q</param>
        /// <returns>GraphNode with lowest weight</returns>
        private GraphNode<T> Dijkstra_GetMinWeight(Dictionary<GraphNode<T>, uint?> weightList, List<GraphNode<T>> q)
        {
            GraphNode<T> minNode = null;
            uint? minWeight = null;

            foreach (GraphNode<T> node in q)
            {
                if (minWeight == null || minWeight > weightList[node])
                {
                    minNode = node;
                    minWeight = weightList[node];
                }

            }
            return minNode;
        }
        #endregion

        #region Dijkstra_Initialize
        /// <summary>
        /// Initialize the lists used to maintain the progress of the algorithm
        /// </summary>
        /// <param name="weightList">Used to dertmine lowest weight</param>
        /// <param name="pathList">Used to dertmine current path between two nodes</param>
        /// <param name="q">q</param>
        private void Dijkstra_Initialize(Dictionary<GraphNode<T>, uint?> weightList
                                        , Dictionary<GraphNode<T>, GraphNode<T>> pathList
                                        , List<GraphNode<T>> q)
        {
            foreach (GraphNode<T> node in nodes)
            {
                weightList.Add(node, null); // Unknown distance function from source to v
                pathList.Add(node, null); // Init paths used for optimization
                q.Add(node); // All nodes in the graph are unoptimized - thus are in Q
            }
        }
        #endregion

        #region Dijkstra_OptimizeEdges
        /// <summary>
        /// Find the optimimum u and its neighbours.
        /// </summary>
        /// <param name="weightList">Used to dertmine lowest weight</param>
        /// <param name="pathList">Used to dertmine current path between two nodes</param>
        /// <param name="q">q</param>
        /// <param name="u">u</param>
        private void Dijkstra_OptimizeEdges(Dictionary<GraphNode<T>, uint?> weightList
                                           , Dictionary<GraphNode<T>, GraphNode<T>> pathList
                                           , List<GraphNode<T>> q
                                           , GraphNode<T> u)
        {
            foreach (GraphEdge<T> v in u.Edges)
            {
                if (q.Contains(v.NodeTo))
                {
                    //-----------------------------------------------------
                    // Relax (u,v)
                    //-----------------------------------------------------
                    uint? weightV = weightList[v.NodeTo];
                    uint? weightU = weightList[u].Value;

                    if (weightV > weightU + v.Weight || weightV == null)
                    {
                        weightList[v.NodeTo] = weightU + v.Weight;
                        pathList[v.NodeTo] = u;
                    }
                }
                Dijkstra_DumpProgress(weightList, pathList, u, v);
            }
        }
        #endregion

        #region Dijkstra_DumpProgress
        private void Dijkstra_DumpProgress(
             Dictionary<GraphNode<T>, uint?> weightList
            , Dictionary<GraphNode<T>, GraphNode<T>> pathList
            , GraphNode<T> u
            , GraphEdge<T> v)
        {
            Debug.WriteLine("--------------------------------------");
            Debug.WriteLine(string.Format(" u: {0}", u.Value));
            Debug.WriteLine(string.Format(" v: {0}", v.NodeTo.Value));
            Debug.WriteLine("--------------------------------------");
            Debug.WriteLine(" Weight / Path List");
            Debug.WriteLine("--------------------------------------");
            foreach (KeyValuePair<GraphNode<T>, uint?> kvp in weightList)
            {
                Debug.Write(kvp.Key.Value.ToString());
                Debug.Write("\t");
                Debug.WriteLine(kvp.Value);
            }
            Debug.WriteLine("--------------------------------------");
            Debug.WriteLine(" Path List");
            Debug.WriteLine("--------------------------------------");
            foreach (KeyValuePair<GraphNode<T>, GraphNode<T>> kvp in pathList)
            {
                Debug.Write(kvp.Key.Value.ToString());
                Debug.Write("\t");
                Debug.WriteLine((kvp.Value == null) ? "null" : kvp.Value.Value.ToString());
            }
            Debug.WriteLine("--------------------------------------");
        }
        #endregion

        #endregion

        #region FindPaths....

        #region FindPathsByDepthLimit

        /// <summary>
        /// Find all paths between to points using a Depth-First search. Depth limiters 
        /// added to avoid infinite recursion.
        /// </summary>
        /// <param name="from">Node representing the starting point in the Graph</param>
        /// <param name="to">Node representing the end point in the Graph</param>
        /// <param name="depth">Depth to which the search will be limited</param>
        /// <param name="exact">If true only match at the specified depth will be returned.
        /// If false all matches inlcude those at the specified depth are returned.</param>
        /// <returns>Collection of all the paths found</returns>
        public ReadOnlyCollection<GraphNode<T>[]> FindPathsByDepthLimit(T from, T to, int depth, bool exact)
        {
            return FindPathsByDepthLimit(this[from], this[to], depth, exact);
        }

        /// <summary>
        /// Find all paths between to points using a Depth-First search. Depth limiters 
        /// added to avoid infinite recursion.
        /// </summary>
        /// <param name="from">GraphNode representing the starting point in the Graph</param>
        /// <param name="to">GraphNode representing the end point in the Graph</param>
        /// <param name="depth">Depth to which the search will be limited</param>
        /// <param name="exact">If true only match at the specified depth will be returned.
        /// If false all matches inlcude those at the specified depth are returned.</param>
        /// <returns>Collection of all the paths found</returns>
        public ReadOnlyCollection<GraphNode<T>[]> FindPathsByDepthLimit(GraphNode<T> from, GraphNode<T> to, int depth, bool exact)
        {
            List<GraphNode<T>[]> results = new List<GraphNode<T>[]>();
            Stack<GraphNode<T>> currPath = new Stack<GraphNode<T>>();

            currPath.Push(from);

            FindPathsByDepthLimit_DeepSearch(results, currPath, to, depth, exact);

            return results.AsReadOnly();
        }

        /// <summary>
        /// Recursive function that executes the Depth-First search.
        /// </summary>
        /// <param name="results">Current known valid paths</param>
        /// <param name="currPath">Path from startin node to current position in graph</param>
        /// <param name="to">Destination node used to compare if we reached it.</param>
        /// <param name="depth">Depth to which the search will be limited</param>
        /// <param name="exact">If true only match at the specified depth will be returned.
        /// If false all matches inlcude those at the specified depth are returned.</param>
        void FindPathsByDepthLimit_DeepSearch(List<GraphNode<T>[]> results, Stack<GraphNode<T>> currPath, GraphNode<T> to, int depth, bool exact)
        {
            GraphNode<T> current;
            GraphNode<T> parent = currPath.Peek();

            foreach (GraphEdge<T> edge in parent.Edges)
            {
                current = edge.NodeTo;
                currPath.Push(current);

                FindAllPaths_DumpProgress(currPath);

                if ((current.Equals(to) && !exact) /* equal when lax match */
                || (currPath.Count == depth && current.Equals(to) && exact)) /* or equal when exact match */
                {
                    results.Add(FindAllPaths_ReverseStackOrder(currPath));
                    currPath.Pop();
                    continue;
                }

                if (currPath.Count == depth) /* depth reached */
                {
                    currPath.Pop();
                    continue;
                }

                FindPathsByDepthLimit_DeepSearch(results, currPath, to, depth, exact);
                currPath.Pop();
            }
        }

        #endregion

        #region FindPathsByWeightLimit

        /// <summary>
        /// Find all paths between to points using a Depth-First search. Weight 
        /// limiters added to avoid infinite recursion.
        /// </summary>
        /// <param name="from">Node representing the starting point in the Graph</param>
        /// <param name="to">Node representing the end point in the Graph</param>
        /// <param name="maxWeight">Total edge Weight to which the search will be limited</param>
        /// <returns>Collection of all the paths found</returns>
        public ReadOnlyCollection<GraphNode<T>[]> FindPathsByWeightLimit(T from, T to, uint weight)
        {
            return FindPathsByWeightLimit(this[from], this[to], weight);
        }

        /// <summary>
        /// Find all paths between to points using a Depth-First search. Weight 
        /// limiters added to avoid infinite recursion.
        /// </summary>
        /// <param name="from">GraphNode representing the starting point in the Graph</param>
        /// <param name="to">GraphNode representing the end point in the Graph</param>
        /// <param name="maxWeight">Total edge Weight to which the search will be limited</param>
        /// <returns>Collection of all the paths found</returns>
        public ReadOnlyCollection<GraphNode<T>[]> FindPathsByWeightLimit(GraphNode<T> from, GraphNode<T> to, uint maxWeight)
        {
            List<GraphNode<T>[]> results = new List<GraphNode<T>[]>();
            Stack<GraphNode<T>> currPath = new Stack<GraphNode<T>>();

            currPath.Push(from);

            FindPathsByWeightLimit_DeepSearch(results, currPath, to, 0, maxWeight);

            return results.AsReadOnly();
        }

        /// <summary>
        /// Recursive function that executes the Depth-First search.
        /// </summary>
        /// <param name="results">Current known valid paths</param>
        /// <param name="currPath">Path from startin node to current position in graph</param>
        /// <param name="to">Destination node used to compare if we reached it.</param>
        /// <param name="curWeight">The current weight of the edges in the currPath</returns>
        /// <param name="maxWeight">Total edge Weight to which the search will be limited</param>
        /// <returns>Collection of all the paths found</returns>
        void FindPathsByWeightLimit_DeepSearch(List<GraphNode<T>[]> results, Stack<GraphNode<T>> currPath, GraphNode<T> to, uint curWeight, uint maxWeight)
        {
            GraphNode<T> current;
            GraphNode<T> parent = currPath.Peek();

            foreach (GraphEdge<T> edge in parent.Edges)
            {
                current = edge.NodeTo;
                currPath.Push(current);

                FindAllPaths_DumpProgress(currPath);

                if (current.Equals(to))
                    results.Add(FindAllPaths_ReverseStackOrder(currPath));

                if (curWeight >= maxWeight)
                {
                    currPath.Pop();
                    continue;
                }

                FindPathsByWeightLimit_DeepSearch(results
                                                , currPath
                                                , to
                                                , curWeight + edge.Weight
                                                , maxWeight );

                currPath.Pop();
            }
        }

        #endregion

        private void FindAllPaths_DumpProgress(Stack<GraphNode<T>> path)
        {
            GraphNode<T>[] tmpPath = path.ToArray();
            Debug.WriteLine("--------------------------------------");
            Debug.Write(" Path : ");
            foreach (GraphNode<T> node in tmpPath)
            {
                Debug.Write(string.Format("<- {0} ", node.Value));
            }
            Debug.WriteLine(string.Format(" | {0} Deep", tmpPath.Length));
            Debug.WriteLine("--------------------------------------");
        }

        /// <summary>
        /// Reverse stack order is used to return the path's to the consumer in 
        /// the right order. I.e. "start to end" and not "end to start"
        /// </summary>
        /// <param name="stack">Stack to be reversed</param>
        /// <returns>Reversed stack</returns>
        private GraphNode<T>[] FindAllPaths_ReverseStackOrder(Stack<GraphNode<T>> stack)
        {
            GraphNode<T>[] from = stack.ToArray();
            GraphNode<T>[] to = new GraphNode<T>[stack.Count];

            for (int i = 0; i < to.Length; i++)
                to[i] = from[to.Length - i - 1];

            return to;
        }

        #endregion

        #endregion

        #region Nested Classes
        /// <summary>
        /// Match class is used to build a Predicate to aid in matchin
        /// GraphNodes
        /// </summary>
        /// <typeparam name="U">Type used for storage in the graph</typeparam>
        class GraphNodeMatch<U>
        {
            U u;

            /// <summary>
            /// Default constructor
            /// </summary>
            /// <param name="u">The type that will be match during the search</param>
            public GraphNodeMatch(U u)
            {
                this.u = u;
            }

            /// <summary>
            /// Predicate representing a simple equality comparer
            /// </summary>
            /// <param name="other">The Object to compare with the current 
            /// Object.</param>
            /// <returns>true if the specified Object is equal to the current 
            /// Object; otherwise, false.</returns>
            public bool Match(GraphNode<U> other)
            {
                return u.Equals(other.Value);
            }
        }
        #endregion
    }

}

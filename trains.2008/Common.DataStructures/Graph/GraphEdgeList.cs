using System.Collections.Generic;

namespace Common.DataStructures.Graph
{
    /// <summary>
    /// Represents alist of edges. This list should only be stored inside an 
    /// node that represents the starting point.
    /// </summary>
    /// <typeparam name="T">Type used for storage in the graph</typeparam>
    public class GraphEdgeList<T> : List<GraphEdge<T>>
    {
        #region Methods

        /// <summary>
        /// Determines whether an element is in the GraphEdgeList. 
        /// </summary>
        /// <param name="node">The GraphNode to locate in the GraphEdgeList. </param>
        /// <returns>true if item is found in the List; otherwise, false</returns>
        public bool Contains(GraphNode<T> node)
        {
            foreach (GraphEdge<T> edge in this)
                if (edge.NodeTo.Equals(node))
                    return true;

            return false;
        }

        /// <summary>
        /// Add an element to this GraphEdgeList. Only adds the to node the 
        /// from node contains this list.
        /// </summary>
        /// <param name="to">GraphNode at witch the edge ends</param>
        /// <param name="weight">Weight of the edge</param>
        public void Add(GraphNode<T> to, uint weight)
        {
            this.Add(new GraphEdge<T>(to, weight));
        }
        /// <summary>
        /// Removes an element from the GraphEdgeList. 
        /// </summary>
        /// <param name="to">GraphNode at witch the edge ends to be removed</param>
        public void Remove(GraphNode<T> to)
        {
            this.Remove(this[to]);
        }

        #endregion

        #region Indexer

        /// <summary>
        /// Indexer to give the iteration of the GraphEdgeList matrix like 
        /// quailities when used in context of the graph.
        /// </summary>
        /// <param name="node">GraphEdge that will be found</param>
        /// <returns>GraphEdge representing an GraphNode searched</returns>
        public GraphEdge<T> this[GraphNode<T> node]
        {
            get
            {
                foreach (GraphEdge<T> edge in this)
                    if (edge.NodeTo.Equals(node))
                        return edge;

                return null;
            }
        }

        #endregion
    }
}

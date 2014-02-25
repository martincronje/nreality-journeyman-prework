namespace Common.DataStructures.Graph
{
    /// <summary>
    /// Representation of an edge within the Graph. Must be used with a 
    /// GraphNode to fully represent an edge.
    /// </summary>
    /// <typeparam name="T">Type used for storage in the graph</typeparam>
    public class GraphEdge<T>
    {
        #region Fields
        readonly GraphNode<T> to;
        readonly uint weight;
        #endregion

        #region Properties
        /// <summary>
        /// The GraphEdge only contains the end node of the edge. The start node
        /// maintains a list of edges internally
        /// </summary>
        public GraphNode<T> NodeTo
        {
            get { return to; }
        }
        /// <summary>
        /// Weight of the edge. Negative weights are not allowed.
        /// </summary>
        public uint Weight
        {
            get { return weight; }
        }
        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor. The GraphEdge only contains the end node of 
        /// the edge. The start node maintains a list of edges internally
        /// </summary>
        /// <param name="to">GraphNode at witch the edge ends</param>
        /// <param name="weight">Weight of the edge</param>
        public GraphEdge(GraphNode<T> to, uint weight)
        {
            this.to = to;
            this.weight = weight;
        }
        #endregion

        #region Methods
        /// <summary>
        /// The GraphEdge is compared on its NodeTo field.
        /// </summary>
        /// <returns>true = equal / false != equal</returns>
        public override bool Equals(object obj)
        {
            GraphEdge<T> edge = obj as GraphEdge<T>;
            if (edge != null)
                return NodeTo.Equals(edge.NodeTo);
            else
                return false;
        }
        /// <summary>
        /// The GraphEdge is compared on its NodeTo field.
        /// </summary>
        /// <returns>Ref. MSDN</returns>
        public override int GetHashCode()
        {
            return NodeTo.GetHashCode();
        }
        #endregion
    }

}

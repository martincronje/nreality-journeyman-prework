namespace Common.DataStructures.Graph
{
    /// <summary>
    /// Representation of a graph node (also referred to as vertex)
    /// </summary>
    /// <typeparam name="T">Type used for storage in the graph</typeparam>
    public class GraphNode<T> 
    {
        #region Fields
        private GraphEdgeList<T> edges;
        private T value;
        #endregion

        #region Properties

        /// <summary>
        /// Value stored in the node
        /// </summary>
        public T Value
        {
            get { return value; }
        }

        /// <summary>
        /// Edges to other nodes
        /// </summary>
        public GraphEdgeList<T> Edges
        {
            get { return edges; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor. Must be constructed with a value
        /// </summary>
        /// <param name="value">Value stored in the node</param>
        public GraphNode(T value)
        {
            this.edges = new GraphEdgeList<T>();
            this.value = value;
        }
        
        #endregion

        #region Methods

        /// <summary>
        /// The GraphNode is compared on its Value field.
        /// </summary>
        /// <returns>true = equal / false != equal</returns>
        public override bool Equals(object obj)
        {
            GraphNode<T> node = obj as GraphNode<T>;
            if (node != null)
                return Value.Equals(node.Value);
            else
                return false;

        }

        /// <summary>
        /// The GraphNode is compared on its Value field.
        /// </summary>
        /// <returns>Ref. MSDN</returns>
        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
        #endregion
    }
}

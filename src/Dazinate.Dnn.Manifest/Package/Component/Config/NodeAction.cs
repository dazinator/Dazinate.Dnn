namespace Dazinate.Dnn.Manifest.Package.Component.Config
{
    public enum NodeAction
    {
        /// <summary>
        /// Adds the node following the target node. If the target node is a collection, the node is added as the last element of the collection.
        /// </summary>
        Add,
        /// <summary>
        ///  Inserts the node before the target node.
        /// </summary>
        InsertBefore,
        /// <summary>
        /// Inserts the node after the target node.
        /// </summary>
        InsertAfter,
        /// <summary>
        /// Removes the target node and its inner content
        /// </summary>
        Remove,
        /// <summary>
        /// Removes a specific attribute/value pair from the target node.
        /// </summary>
        RemoveAttribute,
        /// <summary>
        /// Updates a node
        /// </summary>
        Update,
        /// <summary>
        /// Updates or adds a specific attribute/value pair on the target node.
        /// </summary>
        UpdateAttribute,
    }
}
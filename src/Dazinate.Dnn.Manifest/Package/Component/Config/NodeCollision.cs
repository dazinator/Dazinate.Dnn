namespace Dazinate.Dnn.Manifest.Package.Component.Config
{
    public enum NodeCollision
    {
        /// <summary>
        /// No changes will be made to the target configuration file.
        /// </summary>
        Ignore,
        /// <summary>
        /// The existing node or attribute is overwritten by the modification.
        /// </summary>
        Overwrite,
        /// <summary>
        /// Xml comment markers will be inserted around the existing node and the new or updated node will be inserted in the same location in the document tree.
        /// </summary>
        Save,

    }
}
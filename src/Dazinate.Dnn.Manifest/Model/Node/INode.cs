using System.Xml;
using Csla;
using Dazinate.Dnn.Manifest.Model.File;
using Dazinate.Dnn.Manifest.Writer;

namespace Dazinate.Dnn.Manifest.Model.Node
{
    public interface INode : IBusinessBase, IVisitable<IManifestXmlWriterVisitor>
    {
        string Path { get; set; }
        NodeAction? Action { get; set; }

        /// <summary>
        /// When processing an update action defines a new target based on an XPath that is the value of a named attribute on the original target specified by the path attribute. In other words a relative target.
        /// </summary>
        string TargetPath { get; set; }

        /// <summary>
        /// When processing an update action may be used to specify a child node of the target node that will be modified based on the value of the attribute name (key) that is contained in the source configuration node's immediate child node. Note that targetpath and key are mutually exclusive attributes. </summary>
        string Key { get; set; }

        /// <summary>
        ///  Defines what action will be taken if the node or attribute to be added or updated already exists. The following values may be supplied for the collision attribute:
        /// </summary>
        NodeCollision? Collision { get; set; }

        /// <summary>
        /// Used to specify an attribute only when action is removeattribute or updateattribute
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Used to specify the value for an attribute only when action is updateattribute
        /// </summary>
        string Value { get; set; }

        /// <summary>
        /// Adds the given namespace and/or namespace prefix to the target configuration file's namespace table and uses the XmlNamespace Manager to locate the target node by a namespace qualified path. I have to research this usage more fully in the XmlMerge API code and in the few examples which I found.
        /// </summary>
        string Namespace { get; set; }

        /// <summary>
        /// Adds the given namespace and/or namespace prefix to the target configuration file's namespace table and uses the XmlNamespace Manager to locate the target node by a namespace qualified path. I have to research this usage more fully in the XmlMerge API code and in the few examples which I found.
        /// </summary>
        string NamespacePrefix { get; set; }

        /// <summary>
        /// The inner xml contents of this node. 
        /// </summary>
        string InnerXml { get; set; }


    }
}
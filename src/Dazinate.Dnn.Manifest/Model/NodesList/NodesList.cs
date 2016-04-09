using System;
using Csla;
using Csla.Server;
using Dazinate.Dnn.Manifest.Model.Node;
using Dazinate.Dnn.Manifest.Model.NodesList.ObjectFactory;
using Dazinate.Dnn.Manifest.Writer;

namespace Dazinate.Dnn.Manifest.Model.NodesList
{
    [ObjectFactory(typeof(INodesListObjectFactory))]
    [Serializable]
    public class NodesList : BusinessListBase<NodesList, INode>, INodesList
    {
        // private readonly IPackageFactory _factory;

        public NodesList()
        {
        }

        public void Accept(IManifestXmlWriterVisitor visitor)
        {
            visitor.Visit(this);
        }

    }
}
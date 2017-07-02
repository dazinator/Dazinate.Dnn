using System;
using Csla;
using Csla.Server;
using Dazinate.Dnn.Manifest.Base;
using Dazinate.Dnn.Manifest.Package.Component.Config.ObjectFactory;

namespace Dazinate.Dnn.Manifest.Package.Component.Config
{
    [ObjectFactory(typeof(INodesListObjectFactory))]
    [Serializable]
    public class NodesList : BusinessListBase<NodesList, INode>, INodesList
    {
        // private readonly IPackageFactory _factory;

        public NodesList()
        {
        }

        public void Accept(IManifestVisitor visitor)
        {
            visitor.Visit(this);
        }

        protected override INode AddNewCore()
        {
            var node = Csla.DataPortal.Create<Node>();
            this.Add(node);
            return node;
          
        }

    }
}
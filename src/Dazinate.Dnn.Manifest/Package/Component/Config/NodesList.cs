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

#if !AddNewCoreReturnVoid
        protected override INode AddNewCore()
        {
            //base.AddNewCore();
            var item = Csla.DataPortal.Create<Node>();
            Add(item);
            return item;
        }
#else
        protected override void AddNewCore()
        {
            //base.AddNewCore();
             var item = Csla.DataPortal.Create<Node>();
            Add(item);           
        }
#endif

    }
}
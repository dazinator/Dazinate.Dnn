using System;
using Csla;
using Csla.Server;
using Dazinate.Dnn.Manifest.Base;
using Dazinate.Dnn.Manifest.Package.Component.Container.ObjectFactory;

namespace Dazinate.Dnn.Manifest.Package.Component.Container
{
    [ObjectFactory(typeof(IContainerFilesListObjectFactory))]
    [Serializable]
    public class ContainerFilesList : BusinessListBase<ContainerFilesList, IContainerFile>, IContainerFilesList
    {
        // private readonly IPackageFactory _factory;

        public ContainerFilesList()
        {
        }

        public void Accept(IManifestVisitor visitor)
        {
            visitor.Visit(this);
        }

#if !AddNewCoreReturnVoid
        protected override IContainerFile AddNewCore()
        {
            //base.AddNewCore();
            var item = Csla.DataPortal.Create<ContainerFile>();
            Add(item);
            return item;
        }
#else
        protected override void AddNewCore()
        {
            //base.AddNewCore();
             var item = Csla.DataPortal.Create<ContainerFile>();
            Add(item);           
        }
#endif

    }
}
using System;
using Csla;
using Csla.Server;
using Dazinate.Dnn.Manifest.Base;
using Dazinate.Dnn.Manifest.Package.Component.ResourceFile.ObjectFactory;

namespace Dazinate.Dnn.Manifest.Package.Component.ResourceFile
{
    [ObjectFactory(typeof(IResourceFilesListObjectFactory))]
    [Serializable]
    public class ResourceFilesList : BusinessListBase<ResourceFilesList, IResourceFile>, IResourceFilesList
    {
        // private readonly IPackageFactory _factory;

        public ResourceFilesList()
        {
        }

        public void Accept(IManifestVisitor visitor)
        {
            visitor.Visit(this);
        }

#if !AddNewCoreReturnVoid
        protected override IResourceFile AddNewCore()
        {
            //base.AddNewCore();
            var item = Csla.DataPortal.Create<ResourceFile>();
            Add(item);
            return item;
        }
#else
        protected override void AddNewCore()
        {
            //base.AddNewCore();
             var item = Csla.DataPortal.Create<ResourceFile>();
            Add(item);           
        }
#endif

    }
}
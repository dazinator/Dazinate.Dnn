using System;
using Csla;
using Csla.Server;
using Dazinate.Dnn.Manifest.Base;
using Dazinate.Dnn.Manifest.Package.Component.Module.ObjectFactory;

namespace Dazinate.Dnn.Manifest.Package.Component.Module
{
    [ObjectFactory(typeof(IModuleControlsListObjectFactory))]
    [Serializable]
    public class ModuleControlsList : BusinessListBase<ModuleControlsList, IModuleControl>, IModuleControlsList
    {
        // private readonly IPackageFactory _factory;

        public ModuleControlsList()
        {
        }

        public void Accept(IManifestVisitor visitor)
        {
            visitor.Visit(this);
        }

#if NETDESKTOP
        protected override IModuleControl AddNewCore()
        {
            //base.AddNewCore();
            var item = Csla.DataPortal.Create<ModuleControl>();
            Add(item);
            return item;
        }
#else
        protected override void AddNewCore()
        {
            //base.AddNewCore();
             var item = Csla.DataPortal.Create<ModuleControl>();
            Add(item);           
        }
#endif

    }
}
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
        protected override IModuleControl AddNewCore()
        {
            var item = Csla.DataPortal.Create<ModuleControl>();
            this.Add(item);
            return item;
        }

    }
}
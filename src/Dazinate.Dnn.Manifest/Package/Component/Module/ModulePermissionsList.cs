using System;
using Csla;
using Csla.Server;
using Dazinate.Dnn.Manifest.Base;
using Dazinate.Dnn.Manifest.Package.Component.Module.ObjectFactory;

namespace Dazinate.Dnn.Manifest.Package.Component.Module
{
    [ObjectFactory(typeof(IModulePermissionsListObjectFactory))]
    [Serializable]
    public class ModulePermissionsList : BusinessListBase<ModulePermissionsList, IModulePermission>, IModulePermissionsList
    {
        // private readonly IPackageFactory _factory;

        public ModulePermissionsList()
        {
        }

        public void Accept(IManifestVisitor visitor)
        {
            visitor.Visit(this);
        }

#if !AddNewCoreReturnVoid
        protected override IModulePermission AddNewCore()
        {
            //base.AddNewCore();
            var item = Csla.DataPortal.Create<ModulePermission>();
            Add(item);
            return item;
        }
#else
        protected override void AddNewCore()
        {
            //base.AddNewCore();
             var item = Csla.DataPortal.Create<ModulePermission>();
            Add(item);           
        }
#endif

    }
}
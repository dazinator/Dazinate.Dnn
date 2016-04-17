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

    }
}
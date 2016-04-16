using System;
using Csla;
using Csla.Server;
using Dazinate.Dnn.Manifest.Model.ModulePermission;
using Dazinate.Dnn.Manifest.Model.ModulePermissionsList.ObjectFactory;
using Dazinate.Dnn.Manifest.Writer;

namespace Dazinate.Dnn.Manifest.Model.ModulePermissionsList
{
    [ObjectFactory(typeof(IModulePermissionsListObjectFactory))]
    [Serializable]
    public class ModulePermissionsList : BusinessListBase<ModulePermissionsList, IModulePermission>, IModulePermissionsList
    {
        // private readonly IPackageFactory _factory;

        public ModulePermissionsList()
        {
        }

        public void Accept(IManifestXmlWriterVisitor visitor)
        {
            visitor.Visit(this);
        }

    }
}
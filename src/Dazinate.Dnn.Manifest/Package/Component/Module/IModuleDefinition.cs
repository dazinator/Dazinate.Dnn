using Csla;
using Dazinate.Dnn.Manifest.Base;

namespace Dazinate.Dnn.Manifest.Package.Component.Module
{
    public interface IModuleDefinition : IBusinessBase, IVisitable<IManifestVisitor>
    {
        string FriendlyName { get; set; }

        int? DefaultCacheTime { get; set; }

        IModuleControlsList ModuleControls { get; }

        IModulePermissionsList ModulePermissions { get; }

    }
}
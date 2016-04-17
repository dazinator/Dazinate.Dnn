using Csla;
using Dazinate.Dnn.Manifest.Base;
using Dazinate.Dnn.Manifest.Writer;

namespace Dazinate.Dnn.Manifest.Package.Component.Module
{
    public interface IModuleDefinition : IBusinessBase, IVisitable<IManifestXmlWriterVisitor>
    {
        string FriendlyName { get; set; }

        int? DefaultCacheTime { get; set; }

        IModuleControlsList ModuleControls { get; }

        IModulePermissionsList ModulePermissions { get; }

    }
}
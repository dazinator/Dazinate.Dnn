using Csla;
using Dazinate.Dnn.Manifest.Model.ModuleControlsList;
using Dazinate.Dnn.Manifest.Model.ModulePermissionsList;
using Dazinate.Dnn.Manifest.Writer;

namespace Dazinate.Dnn.Manifest.Model.ModuleDefinition
{
    public interface IModuleDefinition : IBusinessBase, IVisitable<IManifestXmlWriterVisitor>
    {
        string FriendlyName { get; set; }

        int? DefaultCacheTime { get; set; }

        IModuleControlsList ModuleControls { get; }

        IModulePermissionsList ModulePermissions { get; }

    }
}
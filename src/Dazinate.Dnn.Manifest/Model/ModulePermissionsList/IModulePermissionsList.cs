using Csla;
using Dazinate.Dnn.Manifest.Model.ModulePermission;
using Dazinate.Dnn.Manifest.Writer;

namespace Dazinate.Dnn.Manifest.Model.ModulePermissionsList
{
    public interface IModulePermissionsList : IBusinessListBase<IModulePermission>, IVisitable<IManifestXmlWriterVisitor>
    {

    }
}
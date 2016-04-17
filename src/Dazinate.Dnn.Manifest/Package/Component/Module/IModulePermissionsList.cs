using Csla;
using Dazinate.Dnn.Manifest.Base;
using Dazinate.Dnn.Manifest.Writer;

namespace Dazinate.Dnn.Manifest.Package.Component.Module
{
    public interface IModulePermissionsList : IBusinessListBase<IModulePermission>, IVisitable<IManifestXmlWriterVisitor>
    {

    }
}
using Csla;
using Dazinate.Dnn.Manifest.Base;

namespace Dazinate.Dnn.Manifest.Package.Component.ResourceFile
{
    public interface IResourceFilesList : IBusinessListBase<IResourceFile>, IVisitable<IManifestVisitor>
    {
    }
}
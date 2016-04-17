using Csla;
using Dazinate.Dnn.Manifest.Base;
using Dazinate.Dnn.Manifest.Writer;

namespace Dazinate.Dnn.Manifest.Package.Component.ResourceFile
{
    public interface IResourceFilesList : IBusinessListBase<IResourceFile>, IVisitable<IManifestXmlWriterVisitor>
    {
    }
}
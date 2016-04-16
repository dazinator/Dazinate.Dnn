using Csla;
using Dazinate.Dnn.Manifest.Model.ResourceFile;
using Dazinate.Dnn.Manifest.Writer;

namespace Dazinate.Dnn.Manifest.Model.ResourceFilesList
{
    public interface IResourceFilesList : IBusinessListBase<IResourceFile>, IVisitable<IManifestXmlWriterVisitor>
    {
    }
}
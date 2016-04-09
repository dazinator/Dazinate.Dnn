using Csla;
using Dazinate.Dnn.Manifest.Model.ContainerFile;
using Dazinate.Dnn.Manifest.Writer;

namespace Dazinate.Dnn.Manifest.Model.ContainerFilesList
{
    public interface IContainerFilesList : IBusinessListBase<IContainerFile>, IVisitable<IManifestXmlWriterVisitor>
    {

    }
}
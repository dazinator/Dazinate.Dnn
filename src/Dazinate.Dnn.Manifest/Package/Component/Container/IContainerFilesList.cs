using Csla;
using Dazinate.Dnn.Manifest.Base;
using Dazinate.Dnn.Manifest.Writer;

namespace Dazinate.Dnn.Manifest.Package.Component.Container
{
    public interface IContainerFilesList : IBusinessListBase<IContainerFile>, IVisitable<IManifestXmlWriterVisitor>
    {

    }
}
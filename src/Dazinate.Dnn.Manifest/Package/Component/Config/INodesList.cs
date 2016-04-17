using Csla;
using Dazinate.Dnn.Manifest.Base;
using Dazinate.Dnn.Manifest.Writer;

namespace Dazinate.Dnn.Manifest.Package.Component.Config
{
    public interface INodesList : IBusinessListBase<INode>, IVisitable<IManifestXmlWriterVisitor>
    {

    }
}
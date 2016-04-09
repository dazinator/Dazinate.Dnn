using Csla;
using Dazinate.Dnn.Manifest.Model.Node;
using Dazinate.Dnn.Manifest.Writer;

namespace Dazinate.Dnn.Manifest.Model.NodesList
{
    public interface INodesList : IBusinessListBase<INode>, IVisitable<IManifestXmlWriterVisitor>
    {

    }
}
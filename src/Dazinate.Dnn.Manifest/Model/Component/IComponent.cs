using Csla;
using Dazinate.Dnn.Manifest.Writer;

namespace Dazinate.Dnn.Manifest.Model.Component
{
    public interface IComponent : IBusinessBase, IVisitable<IManifestXmlWriterVisitor>
    {


    }
}
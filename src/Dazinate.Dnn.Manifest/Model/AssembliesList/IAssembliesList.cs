using Csla;
using Dazinate.Dnn.Manifest.Model.Assembly;
using Dazinate.Dnn.Manifest.Writer;

namespace Dazinate.Dnn.Manifest.Model.AssembliesList
{
    public interface IAssembliesList : IBusinessListBase<IAssembly>, IVisitable<IManifestXmlWriterVisitor>
    {

    }
}
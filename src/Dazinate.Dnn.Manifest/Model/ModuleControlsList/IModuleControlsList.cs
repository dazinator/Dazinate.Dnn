using Csla;
using Dazinate.Dnn.Manifest.Model.ModuleControl;
using Dazinate.Dnn.Manifest.Writer;

namespace Dazinate.Dnn.Manifest.Model.ModuleControlsList
{
    public interface IModuleControlsList : IBusinessListBase<IModuleControl>, IVisitable<IManifestXmlWriterVisitor>
    {

    }
}
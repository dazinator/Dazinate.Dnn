using Csla;
using Dazinate.Dnn.Manifest.Model.ModuleDefinition;
using Dazinate.Dnn.Manifest.Writer;

namespace Dazinate.Dnn.Manifest.Model.ModuleDefinitionsList
{
    public interface IModuleDefinitionsList : IBusinessListBase<IModuleDefinition>, IVisitable<IManifestXmlWriterVisitor>
    {
        
    }
}
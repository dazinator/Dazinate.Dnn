using Csla;
using Dazinate.Dnn.Manifest.Base;

namespace Dazinate.Dnn.Manifest.Package.Component.Module
{
    public interface IModuleDefinitionsList : IBusinessListBase<IModuleDefinition>, IVisitable<IManifestVisitor>
    {
        
    }
}
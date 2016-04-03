using Dazinate.Dnn.Manifest.Model.AssembliesList;

namespace Dazinate.Dnn.Manifest.Model.Component
{
    public interface IAssemblyComponent : IComponent
    {
        IAssembliesList Assemblies { get; }
    }
}
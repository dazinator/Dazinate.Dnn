namespace Dazinate.Dnn.Manifest.Package.Component.Assembly
{
    public interface IAssemblyComponent : IComponent
    {
        IAssembliesList Assemblies { get; }
    }
}
using Csla;
using Dazinate.Dnn.Manifest.Base;

namespace Dazinate.Dnn.Manifest.Package.Component.Assembly
{
    public interface IAssembly : IBusinessBase, IVisitable<IManifestVisitor>
    {
        string Path { get; set; }
        string Name { get; set; }
        string Version { get; set; }
        AssemblyAction Action { get; set; }
    }
}
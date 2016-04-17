using Csla;
using Dazinate.Dnn.Manifest.Base;
using Dazinate.Dnn.Manifest.Writer;

namespace Dazinate.Dnn.Manifest.Package.Component.Assembly
{
    public interface IAssembly : IBusinessBase, IVisitable<IManifestXmlWriterVisitor>
    {
        string Path { get; set; }
        string Name { get; set; }
        string Version { get; set; }
        AssemblyAction Action { get; set; }
    }
}
using Csla;

namespace Dnn.Contrib.Manifest
{
    public interface IDnnManifest : IBusinessBase, IVisitable<IManifestXmlWriterVisitor>
    {
        ManifestType Type { get; set; }
        string Version { get; set; }

        
    }
}
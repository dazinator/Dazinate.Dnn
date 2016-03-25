using Csla;

namespace Dazinate.Dnn.Manifest
{
    public interface IDnnManifest : IBusinessBase, IVisitable<IManifestXmlWriterVisitor>
    {
        ManifestType Type { get; set; }
        string Version { get; set; }

        
    }
}
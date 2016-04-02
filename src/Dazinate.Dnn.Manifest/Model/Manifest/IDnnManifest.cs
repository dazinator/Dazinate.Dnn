using System.Xml;
using Csla;
using Dazinate.Dnn.Manifest.Writer;

namespace Dazinate.Dnn.Manifest.Model
{
    public interface IDnnManifest : IBusinessBase, IVisitable<IManifestXmlWriterVisitor>
    {
        ManifestType Type { get; set; }
        string Version { get; set; }
        object SaveToXml(XmlWriter writer);
    }
}
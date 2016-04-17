using System.Xml;
using Csla;

namespace Dazinate.Dnn.Manifest.Base
{
    public interface IDnnManifest : IBusinessBase, IVisitable<IManifestVisitor>
    {
        ManifestType Type { get; set; }
        string Version { get; set; }
        IDnnManifest SaveToXml(XmlWriter writer);
    }
}
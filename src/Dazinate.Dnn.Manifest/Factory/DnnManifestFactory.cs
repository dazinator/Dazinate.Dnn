using System.Xml;
using Dazinate.Dnn.Manifest.Model;

namespace Dazinate.Dnn.Manifest.Factory
{
    public abstract class DnnManifestFactory : IDnnManifestFactory<IDnnManifest>
    {
        public abstract IDnnManifest Get(string xmlContents);

        public abstract IDnnManifest Get(XmlReader reader);


    
    }
}
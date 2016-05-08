using System.Xml;
using Dazinate.Dnn.Manifest.Base;

namespace Dazinate.Dnn.Manifest.Factory
{
    public abstract class DnnManifestFactory : IDnnManifestFactory<IDnnManifest>
    {
        public abstract IDnnManifest Get(string xmlContents);

        public abstract IDnnManifest Get(XmlReader reader);
        public IDnnManifest CreateNewManifest()
        {
            throw new System.NotImplementedException();
        }
    }
}
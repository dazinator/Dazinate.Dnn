using System.Xml;
using Dazinate.Dnn.Manifest.Model;

namespace Dazinate.Dnn.Manifest.Factory
{
    public interface IDnnManifestFactory<out TModel> where TModel : IDnnManifest
    {
        TModel Get(string xmlContents);
        TModel Get(XmlReader reader);
    }
}
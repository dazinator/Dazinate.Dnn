using System.Xml;
using Dazinate.Dnn.Manifest.Base;

namespace Dazinate.Dnn.Manifest.Factory
{
    public interface IDnnManifestFactory<out TModel> where TModel : IDnnManifest
    {
        TModel Get(string xmlContents);
        TModel Get(XmlReader reader);
    }
}
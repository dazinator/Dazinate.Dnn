using System.Xml.XPath;
using Dazinate.Dnn.Manifest.Model.LanguageFile;

namespace Dazinate.Dnn.Manifest.Model.JavascriptFile.ObjectFactory
{
    public interface IJavascriptFileObjectFactory
    {
        IJavascriptFile Fetch(XPathNavigator xpathNavigator);
    }
}
using System.Xml.XPath;

namespace Dazinate.Dnn.Manifest.Package.Component.JavascriptFile.ObjectFactory
{
    public interface IJavascriptFileObjectFactory
    {
        IJavascriptFile Fetch(XPathNavigator xpathNavigator);
    }
}
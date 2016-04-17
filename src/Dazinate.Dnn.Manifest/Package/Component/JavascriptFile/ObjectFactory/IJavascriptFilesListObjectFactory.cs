using System.Xml.XPath;

namespace Dazinate.Dnn.Manifest.Package.Component.JavascriptFile.ObjectFactory
{
    public interface IJavascriptFilesListObjectFactory
    {
        IJavascriptFilesList Fetch(XPathNavigator xpathNavigator);
    }
}
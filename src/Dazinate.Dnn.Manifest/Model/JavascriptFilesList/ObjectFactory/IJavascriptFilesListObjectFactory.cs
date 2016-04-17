using System.Xml.XPath;

namespace Dazinate.Dnn.Manifest.Model.JavascriptFilesList.ObjectFactory
{
    public interface IJavascriptFilesListObjectFactory
    {
        IJavascriptFilesList Fetch(XPathNavigator xpathNavigator);
    }
}
using System.Xml.XPath;

namespace Dazinate.Dnn.Manifest.Model.ResourceFilesList.ObjectFactory
{
    public interface IResourceFilesListObjectFactory
    {
        IResourceFilesList Fetch(XPathNavigator xpathNavigator);
    }
}
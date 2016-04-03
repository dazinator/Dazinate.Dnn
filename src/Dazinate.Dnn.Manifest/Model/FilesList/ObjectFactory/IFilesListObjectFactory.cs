using System.Xml.XPath;

namespace Dazinate.Dnn.Manifest.Model.FilesList.ObjectFactory
{
    public interface IFilesListObjectFactory
    {
        IFilesList Fetch(XPathNavigator xpathNavigator);
    }
}
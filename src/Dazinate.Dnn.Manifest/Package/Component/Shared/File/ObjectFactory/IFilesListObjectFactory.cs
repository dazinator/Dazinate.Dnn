using System.Xml.XPath;

namespace Dazinate.Dnn.Manifest.Package.Component.Shared.File.ObjectFactory
{
    public interface IFilesListObjectFactory
    {
        IFilesList Fetch(XPathNavigator xpathNavigator);
    }
}
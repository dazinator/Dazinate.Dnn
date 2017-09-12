using System.Xml.XPath;

namespace Dazinate.Dnn.Manifest.Package.Component.ResourceFile.ObjectFactory
{
    public interface IResourceFilesListObjectFactory
    {
        IResourceFilesList Fetch(XPathNavigator xpathNavigator);
        IResourceFilesList Create();
    }
}
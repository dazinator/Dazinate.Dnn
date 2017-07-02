using System.Xml.XPath;

namespace Dazinate.Dnn.Manifest.Package.Component.Container.ObjectFactory
{
    public interface IContainerFilesListObjectFactory
    {
        IContainerFilesList Fetch(XPathNavigator xpathNavigator);
        IContainerFilesList Create();
    }
}
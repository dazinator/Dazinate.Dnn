using System.Xml.XPath;

namespace Dazinate.Dnn.Manifest.Model.ContainerFilesList.ObjectFactory
{
    public interface IContainerFilesListObjectFactory
    {
        IContainerFilesList Fetch(XPathNavigator xpathNavigator);
    }
}
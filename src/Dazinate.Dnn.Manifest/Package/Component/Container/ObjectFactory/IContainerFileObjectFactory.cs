using System.Xml.XPath;

namespace Dazinate.Dnn.Manifest.Package.Component.Container.ObjectFactory
{
    public interface IContainerFileObjectFactory
    {
        IContainerFile Fetch(XPathNavigator xpathNavigator);
    }
}
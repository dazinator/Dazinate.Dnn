using System.Xml.XPath;

namespace Dazinate.Dnn.Manifest.Package.Component.ObjectFactory
{
    public interface IComponentObjectFactory
    {
        IComponent Fetch(XPathNavigator xpathNavigator);
    }
}
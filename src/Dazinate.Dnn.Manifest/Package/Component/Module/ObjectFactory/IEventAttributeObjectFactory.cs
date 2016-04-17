using System.Xml.XPath;

namespace Dazinate.Dnn.Manifest.Package.Component.Module.ObjectFactory
{
    public interface IEventAttributeObjectFactory
    {
        IEventAttribute Fetch(XPathNavigator xpathNavigator);
    }
}
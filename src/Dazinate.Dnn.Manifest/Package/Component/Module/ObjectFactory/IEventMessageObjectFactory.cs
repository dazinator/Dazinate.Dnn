using System.Xml.XPath;

namespace Dazinate.Dnn.Manifest.Package.Component.Module.ObjectFactory
{
    public interface IEventMessageObjectFactory
    {
        IEventMessage Fetch(XPathNavigator xpathNavigator);
    }
}
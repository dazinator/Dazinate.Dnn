using System.Xml.XPath;

namespace Dazinate.Dnn.Manifest.Model.EventMessage.ObjectFactory
{
    public interface IEventMessageObjectFactory
    {
        IEventMessage Fetch(XPathNavigator xpathNavigator);
    }
}
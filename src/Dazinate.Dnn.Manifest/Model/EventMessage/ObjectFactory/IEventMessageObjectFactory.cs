using System.Xml.XPath;

namespace Dazinate.Dnn.Manifest.Model.EventMessage
{
    public interface IEventMessageObjectFactory
    {
        IEventMessage Fetch(XPathNavigator xpathNavigator);
    }
}
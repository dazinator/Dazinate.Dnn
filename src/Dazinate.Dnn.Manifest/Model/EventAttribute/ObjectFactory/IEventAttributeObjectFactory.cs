using System.Xml.XPath;

namespace Dazinate.Dnn.Manifest.Model.EventAttribute.ObjectFactory
{
    public interface IEventAttributeObjectFactory
    {
        IEventAttribute Fetch(XPathNavigator xpathNavigator);
    }
}
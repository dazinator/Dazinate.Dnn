using System.Xml.XPath;

namespace Dazinate.Dnn.Manifest.Model.EventAttributesList.ObjectFactory
{
    public interface IEventAttributesListObjectFactory
    {
        IEventAttributesList Fetch(XPathNavigator xpathNavigator);
    }
}
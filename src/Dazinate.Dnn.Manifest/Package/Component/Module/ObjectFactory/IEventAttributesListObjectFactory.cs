using System.Xml.XPath;

namespace Dazinate.Dnn.Manifest.Package.Component.Module.ObjectFactory
{
    public interface IEventAttributesListObjectFactory
    {
        IEventAttributesList Fetch(XPathNavigator xpathNavigator);
    }
}
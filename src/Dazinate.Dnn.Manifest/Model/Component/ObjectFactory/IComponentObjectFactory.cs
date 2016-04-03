using System.Xml.XPath;

namespace Dazinate.Dnn.Manifest.Model.Component.ObjectFactory
{
    public interface IComponentObjectFactory
    {
        IComponent Fetch(XPathNavigator xpathNavigator);
    }
}
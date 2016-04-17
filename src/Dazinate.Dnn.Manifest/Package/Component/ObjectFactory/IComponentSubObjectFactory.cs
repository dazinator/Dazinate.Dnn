using System.Xml.XPath;

namespace Dazinate.Dnn.Manifest.Package.Component.ObjectFactory
{
    public interface IComponentSubObjectFactory
    {
        string ComponentTypeName { get; }

        IComponent Fetch(XPathNavigator xpathNavigator);
    }
}
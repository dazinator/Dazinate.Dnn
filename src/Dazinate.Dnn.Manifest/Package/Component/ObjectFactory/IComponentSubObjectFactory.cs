using System.Xml.XPath;

namespace Dazinate.Dnn.Manifest.Package.Component.ObjectFactory
{
    public interface IComponentSubObjectFactory
    {
        ComponentType ComponentType { get; }

        IComponent Fetch(XPathNavigator xpathNavigator);
        IComponent Create(ComponentType componentType);
    }
}
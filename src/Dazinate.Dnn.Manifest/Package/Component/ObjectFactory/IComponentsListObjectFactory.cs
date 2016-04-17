using System.Xml.XPath;

namespace Dazinate.Dnn.Manifest.Package.Component.ObjectFactory
{
    public interface IComponentsListObjectFactory
    {
        IComponentsList Fetch(XPathNavigator xpathNavigator);
        IComponentsList Create();
    }
}
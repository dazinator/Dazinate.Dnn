using System.Xml.XPath;

namespace Dazinate.Dnn.Manifest.Model.ComponentsList.ObjectFactory
{
    public interface IComponentsListObjectFactory
    {
        IComponentsList Fetch(XPathNavigator xpathNavigator);
        IComponentsList Create();
    }
}
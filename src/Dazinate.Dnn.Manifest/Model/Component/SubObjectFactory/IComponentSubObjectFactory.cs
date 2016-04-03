using System.Xml.XPath;

namespace Dazinate.Dnn.Manifest.Model.Component.SubObjectFactory
{
    public interface IComponentSubObjectFactory
    {
        string ComponentTypeName { get; }

        IComponent Fetch(XPathNavigator xpathNavigator);
    }
}
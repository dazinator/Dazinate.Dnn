using System.Xml.XPath;

namespace Dazinate.Dnn.Manifest.Package.Component.Config.ObjectFactory
{
    public interface INodesListObjectFactory
    {
        INodesList Fetch(XPathNavigator xpathNavigator);
    }
}
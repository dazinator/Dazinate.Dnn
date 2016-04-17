using System.Xml.XPath;

namespace Dazinate.Dnn.Manifest.Package.Component.Config.ObjectFactory
{
    public interface INodeObjectFactory
    {
        INode Fetch(XPathNavigator xpathNavigator);
    }
}
using System.Xml.XPath;

namespace Dazinate.Dnn.Manifest.Model.Node.ObjectFactory
{
    public interface INodeObjectFactory
    {
        INode Fetch(XPathNavigator xpathNavigator);
    }
}
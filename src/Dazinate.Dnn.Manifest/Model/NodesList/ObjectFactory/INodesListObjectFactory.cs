using System.Xml.XPath;

namespace Dazinate.Dnn.Manifest.Model.NodesList.ObjectFactory
{
    public interface INodesListObjectFactory
    {
        INodesList Fetch(XPathNavigator xpathNavigator);
    }
}
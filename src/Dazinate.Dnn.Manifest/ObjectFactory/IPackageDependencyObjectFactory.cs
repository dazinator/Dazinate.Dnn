using System.Xml.XPath;

namespace Dazinate.Dnn.Manifest.ObjectFactory
{
    public interface IPackageDependencyObjectFactory
    {
        IPackageDependency Fetch(XPathNavigator xpathNavigator);
    }
}
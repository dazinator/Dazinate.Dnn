using System.Xml.XPath;

namespace Dazinate.Dnn.Manifest.ObjectFactory
{
    public interface IPackageObjectFactory
    {
        Package Fetch(XPathNavigator xpathNavigator);
    }
}
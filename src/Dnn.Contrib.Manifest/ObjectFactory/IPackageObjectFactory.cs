using System.Xml.XPath;

namespace Dnn.Contrib.Manifest.ObjectFactory
{
    public interface IPackageObjectFactory
    {
        Package Fetch(XPathNavigator xpathNavigator);
    }
}
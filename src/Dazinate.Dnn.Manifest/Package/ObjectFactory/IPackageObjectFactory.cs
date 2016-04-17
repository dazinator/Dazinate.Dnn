using System.Xml.XPath;

namespace Dazinate.Dnn.Manifest.Package.ObjectFactory
{
    public interface IPackageObjectFactory
    {
        Package Fetch(XPathNavigator xpathNavigator);
    }
}
using System.Xml.XPath;

namespace Dazinate.Dnn.Manifest.Model.Package.ObjectFactory
{
    public interface IPackageObjectFactory
    {
        Package Fetch(XPathNavigator xpathNavigator);
    }
}
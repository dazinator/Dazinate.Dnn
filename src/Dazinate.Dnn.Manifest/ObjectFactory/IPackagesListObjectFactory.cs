using System.Xml.XPath;

namespace Dazinate.Dnn.Manifest.ObjectFactory
{
    public interface IPackagesListObjectFactory
    {
        PackagesList Fetch(XPathNavigator xpathNavigator);
    }
}
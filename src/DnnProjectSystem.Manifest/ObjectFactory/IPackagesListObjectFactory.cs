using System.Xml.XPath;

namespace Dnn.Contrib.Manifest.ObjectFactory
{
    public interface IPackagesListObjectFactory
    {
        PackagesList Fetch(XPathNavigator xpathNavigator);
    }
}
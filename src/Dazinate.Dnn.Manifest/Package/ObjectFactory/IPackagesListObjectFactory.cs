using System.Xml.XPath;

namespace Dazinate.Dnn.Manifest.Package.ObjectFactory
{
    public interface IPackagesListObjectFactory
    {
        PackagesList Fetch(XPathNavigator xpathNavigator);
        IPackagesList Create();
    }
}

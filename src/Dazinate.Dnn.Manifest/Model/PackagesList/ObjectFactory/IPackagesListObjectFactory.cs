using System.Xml.XPath;

namespace Dazinate.Dnn.Manifest.Model.PackagesList.ObjectFactory
{
    public interface IPackagesListObjectFactory
    {
        PackagesList Fetch(XPathNavigator xpathNavigator);
    }
}

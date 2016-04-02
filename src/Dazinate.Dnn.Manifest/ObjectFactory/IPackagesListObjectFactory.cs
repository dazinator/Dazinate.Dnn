using System.Xml.XPath;
using Dazinate.Dnn.Manifest.Model.Package;

namespace Dazinate.Dnn.Manifest.ObjectFactory
{
    public interface IPackagesListObjectFactory
    {
        PackagesList Fetch(XPathNavigator xpathNavigator);
    }
}

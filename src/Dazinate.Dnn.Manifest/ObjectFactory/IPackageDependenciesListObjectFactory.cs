using System.Xml.XPath;

namespace Dazinate.Dnn.Manifest.ObjectFactory
{
    public interface IPackageDependenciesListObjectFactory
    {
        PackageDependenciesList Fetch(XPathNavigator xpathNavigator);
    }
}
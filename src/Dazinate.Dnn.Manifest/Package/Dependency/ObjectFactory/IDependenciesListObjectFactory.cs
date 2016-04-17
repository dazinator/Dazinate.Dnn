using System.Xml.XPath;

namespace Dazinate.Dnn.Manifest.Package.Dependency.ObjectFactory
{
    public interface IDependenciesListObjectFactory
    {
        DependenciesList Fetch(XPathNavigator xpathNavigator);

        IDependenciesList Create();
    }
}
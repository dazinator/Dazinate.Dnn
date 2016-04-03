using System.Xml.XPath;

namespace Dazinate.Dnn.Manifest.Model.DependencyList.ObjectFactory
{
    public interface IDependenciesListObjectFactory
    {
        DependenciesList Fetch(XPathNavigator xpathNavigator);

        IDependenciesList Create();
    }
}
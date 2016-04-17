using System.Xml.XPath;

namespace Dazinate.Dnn.Manifest.Package.Dependency.ObjectFactory
{
    public interface IDependencyObjectFactory
    {
        IDependency Fetch(XPathNavigator xpathNavigator);
    }
}
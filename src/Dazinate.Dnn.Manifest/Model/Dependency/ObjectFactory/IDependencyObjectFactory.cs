using System.Xml.XPath;

namespace Dazinate.Dnn.Manifest.Model.Dependency.ObjectFactory
{
    public interface IDependencyObjectFactory
    {
        IDependency Fetch(XPathNavigator xpathNavigator);
    }
}
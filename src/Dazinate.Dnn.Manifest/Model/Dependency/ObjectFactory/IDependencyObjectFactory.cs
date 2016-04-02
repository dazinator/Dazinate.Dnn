using System.Xml.XPath;
using Dazinate.Dnn.Manifest.Model.Dependency;

namespace Dazinate.Dnn.Manifest.ObjectFactory
{
    public interface IDependencyObjectFactory
    {
        IDependency Fetch(XPathNavigator xpathNavigator);
    }
}
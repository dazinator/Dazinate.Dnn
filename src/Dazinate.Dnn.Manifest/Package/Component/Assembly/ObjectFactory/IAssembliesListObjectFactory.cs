using System.Xml.XPath;

namespace Dazinate.Dnn.Manifest.Package.Component.Assembly.ObjectFactory
{
    public interface IAssembliesListObjectFactory
    {
        AssembliesList Fetch(XPathNavigator xpathNavigator);
    }
}
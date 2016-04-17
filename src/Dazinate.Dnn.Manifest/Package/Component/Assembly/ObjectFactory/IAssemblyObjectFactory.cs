using System.Xml.XPath;

namespace Dazinate.Dnn.Manifest.Package.Component.Assembly.ObjectFactory
{
    public interface IAssemblyObjectFactory
    {
        IAssembly Fetch(XPathNavigator xpathNavigator);
    }
}
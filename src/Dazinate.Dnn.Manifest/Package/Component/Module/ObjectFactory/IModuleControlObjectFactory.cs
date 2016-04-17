using System.Xml.XPath;

namespace Dazinate.Dnn.Manifest.Package.Component.Module.ObjectFactory
{
    public interface IModuleControlObjectFactory
    {
        IModuleControl Fetch(XPathNavigator xpathNavigator);
    }
}
using System.Xml.XPath;

namespace Dazinate.Dnn.Manifest.Package.Component.Module.ObjectFactory
{
    public interface IModuleControlsListObjectFactory
    {
        IModuleControlsList Fetch(XPathNavigator xpathNavigator);
    }
}
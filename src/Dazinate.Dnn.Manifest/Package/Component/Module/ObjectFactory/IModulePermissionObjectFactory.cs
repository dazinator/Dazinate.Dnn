using System.Xml.XPath;

namespace Dazinate.Dnn.Manifest.Package.Component.Module.ObjectFactory
{
    public interface IModulePermissionObjectFactory
    {
        IModulePermission Fetch(XPathNavigator xpathNavigator);
    }
}
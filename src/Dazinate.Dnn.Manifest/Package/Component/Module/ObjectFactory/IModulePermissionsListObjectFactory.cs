using System.Xml.XPath;

namespace Dazinate.Dnn.Manifest.Package.Component.Module.ObjectFactory
{
    public interface IModulePermissionsListObjectFactory
    {
        IModulePermissionsList Fetch(XPathNavigator xpathNavigator);
        IModulePermissionsList Create();
    }
}
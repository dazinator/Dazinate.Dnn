using System.Xml.XPath;

namespace Dazinate.Dnn.Manifest.Model.ModulePermissionsList.ObjectFactory
{
    public interface IModulePermissionsListObjectFactory
    {
        IModulePermissionsList Fetch(XPathNavigator xpathNavigator);
    }
}
using System.Xml.XPath;

namespace Dazinate.Dnn.Manifest.Model.ModulePermission.ObjectFactory
{
    public interface IModulePermissionObjectFactory
    {
        IModulePermission Fetch(XPathNavigator xpathNavigator);
    }
}
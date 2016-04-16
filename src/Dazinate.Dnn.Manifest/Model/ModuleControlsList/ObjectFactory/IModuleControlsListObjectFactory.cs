using System.Xml.XPath;

namespace Dazinate.Dnn.Manifest.Model.ModuleControlsList.ObjectFactory
{
    public interface IModuleControlsListObjectFactory
    {
        IModuleControlsList Fetch(XPathNavigator xpathNavigator);
    }
}
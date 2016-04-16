using System.Xml.XPath;

namespace Dazinate.Dnn.Manifest.Model.ModuleControl.ObjectFactory
{
    public interface IModuleControlObjectFactory
    {
        IModuleControl Fetch(XPathNavigator xpathNavigator);
    }
}
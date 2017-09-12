using System.Xml.XPath;

namespace Dazinate.Dnn.Manifest.Package.Component.DashboardControl.ObjectFactory
{
    public interface IDashboardControlsListObjectFactory
    {
        IDashboardControlsList Fetch(XPathNavigator xpathNavigator);
        IDashboardControlsList Create();
    }
}
using System.Xml.XPath;

namespace Dazinate.Dnn.Manifest.Package.Component.DashboardControl.ObjectFactory
{
    public interface IDashboardControlObjectFactory
    {
        IDashboardControl Fetch(XPathNavigator xpathNavigator);
    }
}
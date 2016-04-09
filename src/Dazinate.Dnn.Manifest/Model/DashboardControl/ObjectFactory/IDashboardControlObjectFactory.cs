using System.Xml.XPath;

namespace Dazinate.Dnn.Manifest.Model.DashboardControl.ObjectFactory
{
    public interface IDashboardControlObjectFactory
    {
        IDashboardControl Fetch(XPathNavigator xpathNavigator);
    }
}
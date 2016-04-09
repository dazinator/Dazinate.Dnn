using System.Xml.XPath;

namespace Dazinate.Dnn.Manifest.Model.DashboardControlsList.ObjectFactory
{
    public interface IDashboardControlsListObjectFactory
    {
        IDashboardControlsList Fetch(XPathNavigator xpathNavigator);
    }
}
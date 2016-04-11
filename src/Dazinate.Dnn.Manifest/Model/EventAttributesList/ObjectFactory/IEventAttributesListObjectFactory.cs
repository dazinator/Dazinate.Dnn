using System.Xml.XPath;
using Dazinate.Dnn.Manifest.Model.FilesList;

namespace Dazinate.Dnn.Manifest.Model.EventAttributesList
{
    public interface IEventAttributesListObjectFactory
    {
        IEventAttributesList Fetch(XPathNavigator xpathNavigator);
    }
}
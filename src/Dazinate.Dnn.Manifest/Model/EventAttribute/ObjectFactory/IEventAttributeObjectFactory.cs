using System.Xml.XPath;
using Dazinate.Dnn.Manifest.Model.File;
using Dazinate.Dnn.Manifest.Utils;

namespace Dazinate.Dnn.Manifest.Model.EventAttribute
{
    public interface IEventAttributeObjectFactory
    {
        IEventAttribute Fetch(XPathNavigator xpathNavigator);
    }
}
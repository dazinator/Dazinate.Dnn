using System.Xml.XPath;
using Dazinate.Dnn.Manifest.Model.File;

namespace Dazinate.Dnn.Manifest.Model.Script.ObjectFactory
{
    public interface IScriptObjectFactory
    {
        IScript Fetch(XPathNavigator xpathNavigator);
    }
}
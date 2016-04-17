using System.Xml.XPath;

namespace Dazinate.Dnn.Manifest.Package.Component.Script.ObjectFactory
{
    public interface IScriptObjectFactory
    {
        IScript Fetch(XPathNavigator xpathNavigator);
    }
}
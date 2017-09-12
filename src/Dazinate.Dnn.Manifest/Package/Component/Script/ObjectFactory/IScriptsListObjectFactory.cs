using System.Xml.XPath;

namespace Dazinate.Dnn.Manifest.Package.Component.Script.ObjectFactory
{
    public interface IScriptsListObjectFactory
    {
        IScriptsList Fetch(XPathNavigator xpathNavigator);
        IScriptsList Create();
    }
}
using System.Xml.XPath;

namespace Dazinate.Dnn.Manifest.Model.ScriptsList.ObjectFactory
{
    public interface IScriptsListObjectFactory
    {
        IScriptsList Fetch(XPathNavigator xpathNavigator);
    }
}
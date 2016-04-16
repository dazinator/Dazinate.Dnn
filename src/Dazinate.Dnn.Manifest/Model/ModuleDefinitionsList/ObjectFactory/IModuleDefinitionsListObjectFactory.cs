using System.Xml.XPath;

namespace Dazinate.Dnn.Manifest.Model.ModuleDefinitionsList.ObjectFactory
{
    public interface IModuleDefinitionsListObjectFactory
    {
        IModuleDefinitionsList Fetch(XPathNavigator xpathNavigator);
    }
}
using System.Xml.XPath;

namespace Dazinate.Dnn.Manifest.Package.Component.Module.ObjectFactory
{
    public interface IModuleDefinitionsListObjectFactory
    {
        IModuleDefinitionsList Fetch(XPathNavigator xpathNavigator);
        IModuleDefinitionsList Create();
    }
}
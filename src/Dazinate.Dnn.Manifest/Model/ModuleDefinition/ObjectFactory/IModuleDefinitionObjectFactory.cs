using System.Xml.XPath;

namespace Dazinate.Dnn.Manifest.Model.ModuleDefinition.ObjectFactory
{
    public interface IModuleDefinitionObjectFactory
    {
        IModuleDefinition Fetch(XPathNavigator xpathNavigator);
    }
}
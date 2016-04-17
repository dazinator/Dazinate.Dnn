using System.Xml.XPath;

namespace Dazinate.Dnn.Manifest.Package.Component.Module.ObjectFactory
{
    public interface IModuleDefinitionObjectFactory
    {
        IModuleDefinition Fetch(XPathNavigator xpathNavigator);
    }
}
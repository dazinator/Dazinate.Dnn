using System.Xml.XPath;

namespace Dazinate.Dnn.Manifest.Model.AssembliesList.ObjectFactory
{
    public interface IAssembliesListObjectFactory
    {
        AssembliesList Fetch(XPathNavigator xpathNavigator);
    }
}
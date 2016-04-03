using System.Xml.XPath;

namespace Dazinate.Dnn.Manifest.Model.Assembly.ObjectFactory
{
    public interface IAssemblyObjectFactory
    {
        IAssembly Fetch(XPathNavigator xpathNavigator);
    }
}
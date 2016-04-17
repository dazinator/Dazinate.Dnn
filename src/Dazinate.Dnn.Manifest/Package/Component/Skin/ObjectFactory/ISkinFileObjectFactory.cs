using System.Xml.XPath;

namespace Dazinate.Dnn.Manifest.Package.Component.Skin.ObjectFactory
{
    public interface ISkinFileObjectFactory
    {
        ISkinFile Fetch(XPathNavigator xpathNavigator);

    }
}
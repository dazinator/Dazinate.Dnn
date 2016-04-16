using System.Xml.XPath;

namespace Dazinate.Dnn.Manifest.Model.SkinFile.ObjectFactory
{
    public interface ISkinFileObjectFactory
    {
        ISkinFile Fetch(XPathNavigator xpathNavigator);

    }
}
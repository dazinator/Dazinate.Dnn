using System.Xml.XPath;

namespace Dazinate.Dnn.Manifest.Package.Component.Skin.ObjectFactory
{
    public interface ISkinFilesListObjectFactory
    {
        ISkinFilesList Fetch(XPathNavigator xpathNavigator);
        ISkinFilesList Create();
    }
}
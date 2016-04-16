using System.Xml.XPath;

namespace Dazinate.Dnn.Manifest.Model.SkinFilesList.ObjectFactory
{
    public interface ISkinFilesListObjectFactory
    {
        ISkinFilesList Fetch(XPathNavigator xpathNavigator);
    }
}
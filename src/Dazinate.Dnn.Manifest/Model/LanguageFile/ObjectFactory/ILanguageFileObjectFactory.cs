using System.Xml.XPath;

namespace Dazinate.Dnn.Manifest.Model.LanguageFile.ObjectFactory
{
    public interface ILanguageFileObjectFactory
    {
        ILanguageFile Fetch(XPathNavigator xpathNavigator);
    }
}
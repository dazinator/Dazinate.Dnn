using System.Xml.XPath;

namespace Dazinate.Dnn.Manifest.Package.Component.Shared.LanguageFile.ObjectFactory
{
    public interface ILanguageFileObjectFactory
    {
        ILanguageFile Fetch(XPathNavigator xpathNavigator);
    }
}
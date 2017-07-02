using System.Xml.XPath;

namespace Dazinate.Dnn.Manifest.Package.Component.Shared.LanguageFile.ObjectFactory
{
    public interface ILanguageFilesListObjectFactory
    {
        ILanguageFilesList Fetch(XPathNavigator xpathNavigator);
        ILanguageFilesList Create();
    }
}
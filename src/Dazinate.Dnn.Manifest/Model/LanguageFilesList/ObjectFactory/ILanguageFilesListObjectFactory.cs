using System.Xml.XPath;

namespace Dazinate.Dnn.Manifest.Model.LanguageFilesList.ObjectFactory
{
    public interface ILanguageFilesListObjectFactory
    {
        ILanguageFilesList Fetch(XPathNavigator xpathNavigator);
    }
}
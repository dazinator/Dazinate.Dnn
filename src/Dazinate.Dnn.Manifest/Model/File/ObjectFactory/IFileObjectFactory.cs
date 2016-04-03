using System.Xml.XPath;

namespace Dazinate.Dnn.Manifest.Model.File.ObjectFactory
{
    public interface IFileObjectFactory
    {
        IFile Fetch(XPathNavigator xpathNavigator);
    }
}
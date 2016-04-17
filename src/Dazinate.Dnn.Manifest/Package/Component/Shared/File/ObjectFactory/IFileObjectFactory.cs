using System.Xml.XPath;

namespace Dazinate.Dnn.Manifest.Package.Component.Shared.File.ObjectFactory
{
    public interface IFileObjectFactory
    {
        IFile Fetch(XPathNavigator xpathNavigator);
    }
}
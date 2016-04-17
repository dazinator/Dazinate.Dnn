using System.Xml.XPath;

namespace Dazinate.Dnn.Manifest.Package.Component.ResourceFile.ObjectFactory
{
    public interface IResourceFileObjectFactory
    {
        IResourceFile Fetch(XPathNavigator xpathNavigator);
    }
}
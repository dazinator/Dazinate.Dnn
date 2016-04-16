using System.Xml.XPath;
using Dazinate.Dnn.Manifest.Model.Component;

namespace Dazinate.Dnn.Manifest.Model.ResourceFile.ObjectFactory
{
    public interface IResourceFileObjectFactory
    {
        IResourceFile Fetch(XPathNavigator xpathNavigator);
    }
}
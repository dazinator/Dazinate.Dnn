using System.Xml.XPath;
using Dazinate.Dnn.Manifest.Model.File;

namespace Dazinate.Dnn.Manifest.Model.ContainerFile.ObjectFactory
{
    public interface IContainerFileObjectFactory
    {
        IContainerFile Fetch(XPathNavigator xpathNavigator);
    }
}
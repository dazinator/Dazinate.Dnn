using System.Runtime.InteropServices;
using System.Xml.XPath;

namespace Dazinate.Dnn.Manifest.Model.SupportedFeature.ObjectFactory
{
    public interface ISupportedFeatureObjectFactory
    {
        ISupportedFeature Fetch(XPathNavigator xpathNavigator);
    }
}
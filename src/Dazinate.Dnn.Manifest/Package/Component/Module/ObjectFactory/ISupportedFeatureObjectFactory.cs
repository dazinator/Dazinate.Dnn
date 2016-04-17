using System.Xml.XPath;

namespace Dazinate.Dnn.Manifest.Package.Component.Module.ObjectFactory
{
    public interface ISupportedFeatureObjectFactory
    {
        ISupportedFeature Fetch(XPathNavigator xpathNavigator);
    }
}
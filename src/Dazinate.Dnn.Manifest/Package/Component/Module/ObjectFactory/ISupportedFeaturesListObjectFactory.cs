using System.Xml.XPath;

namespace Dazinate.Dnn.Manifest.Package.Component.Module.ObjectFactory
{
    public interface ISupportedFeaturesListObjectFactory
    {
        ISupportedFeaturesList Fetch(XPathNavigator xpathNavigator);
        ISupportedFeaturesList Create();
    }
}
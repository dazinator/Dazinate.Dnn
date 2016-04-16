using System.Xml.XPath;

namespace Dazinate.Dnn.Manifest.Model.SupportedFeaturesList.ObjectFactory
{
    public interface ISupportedFeaturesListObjectFactory
    {
        ISupportedFeaturesList Fetch(XPathNavigator xpathNavigator);
    }
}
using System.Xml.XPath;
using Dazinate.Dnn.Manifest.Base;
using Dazinate.Dnn.Manifest.Exceptions;
using Dazinate.Dnn.Manifest.Ioc;
using Dazinate.Dnn.Manifest.Utils;

namespace Dazinate.Dnn.Manifest.Package.Component.Module.ObjectFactory
{
    public class SupportedFeatureObjectFactory : BaseObjectFactory, ISupportedFeatureObjectFactory
    {

        public SupportedFeatureObjectFactory(IObjectActivator activator) : base(activator)
        {
            //_packagesListFactory = packagesListFactory;
        }

        public ISupportedFeature Fetch(XPathNavigator nav)
        {
            // Create the correct concrete dependency based on the xml.
            var businessObject = CreateInstance<SupportedFeature>();

            var type = XmlUtils.ReadAttribute(nav, "type");
            SupportedFeatureType featureType;
            var parsed = System.Enum.TryParse(type, out featureType);
            if (!parsed)
            {
                throw new InvalidManifestFormatException();
            }

            LoadProperty(businessObject, SupportedFeature.FeatureTypeProperty, featureType);

            MarkAsChild(businessObject);
            MarkOld(businessObject);
            CheckRules(businessObject);
            return businessObject;
        }
    }
}
using System.Xml.XPath;
using Dazinate.Dnn.Manifest.Base;
using Dazinate.Dnn.Manifest.Ioc;

namespace Dazinate.Dnn.Manifest.Package.Component.Module.ObjectFactory
{
    public class SupportedFeaturesListObjectFactory : BaseObjectFactory, ISupportedFeaturesListObjectFactory
    {
        private readonly ISupportedFeatureObjectFactory _featureObjectFactory;

        public SupportedFeaturesListObjectFactory(IObjectActivator activator, ISupportedFeatureObjectFactory featureObjectFactory) : base(activator)
        {
            _featureObjectFactory = featureObjectFactory;
        }

        public ISupportedFeaturesList Create()
        {
            var businessObject = CreateInstance<SupportedFeaturesList>();
            MarkNew(businessObject);
            MarkAsChild(businessObject);
            return businessObject;
        }

        public ISupportedFeaturesList Fetch(XPathNavigator xpathNavigator)
        {
            //  var packagesNav = xpathNavigator.Select("packages/package");

            var list = CreateInstance<SupportedFeaturesList>();
            list.RaiseListChangedEvents = false;

            if (xpathNavigator != null)
            {
                // loop through packages.
                foreach (XPathNavigator itemNav in xpathNavigator.Select("supportedFeature"))
                {
                    LoadFileItem(itemNav, list);
                }
            }

            list.RaiseListChangedEvents = true;

            MarkOld(list);
            MarkAsChild(list);
            CheckRules(list);
            return list;
        }


        private void LoadFileItem(XPathNavigator nav, ISupportedFeaturesList list)
        {
            var item = _featureObjectFactory.Fetch(nav);
            list.Add(item);
        }

    }
}
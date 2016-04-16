using System.Xml.XPath;
using Dazinate.Dnn.Manifest.Ioc;
using Dazinate.Dnn.Manifest.Model.SupportedFeature.ObjectFactory;

namespace Dazinate.Dnn.Manifest.Model.SupportedFeaturesList.ObjectFactory
{
    public class SupportedFeaturesListObjectFactory : BaseObjectFactory, ISupportedFeaturesListObjectFactory
    {
        private readonly ISupportedFeatureObjectFactory _featureObjectFactory;

        public SupportedFeaturesListObjectFactory(IObjectActivator activator, ISupportedFeatureObjectFactory featureObjectFactory) : base(activator)
        {
            _featureObjectFactory = featureObjectFactory;
        }

        public ISupportedFeaturesList Fetch(XPathNavigator xpathNavigator)
        {
            //  var packagesNav = xpathNavigator.Select("packages/package");

            var list = CreateInstance<SupportedFeaturesList>();
            list.RaiseListChangedEvents = false;

            // loop through packages.
            foreach (XPathNavigator itemNav in xpathNavigator.Select("supportedFeature"))
            {
                LoadFileItem(itemNav, list);
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
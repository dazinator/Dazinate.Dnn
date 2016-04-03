using System.Xml.XPath;
using Dazinate.Dnn.Manifest.Ioc;
using Dazinate.Dnn.Manifest.Utils;

namespace Dazinate.Dnn.Manifest.Model.Component.SubObjectFactory
{
    public class AuthenticationSystemSubObjectFactory : BaseObjectFactory, IComponentSubObjectFactory
    {

        public AuthenticationSystemSubObjectFactory(IObjectActivator activator) : base(activator)
        {
            //_packagesListFactory = packagesListFactory;
        }

        public string ComponentTypeName { get { return "AuthenticationSystem"; } }

        public IComponent Fetch(XPathNavigator nav)
        {
            // Create the correct concrete dependency based on the xml.
            var businessObject = CreateInstance<AuthenticationSystemComponent>();

            var authServiceNav = nav.SelectSingleNode("authenticationService");

            var type = XmlUtils.ReadElement(authServiceNav, "type").ToLowerInvariant();
            var settingsControlSrc = XmlUtils.ReadElement(authServiceNav, "settingsControlSrc").ToLowerInvariant();
            var loginControlSrc = XmlUtils.ReadElement(authServiceNav, "loginControlSrc").ToLowerInvariant();
            var logoffControlSrc = XmlUtils.ReadElement(authServiceNav, "logoffControlSrc").ToLowerInvariant();

            LoadProperty(businessObject, AuthenticationSystemComponent.TypeProperty, type);
            LoadProperty(businessObject, AuthenticationSystemComponent.SettingsControlSourceProperty, settingsControlSrc);
            LoadProperty(businessObject, AuthenticationSystemComponent.LoginControlSourceProperty, loginControlSrc);
            LoadProperty(businessObject, AuthenticationSystemComponent.LogoffControlSourceProperty, logoffControlSrc);

            MarkAsChild(businessObject);
            MarkOld(businessObject);
            CheckRules(businessObject);
            return businessObject;
        }
    }
}
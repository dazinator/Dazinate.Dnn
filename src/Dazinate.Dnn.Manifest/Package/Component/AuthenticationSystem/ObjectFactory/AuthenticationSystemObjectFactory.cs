using System;
using System.Xml.XPath;
using Dazinate.Dnn.Manifest.Base;
using Dazinate.Dnn.Manifest.Ioc;
using Dazinate.Dnn.Manifest.Package.Component.ObjectFactory;
using Dazinate.Dnn.Manifest.Utils;

namespace Dazinate.Dnn.Manifest.Package.Component.AuthenticationSystem.ObjectFactory
{
    public class AuthenticationSystemSubObjectFactory : BaseObjectFactory, IComponentSubObjectFactory
    {

        public AuthenticationSystemSubObjectFactory(IObjectActivator activator) : base(activator)
        {
            //_packagesListFactory = packagesListFactory;
        }

        public ComponentType ComponentType
        {
            get
            {
                return ComponentType.AuthenticationSystem;
            }
        }

     

        public IComponent Create(ComponentType componentType)
        {
            var component = CreateInstance<AuthenticationSystemComponent>();
            MarkAsChild(component);
            MarkNew(component);
            return component;
        }

        public IComponent Fetch(XPathNavigator nav)
        {
            // Create the correct concrete dependency based on the xml.
            var businessObject = CreateInstance<AuthenticationSystemComponent>();

            var authServiceNav = nav.SelectSingleNode("authenticationService");

            var type = XmlUtils.ReadElement(authServiceNav, "type");
            var settingsControlSrc = XmlUtils.ReadElement(authServiceNav, "settingsControlSrc");
            var loginControlSrc = XmlUtils.ReadElement(authServiceNav, "loginControlSrc");
            var logoffControlSrc = XmlUtils.ReadElement(authServiceNav, "logoffControlSrc");

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
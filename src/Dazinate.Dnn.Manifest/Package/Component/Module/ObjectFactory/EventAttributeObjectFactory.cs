using System.Xml.XPath;
using Dazinate.Dnn.Manifest.Base;
using Dazinate.Dnn.Manifest.Ioc;

namespace Dazinate.Dnn.Manifest.Package.Component.Module.ObjectFactory
{
    public class EventAttributeObjectFactory : BaseObjectFactory, IEventAttributeObjectFactory
    {

        public EventAttributeObjectFactory(IObjectActivator activator) : base(activator)
        {
            //_packagesListFactory = packagesListFactory;
        }

        public IEventAttribute Fetch(XPathNavigator nav)
        {
            // Create the correct concrete dependency based on the xml.
            var businessObject = CreateInstance<EventAttribute>();

            var name = nav.LocalName;
            var value = nav.Value;
       
            LoadProperty(businessObject, EventAttribute.NameProperty, name);
            LoadProperty(businessObject, EventAttribute.ValueProperty, value);

            MarkAsChild(businessObject);
            MarkOld(businessObject);
            CheckRules(businessObject);
            return businessObject;
        }
    }
}
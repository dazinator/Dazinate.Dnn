using System.Xml.XPath;
using Dazinate.Dnn.Manifest.Ioc;
using Dazinate.Dnn.Manifest.Model.EventAttributesList.ObjectFactory;
using Dazinate.Dnn.Manifest.Utils;

namespace Dazinate.Dnn.Manifest.Model.EventMessage.ObjectFactory
{
    public class EventMessageObjectFactory : BaseObjectFactory, IEventMessageObjectFactory
    {

        private readonly IEventAttributesListObjectFactory _listFactory;

        public EventMessageObjectFactory(IObjectActivator activator, IEventAttributesListObjectFactory listFactory) : base(activator)
        {
            _listFactory = listFactory;
        }

        public IEventMessage Fetch(XPathNavigator nav)
        {
            // Create the correct concrete dependency based on the xml.
            var businessObject = CreateInstance<EventMessage>();

            var processorType = XmlUtils.ReadElement(nav, "processorType");
            LoadProperty(businessObject, EventMessage.ProcessorTypeProperty, processorType);

            var processorCommand = XmlUtils.ReadElement(nav, "processorCommand");
            LoadProperty(businessObject, EventMessage.ProcessorCommandProperty, processorCommand);

            var attributesNode = nav.SelectSingleNode("attributes");
            var list = _listFactory.Fetch(attributesNode);
          
            LoadProperty(businessObject, EventMessage.AttributesProperty, list);

            MarkAsChild(businessObject);
            MarkOld(businessObject);
            CheckRules(businessObject);
            return businessObject;
        }
    }
}
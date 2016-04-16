using System;
using Csla;
using Csla.Server;
using Dazinate.Dnn.Manifest.Model.EventAttributesList;
using Dazinate.Dnn.Manifest.Model.EventMessage.ObjectFactory;
using Dazinate.Dnn.Manifest.Writer;

namespace Dazinate.Dnn.Manifest.Model.EventMessage
{
    [ObjectFactory(typeof(IEventMessageObjectFactory))]
    [Serializable]
    public class EventMessage : BusinessBase<EventMessage>, IEventMessage
    {
        public static readonly PropertyInfo<string> ProcessorTypeProperty = RegisterProperty<string>(c => c.ProcessorType);
        public string ProcessorType
        {
            get { return GetProperty(ProcessorTypeProperty); }
            set { SetProperty(ProcessorTypeProperty, value); }
        }

        public static readonly PropertyInfo<string> ProcessorCommandProperty = RegisterProperty<string>(c => c.ProcessorCommand);
        public string ProcessorCommand
        {
            get { return GetProperty(ProcessorCommandProperty); }
            set { SetProperty(ProcessorCommandProperty, value); }
        }

        public static readonly PropertyInfo<EventAttributesList.EventAttributesList> AttributesProperty = RegisterProperty<EventAttributesList.EventAttributesList>(c => c.Attributes);
        public IEventAttributesList Attributes
        {
            get { return GetProperty(AttributesProperty); }
            set { SetProperty(AttributesProperty, value); }
        }


        public void Accept(IManifestXmlWriterVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
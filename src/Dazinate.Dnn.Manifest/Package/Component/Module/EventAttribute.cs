using System;
using Csla;
using Csla.Server;
using Dazinate.Dnn.Manifest.Package.Component.Module.ObjectFactory;
using Dazinate.Dnn.Manifest.Writer;

namespace Dazinate.Dnn.Manifest.Package.Component.Module
{
    [ObjectFactory(typeof(IEventAttributeObjectFactory))]
    [Serializable]
    public class EventAttribute : BusinessBase<EventAttribute>, IEventAttribute
    {
        public static readonly PropertyInfo<string> NameProperty = RegisterProperty<string>(c => c.Name);
        public string Name
        {
            get { return GetProperty(NameProperty); }
            set { SetProperty(NameProperty, value); }
        }

        public static readonly PropertyInfo<string> ValueProperty = RegisterProperty<string>(c => c.Value);
        public string Value
        {
            get { return GetProperty(ValueProperty); }
            set { SetProperty(ValueProperty, value); }
        }

        public void Accept(IManifestXmlWriterVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
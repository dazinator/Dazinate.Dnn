using System;
using Csla;
using Csla.Server;
using Dazinate.Dnn.Manifest.Model.EventAttribute;
using Dazinate.Dnn.Manifest.Writer;

namespace Dazinate.Dnn.Manifest.Model.EventAttributesList
{
    [ObjectFactory(typeof(IEventAttributesListObjectFactory))]
    [Serializable]
    public class EventAttributesList : BusinessListBase<EventAttributesList, IEventAttribute>, IEventAttributesList
    {
        // private readonly IPackageFactory _factory;

        public EventAttributesList()
        {
        }

        public void Accept(IManifestXmlWriterVisitor visitor)
        {
            visitor.Visit(this);
        }

    }
}
using System;
using Csla;
using Csla.Server;
using Dazinate.Dnn.Manifest.Base;
using Dazinate.Dnn.Manifest.Package.Component.Module.ObjectFactory;

namespace Dazinate.Dnn.Manifest.Package.Component.Module
{
    [ObjectFactory(typeof(IEventAttributesListObjectFactory))]
    [Serializable]
    public class EventAttributesList : BusinessListBase<EventAttributesList, IEventAttribute>, IEventAttributesList
    {
        // private readonly IPackageFactory _factory;

        public EventAttributesList()
        {
        }

        public void Accept(IManifestVisitor visitor)
        {
            visitor.Visit(this);
        }

    }
}
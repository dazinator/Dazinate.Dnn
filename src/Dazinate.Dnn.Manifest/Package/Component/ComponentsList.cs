using System;
using Csla;
using Csla.Server;
using Dazinate.Dnn.Manifest.Package.Component.ObjectFactory;
using Dazinate.Dnn.Manifest.Writer;

namespace Dazinate.Dnn.Manifest.Package.Component
{
    [ObjectFactory(typeof(IComponentsListObjectFactory))]
    [Serializable]
    public class ComponentsList : BusinessListBase<ComponentsList, IComponent>, IComponentsList
    {
        public ComponentsList()
        {
        }

        public void Accept(IManifestXmlWriterVisitor visitor)
        {
            visitor.Visit(this);
        }

    }
}
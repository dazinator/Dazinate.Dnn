using System;
using Csla;
using Csla.Server;
using Dazinate.Dnn.Manifest.Model.Component;
using Dazinate.Dnn.Manifest.Model.ComponentsList.ObjectFactory;
using Dazinate.Dnn.Manifest.Writer;

namespace Dazinate.Dnn.Manifest.Model.ComponentsList
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
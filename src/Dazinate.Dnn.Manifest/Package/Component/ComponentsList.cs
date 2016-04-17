using System;
using Csla;
using Csla.Server;
using Dazinate.Dnn.Manifest.Base;
using Dazinate.Dnn.Manifest.Package.Component.ObjectFactory;

namespace Dazinate.Dnn.Manifest.Package.Component
{
    [ObjectFactory(typeof(IComponentsListObjectFactory))]
    [Serializable]
    public class ComponentsList : BusinessListBase<ComponentsList, IComponent>, IComponentsList
    {
        public ComponentsList()
        {
        }

        public void Accept(IManifestVisitor visitor)
        {
            visitor.Visit(this);
        }

    }
}
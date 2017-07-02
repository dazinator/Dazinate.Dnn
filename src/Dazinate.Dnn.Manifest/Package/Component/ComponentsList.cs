using System;
using Csla;
using Csla.Server;
using Dazinate.Dnn.Manifest.Base;
using Dazinate.Dnn.Manifest.Package.Component.ObjectFactory;
using Dazinate.Dnn.Manifest.Package.Component.Assembly;
using Dazinate.Dnn.Manifest.Factory;

namespace Dazinate.Dnn.Manifest.Package.Component
{
    [ObjectFactory(typeof(IComponentsListObjectFactory))]
    [Serializable]
    public class ComponentsList : BusinessListBase<ComponentsList, IComponent>, IComponentsList
    {

        private readonly IComponentFactory _componentFactory;

        public ComponentsList() : this(new ComponentFactory())
        {
        }

        public ComponentsList(IComponentFactory factory)
        {
            _componentFactory = factory;
        }

        public void Accept(IManifestVisitor visitor)
        {
            visitor.Visit(this);
        }

        public IAssemblyComponent AddNewAssemblyComponent()
        {
            var component = _componentFactory.CreateNewAssemblyComponent();
            this.Add(component);
            return component;
        }

    }
}
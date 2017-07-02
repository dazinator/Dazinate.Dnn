using System;
using System.Xml.XPath;
using Dazinate.Dnn.Manifest.Base;
using Dazinate.Dnn.Manifest.Ioc;
using Dazinate.Dnn.Manifest.Package.Component.ObjectFactory;

namespace Dazinate.Dnn.Manifest.Package.Component.DashboardControl.ObjectFactory
{
    public class DashboardControlComponentSubObjectFactory : BaseObjectFactory, IComponentSubObjectFactory
    {

        private readonly IDashboardControlsListObjectFactory _controlsListFactory;

        public DashboardControlComponentSubObjectFactory(IObjectActivator activator, IDashboardControlsListObjectFactory controlsListFactory) : base(activator)
        {
            _controlsListFactory = controlsListFactory;
        }

        public ComponentType ComponentType
        {
            get
            {
                return ComponentType.DashboardControl;
            }
        }


        public IComponent Create(ComponentType componentType)
        {
            var component = CreateInstance<DashboardControlComponent>();
            MarkAsChild(component);
            MarkNew(component);
            return component;
        }

        public IComponent Fetch(XPathNavigator nav)
        {
            var component = CreateInstance<DashboardControlComponent>();

            var list = _controlsListFactory.Fetch(nav);
            LoadProperty(component, DashboardControlComponent.DashboardControlsProperty, list);


            return component;

        }


    }
}
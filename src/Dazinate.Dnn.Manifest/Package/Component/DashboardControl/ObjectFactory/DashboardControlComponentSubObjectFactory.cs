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

        public string ComponentTypeName { get { return "DashboardControl"; } }

        public IComponent Fetch(XPathNavigator nav)
        {
            var component = CreateInstance<DashboardControlComponent>();
            
            var list = _controlsListFactory.Fetch(nav);
            LoadProperty(component, DashboardControlComponent.DashboardControlsProperty, list);


            return component;

        }


    }
}
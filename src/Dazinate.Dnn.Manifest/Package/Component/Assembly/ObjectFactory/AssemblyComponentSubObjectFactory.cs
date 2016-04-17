using System.Xml.XPath;
using Dazinate.Dnn.Manifest.Base;
using Dazinate.Dnn.Manifest.Ioc;
using Dazinate.Dnn.Manifest.Package.Component.ObjectFactory;

namespace Dazinate.Dnn.Manifest.Package.Component.Assembly.ObjectFactory
{
    public class AssemblyComponentSubObjectFactory : BaseObjectFactory, IComponentSubObjectFactory
    {

        private readonly IAssembliesListObjectFactory _assembliesListObjectFactory;

        public AssemblyComponentSubObjectFactory(IObjectActivator activator, IAssembliesListObjectFactory assembliesListObjectFactory) : base(activator)
        {
            _assembliesListObjectFactory = assembliesListObjectFactory;
        }

        public string ComponentTypeName { get { return "Assembly"; } }

        public IComponent Fetch(XPathNavigator nav)
        {
            var component = CreateInstance<AssemblyComponent>();

            var list = _assembliesListObjectFactory.Fetch(nav);
            LoadProperty(component, AssemblyComponent.AssembliesListProperty, list);

            return component;
            //var type = XmlUtils.ReadAttribute(nav, "type");
            //LoadProperty(dep, CustomDependency.TypeProperty, type);

            //var value = nav.Value;
            //LoadProperty(dep, CustomDependency.ValueProperty, value);
            //return dep;
        }


    }
}
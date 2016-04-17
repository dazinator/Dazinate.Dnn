using System.Xml.XPath;
using Dazinate.Dnn.Manifest.Base;
using Dazinate.Dnn.Manifest.Ioc;
using Dazinate.Dnn.Manifest.Package.Component.ObjectFactory;

namespace Dazinate.Dnn.Manifest.Package.Component.Provider.ObjectFactory
{
    public class ProviderComponentSubObjectFactory : BaseObjectFactory, IComponentSubObjectFactory
    {
        public ProviderComponentSubObjectFactory(IObjectActivator activator) : base(activator)
        {
            
        }

        public string ComponentTypeName { get { return "Provider"; } }

        public IComponent Fetch(XPathNavigator nav)
        {
            var component = CreateInstance<ProviderComponent>();
            return component;
        }


    }
}
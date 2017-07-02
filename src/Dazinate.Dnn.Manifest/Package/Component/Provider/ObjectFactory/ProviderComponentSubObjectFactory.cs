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

        public ComponentType ComponentType
        {
            get
            {
                return ComponentType.Provider;
            }
        }


        public IComponent Create(ComponentType componentType)
        {
            var component = CreateInstance<ProviderComponent>();
            MarkAsChild(component);
            MarkNew(component);
            return component;
        }

        public IComponent Fetch(XPathNavigator nav)
        {
            var component = CreateInstance<ProviderComponent>();
            return component;
        }


    }
}
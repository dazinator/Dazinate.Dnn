using System.Xml.XPath;
using Dazinate.Dnn.Manifest.Ioc;

namespace Dazinate.Dnn.Manifest.Model.Component.SubObjectFactory
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
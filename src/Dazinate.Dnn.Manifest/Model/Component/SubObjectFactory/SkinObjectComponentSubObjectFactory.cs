using System.Xml.XPath;
using Dazinate.Dnn.Manifest.Ioc;
using Dazinate.Dnn.Manifest.Model.Manifest;
using Dazinate.Dnn.Manifest.Utils;

namespace Dazinate.Dnn.Manifest.Model.Component.SubObjectFactory
{
    public class SkinObjectComponentSubObjectFactory : BaseObjectFactory, IComponentSubObjectFactory
    {
        public SkinObjectComponentSubObjectFactory(IObjectActivator activator) : base(activator)
        {

        }

        public string ComponentTypeName { get { return "SkinObject"; } }

        public IComponent Fetch(XPathNavigator nav)
        {
            var component = CreateInstance<SkinObjectComponent>();
            var node = nav.SelectSingleNode("moduleControl");
            if (node == null)
            {
                throw new InvalidManifestFormatException();
            }

            var controlKey = XmlUtils.ReadElement(node, "controlKey");
            LoadProperty(component, SkinObjectComponent.ControlKeyProperty, controlKey);

            var controlSrc = XmlUtils.ReadElement(node, "controlSrc");
            LoadProperty(component, SkinObjectComponent.ControlSourceProperty, controlSrc);

            var supportsPartialRendering = XmlUtils.ReadElement(node, "supportsPartialRendering");
            var boolValue = bool.Parse(supportsPartialRendering);
            LoadProperty(component, SkinObjectComponent.SupportsPartialRenderingProperty, boolValue);
            
            return component;
        }


    }
}
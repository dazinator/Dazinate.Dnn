using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.XPath;
using Dazinate.Dnn.Manifest.Ioc;
using Dazinate.Dnn.Manifest.Model.Component.SubObjectFactory;
using Dazinate.Dnn.Manifest.Utils;

namespace Dazinate.Dnn.Manifest.Model.Component.ObjectFactory
{
    public class ComponentObjectFactory : BaseObjectFactory, IComponentObjectFactory
    {

        private readonly IList<IComponentSubObjectFactory> _subFactories;

        public ComponentObjectFactory(IObjectActivator activator, IEnumerable<IComponentSubObjectFactory> subFactories) : base(activator)
        {
            _subFactories = subFactories.ToArray();
        }

        public IComponent Fetch(XPathNavigator nav)
        {
            // Create the correct concrete dependency based on the xml.

            var componentType = XmlUtils.ReadRequiredAttribute(nav, "type").ToLowerInvariant();
            IComponentSubObjectFactory subFactory = ResolveSubFactory(componentType);

            IComponent component = subFactory.Fetch(nav);

            MarkAsChild(component);
            MarkOld(component);
            CheckRules(component);
            return component;
        }

        private IComponentSubObjectFactory ResolveSubFactory(string componentType)
        {
            var subFactory =
                _subFactories.FirstOrDefault(
                    a => a.ComponentTypeName.ToLowerInvariant() == componentType.ToLowerInvariant());
            if (subFactory == null)
            {
                throw new Exception("Unsupported component type in manifest: " + componentType);
            }

            return subFactory;

        }


    }
}
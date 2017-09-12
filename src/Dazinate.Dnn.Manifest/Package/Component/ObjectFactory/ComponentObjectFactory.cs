using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.XPath;
using Dazinate.Dnn.Manifest.Base;
using Dazinate.Dnn.Manifest.Ioc;
using Dazinate.Dnn.Manifest.Utils;
using Dazinate.Dnn.Manifest.Factory;

namespace Dazinate.Dnn.Manifest.Package.Component.ObjectFactory
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

            var componentTypeName = XmlUtils.ReadRequiredAttribute(nav, "type");
            var componentType = (ComponentType)Enum.Parse(typeof(ComponentType), componentTypeName);

            IComponentSubObjectFactory subFactory = ResolveSubFactory(componentType);

            IComponent component = subFactory.Fetch(nav);

            MarkAsChild(component);
            MarkOld(component);
            CheckRules(component);
            return component;
        }

        private IComponentSubObjectFactory ResolveSubFactory(ComponentType componentType)
        {
            var subFactory =
                _subFactories.FirstOrDefault(
                    a => a.ComponentType == componentType);
            if (subFactory == null)
            {
                throw new Exception("Unsupported component type in manifest: " + componentType);
            }

            return subFactory;

        }

        private IComponent Create(ComponentType componentType)
        {
            var subFactory =
                _subFactories.FirstOrDefault(
                    a => a.ComponentType == componentType);
            if (subFactory == null)
            {
                throw new Exception("Unsupported component type in manifest: " + componentType);
            }

            IComponent newInstance = (IComponent)subFactory.Create(componentType);

            return newInstance;

        }


    }
}
using System;
using System.Xml.XPath;
using Dazinate.Dnn.Manifest.Base;
using Dazinate.Dnn.Manifest.Exceptions;
using Dazinate.Dnn.Manifest.Ioc;
using Dazinate.Dnn.Manifest.Utils;

namespace Dazinate.Dnn.Manifest.Package.Component.Config.ObjectFactory
{
    public class NodeObjectFactory : BaseObjectFactory, INodeObjectFactory
    {

        public NodeObjectFactory(IObjectActivator activator) : base(activator)
        {
            //_packagesListFactory = packagesListFactory;
        }

        public INode Fetch(XPathNavigator nav)
        {
            // Create the correct concrete dependency based on the xml.
            var businessObject = CreateInstance<Node>();

            var path = XmlUtils.ReadAttribute(nav, "path");
            LoadProperty(businessObject, Node.PathProperty, path);


            var action = XmlUtils.ReadAttribute(nav, "action");
            if (!string.IsNullOrWhiteSpace(action))
            {
                NodeAction actionEnum;
                if (Enum.TryParse(action, true, out actionEnum))
                {
                    LoadProperty(businessObject, Node.ActionProperty, actionEnum);
                }
                else
                {
                    throw new InvalidManifestFormatException();
                }
            }
            else
            {
                LoadProperty(businessObject, Node.ActionProperty, null);
            }

            var collision = XmlUtils.ReadAttribute(nav, "collision");
            if (!string.IsNullOrWhiteSpace(collision))
            {
                NodeCollision collisionEnum;
                if (Enum.TryParse(collision, true, out collisionEnum))
                {
                    LoadProperty(businessObject, Node.CollisionProperty, collisionEnum);
                }
                else
                {
                    throw new InvalidManifestFormatException();
                }
            }
            else
            {
                LoadProperty(businessObject, Node.CollisionProperty, null);
            }


            var targetpath = XmlUtils.ReadAttribute(nav, "targetpath");
            if (!string.IsNullOrWhiteSpace(targetpath))
            {
                LoadProperty(businessObject, Node.TargetPathProperty, targetpath);
            }

            var key = XmlUtils.ReadAttribute(nav, "key");
            if (!string.IsNullOrWhiteSpace(key))
            {
                LoadProperty(businessObject, Node.KeyProperty, key);
            }

            var name = XmlUtils.ReadAttribute(nav, "name");
            if (!string.IsNullOrWhiteSpace(name))
            {
                LoadProperty(businessObject, Node.NameProperty, name);
            }

            var value = XmlUtils.ReadAttribute(nav, "value");
            if (!string.IsNullOrWhiteSpace(value))
            {
                LoadProperty(businessObject, Node.ValueProperty, value);
            }

            var namespaceValue = XmlUtils.ReadAttribute(nav, "nameSpace");
            if (!string.IsNullOrWhiteSpace(namespaceValue))
            {
                LoadProperty(businessObject, Node.NamespaceProperty, namespaceValue);
            }

            var namespacePrefix = XmlUtils.ReadAttribute(nav, "nameSpacePrefix");
            if (!string.IsNullOrWhiteSpace(namespacePrefix))
            {
                LoadProperty(businessObject, Node.NamespaceProperty, namespacePrefix);
            }


            LoadProperty(businessObject, Node.InnerXmlProperty, nav.InnerXml);


            MarkAsChild(businessObject);
            MarkOld(businessObject);
            CheckRules(businessObject);
            return businessObject;
        }
    }
}
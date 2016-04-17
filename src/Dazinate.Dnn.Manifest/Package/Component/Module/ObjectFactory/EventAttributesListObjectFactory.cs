using System.Xml.XPath;
using Dazinate.Dnn.Manifest.Base;
using Dazinate.Dnn.Manifest.Ioc;

namespace Dazinate.Dnn.Manifest.Package.Component.Module.ObjectFactory
{
    public class EventAttributesListObjectFactory : BaseObjectFactory, IEventAttributesListObjectFactory
    {
        private readonly IEventAttributeObjectFactory _attributeObjectFactory;

        public EventAttributesListObjectFactory(IObjectActivator activator, IEventAttributeObjectFactory attributeObjectFactory) : base(activator)
        {
            _attributeObjectFactory = attributeObjectFactory;
        }

        public IEventAttributesList Fetch(XPathNavigator xpathNavigator)
        {
            //  var packagesNav = xpathNavigator.Select("packages/package");

            var list = CreateInstance<EventAttributesList>();
            list.RaiseListChangedEvents = false;

            // loop through packages.
            foreach (XPathNavigator dependencyNav in xpathNavigator.SelectChildren(XPathNodeType.Element))
            {
                LoadFileItem(dependencyNav, list);
            }

            list.RaiseListChangedEvents = true;

            MarkOld(list);
            MarkAsChild(list);
            CheckRules(list);
            return list;
        }


        private void LoadFileItem(XPathNavigator nav, IEventAttributesList list)
        {
            var item = _attributeObjectFactory.Fetch(nav);
            list.Add(item);
        }

    }
}
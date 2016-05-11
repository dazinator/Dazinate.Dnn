using System.Xml.XPath;
using Dazinate.Dnn.Manifest.Base;
using Dazinate.Dnn.Manifest.Ioc;

namespace Dazinate.Dnn.Manifest.Package.Component.ObjectFactory
{
    public class ComponentsListObjectFactory : BaseObjectFactory, IComponentsListObjectFactory
    {
        private readonly IComponentObjectFactory _componentFactory;

        public ComponentsListObjectFactory(IObjectActivator activator, IComponentObjectFactory componentFactory) : base(activator)
        {
            _componentFactory = componentFactory;
        }

        public IComponentsList Fetch(XPathNavigator xpathNavigator)
        {
            //  var packagesNav = xpathNavigator.Select("packages/package");

            var list = CreateInstance<ComponentsList>();
            list.RaiseListChangedEvents = false;

            if (xpathNavigator != null)
            {
                foreach (XPathNavigator nav in xpathNavigator.Select("components/component"))
                {
                    LoadComponent(nav, list);
                }
            }

            list.RaiseListChangedEvents = true;
          
            MarkOld(list);
            MarkAsChild(list);
            CheckRules(list);
            return list;
        }


        private void LoadComponent(XPathNavigator nav, IComponentsList list)
        {
            var item = _componentFactory.Fetch(nav);
            list.Add(item);
        }

        public IComponentsList Create()
        {
            var list = CreateInstance<ComponentsList>();
            MarkAsChild(list);
            CheckRules(list);
            return list;
        }

    }
}
using System.Xml.XPath;
using Dazinate.Dnn.Manifest.Base;
using Dazinate.Dnn.Manifest.Ioc;

namespace Dazinate.Dnn.Manifest.Package.Component.Module.ObjectFactory
{
    public class ModuleDefinitionsListObjectFactory : BaseObjectFactory, IModuleDefinitionsListObjectFactory
    {
        private readonly IModuleDefinitionObjectFactory _definitionObjectFactory;

        public ModuleDefinitionsListObjectFactory(IObjectActivator activator, IModuleDefinitionObjectFactory definitionObjectFactory) : base(activator)
        {
            _definitionObjectFactory = definitionObjectFactory;
        }
        public IModuleDefinitionsList Create()
        {
            var list = CreateInstance<ModuleDefinitionsList>();
            MarkNew(list);
            MarkAsChild(list);
            return list;
        }

        public IModuleDefinitionsList Fetch(XPathNavigator xpathNavigator)
        {
            //  var packagesNav = xpathNavigator.Select("packages/package");

            var list = CreateInstance<ModuleDefinitionsList>();
            list.RaiseListChangedEvents = false;

            if (xpathNavigator != null)
            {
                foreach (XPathNavigator itemNav in xpathNavigator.Select("moduleDefinition"))
                {
                    LoadFileItem(itemNav, list);
                }
            }

            list.RaiseListChangedEvents = true;

            MarkOld(list);
            MarkAsChild(list);
            CheckRules(list);
            return list;
        }


        private void LoadFileItem(XPathNavigator nav, IModuleDefinitionsList list)
        {
            var item = _definitionObjectFactory.Fetch(nav);
            list.Add(item);
        }

    }
}
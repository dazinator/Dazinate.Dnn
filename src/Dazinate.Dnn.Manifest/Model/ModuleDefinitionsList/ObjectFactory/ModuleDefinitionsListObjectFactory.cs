using System.Xml.XPath;
using Dazinate.Dnn.Manifest.Ioc;
using Dazinate.Dnn.Manifest.Model.ModuleDefinition.ObjectFactory;

namespace Dazinate.Dnn.Manifest.Model.ModuleDefinitionsList.ObjectFactory
{
    public class ModuleDefinitionsListObjectFactory : BaseObjectFactory, IModuleDefinitionsListObjectFactory
    {
        private readonly IModuleDefinitionObjectFactory _definitionObjectFactory;

        public ModuleDefinitionsListObjectFactory(IObjectActivator activator, IModuleDefinitionObjectFactory definitionObjectFactory) : base(activator)
        {
            _definitionObjectFactory = definitionObjectFactory;
        }

        public IModuleDefinitionsList Fetch(XPathNavigator xpathNavigator)
        {
            //  var packagesNav = xpathNavigator.Select("packages/package");

            var list = CreateInstance<ModuleDefinitionsList>();
            list.RaiseListChangedEvents = false;

            // loop through packages.
            foreach (XPathNavigator itemNav in xpathNavigator.Select("moduleDefinition"))
            {
                LoadFileItem(itemNav, list);
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
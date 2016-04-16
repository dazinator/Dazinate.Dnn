using System.Xml.XPath;
using Dazinate.Dnn.Manifest.Ioc;
using Dazinate.Dnn.Manifest.Model.ModuleControl.ObjectFactory;

namespace Dazinate.Dnn.Manifest.Model.ModuleControlsList.ObjectFactory
{
    public class ModuleControlsListObjectFactory : BaseObjectFactory, IModuleControlsListObjectFactory
    {
        private readonly IModuleControlObjectFactory _controlFactory;

        public ModuleControlsListObjectFactory(IObjectActivator activator, IModuleControlObjectFactory controlFactory) : base(activator)
        {
            _controlFactory = controlFactory;
        }

        public IModuleControlsList Fetch(XPathNavigator xpathNavigator)
        {
          
            var list = CreateInstance<ModuleControlsList>();
            list.RaiseListChangedEvents = false;

            // loop through packages.
            foreach (XPathNavigator itemNav in xpathNavigator.Select("moduleControl"))
            {
                LoadFileItem(itemNav, list);
            }

            list.RaiseListChangedEvents = true;

            MarkOld(list);
            MarkAsChild(list);
            CheckRules(list);
            return list;
        }


        private void LoadFileItem(XPathNavigator nav, IModuleControlsList list)
        {
            var item = _controlFactory.Fetch(nav);
            list.Add(item);
        }

    }
}
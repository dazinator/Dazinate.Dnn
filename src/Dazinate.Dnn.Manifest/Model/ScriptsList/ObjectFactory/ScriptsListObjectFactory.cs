using System.Xml.XPath;
using Dazinate.Dnn.Manifest.Ioc;
using Dazinate.Dnn.Manifest.Model.Script.ObjectFactory;

namespace Dazinate.Dnn.Manifest.Model.ScriptsList.ObjectFactory
{
    public class ScriptsListObjectFactory : BaseObjectFactory, IScriptsListObjectFactory
    {
        private readonly IScriptObjectFactory _scriptObjectFactory;

        public ScriptsListObjectFactory(IObjectActivator activator, IScriptObjectFactory scriptObjectFactory) : base(activator)
        {
            _scriptObjectFactory = scriptObjectFactory;
        }

        public IScriptsList Fetch(XPathNavigator xpathNavigator)
        {
            //  var packagesNav = xpathNavigator.Select("packages/package");

            var list = CreateInstance<ScriptsList>();
            list.RaiseListChangedEvents = false;

            // loop through packages.
            foreach (XPathNavigator item in xpathNavigator.Select("scripts/script"))
            {
                LoadFileItem(item, list);
            }

            list.RaiseListChangedEvents = true;

            MarkOld(list);
            MarkAsChild(list);
            CheckRules(list);
            return list;
        }


        private void LoadFileItem(XPathNavigator nav, IScriptsList list)
        {
            var item = _scriptObjectFactory.Fetch(nav);
            list.Add(item);
        }

    }
}
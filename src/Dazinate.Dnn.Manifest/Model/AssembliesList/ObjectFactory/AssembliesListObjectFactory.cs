using System.Xml.XPath;
using Dazinate.Dnn.Manifest.Ioc;
using Dazinate.Dnn.Manifest.Model.Assembly.ObjectFactory;

namespace Dazinate.Dnn.Manifest.Model.AssembliesList.ObjectFactory
{
    public class AssembliesListObjectFactory : BaseObjectFactory, IAssembliesListObjectFactory
    {
        private readonly IAssemblyObjectFactory _assemblyObjectFactory;

        public AssembliesListObjectFactory(IObjectActivator activator, IAssemblyObjectFactory assemblyObjectFactory) : base(activator)
        {
            _assemblyObjectFactory = assemblyObjectFactory;
        }

        public AssembliesList Fetch(XPathNavigator xpathNavigator)
        {
            //  var packagesNav = xpathNavigator.Select("packages/package");

            var list = CreateInstance<AssembliesList>();
            list.RaiseListChangedEvents = false;

            // loop through packages.
            foreach (XPathNavigator dependencyNav in xpathNavigator.Select("assemblies/assembly"))
            {
                LoadAssemblyItem(dependencyNav, list);
            }

            list.RaiseListChangedEvents = true;
            
            MarkOld(list);
            MarkAsChild(list);
            CheckRules(list);
            return list;
        }


        private void LoadAssemblyItem(XPathNavigator nav, AssembliesList list)
        {
            var item = _assemblyObjectFactory.Fetch(nav);
            list.Add(item);
        }
        
    }
}
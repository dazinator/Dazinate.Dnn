using System;
using System.Xml.XPath;
using Dazinate.Dnn.Manifest.Ioc;
using Dazinate.Dnn.Manifest.Utils;

namespace Dazinate.Dnn.Manifest.Model.Assembly.ObjectFactory
{
    public class AssemblyObjectFactory : BaseObjectFactory, IAssemblyObjectFactory
    {

        public AssemblyObjectFactory(IObjectActivator activator) : base(activator)
        {
            //_packagesListFactory = packagesListFactory;
        }

        public IAssembly Fetch(XPathNavigator nav)
        {
            // Create the correct concrete dependency based on the xml.
            var assy = CreateInstance<Assembly>();
            var action = XmlUtils.ReadAttribute(nav, "action");
            AssemblyAction assemblyAction = AssemblyAction.Install;
            if (!string.IsNullOrWhiteSpace(action))
            {
                if (action.ToLowerInvariant() == "unregister")
                {
                    assemblyAction = AssemblyAction.Unregister;
                }
                else
                {
                    throw new Exception("unsupported assembly action: " + action);
                }
            }
            LoadProperty(assy, Assembly.ActionProperty, assemblyAction);

            var path = XmlUtils.ReadElement(nav, "path").ToLowerInvariant();
            LoadProperty(assy, Assembly.PathProperty, path);

            var name = XmlUtils.ReadElement(nav, "name").ToLowerInvariant();
            LoadProperty(assy, Assembly.NameProperty, name);

            var version = XmlUtils.ReadElement(nav, "version").ToLowerInvariant();
            LoadProperty(assy, Assembly.VersionProperty, version);

            MarkAsChild(assy);
            MarkOld(assy);
            CheckRules(assy);
            return assy;
        }
    }
}
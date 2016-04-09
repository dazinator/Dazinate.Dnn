using System.Xml.XPath;
using Dazinate.Dnn.Manifest.Ioc;
using Dazinate.Dnn.Manifest.Model.AssembliesList.ObjectFactory;
using Dazinate.Dnn.Manifest.Model.FilesList.ObjectFactory;
using Dazinate.Dnn.Manifest.Utils;

namespace Dazinate.Dnn.Manifest.Model.Component.SubObjectFactory
{
    public class CleanupComponentSubObjectFactory : BaseObjectFactory, IComponentSubObjectFactory
    {

        private readonly IFilesListObjectFactory _filesListObjectFactory;

        public CleanupComponentSubObjectFactory(IObjectActivator activator, IFilesListObjectFactory filesListObjectFactory) : base(activator)
        {
            _filesListObjectFactory = filesListObjectFactory;
        }

        public string ComponentTypeName { get { return "Cleanup"; } }

        public IComponent Fetch(XPathNavigator nav)
        {
            var component = CreateInstance<CleanupComponent>();

            var version = XmlUtils.ReadAttribute(nav, "version");
            LoadProperty(component, CleanupComponent.VersionProperty, version);

            var fileName = XmlUtils.ReadAttribute(nav, "fileName");
            LoadProperty(component, CleanupComponent.FileNameProperty, fileName);

            var list = _filesListObjectFactory.Fetch(nav);
            LoadProperty(component, CleanupComponent.FilesListProperty, list);

            return component;
          
        }


    }
}
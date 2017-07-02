using System.Xml.XPath;
using Dazinate.Dnn.Manifest.Base;
using Dazinate.Dnn.Manifest.Exceptions;
using Dazinate.Dnn.Manifest.Ioc;
using Dazinate.Dnn.Manifest.Package.Component.ObjectFactory;
using Dazinate.Dnn.Manifest.Utils;

namespace Dazinate.Dnn.Manifest.Package.Component.ResourceFile.ObjectFactory
{
    public class ResourceFileComponentSubObjectFactory : BaseObjectFactory, IComponentSubObjectFactory
    {

        private readonly IResourceFilesListObjectFactory _filesListObjectFactory;

        public ResourceFileComponentSubObjectFactory(IObjectActivator activator, IResourceFilesListObjectFactory filesListObjectFactory) : base(activator)
        {
            _filesListObjectFactory = filesListObjectFactory;
        }

        public ComponentType ComponentType
        {
            get
            {
                return ComponentType.ResourceFile;
            }
        }


        public IComponent Create(ComponentType componentType)
        {
            var component = CreateInstance<ResourceFileComponent>();
            MarkAsChild(component);
            MarkNew(component);
            return component;
        }

        public IComponent Fetch(XPathNavigator nav)
        {
            var component = CreateInstance<ResourceFileComponent>();

            var filesNode = nav.SelectSingleNode("resourceFiles");
            if (filesNode == null)
            {
                throw new InvalidManifestFormatException();
            }

            var basePath = XmlUtils.ReadElement(filesNode, "basePath");
            LoadProperty(component, ResourceFileComponent.BasePathProperty, basePath);

            var filesList = _filesListObjectFactory.Fetch(nav);
            LoadProperty(component, ResourceFileComponent.FilesProperty, filesList);

            return component;

        }


    }
}
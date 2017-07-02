using System;
using System.Xml.XPath;
using Dazinate.Dnn.Manifest.Base;
using Dazinate.Dnn.Manifest.Exceptions;
using Dazinate.Dnn.Manifest.Ioc;
using Dazinate.Dnn.Manifest.Package.Component.ObjectFactory;
using Dazinate.Dnn.Manifest.Utils;

namespace Dazinate.Dnn.Manifest.Package.Component.Container.ObjectFactory
{
    public class ContainerComponentSubObjectFactory : BaseObjectFactory, IComponentSubObjectFactory
    {

        private readonly IContainerFilesListObjectFactory _filesListObjectFactory;

        public ContainerComponentSubObjectFactory(IObjectActivator activator, IContainerFilesListObjectFactory filesListObjectFactory) : base(activator)
        {
            _filesListObjectFactory = filesListObjectFactory;
        }

        public ComponentType ComponentType
        {
            get
            {
                return ComponentType.Container;
            }
        }

        public IComponent Create(ComponentType componentType)
        {
            var component = CreateInstance<ContainerComponent>();
            MarkAsChild(component);
            MarkNew(component);
            return component;
        }

        public IComponent Fetch(XPathNavigator nav)
        {
            var component = CreateInstance<ContainerComponent>();

            var containerFilesNode = nav.SelectSingleNode("containerFiles");
            if (containerFilesNode == null)
            {
                throw new InvalidManifestFormatException();
            }

            var basePath = XmlUtils.ReadElement(containerFilesNode, "basePath");
            LoadProperty(component, ContainerComponent.BasePathProperty, basePath);

            var containerName = XmlUtils.ReadElement(containerFilesNode, "containerName");
            LoadProperty(component, ContainerComponent.ContainerNameProperty, containerName);

          
            var filesList = _filesListObjectFactory.Fetch(containerFilesNode);
            LoadProperty(component, ContainerComponent.FilesProperty, filesList);

            return component;

        }


    }
}
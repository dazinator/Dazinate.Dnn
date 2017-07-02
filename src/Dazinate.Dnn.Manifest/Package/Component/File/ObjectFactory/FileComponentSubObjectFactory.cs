using System;
using System.Xml.XPath;
using Dazinate.Dnn.Manifest.Base;
using Dazinate.Dnn.Manifest.Exceptions;
using Dazinate.Dnn.Manifest.Ioc;
using Dazinate.Dnn.Manifest.Package.Component.ObjectFactory;
using Dazinate.Dnn.Manifest.Package.Component.Shared.File.ObjectFactory;
using Dazinate.Dnn.Manifest.Utils;

namespace Dazinate.Dnn.Manifest.Package.Component.File.ObjectFactory
{
    public class FileComponentSubObjectFactory : BaseObjectFactory, IComponentSubObjectFactory
    {

        private readonly IFilesListObjectFactory _filesListObjectFactory;

        public FileComponentSubObjectFactory(IObjectActivator activator, IFilesListObjectFactory filesListObjectFactory) : base(activator)
        {
            _filesListObjectFactory = filesListObjectFactory;
        }

        public ComponentType ComponentType
        {
            get
            {
                return ComponentType.File;
            }
        }

        public IComponent Create(ComponentType componentType)
        {
            var component = CreateInstance<FileComponent>();
            component.Files = _filesListObjectFactory.Create();
            MarkAsChild(component);
            MarkNew(component);
            return component;
        }

        public IComponent Fetch(XPathNavigator nav)
        {
            var component = CreateInstance<FileComponent>();

            var filesNode = nav.SelectSingleNode("files");
            if (filesNode == null)
            {
                throw new InvalidManifestFormatException();
            }

            var basePath = XmlUtils.ReadElement(filesNode, "basePath");
            LoadProperty(component, FileComponent.BasePathProperty, basePath);
          
            var filesList = _filesListObjectFactory.Fetch(nav);
            LoadProperty(component, FileComponent.FilesProperty, filesList);

            return component;

        }


    }
}
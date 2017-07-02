using System;
using System.Xml.XPath;
using Dazinate.Dnn.Manifest.Base;
using Dazinate.Dnn.Manifest.Exceptions;
using Dazinate.Dnn.Manifest.Ioc;
using Dazinate.Dnn.Manifest.Package.Component.ObjectFactory;
using Dazinate.Dnn.Manifest.Utils;

namespace Dazinate.Dnn.Manifest.Package.Component.JavascriptFile.ObjectFactory
{
    public class JavascriptFileComponentSubObjectFactory : BaseObjectFactory, IComponentSubObjectFactory
    {

        private readonly IJavascriptFilesListObjectFactory _filesListObjectFactory;

        public JavascriptFileComponentSubObjectFactory(IObjectActivator activator, IJavascriptFilesListObjectFactory filesListObjectFactory) : base(activator)
        {
            _filesListObjectFactory = filesListObjectFactory;
        }

        public ComponentType ComponentType
        {
            get
            {
                return ComponentType.JavaScriptFile;
            }
        }

        public IComponent Create(ComponentType componentType)
        {
            var component = CreateInstance<JavascriptFileComponent>();
            MarkAsChild(component);
            MarkNew(component);
            return component;
        }

        public IComponent Fetch(XPathNavigator nav)
        {
            var component = CreateInstance<JavascriptFileComponent>();

            var filesNode = nav.SelectSingleNode("jsfiles");
            if (filesNode == null)
            {
                throw new InvalidManifestFormatException();
            }

            var libraryFolderName = XmlUtils.ReadElement(filesNode, "libraryFolderName");
            LoadProperty(component, JavascriptFileComponent.LibraryFolderNameProperty, libraryFolderName);
          
            var filesList = _filesListObjectFactory.Fetch(nav);
            LoadProperty(component, JavascriptFileComponent.FilesProperty, filesList);

            return component;

        }


    }
}
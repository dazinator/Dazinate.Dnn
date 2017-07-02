using System;
using System.Xml.XPath;
using Dazinate.Dnn.Manifest.Base;
using Dazinate.Dnn.Manifest.Exceptions;
using Dazinate.Dnn.Manifest.Ioc;
using Dazinate.Dnn.Manifest.Package.Component.ObjectFactory;
using Dazinate.Dnn.Manifest.Utils;

namespace Dazinate.Dnn.Manifest.Package.Component.JavascriptLibrary.ObjectFactory
{
    public class JavascriptLibraryComponentSubObjectFactory : BaseObjectFactory, IComponentSubObjectFactory
    {

       
        public JavascriptLibraryComponentSubObjectFactory(IObjectActivator activator) : base(activator)
        {
        }

        public ComponentType ComponentType
        {
            get
            {
                return ComponentType.JavaScript_Library;
            }
        }


        public IComponent Create(ComponentType componentType)
        {
            var component = CreateInstance<JavascriptLibraryComponent>();
            MarkAsChild(component);
            MarkNew(component);
            return component;
        }

        public IComponent Fetch(XPathNavigator nav)
        {
            var component = CreateInstance<JavascriptLibraryComponent>();

            var filesNode = nav.SelectSingleNode("javaScriptLibrary");
            if (filesNode == null)
            {
                throw new InvalidManifestFormatException();
            }

            var libraryName = XmlUtils.ReadElement(filesNode, "libraryName");
            LoadProperty(component, JavascriptLibraryComponent.LibraryNameProperty, libraryName);

            var fileName = XmlUtils.ReadElement(filesNode, "fileName");
            LoadProperty(component, JavascriptLibraryComponent.FileNameProperty, fileName);

            var preferredScriptLocation = XmlUtils.ReadElement(filesNode, "preferredScriptLocation");
            LoadProperty(component, JavascriptLibraryComponent.PreferredScriptLocationProperty, preferredScriptLocation);

            var cdnPath = XmlUtils.ReadElement(filesNode, "CDNPath");
            LoadProperty(component, JavascriptLibraryComponent.CdnPathProperty, cdnPath);

            var objectName = XmlUtils.ReadElement(filesNode, "objectName");
            LoadProperty(component, JavascriptLibraryComponent.ObjectNameProperty, objectName);


            return component;

        }


    }
}
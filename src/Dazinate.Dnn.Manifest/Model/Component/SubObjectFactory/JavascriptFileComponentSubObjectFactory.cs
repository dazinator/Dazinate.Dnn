using System.Xml.XPath;
using Dazinate.Dnn.Manifest.Ioc;
using Dazinate.Dnn.Manifest.Model.JavascriptFilesList.ObjectFactory;
using Dazinate.Dnn.Manifest.Model.Manifest;
using Dazinate.Dnn.Manifest.Utils;

namespace Dazinate.Dnn.Manifest.Model.Component.SubObjectFactory
{
    public class JavascriptFileComponentSubObjectFactory : BaseObjectFactory, IComponentSubObjectFactory
    {

        private readonly IJavascriptFilesListObjectFactory _filesListObjectFactory;

        public JavascriptFileComponentSubObjectFactory(IObjectActivator activator, IJavascriptFilesListObjectFactory filesListObjectFactory) : base(activator)
        {
            _filesListObjectFactory = filesListObjectFactory;
        }

        public string ComponentTypeName { get { return "JavaScriptFile"; } }

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
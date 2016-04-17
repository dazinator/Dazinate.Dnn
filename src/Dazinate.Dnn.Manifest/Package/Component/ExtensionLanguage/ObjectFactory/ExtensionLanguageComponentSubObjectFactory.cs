using System.Xml.XPath;
using Dazinate.Dnn.Manifest.Base;
using Dazinate.Dnn.Manifest.Exceptions;
using Dazinate.Dnn.Manifest.Ioc;
using Dazinate.Dnn.Manifest.Package.Component.ObjectFactory;
using Dazinate.Dnn.Manifest.Package.Component.Shared.LanguageFile.ObjectFactory;
using Dazinate.Dnn.Manifest.Utils;

namespace Dazinate.Dnn.Manifest.Package.Component.ExtensionLanguage.ObjectFactory
{
    public class ExtensionLanguageComponentSubObjectFactory : BaseObjectFactory, IComponentSubObjectFactory
    {

        private readonly ILanguageFilesListObjectFactory _filesListObjectFactory;

        public ExtensionLanguageComponentSubObjectFactory(IObjectActivator activator, ILanguageFilesListObjectFactory filesListObjectFactory) : base(activator)
        {
            _filesListObjectFactory = filesListObjectFactory;
        }

        public string ComponentTypeName { get { return "ExtensionLanguage"; } }

        public IComponent Fetch(XPathNavigator nav)
        {
            var component = CreateInstance<ExtensionLanguageComponent>();

            var containerFilesNode = nav.SelectSingleNode("languageFiles");
            if (containerFilesNode == null)
            {
                throw new InvalidManifestFormatException();
            }

            var core = XmlUtils.ReadElement(containerFilesNode, "code");
            LoadProperty(component, ExtensionLanguageComponent.CodeProperty, core);

            var package = XmlUtils.ReadElement(containerFilesNode, "package");
            LoadProperty(component, ExtensionLanguageComponent.PackageProperty, package);

            var basepath = XmlUtils.ReadElement(containerFilesNode, "basePath ");
            LoadProperty(component, ExtensionLanguageComponent.BasePathProperty, basepath);

            var filesList = _filesListObjectFactory.Fetch(containerFilesNode);
            LoadProperty(component, ExtensionLanguageComponent.FilesProperty, filesList);

            return component;

        }


    }
}
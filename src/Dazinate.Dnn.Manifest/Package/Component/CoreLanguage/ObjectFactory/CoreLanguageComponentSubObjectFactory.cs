using System.Xml.XPath;
using Dazinate.Dnn.Manifest.Base;
using Dazinate.Dnn.Manifest.Exceptions;
using Dazinate.Dnn.Manifest.Ioc;
using Dazinate.Dnn.Manifest.Package.Component.ObjectFactory;
using Dazinate.Dnn.Manifest.Package.Component.Shared.LanguageFile.ObjectFactory;
using Dazinate.Dnn.Manifest.Utils;

namespace Dazinate.Dnn.Manifest.Package.Component.CoreLanguage.ObjectFactory
{
    public class CoreLanguageComponentSubObjectFactory : BaseObjectFactory, IComponentSubObjectFactory
    {

        private readonly ILanguageFilesListObjectFactory _filesListObjectFactory;

        public CoreLanguageComponentSubObjectFactory(IObjectActivator activator, ILanguageFilesListObjectFactory filesListObjectFactory) : base(activator)
        {
            _filesListObjectFactory = filesListObjectFactory;
        }

        public string ComponentTypeName { get { return "CoreLanguage"; } }

        public IComponent Fetch(XPathNavigator nav)
        {
            var component = CreateInstance<CoreLanguageComponent>();

            var containerFilesNode = nav.SelectSingleNode("languageFiles");
            if (containerFilesNode == null)
            {
                throw new InvalidManifestFormatException();
            }

            var core = XmlUtils.ReadElement(containerFilesNode, "code");
            LoadProperty(component, CoreLanguageComponent.CodeProperty, core);

            var displayName = XmlUtils.ReadElement(containerFilesNode, "displayName");
            LoadProperty(component, CoreLanguageComponent.DisplayNameProperty, displayName);

            var fallback = XmlUtils.ReadElement(containerFilesNode, "fallback");
            LoadProperty(component, CoreLanguageComponent.FallbackProperty, fallback);
            
            var filesList = _filesListObjectFactory.Fetch(containerFilesNode);
            LoadProperty(component, CoreLanguageComponent.FilesProperty, filesList);

            return component;

        }


    }
}
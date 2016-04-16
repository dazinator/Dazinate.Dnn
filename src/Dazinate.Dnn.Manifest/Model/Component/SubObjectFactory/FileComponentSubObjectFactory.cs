using System.Xml.XPath;
using Dazinate.Dnn.Manifest.Ioc;
using Dazinate.Dnn.Manifest.Model.FilesList.ObjectFactory;
using Dazinate.Dnn.Manifest.Model.Manifest;
using Dazinate.Dnn.Manifest.Utils;

namespace Dazinate.Dnn.Manifest.Model.Component.SubObjectFactory
{
    public class FileComponentSubObjectFactory : BaseObjectFactory, IComponentSubObjectFactory
    {

        private readonly IFilesListObjectFactory _filesListObjectFactory;

        public FileComponentSubObjectFactory(IObjectActivator activator, IFilesListObjectFactory filesListObjectFactory) : base(activator)
        {
            _filesListObjectFactory = filesListObjectFactory;
        }

        public string ComponentTypeName { get { return "File"; } }

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
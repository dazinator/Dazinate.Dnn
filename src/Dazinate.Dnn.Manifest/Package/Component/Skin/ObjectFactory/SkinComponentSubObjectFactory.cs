using System.Xml.XPath;
using Dazinate.Dnn.Manifest.Base;
using Dazinate.Dnn.Manifest.Exceptions;
using Dazinate.Dnn.Manifest.Ioc;
using Dazinate.Dnn.Manifest.Package.Component.ObjectFactory;
using Dazinate.Dnn.Manifest.Utils;

namespace Dazinate.Dnn.Manifest.Package.Component.Skin.ObjectFactory
{
    public class SkinComponentSubObjectFactory : BaseObjectFactory, IComponentSubObjectFactory
    {

        private readonly ISkinFilesListObjectFactory _skinFilesListFactory;
        public SkinComponentSubObjectFactory(IObjectActivator activator, ISkinFilesListObjectFactory skinFilesListFactory) : base(activator)
        {
            _skinFilesListFactory = skinFilesListFactory;
        }

        public ComponentType ComponentType
        {
            get
            {
                return ComponentType.Skin;
            }
        }


        public IComponent Create(ComponentType componentType)
        {
            var component = CreateInstance<SkinComponent>();
            MarkAsChild(component);
            MarkNew(component);
            return component;
        }

        public IComponent Fetch(XPathNavigator nav)
        {
            var component = CreateInstance<SkinComponent>();
            var node = nav.SelectSingleNode("skinFiles");
            if (node == null)
            {
                throw new InvalidManifestFormatException();
            }

            var basePath = XmlUtils.ReadElement(node, "basePath");
            LoadProperty(component, SkinComponent.BasePathProperty, basePath);

            var skinName = XmlUtils.ReadElement(node, "skinName");
            LoadProperty(component, SkinComponent.SkinNameProperty, skinName);

            var filesList = _skinFilesListFactory.Fetch(nav);
            LoadProperty(component, SkinComponent.FilesProperty, filesList);

            return component;
        }


    }
}
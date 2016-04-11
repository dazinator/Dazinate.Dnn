using System;
using System.Xml.XPath;
using Dazinate.Dnn.Manifest.Ioc;
using Dazinate.Dnn.Manifest.Model.ContainerFilesList.ObjectFactory;
using Dazinate.Dnn.Manifest.Model.EventMessage;
using Dazinate.Dnn.Manifest.Model.Manifest;
using Dazinate.Dnn.Manifest.Utils;

namespace Dazinate.Dnn.Manifest.Model.Component.SubObjectFactory
{
    public class ModuleComponentSubObjectFactory : BaseObjectFactory, IComponentSubObjectFactory
    {

        private readonly IEventMessageObjectFactory _eventMessageObjectFactory;

        public ModuleComponentSubObjectFactory(IObjectActivator activator, IEventMessageObjectFactory eventMessageObjectFactory) : base(activator)
        {
            _eventMessageObjectFactory = eventMessageObjectFactory;
        }

        public string ComponentTypeName { get { return "Module"; } }

        public IComponent Fetch(XPathNavigator nav)
        {
            var component = CreateInstance<ModuleComponent>();

            // todo: DesktopModule.
           

            var eventMessageNode = nav.SelectSingleNode("eventMessage");
            
            if (eventMessageNode != null)
            {
                var eventMessage = _eventMessageObjectFactory.Fetch(eventMessageNode);
                LoadProperty(component, ModuleComponent.EventMessageProperty, eventMessage);
            }

            throw new NotImplementedException();

            //var basePath = XmlUtils.ReadElement(containerFilesNode, "basePath");
            //LoadProperty(component, ContainerComponent.BasePathProperty, basePath);

            //var containerName = XmlUtils.ReadElement(containerFilesNode, "containerName");
            //LoadProperty(component, ContainerComponent.ContainerNameProperty, containerName);


            //var filesList = _filesListObjectFactory.Fetch(containerFilesNode);
            //LoadProperty(component, ContainerComponent.FilesProperty, filesList);

            return component;

        }


    }
}
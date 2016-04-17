using System.Xml.XPath;
using Dazinate.Dnn.Manifest.Base;
using Dazinate.Dnn.Manifest.Exceptions;
using Dazinate.Dnn.Manifest.Ioc;
using Dazinate.Dnn.Manifest.Package.Component.ObjectFactory;
using Dazinate.Dnn.Manifest.Utils;

namespace Dazinate.Dnn.Manifest.Package.Component.Config.ObjectFactory
{
    public class ConfigComponentSubObjectFactory : BaseObjectFactory, IComponentSubObjectFactory
    {

        private readonly INodesListObjectFactory _nodesListObjectFactory;

        public ConfigComponentSubObjectFactory(IObjectActivator activator, INodesListObjectFactory nodesListObjectFactory) : base(activator)
        {
            _nodesListObjectFactory = nodesListObjectFactory;
        }

        public string ComponentTypeName { get { return "Config"; } }

        public IComponent Fetch(XPathNavigator nav)
        {
            var component = CreateInstance<ConfigComponent>();

            var configNode = nav.SelectSingleNode("config");
            if (configNode == null)
            {
                throw new InvalidManifestFormatException();
            }

            var configFile = XmlUtils.ReadElement(configNode, "configFile");
            LoadProperty(component, ConfigComponent.ConfigFileProperty, configFile);

            var installNode = configNode.SelectSingleNode("install");
            var uninstallNode = configNode.SelectSingleNode("uninstall");

            var intallList = _nodesListObjectFactory.Fetch(installNode);
            LoadProperty(component, ConfigComponent.InstallNodesProperty, intallList);

            var unintallList = _nodesListObjectFactory.Fetch(uninstallNode);
            LoadProperty(component, ConfigComponent.UninstallNodesProperty, unintallList);

            return component;

        }


    }
}
using System.Xml.XPath;
using Dazinate.Dnn.Manifest.Ioc;
using Dazinate.Dnn.Manifest.Model.Manifest;
using Dazinate.Dnn.Manifest.Model.ScriptsList.ObjectFactory;
using Dazinate.Dnn.Manifest.Utils;

namespace Dazinate.Dnn.Manifest.Model.Component.SubObjectFactory
{
    public class ScriptComponentSubObjectFactory : BaseObjectFactory, IComponentSubObjectFactory
    {

        private readonly IScriptsListObjectFactory _scriptsListObjectFactory;

        public ScriptComponentSubObjectFactory(IObjectActivator activator, IScriptsListObjectFactory scriptsListObjectFactory) : base(activator)
        {
            _scriptsListObjectFactory = scriptsListObjectFactory;
        }

        public string ComponentTypeName { get { return "Script"; } }

        public IComponent Fetch(XPathNavigator nav)
        {
           
            var component = CreateInstance<ScriptComponent>();

            var scriptsNode = nav.SelectSingleNode("scripts");
            if (scriptsNode == null)
            {
                throw new InvalidManifestFormatException();
            }

            var basePath = XmlUtils.ReadElement(scriptsNode, "basePath");
            LoadProperty(component, ScriptComponent.BasePathProperty, basePath);

            var scriptsList = _scriptsListObjectFactory.Fetch(nav);
            LoadProperty(component, ScriptComponent.ScriptsProperty, scriptsList);

            return component;

        }


    }
}
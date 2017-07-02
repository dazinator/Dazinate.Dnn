using System.Xml.XPath;
using Dazinate.Dnn.Manifest.Base;
using Dazinate.Dnn.Manifest.Exceptions;
using Dazinate.Dnn.Manifest.Ioc;
using Dazinate.Dnn.Manifest.Package.Component.ObjectFactory;
using Dazinate.Dnn.Manifest.Utils;

namespace Dazinate.Dnn.Manifest.Package.Component.Script.ObjectFactory
{
    public class ScriptComponentSubObjectFactory : BaseObjectFactory, IComponentSubObjectFactory
    {

        private readonly IScriptsListObjectFactory _scriptsListObjectFactory;

        public ScriptComponentSubObjectFactory(IObjectActivator activator, IScriptsListObjectFactory scriptsListObjectFactory) : base(activator)
        {
            _scriptsListObjectFactory = scriptsListObjectFactory;
        }

        public ComponentType ComponentType
        {
            get
            {
                return ComponentType.Script;
            }
        }


        public IComponent Create(ComponentType componentType)
        {
            var component = CreateInstance<ScriptComponent>();
            component.Scripts = _scriptsListObjectFactory.Create();
            MarkAsChild(component);
            MarkNew(component);
            return component;
        }

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
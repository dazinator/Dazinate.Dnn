using System;
using System.Xml.XPath;
using Dazinate.Dnn.Manifest.Base;
using Dazinate.Dnn.Manifest.Exceptions;
using Dazinate.Dnn.Manifest.Ioc;
using Dazinate.Dnn.Manifest.Utils;

namespace Dazinate.Dnn.Manifest.Package.Component.Script.ObjectFactory
{
    public class ScriptObjectFactory : BaseObjectFactory, IScriptObjectFactory
    {

        public ScriptObjectFactory(IObjectActivator activator) : base(activator)
        {
            //_packagesListFactory = packagesListFactory;
        }

        public IScript Fetch(XPathNavigator nav)
        {
            // Create the correct concrete dependency based on the xml.
            var businessObject = CreateInstance<Script>();

            var type = XmlUtils.ReadRequiredAttribute(nav, "type");
            ScriptType scriptType;
            if (!Enum.TryParse(type, out scriptType))
            {
                throw new InvalidManifestFormatException();
            }

            LoadProperty(businessObject, Script.ScriptTypeProperty, scriptType);

            var path = XmlUtils.ReadElement(nav, "path");
            LoadProperty(businessObject, Script.PathProperty, path);

            var name = XmlUtils.ReadElement(nav, "name");
            LoadProperty(businessObject, Script.NameProperty, name);

            var sourceFileName = XmlUtils.ReadElement(nav, "sourceFileName");
            LoadProperty(businessObject, Script.SourceFileNameProperty, sourceFileName);

            var version = XmlUtils.ReadElement(nav, "version");
            LoadProperty(businessObject, Script.VersionProperty, version);

            MarkAsChild(businessObject);
            MarkOld(businessObject);
            CheckRules(businessObject);
            return businessObject;
        }
    }
}
using System;
using Csla;
using Csla.Server;
using Dazinate.Dnn.Manifest.Package.Component.ObjectFactory;
using Dazinate.Dnn.Manifest.Writer;

namespace Dazinate.Dnn.Manifest.Package.Component.Script
{
    [ObjectFactory(typeof(IComponentObjectFactory))]
    [Serializable]
    public class ScriptComponent : BusinessBase<ScriptComponent>, IScriptComponent
    {

        public static readonly PropertyInfo<string> BasePathProperty = RegisterProperty<string>(c => c.BasePath);
        public string BasePath
        {
            get { return GetProperty(BasePathProperty); }
            set { SetProperty(BasePathProperty, value); }
        }

        public static readonly PropertyInfo<ScriptsList> ScriptsProperty = RegisterProperty<ScriptsList>(c => c.Scripts);
        public IScriptsList Scripts
        {
            get { return GetProperty(ScriptsProperty); }
            set { SetProperty(ScriptsProperty, value); }
        }

        public void Accept(IManifestXmlWriterVisitor visitor)
        {
            visitor.Visit(this);
        }

        public bool IsCompatibleWithPackage(IPackage package)
        {
            return true;
        }
    }
}
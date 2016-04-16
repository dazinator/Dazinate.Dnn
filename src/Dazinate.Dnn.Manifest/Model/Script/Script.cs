using System;
using Csla;
using Csla.Server;
using Dazinate.Dnn.Manifest.Model.Script.ObjectFactory;
using Dazinate.Dnn.Manifest.Writer;

namespace Dazinate.Dnn.Manifest.Model.Script
{
    [ObjectFactory(typeof(IScriptObjectFactory))]
    [Serializable]
    public class Script : BusinessBase<Script>, IScript
    {
        public static readonly PropertyInfo<string> PathProperty = RegisterProperty<string>(c => c.Path);
        /// <summary>
        /// Target file folder. Relative to basePath.
        /// </summary>
        public string Path
        {
            get { return GetProperty(PathProperty); }
            set { SetProperty(PathProperty, value); }
        }

        public static readonly PropertyInfo<string> NameProperty = RegisterProperty<string>(c => c.Name);
        public string Name
        {
            get { return GetProperty(NameProperty); }
            set { SetProperty(NameProperty, value); }
        }

        public static readonly PropertyInfo<string> SourceFileNameProperty = RegisterProperty<string>(c => c.SourceFileName);
        public string SourceFileName
        {
            get { return GetProperty(SourceFileNameProperty); }
            set { SetProperty(SourceFileNameProperty, value); }
        }

        public static readonly PropertyInfo<ScriptType> ScriptTypeProperty = RegisterProperty<ScriptType>(c => c.Type);
        public ScriptType Type
        {
            get { return GetProperty(ScriptTypeProperty); }
            set { SetProperty(ScriptTypeProperty, value); }
        }

        public static readonly PropertyInfo<string> VersionProperty = RegisterProperty<string>(c => c.Version);
        public string Version
        {
            get { return GetProperty(VersionProperty); }
            set { SetProperty(VersionProperty, value); }
        }

        protected override void AddBusinessRules()
        {
            //todo: pretty sure sourcefilename is invalid for container files.
            base.AddBusinessRules();
        }


        public void Accept(IManifestXmlWriterVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
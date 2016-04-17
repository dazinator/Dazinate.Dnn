using System;
using Csla;
using Csla.Server;
using Dazinate.Dnn.Manifest.Package.Component.Assembly.ObjectFactory;
using Dazinate.Dnn.Manifest.Writer;

namespace Dazinate.Dnn.Manifest.Package.Component.Assembly
{
    [ObjectFactory(typeof(IAssemblyObjectFactory))]
    [Serializable]
    public class Assembly : BusinessBase<Assembly>, IAssembly
    {
        public static readonly PropertyInfo<string> PathProperty = RegisterProperty<string>(c => c.Path);
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

        public static readonly PropertyInfo<string> VersionProperty = RegisterProperty<string>(c => c.Version);
        public string Version
        {
            get { return GetProperty(VersionProperty); }
            set { SetProperty(VersionProperty, value); }
        }

        public static readonly PropertyInfo<AssemblyAction> ActionProperty = RegisterProperty<AssemblyAction>(c => c.Action);
        public AssemblyAction Action
        {
            get { return GetProperty(ActionProperty); }
            set { SetProperty(ActionProperty, value); }
        }

        public void Accept(IManifestXmlWriterVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
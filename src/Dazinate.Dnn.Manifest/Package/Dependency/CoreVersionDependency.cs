using System;
using Csla;
using Csla.Server;
using Dazinate.Dnn.Manifest.Base;
using Dazinate.Dnn.Manifest.Package.Dependency.ObjectFactory;

namespace Dazinate.Dnn.Manifest.Package.Dependency
{
    [ObjectFactory(typeof(IDependencyObjectFactory))]
    [Serializable]
    public class CoreVersionDependency : BusinessBase<CoreVersionDependency>, IDependency
    {

        public static readonly PropertyInfo<string> VersionProperty = RegisterProperty<string>(c => c.Version);
        public string Version
        {
            get { return GetProperty(VersionProperty); }
            set { SetProperty(VersionProperty, value); }
        }

        public void Accept(IManifestVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override string ToString()
        {
            return "Core Version";
        }
    }
}
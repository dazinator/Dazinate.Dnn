using System;
using Csla;
using Csla.Server;
using Dazinate.Dnn.Manifest.Model.Dependency.ObjectFactory;
using Dazinate.Dnn.Manifest.Writer;

namespace Dazinate.Dnn.Manifest.Model.Dependency
{
    [ObjectFactory(typeof(IDependencyObjectFactory))]
    [Serializable]
    public class ManagedPackageDependency : BusinessBase<ManagedPackageDependency>, IDependency
    {
        public static readonly PropertyInfo<string> VersionProperty = RegisterProperty<string>(c => c.Version);
        public string Version
        {
            get { return GetProperty(VersionProperty); }
            set { SetProperty(VersionProperty, value); }
        }

        public static readonly PropertyInfo<string> PackageNameProperty = RegisterProperty<string>(c => c.PackageName);
        public string PackageName
        {
            get { return GetProperty(PackageNameProperty); }
            set { SetProperty(PackageNameProperty, value); }
        }

        public void Accept(IManifestXmlWriterVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
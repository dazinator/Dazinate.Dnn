using System;
using Csla;
using Csla.Server;
using Dazinate.Dnn.Manifest.ObjectFactory;

namespace Dazinate.Dnn.Manifest
{
    [ObjectFactory(typeof(IPackageDependencyObjectFactory))]
    [Serializable]
    public class ManagedPackageDependency : BusinessBase<ManagedPackageDependency>, IPackageDependency
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
    }
}
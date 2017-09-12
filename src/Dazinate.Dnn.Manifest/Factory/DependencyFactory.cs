using System;
using Dazinate.Dnn.Manifest.Package.Dependency;
using Dazinate.Dnn.Manifest.Package.Dependency.ObjectFactory;

namespace Dazinate.Dnn.Manifest.Factory
{
    [Serializable]
    internal class DependencyFactory : IDependencyFactory
    {

        public IDependency CreateNewCoreVersionDependency()
        {
            return Csla.DataPortal.Create<CoreVersionDependency>(DependencyType.CoreVersion);
        }

        public IDependency CreateNewCustomDependency()
        {
            return Csla.DataPortal.Create<CustomDependency>(DependencyType.Custom);
        }

        public IDependency CreateNewManagedPackageDependency()
        {
            return Csla.DataPortal.Create<ManagedPackageDependency>(DependencyType.ManagedPackage);
        }

        public IDependency CreateNewPackageDependency()
        {
            return Csla.DataPortal.Create<PackageDependency>(DependencyType.Package);
        }

        public IDependency CreateNewTypeDependency()
        {
            return Csla.DataPortal.Create<TypeDependency>(DependencyType.Type);
        }
    }
}
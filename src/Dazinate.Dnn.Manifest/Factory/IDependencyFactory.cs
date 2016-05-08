using Dazinate.Dnn.Manifest.Package.Dependency;

namespace Dazinate.Dnn.Manifest.Factory
{
    public interface IDependencyFactory
    {
        IDependency CreateNewCoreVersionDependency();
        IDependency CreateNewCustomDependency();
        IDependency CreateNewManagedPackageDependency();
        IDependency CreateNewPackageDependency();
        IDependency CreateNewTypeDependency();

    }
}
using Dazinate.Dnn.Manifest.Model;
using Dazinate.Dnn.Manifest.Model.Dependency;
using Dazinate.Dnn.Manifest.Model.Package;

namespace Dazinate.Dnn.Manifest.Writer
{
    public interface IManifestXmlWriterVisitor
    {
        void Visit(IPackagesDnnManifest packagesManifest);
        void Visit(ILicense license);
        void Visit(IOwner owner);
        void Visit(IPackage package);
        void Visit(IPackagesList packagesList);
        void Visit(IReleaseNotes releaseNotes);
        void Visit(IDependenciesList dependenciesList);
        void Visit(CoreVersionDependency dependency);
        void Visit(ManagedPackageDependency managedPackageDependency);
        void Visit(PackageDependency packageDependency);
        void Visit(TypeDependency managedPackageDependency);
        void Visit(CustomDependency managedPackageDependency);
    }
}
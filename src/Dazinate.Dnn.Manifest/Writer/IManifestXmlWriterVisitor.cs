using Dazinate.Dnn.Manifest.Model;
using Dazinate.Dnn.Manifest.Model.AssembliesList;
using Dazinate.Dnn.Manifest.Model.Assembly;
using Dazinate.Dnn.Manifest.Model.Assembly.ObjectFactory;
using Dazinate.Dnn.Manifest.Model.Component;
using Dazinate.Dnn.Manifest.Model.ComponentsList;
using Dazinate.Dnn.Manifest.Model.Dependency;
using Dazinate.Dnn.Manifest.Model.DependencyList;
using Dazinate.Dnn.Manifest.Model.File;
using Dazinate.Dnn.Manifest.Model.FilesList;
using Dazinate.Dnn.Manifest.Model.Manifest;
using Dazinate.Dnn.Manifest.Model.Node;
using Dazinate.Dnn.Manifest.Model.NodesList;
using Dazinate.Dnn.Manifest.Model.Package;
using Dazinate.Dnn.Manifest.Model.PackagesList;

namespace Dazinate.Dnn.Manifest.Writer
{
    public interface IManifestXmlWriterVisitor
    {
        void Visit(IPackagesDnnManifest packagesManifest);
        void Visit(ILicense license);
        void Visit(IOwner owner);
        void Visit(IPackage package);
        void Visit(IPackagesList list);
        void Visit(IReleaseNotes releaseNotes);
        void Visit(IDependenciesList list);
        void Visit(CoreVersionDependency dependency);
        void Visit(ManagedPackageDependency dependency);
        void Visit(PackageDependency dependency);
        void Visit(TypeDependency dependency);
        void Visit(CustomDependency dependency);
        void Visit(IAssemblyComponent component);
        void Visit(IAssembliesList list);
        void Visit(IAssembly assembly);
        void Visit(IComponentsList list);
        void Visit(IAuthenticationSystemComponent component);
        void Visit(IFile file);
        void Visit(IFilesList list);
        void Visit(ICleanupComponent component);
        void Visit(INode node);
        void Visit(INodesList list);
        void Visit(IConfigComponent component);
    }
}
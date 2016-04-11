using Dazinate.Dnn.Manifest.Model.AssembliesList;
using Dazinate.Dnn.Manifest.Model.Assembly;
using Dazinate.Dnn.Manifest.Model.Component;
using Dazinate.Dnn.Manifest.Model.ComponentsList;
using Dazinate.Dnn.Manifest.Model.ContainerFile;
using Dazinate.Dnn.Manifest.Model.ContainerFilesList;
using Dazinate.Dnn.Manifest.Model.DashboardControl;
using Dazinate.Dnn.Manifest.Model.DashboardControlsList;
using Dazinate.Dnn.Manifest.Model.Dependency;
using Dazinate.Dnn.Manifest.Model.DependencyList;
using Dazinate.Dnn.Manifest.Model.EventAttribute;
using Dazinate.Dnn.Manifest.Model.EventAttributesList;
using Dazinate.Dnn.Manifest.Model.EventMessage;
using Dazinate.Dnn.Manifest.Model.File;
using Dazinate.Dnn.Manifest.Model.FilesList;
using Dazinate.Dnn.Manifest.Model.LanguageFile;
using Dazinate.Dnn.Manifest.Model.LanguageFilesList;
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
        void Visit(File file);
        void Visit(IFilesList list);
        void Visit(ICleanupComponent component);
        void Visit(INode node);
        void Visit(INodesList list);
        void Visit(IConfigComponent component);
        void Visit(IContainerFilesList list);
        void Visit(IContainerComponent component);
        void Visit(ContainerFile file);
        void Visit(DashboardControlComponent component);
        void Visit(DashboardControlsList list);
        void Visit(DashboardControl dashboardControl);
        void Visit(FileComponent component);
        void Visit(LanguageFilesList list);
        void Visit(LanguageFile languageFile);
        void Visit(CoreLanguageComponent component);
        void Visit(ExtensionLanguageComponent component);
        void Visit(ModuleComponent component);
        void Visit(EventMessage eventMessage);
        void Visit(EventAttribute eventAttribute);
        void Visit(EventAttributesList list);
    }
}
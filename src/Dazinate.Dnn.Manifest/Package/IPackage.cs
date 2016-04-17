using Csla;
using Dazinate.Dnn.Manifest.Base;
using Dazinate.Dnn.Manifest.Package.Component;
using Dazinate.Dnn.Manifest.Package.Dependency;

namespace Dazinate.Dnn.Manifest.Package
{
    public interface IPackage : IBusinessBase, IVisitable<IManifestVisitor>
    {

        //IDnnManifest Manifest { get; set; }

        string Name { get; set; }

        string Type { get; set; }

        string Version { get; set; }

        string FriendlyName { get; set; }

        string Description { get; set; }

        string IconFile { get; set; }

        bool? AzureCompatible { get; set; }

        IOwner Owner { get; set; }

        ILicense License { get; set; }

        IReleaseNotes ReleaseNotes { get; set; }

        IDependenciesList Dependencies { get; }

        IComponentsList Components { get; }

    }
}
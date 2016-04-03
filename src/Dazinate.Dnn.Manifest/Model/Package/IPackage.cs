using Csla;
using Dazinate.Dnn.Manifest.Model.ComponentsList;
using Dazinate.Dnn.Manifest.Model.Dependency;
using Dazinate.Dnn.Manifest.Model.DependencyList;
using Dazinate.Dnn.Manifest.Writer;

namespace Dazinate.Dnn.Manifest.Model.Package
{
    public interface IPackage : IBusinessBase, IVisitable<IManifestXmlWriterVisitor>
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
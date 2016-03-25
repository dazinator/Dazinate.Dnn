using Csla;

namespace Dazinate.Dnn.Manifest
{
    public interface IPackage : IBusinessBase
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

    }
}
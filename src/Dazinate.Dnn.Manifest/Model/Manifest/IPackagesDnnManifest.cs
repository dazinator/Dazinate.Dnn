using Dazinate.Dnn.Manifest.Model.PackagesList;

namespace Dazinate.Dnn.Manifest.Model.Manifest
{
    public interface IPackagesDnnManifest : IDnnManifest
    {
        IPackagesList Packages { get; }
    }


}
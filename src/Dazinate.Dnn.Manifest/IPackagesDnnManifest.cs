using Dazinate.Dnn.Manifest.Base;
using Dazinate.Dnn.Manifest.Package;

namespace Dazinate.Dnn.Manifest
{
    public interface IPackagesDnnManifest : IDnnManifest
    {
        IPackagesList Packages { get; }
    }


}
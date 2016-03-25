using Csla;

namespace Dazinate.Dnn.Manifest.ObjectFactory
{
    public interface IPackagesDnnManifestObjectFactory
    {
        PackagesDnnManifest Fetch(SingleCriteria<string> xmlContents);
    }
}
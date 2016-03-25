using Csla;

namespace Dnn.Contrib.Manifest.ObjectFactory
{
    public interface IPackagesDnnManifestObjectFactory
    {
        PackagesDnnManifest Fetch(SingleCriteria<string> xmlContents);
    }
}
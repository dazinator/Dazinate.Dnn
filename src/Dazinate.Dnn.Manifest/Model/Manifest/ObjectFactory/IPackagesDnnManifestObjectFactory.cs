using Csla;

namespace Dazinate.Dnn.Manifest.Model.Manifest.ObjectFactory
{
    public interface IPackagesDnnManifestObjectFactory
    {
        PackagesDnnManifest Fetch(SingleCriteria<string> xmlContents);
    }
}
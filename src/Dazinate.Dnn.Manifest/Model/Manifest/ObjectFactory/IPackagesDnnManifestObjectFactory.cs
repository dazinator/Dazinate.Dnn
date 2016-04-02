using Csla;
using Dazinate.Dnn.Manifest.Model;

namespace Dazinate.Dnn.Manifest.ObjectFactory
{
    public interface IPackagesDnnManifestObjectFactory
    {
        PackagesDnnManifest Fetch(SingleCriteria<string> xmlContents);
    }
}
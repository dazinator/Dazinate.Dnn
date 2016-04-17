using Csla;
using Dazinate.Dnn.Manifest.Base;

namespace Dazinate.Dnn.Manifest.Package
{
    public interface IPackagesList : IBusinessListBase<IPackage>, IVisitable<IManifestVisitor>
    {
        IPackage AddNewPackage();
    }
}
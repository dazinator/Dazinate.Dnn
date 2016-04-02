using Csla;
using Dazinate.Dnn.Manifest.Model.Package;
using Dazinate.Dnn.Manifest.Writer;

namespace Dazinate.Dnn.Manifest.Model.PackagesList
{
    public interface IPackagesList : IBusinessListBase<IPackage>, IVisitable<IManifestXmlWriterVisitor>
    {
        IPackage AddNewPackage();
    }
}
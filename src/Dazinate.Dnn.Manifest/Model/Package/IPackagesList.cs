using Csla;
using Dazinate.Dnn.Manifest.Writer;

namespace Dazinate.Dnn.Manifest.Model.Package
{
    public interface IPackagesList : IBusinessListBase<IPackage>, IVisitable<IManifestXmlWriterVisitor>
    {
        IPackage AddNewPackage();
    }
}
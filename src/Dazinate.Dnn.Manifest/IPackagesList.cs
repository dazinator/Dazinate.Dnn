using Csla;

namespace Dazinate.Dnn.Manifest
{
    public interface IPackagesList : IBusinessListBase<IPackage>, IVisitable<IManifestXmlWriterVisitor>
    {
        IPackage AddNewPackage();
    }
}
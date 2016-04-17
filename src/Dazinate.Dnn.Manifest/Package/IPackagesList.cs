using Csla;
using Dazinate.Dnn.Manifest.Base;
using Dazinate.Dnn.Manifest.Writer;

namespace Dazinate.Dnn.Manifest.Package
{
    public interface IPackagesList : IBusinessListBase<IPackage>, IVisitable<IManifestXmlWriterVisitor>
    {
        IPackage AddNewPackage();
    }
}
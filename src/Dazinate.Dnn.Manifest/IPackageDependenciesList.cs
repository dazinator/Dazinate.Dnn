using Csla;

namespace Dazinate.Dnn.Manifest
{
    public interface IPackageDependenciesList : IBusinessListBase<IPackageDependency>, IVisitable<IManifestXmlWriterVisitor>
    {
       
    }
}
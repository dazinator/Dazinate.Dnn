using Csla;
using Dazinate.Dnn.Manifest.Writer;

namespace Dazinate.Dnn.Manifest.Model.Dependency
{
    public interface IDependenciesList : IBusinessListBase<IDependency>, IVisitable<IManifestXmlWriterVisitor>
    {
       
    }
}
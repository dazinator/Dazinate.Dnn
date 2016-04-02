using Csla;
using Dazinate.Dnn.Manifest.Model.Dependency;
using Dazinate.Dnn.Manifest.Writer;

namespace Dazinate.Dnn.Manifest.Model.DependencyList
{
    public interface IDependenciesList : IBusinessListBase<IDependency>, IVisitable<IManifestXmlWriterVisitor>
    {
       
    }
}
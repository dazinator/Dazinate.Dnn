using Csla;
using Dazinate.Dnn.Manifest.Base;
using Dazinate.Dnn.Manifest.Writer;

namespace Dazinate.Dnn.Manifest.Package.Dependency
{
    public interface IDependenciesList : IBusinessListBase<IDependency>, IVisitable<IManifestXmlWriterVisitor>
    {
       
    }
}
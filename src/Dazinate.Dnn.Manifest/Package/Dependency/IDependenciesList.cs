using Csla;
using Csla.Core;
using Dazinate.Dnn.Manifest.Base;

namespace Dazinate.Dnn.Manifest.Package.Dependency
{
    public interface IDependenciesList : IBusinessListBase<IDependency>, IVisitable<IManifestVisitor>
    {
       
    }
}
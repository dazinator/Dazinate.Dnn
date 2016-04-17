using Csla;
using Dazinate.Dnn.Manifest.Base;

namespace Dazinate.Dnn.Manifest.Package.Component
{
    public interface IComponentsList : IBusinessListBase<IComponent>, IVisitable<IManifestVisitor>
    {
       
    }
}
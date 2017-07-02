using Csla;
using Dazinate.Dnn.Manifest.Base;
using Dazinate.Dnn.Manifest.Package.Component.Assembly;

namespace Dazinate.Dnn.Manifest.Package.Component
{
    public interface IComponentsList : IBusinessListBase<IComponent>, IVisitable<IManifestVisitor>
    {
        IAssemblyComponent AddNewAssemblyComponent();
    }
}
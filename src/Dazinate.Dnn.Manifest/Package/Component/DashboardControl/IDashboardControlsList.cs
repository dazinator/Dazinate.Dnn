using Csla;
using Dazinate.Dnn.Manifest.Base;

namespace Dazinate.Dnn.Manifest.Package.Component.DashboardControl
{
    public interface IDashboardControlsList : IBusinessListBase<IDashboardControl>, IVisitable<IManifestVisitor>
    {
    }
}
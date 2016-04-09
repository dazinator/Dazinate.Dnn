using Dazinate.Dnn.Manifest.Model.DashboardControlsList;

namespace Dazinate.Dnn.Manifest.Model.Component
{
    public interface IDashboardControlComponent : IComponent
    {
        IDashboardControlsList Controls { get; }
    }
}
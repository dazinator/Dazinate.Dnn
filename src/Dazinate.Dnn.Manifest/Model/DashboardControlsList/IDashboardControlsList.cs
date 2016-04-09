using Csla;
using Dazinate.Dnn.Manifest.Model.DashboardControl;
using Dazinate.Dnn.Manifest.Writer;

namespace Dazinate.Dnn.Manifest.Model.DashboardControlsList
{
    public interface IDashboardControlsList : IBusinessListBase<IDashboardControl>, IVisitable<IManifestXmlWriterVisitor>
    {
    }
}
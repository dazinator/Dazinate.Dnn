using Csla;
using Dazinate.Dnn.Manifest.Base;
using Dazinate.Dnn.Manifest.Writer;

namespace Dazinate.Dnn.Manifest.Package.Component.DashboardControl
{
    public interface IDashboardControlsList : IBusinessListBase<IDashboardControl>, IVisitable<IManifestXmlWriterVisitor>
    {
    }
}
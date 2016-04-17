using Csla;
using Dazinate.Dnn.Manifest.Base;
using Dazinate.Dnn.Manifest.Writer;

namespace Dazinate.Dnn.Manifest.Package.Component.DashboardControl
{
    public interface IDashboardControl : IBusinessBase, IVisitable<IManifestXmlWriterVisitor>
    {
        string Key { get; set; }
        string Source { get; set; }
        string LocalResourcesFile { get; set; }
        string ControllerClass { get; set; }
        bool IsEnabled { get; set; }
        int ViewOrder { get; set; }

    }
}
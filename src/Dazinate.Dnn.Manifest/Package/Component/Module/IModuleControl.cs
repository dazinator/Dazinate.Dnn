using Csla;
using Dazinate.Dnn.Manifest.Base;
using Dazinate.Dnn.Manifest.Writer;

namespace Dazinate.Dnn.Manifest.Package.Component.Module
{
    public interface IModuleControl : IBusinessBase, IVisitable<IManifestXmlWriterVisitor>
    {
        string ControlKey { get; set; }

        string ControlSource { get; set; }

        bool? SupportsPartialRendering { get; set; }

        string ControlTitle { get; set; }

        string ControlType { get; set; }

        string IconFile { get; set; }

        string HelpUrl { get; set; }

        int? ViewOrder { get; set; }
      

    }
}
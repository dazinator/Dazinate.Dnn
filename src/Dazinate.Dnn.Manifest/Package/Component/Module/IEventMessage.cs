using Csla;
using Dazinate.Dnn.Manifest.Base;
using Dazinate.Dnn.Manifest.Writer;

namespace Dazinate.Dnn.Manifest.Package.Component.Module
{
    public interface IEventMessage : IBusinessBase, IVisitable<IManifestXmlWriterVisitor>
    {
        string ProcessorType { get; set; }
        string ProcessorCommand { get; set; }

        IEventAttributesList Attributes { get; }


    }
}
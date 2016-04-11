using Csla;
using Dazinate.Dnn.Manifest.Model.EventAttributesList;
using Dazinate.Dnn.Manifest.Model.File;
using Dazinate.Dnn.Manifest.Writer;

namespace Dazinate.Dnn.Manifest.Model.EventMessage
{
    public interface IEventMessage : IBusinessBase, IVisitable<IManifestXmlWriterVisitor>
    {
        string ProcessorType { get; set; }
        string ProcessorCommand { get; set; }

        IEventAttributesList Attributes { get; }


    }
}
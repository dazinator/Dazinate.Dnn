using Csla;
using Dazinate.Dnn.Manifest.Base;

namespace Dazinate.Dnn.Manifest.Package.Component.Module
{
    public interface IEventMessage : IBusinessBase, IVisitable<IManifestVisitor>
    {
        string ProcessorType { get; set; }
        string ProcessorCommand { get; set; }

        IEventAttributesList Attributes { get; }


    }
}
using Csla;
using Dazinate.Dnn.Manifest.Base;
using Dazinate.Dnn.Manifest.Writer;

namespace Dazinate.Dnn.Manifest.Package.Component.Module
{
    public interface IEventAttribute : IBusinessBase, IVisitable<IManifestXmlWriterVisitor>
    {
        string Name { get; set; }
        string Value { get; set; }

    }
}
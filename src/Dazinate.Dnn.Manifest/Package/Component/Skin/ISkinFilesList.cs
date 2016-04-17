using Csla;
using Dazinate.Dnn.Manifest.Base;
using Dazinate.Dnn.Manifest.Writer;

namespace Dazinate.Dnn.Manifest.Package.Component.Skin
{
    public interface ISkinFilesList : IBusinessListBase<ISkinFile>, IVisitable<IManifestXmlWriterVisitor>
    {
    }
}
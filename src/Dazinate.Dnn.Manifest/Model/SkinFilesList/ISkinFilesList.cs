using Csla;
using Dazinate.Dnn.Manifest.Model.SkinFile;
using Dazinate.Dnn.Manifest.Writer;

namespace Dazinate.Dnn.Manifest.Model.SkinFilesList
{
    public interface ISkinFilesList : IBusinessListBase<ISkinFile>, IVisitable<IManifestXmlWriterVisitor>
    {
    }
}
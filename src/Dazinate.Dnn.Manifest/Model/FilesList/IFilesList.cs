using Csla;
using Dazinate.Dnn.Manifest.Model.File;
using Dazinate.Dnn.Manifest.Writer;

namespace Dazinate.Dnn.Manifest.Model.FilesList
{
    public interface IFilesList : IBusinessListBase<IFile>, IVisitable<IManifestXmlWriterVisitor>
    {

    }
}
using Csla;
using Dazinate.Dnn.Manifest.Base;
using Dazinate.Dnn.Manifest.Writer;

namespace Dazinate.Dnn.Manifest.Package.Component.Shared.File
{
    public interface IFilesList : IBusinessListBase<IFile>, IVisitable<IManifestXmlWriterVisitor>
    {

    }
}
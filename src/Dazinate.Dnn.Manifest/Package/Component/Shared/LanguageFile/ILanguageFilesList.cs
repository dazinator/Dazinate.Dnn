using Csla;
using Dazinate.Dnn.Manifest.Base;
using Dazinate.Dnn.Manifest.Writer;

namespace Dazinate.Dnn.Manifest.Package.Component.Shared.LanguageFile
{
    public interface ILanguageFilesList : IBusinessListBase<ILanguageFile>, IVisitable<IManifestXmlWriterVisitor>
    {

    }
}
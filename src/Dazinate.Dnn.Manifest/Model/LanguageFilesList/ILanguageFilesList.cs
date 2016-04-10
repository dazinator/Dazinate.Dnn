using Csla;
using Dazinate.Dnn.Manifest.Model.LanguageFile;
using Dazinate.Dnn.Manifest.Writer;

namespace Dazinate.Dnn.Manifest.Model.LanguageFilesList
{
    public interface ILanguageFilesList : IBusinessListBase<ILanguageFile>, IVisitable<IManifestXmlWriterVisitor>
    {

    }
}
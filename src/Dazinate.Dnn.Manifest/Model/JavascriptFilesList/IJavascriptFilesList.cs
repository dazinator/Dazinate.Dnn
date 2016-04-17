using Csla;
using Dazinate.Dnn.Manifest.Model.JavascriptFile;
using Dazinate.Dnn.Manifest.Writer;

namespace Dazinate.Dnn.Manifest.Model.JavascriptFilesList
{
    public interface IJavascriptFilesList : IBusinessListBase<IJavascriptFile>, IVisitable<IManifestXmlWriterVisitor>
    {

    }
}
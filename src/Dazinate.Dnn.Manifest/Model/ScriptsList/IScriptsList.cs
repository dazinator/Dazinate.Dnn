using Csla;
using Dazinate.Dnn.Manifest.Model.File;
using Dazinate.Dnn.Manifest.Model.Script;
using Dazinate.Dnn.Manifest.Writer;

namespace Dazinate.Dnn.Manifest.Model.ScriptsList
{
    public interface IScriptsList : IBusinessListBase<IScript>, IVisitable<IManifestXmlWriterVisitor>
    {

    }
}
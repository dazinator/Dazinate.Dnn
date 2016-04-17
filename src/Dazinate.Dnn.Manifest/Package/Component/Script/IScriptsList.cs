using Csla;
using Dazinate.Dnn.Manifest.Base;
using Dazinate.Dnn.Manifest.Writer;

namespace Dazinate.Dnn.Manifest.Package.Component.Script
{
    public interface IScriptsList : IBusinessListBase<IScript>, IVisitable<IManifestXmlWriterVisitor>
    {

    }
}
using Csla;
using Dazinate.Dnn.Manifest.Base;
using Dazinate.Dnn.Manifest.Writer;

namespace Dazinate.Dnn.Manifest.Package.Component.Module
{
    public interface IModulePermission : IBusinessBase, IVisitable<IManifestXmlWriterVisitor>
    {
        string Code { get; set; }

        string Key { get; set; }

        string Name { get; set; }

    }
}
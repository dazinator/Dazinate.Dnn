using Csla;
using Dazinate.Dnn.Manifest.Base;

namespace Dazinate.Dnn.Manifest.Package.Component.Module
{
    public interface IModulePermission : IBusinessBase, IVisitable<IManifestVisitor>
    {
        string Code { get; set; }

        string Key { get; set; }

        string Name { get; set; }

    }
}
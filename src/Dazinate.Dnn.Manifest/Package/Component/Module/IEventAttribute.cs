using Csla;
using Dazinate.Dnn.Manifest.Base;

namespace Dazinate.Dnn.Manifest.Package.Component.Module
{
    public interface IEventAttribute : IBusinessBase, IVisitable<IManifestVisitor>
    {
        string Name { get; set; }
        string Value { get; set; }

    }
}
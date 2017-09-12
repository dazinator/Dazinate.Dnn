using Csla;
using Dazinate.Dnn.Manifest.Base;

namespace Dazinate.Dnn.Manifest.Package
{
    public interface IOwner : IBusinessBase, IVisitable<IManifestVisitor>
    {
        string Name { get; set; }
        string Organisation { get; set; }
        string Url { get; set; }
        string Email { get; set; }

        bool IsEmpty();

    }
}
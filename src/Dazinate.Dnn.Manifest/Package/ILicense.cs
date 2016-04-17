using Csla;
using Dazinate.Dnn.Manifest.Base;

namespace Dazinate.Dnn.Manifest.Package
{
    public interface ILicense : IBusinessBase, IVisitable<IManifestVisitor>
    {
        string SourceFile { get; set; }
        string Contents { get; set; }

        bool IsEmpty();
    }
}
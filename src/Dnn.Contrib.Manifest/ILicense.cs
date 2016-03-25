using Csla;

namespace Dnn.Contrib.Manifest
{
    public interface ILicense : IBusinessBase
    {
        string SourceFile { get; set; }
        string Contents { get; set; }

        bool IsEmpty();
    }
}
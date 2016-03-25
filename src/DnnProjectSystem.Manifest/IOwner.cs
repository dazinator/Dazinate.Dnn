using Csla;

namespace Dnn.Contrib.Manifest
{
    public interface IOwner : IBusinessBase
    {
        string Name { get; set; }
        string Organisation { get; set; }
        string Url { get; set; }
        string Email { get; set; }

    }
}
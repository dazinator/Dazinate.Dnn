using Csla;

namespace Dazinate.Dnn.Manifest
{
    public interface IOwner : IBusinessBase
    {
        string Name { get; set; }
        string Organisation { get; set; }
        string Url { get; set; }
        string Email { get; set; }

    }
}
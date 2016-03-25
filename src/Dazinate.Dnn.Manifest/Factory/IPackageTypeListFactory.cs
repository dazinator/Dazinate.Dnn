using Csla;

namespace Dazinate.Dnn.Manifest.Factory
{
    public interface IPackageTypeListFactory
    {
        NameValueListBase<string, string> Get();
    }
}
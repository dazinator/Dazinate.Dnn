using Csla;

namespace Dnn.Contrib.Manifest.Factory
{
    public interface IPackageTypeListFactory
    {
        NameValueListBase<string, string> Get();
    }
}
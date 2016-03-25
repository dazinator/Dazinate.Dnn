using Csla;

namespace Dnn.Contrib.Manifest.Factory
{
    public class PackageTypeListFactory : IPackageTypeListFactory
    {
        private PackageTypeList _cache;


        public NameValueListBase<string, string> Get()
        {
            if (_cache == null)
                _cache = DataPortal.Fetch<PackageTypeList>();

            return _cache;
            //return Csla.DataPortal.Fetch<PackageTypeList>();
        }
    }
}
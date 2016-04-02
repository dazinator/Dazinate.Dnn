using Csla;
using Dazinate.Dnn.Manifest.Model;
using Dazinate.Dnn.Manifest.Model.PackageType;

namespace Dazinate.Dnn.Manifest.Factory
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
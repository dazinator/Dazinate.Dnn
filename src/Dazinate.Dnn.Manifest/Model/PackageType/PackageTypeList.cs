using System;
using Csla;
using Csla.Server;
using Dazinate.Dnn.Manifest.Model.PackageType.ObjectFactory;

namespace Dazinate.Dnn.Manifest.Model.PackageType
{
    [ObjectFactory(typeof(IPackageTypeListObjectFactory))]
    [Serializable]
    public class PackageTypeList : NameValueListBase<string, string>
    {
        
    }
}
using System;
using Csla;
using Csla.Server;
using Dazinate.Dnn.Manifest.ObjectFactory;

namespace Dazinate.Dnn.Manifest.Model
{
    [ObjectFactory(typeof(IPackageTypeListObjectFactory))]
    [Serializable]
    public class PackageTypeList : NameValueListBase<string, string>
    {
        
    }
}
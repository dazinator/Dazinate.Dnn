using System;
using Csla;
using Csla.Server;
using Dazinate.Dnn.Manifest.Package.ObjectFactory;

namespace Dazinate.Dnn.Manifest.Package
{
    [ObjectFactory(typeof(IPackageTypeListObjectFactory))]
    [Serializable]
    public class PackageTypeList : NameValueListBase<string, string>
    {
        
    }
}
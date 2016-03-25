using System;
using Csla;
using Csla.Server;
using Dnn.Contrib.Manifest.ObjectFactory;

namespace Dnn.Contrib.Manifest
{
    [ObjectFactory(typeof(IPackageTypeListObjectFactory))]
    [Serializable]
    public class PackageTypeList : NameValueListBase<string, string>
    {
        
    }
}
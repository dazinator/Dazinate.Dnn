using System;
using Csla;
using Csla.Server;
using Dazinate.Dnn.Manifest.ObjectFactory;

namespace Dazinate.Dnn.Manifest
{
    [ObjectFactory(typeof(IPackageDependencyObjectFactory))]
    [Serializable]
    public class CoreVersionPackageDependency : BusinessBase<CoreVersionPackageDependency>, IPackageDependency
    {

        public static readonly PropertyInfo<string> VersionProperty = RegisterProperty<string>(c => c.Version);
        public string Version
        {
            get { return GetProperty(VersionProperty); }
            set { SetProperty(VersionProperty, value); }
        }

    }
}
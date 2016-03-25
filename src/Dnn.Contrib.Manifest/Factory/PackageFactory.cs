using System;

namespace Dnn.Contrib.Manifest.Factory
{
    [Serializable]
    internal class PackageFactory : IPackageFactory
    {
        public IPackage CreateNew()
        {
            return Csla.DataPortal.Create<Package>();
        }
    }
}
using System;
using Csla;
using Csla.Server;
using Dnn.Contrib.Manifest.Factory;
using Dnn.Contrib.Manifest.ObjectFactory;

namespace Dnn.Contrib.Manifest
{
    [ObjectFactory(typeof(IPackagesListObjectFactory))]
    [Serializable]
    public class PackagesList : BusinessListBase<PackagesList, IPackage>, IPackagesList
    {
        private readonly IPackageFactory _factory;

        public PackagesList() : this(new PackageFactory())
        {
        }

        internal PackagesList(IPackageFactory factory)
        {
            _factory = factory;
        }

        protected override IPackage AddNewCore()
        {
            //base.AddNewCore();
            var child = _factory.CreateNew();
            Add(child);
            return child;
            
        }
      
    }
}
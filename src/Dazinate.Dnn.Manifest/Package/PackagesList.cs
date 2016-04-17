using System;
using Csla;
using Csla.Server;
using Dazinate.Dnn.Manifest.Factory;
using Dazinate.Dnn.Manifest.Package.ObjectFactory;
using Dazinate.Dnn.Manifest.Writer;

namespace Dazinate.Dnn.Manifest.Package
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

        public void Accept(IManifestXmlWriterVisitor visitor)
        {
            visitor.Visit(this);
        }

        public IPackage AddNewPackage()
        {
            var result = this.AddNew();
            return (IPackage)result;
        }
    }
}
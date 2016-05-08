using System;
using Csla;
using Csla.Server;
using Dazinate.Dnn.Manifest.Base;
using Dazinate.Dnn.Manifest.Factory;
using Dazinate.Dnn.Manifest.Package.Dependency.ObjectFactory;

namespace Dazinate.Dnn.Manifest.Package.Dependency
{
    [ObjectFactory(typeof(IDependenciesListObjectFactory))]
    [Serializable]
    public class DependenciesList : BusinessListBase<DependenciesList, IDependency>, IDependenciesList
    {
        private readonly IDependencyFactory _factory;

        public DependenciesList() : this(new DependencyFactory())
        {
        }

        internal DependenciesList(IDependencyFactory factory)
        {
            _factory = factory;
        }

        public void Accept(IManifestVisitor visitor)
        {
            visitor.Visit(this);
        }

        public IDependency AddNewCoreVersionDependency()
        {
            var dep = _factory.CreateNewCoreVersionDependency();
            this.Add(dep);
            return dep;
        }

        public IDependency AddNewCustomDependency()
        {
            var dep = _factory.CreateNewCustomDependency();
            this.Add(dep);
            return dep;
        }

        public IDependency AddNewManagedPackageDependency()
        {
            var dep = _factory.CreateNewManagedPackageDependency();
            this.Add(dep);
            return dep;
        }

        public IDependency AddNewPackageDependency()
        {
            var dep = _factory.CreateNewPackageDependency();
            this.Add(dep);
            return dep;
        }

        public IDependency AddNewTypeDependency()
        {
            var dep = _factory.CreateNewTypeDependency();
            this.Add(dep);
            return dep;
        }
    }
}
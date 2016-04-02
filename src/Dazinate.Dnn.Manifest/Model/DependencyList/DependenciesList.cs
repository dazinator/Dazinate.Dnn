using System;
using Csla;
using Csla.Server;
using Dazinate.Dnn.Manifest.Factory;
using Dazinate.Dnn.Manifest.Model.Dependency;
using Dazinate.Dnn.Manifest.Model.DependencyList.ObjectFactory;
using Dazinate.Dnn.Manifest.Writer;

namespace Dazinate.Dnn.Manifest.Model.DependencyList
{
    [ObjectFactory(typeof(IDependenciesListObjectFactory))]
    [Serializable]
    public class DependenciesList : BusinessListBase<DependenciesList, IDependency>, IDependenciesList
    {
        private readonly IPackageFactory _factory;

        public DependenciesList() : this(new PackageFactory())
        {
        }

        internal DependenciesList(IPackageFactory factory)
        {
            _factory = factory;
        }

        public void Accept(IManifestXmlWriterVisitor visitor)
        {
            visitor.Visit(this);
        }
       
    }
}
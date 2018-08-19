using System;
using Csla;
using Csla.Server;
using Dazinate.Dnn.Manifest.Base;
using Dazinate.Dnn.Manifest.Package.Component.Assembly.ObjectFactory;

namespace Dazinate.Dnn.Manifest.Package.Component.Assembly
{
    [ObjectFactory(typeof(IAssembliesListObjectFactory))]
    [Serializable]
    public class AssembliesList : BusinessListBase<AssembliesList, IAssembly>, IAssembliesList
    {
        // private readonly IPackageFactory _factory;

        public AssembliesList()
        {
        }

        public void Accept(IManifestVisitor visitor)
        {
            visitor.Visit(this);
        }

#if !AddNewCoreReturnVoid
        protected override IAssembly AddNewCore()
        {
            //base.AddNewCore();
            var item = Csla.DataPortal.Create<Assembly>();
            Add(item);
            return item;
        }
#else
        protected override void AddNewCore()
        {
            //base.AddNewCore();
             var item = Csla.DataPortal.Create<Assembly>();
            Add(item);           
        }
#endif

    }
}
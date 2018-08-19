using System;
using Csla;
using Csla.Server;
using Dazinate.Dnn.Manifest.Base;
using Dazinate.Dnn.Manifest.Package.Component.DashboardControl.ObjectFactory;

namespace Dazinate.Dnn.Manifest.Package.Component.DashboardControl
{
    [ObjectFactory(typeof(IDashboardControlsListObjectFactory))]
    [Serializable]
    public class DashboardControlsList : BusinessListBase<DashboardControlsList, IDashboardControl>, IDashboardControlsList
    {
        // private readonly IPackageFactory _factory;

        public DashboardControlsList()
        {
        }

        public void Accept(IManifestVisitor visitor)
        {
            visitor.Visit(this);
        }

#if !AddNewCoreReturnVoid
        protected override IDashboardControl AddNewCore()
        {
            //base.AddNewCore();
            var item = Csla.DataPortal.Create<DashboardControl>();
            Add(item);
            return item;
        }
#else
        protected override void AddNewCore()
        {
            //base.AddNewCore();
             var item = Csla.DataPortal.Create<DashboardControl>();
            Add(item);           
        }
#endif       

    }
}
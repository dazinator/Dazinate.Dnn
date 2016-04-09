using System;
using Csla;
using Csla.Server;
using Dazinate.Dnn.Manifest.Model.Component.ObjectFactory;
using Dazinate.Dnn.Manifest.Model.DashboardControlsList;
using Dazinate.Dnn.Manifest.Model.Package;
using Dazinate.Dnn.Manifest.Writer;

namespace Dazinate.Dnn.Manifest.Model.Component
{
    [ObjectFactory(typeof(IComponentObjectFactory))]
    [Serializable]
    public class DashboardControlComponent : BusinessBase<DashboardControlComponent>, IDashboardControlComponent
    {

        public static readonly PropertyInfo<DashboardControlsList.DashboardControlsList> DashboardControlsProperty = RegisterProperty<DashboardControlsList.DashboardControlsList>(c => c.Controls);
        public IDashboardControlsList Controls
        {
            get { return GetProperty(DashboardControlsProperty); }
            set { SetProperty(DashboardControlsProperty, value); }
        }

        public void Accept(IManifestXmlWriterVisitor visitor)
        {
            visitor.Visit(this);
        }

        public bool IsCompatibleWithPackage(IPackage package)
        {
            return package.Type.ToLowerInvariant() == "dashboardcontrol";
        }
    }
}
using System;
using Csla;
using Csla.Server;
using Dazinate.Dnn.Manifest.Package.Component.ObjectFactory;
using Dazinate.Dnn.Manifest.Writer;

namespace Dazinate.Dnn.Manifest.Package.Component.DashboardControl
{
    [ObjectFactory(typeof(IComponentObjectFactory))]
    [Serializable]
    public class DashboardControlComponent : BusinessBase<DashboardControlComponent>, IDashboardControlComponent
    {

        public static readonly PropertyInfo<DashboardControlsList> DashboardControlsProperty = RegisterProperty<DashboardControlsList>(c => c.Controls);
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
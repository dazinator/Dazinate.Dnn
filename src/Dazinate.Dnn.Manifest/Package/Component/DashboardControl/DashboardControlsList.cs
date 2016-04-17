using System;
using Csla;
using Csla.Server;
using Dazinate.Dnn.Manifest.Package.Component.DashboardControl.ObjectFactory;
using Dazinate.Dnn.Manifest.Writer;

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

        public void Accept(IManifestXmlWriterVisitor visitor)
        {
            visitor.Visit(this);
        }

    }
}
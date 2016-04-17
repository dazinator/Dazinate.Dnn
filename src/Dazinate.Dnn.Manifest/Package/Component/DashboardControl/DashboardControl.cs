using System;
using Csla;
using Csla.Server;
using Dazinate.Dnn.Manifest.Package.Component.DashboardControl.ObjectFactory;
using Dazinate.Dnn.Manifest.Writer;

namespace Dazinate.Dnn.Manifest.Package.Component.DashboardControl
{
    [ObjectFactory(typeof(IDashboardControlObjectFactory))]
    [Serializable]
    public class DashboardControl : BusinessBase<DashboardControl>, IDashboardControl
    {
        public static readonly PropertyInfo<string> KeyProperty = RegisterProperty<string>(c => c.Key);
        public string Key
        {
            get { return GetProperty(KeyProperty); }
            set { SetProperty(KeyProperty, value); }
        }

        public static readonly PropertyInfo<string> SourceProperty = RegisterProperty<string>(c => c.Source);
        public string Source
        {
            get { return GetProperty(SourceProperty); }
            set { SetProperty(SourceProperty, value); }
        }


        public static readonly PropertyInfo<string> LocalResourcesFileProperty = RegisterProperty<string>(c => c.LocalResourcesFile);
        public string LocalResourcesFile
        {
            get { return GetProperty(LocalResourcesFileProperty); }
            set { SetProperty(LocalResourcesFileProperty, value); }
        }

        public static readonly PropertyInfo<string> ControllerClassProperty = RegisterProperty<string>(c => c.ControllerClass);
        public string ControllerClass
        {
            get { return GetProperty(ControllerClassProperty); }
            set { SetProperty(ControllerClassProperty, value); }
        }


        public static readonly PropertyInfo<bool> IsEnabledProperty = RegisterProperty<bool>(c => c.IsEnabled);
        public bool IsEnabled
        {
            get { return GetProperty(IsEnabledProperty); }
            set { SetProperty(IsEnabledProperty, value); }
        }

        public static readonly PropertyInfo<int> ViewOrderProperty = RegisterProperty<int>(c => c.ViewOrder);
        public int ViewOrder
        {
            get { return GetProperty(ViewOrderProperty); }
            set { SetProperty(ViewOrderProperty, value); }
        }

        public void Accept(IManifestXmlWriterVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
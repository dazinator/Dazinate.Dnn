using System;
using Csla;
using Csla.Server;
using Dazinate.Dnn.Manifest.Base;
using Dazinate.Dnn.Manifest.Package.Component.Module.ObjectFactory;

namespace Dazinate.Dnn.Manifest.Package.Component.Module
{
    [ObjectFactory(typeof(IModuleControlObjectFactory))]
    [Serializable]
    public class ModuleControl : BusinessBase<ModuleControl>, IModuleControl
    {

        public static readonly PropertyInfo<string> ControlKeyProperty = RegisterProperty<string>(c => c.ControlKey);
        public string ControlKey
        {
            get { return GetProperty(ControlKeyProperty); }
            set { SetProperty(ControlKeyProperty, value); }
        }

        public static readonly PropertyInfo<string> ControlSourceProperty = RegisterProperty<string>(c => c.ControlSource);
        public string ControlSource
        {
            get { return GetProperty(ControlSourceProperty); }
            set { SetProperty(ControlSourceProperty, value); }
        }


        public static readonly PropertyInfo<bool?> SupportsPartialRenderingProperty = RegisterProperty<bool?>(c => c.SupportsPartialRendering);
        public bool? SupportsPartialRendering
        {
            get { return GetProperty(SupportsPartialRenderingProperty); }
            set { SetProperty(SupportsPartialRenderingProperty, value); }
        }

        public static readonly PropertyInfo<string> ControlTitleProperty = RegisterProperty<string>(c => c.ControlTitle);
        public string ControlTitle
        {
            get { return GetProperty(ControlTitleProperty); }
            set { SetProperty(ControlTitleProperty, value); }
        }

        public static readonly PropertyInfo<string> ControlTypeProperty = RegisterProperty<string>(c => c.ControlType);
        public string ControlType
        {
            get { return GetProperty(ControlTypeProperty); }
            set { SetProperty(ControlTypeProperty, value); }
        }

        public static readonly PropertyInfo<string> IconFileProperty = RegisterProperty<string>(c => c.IconFile);
        public string IconFile
        {
            get { return GetProperty(IconFileProperty); }
            set { SetProperty(IconFileProperty, value); }
        }

        public static readonly PropertyInfo<string> HelpUrlProperty = RegisterProperty<string>(c => c.HelpUrl);
        public string HelpUrl
        {
            get { return GetProperty(HelpUrlProperty); }
            set { SetProperty(HelpUrlProperty, value); }
        }

        public static readonly PropertyInfo<int?> ViewOrderProperty = RegisterProperty<int?>(c => c.ViewOrder);
        public int? ViewOrder
        {
            get { return GetProperty(ViewOrderProperty); }
            set { SetProperty(ViewOrderProperty, value); }
        }


        public void Accept(IManifestVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
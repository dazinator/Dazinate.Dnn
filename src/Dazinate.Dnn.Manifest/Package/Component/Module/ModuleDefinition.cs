using System;
using Csla;
using Csla.Server;
using Dazinate.Dnn.Manifest.Package.Component.Module.ObjectFactory;
using Dazinate.Dnn.Manifest.Writer;

namespace Dazinate.Dnn.Manifest.Package.Component.Module
{
    [ObjectFactory(typeof(IModuleDefinitionObjectFactory))]
    [Serializable]
    public class ModuleDefinition : BusinessBase<ModuleDefinition>, IModuleDefinition
    {

        public static readonly PropertyInfo<string> FriendlyNameProperty = RegisterProperty<string>(c => c.FriendlyName);
        public string FriendlyName
        {
            get { return GetProperty(FriendlyNameProperty); }
            set { SetProperty(FriendlyNameProperty, value); }
        }

        public static readonly PropertyInfo<int?> DefaultCacheTimeProperty = RegisterProperty<int?>(c => c.DefaultCacheTime);
        public int? DefaultCacheTime
        {
            get { return GetProperty(DefaultCacheTimeProperty); }
            set { SetProperty(DefaultCacheTimeProperty, value); }
        }

        public static readonly PropertyInfo<ModuleControlsList> ModuleControlsListProperty = RegisterProperty<ModuleControlsList>(c => c.ModuleControls);
        public IModuleControlsList ModuleControls
        {
            get { return GetProperty(ModuleControlsListProperty); }
            set { SetProperty(ModuleControlsListProperty, value); }
        }

        public static readonly PropertyInfo<ModulePermissionsList> ModulePermissionsListProperty = RegisterProperty<ModulePermissionsList>(c => c.ModulePermissions);
        public IModulePermissionsList ModulePermissions
        {
            get { return GetProperty(ModulePermissionsListProperty); }
            set { SetProperty(ModulePermissionsListProperty, value); }
        }


        public void Accept(IManifestXmlWriterVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
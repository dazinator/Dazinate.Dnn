using System;
using Csla;
using Csla.Server;
using Dazinate.Dnn.Manifest.Base;
using Dazinate.Dnn.Manifest.Package.Component.ObjectFactory;

namespace Dazinate.Dnn.Manifest.Package.Component.Module
{
    [ObjectFactory(typeof(IComponentObjectFactory))]
    [Serializable]
    public class ModuleComponent : BusinessBase<ModuleComponent>, IModuleComponent
    {

        public static readonly PropertyInfo<string> ModuleNameProperty = RegisterProperty<string>(c => c.ModuleName);
        public string ModuleName
        {
            get { return GetProperty(ModuleNameProperty); }
            set { SetProperty(ModuleNameProperty, value); }
        }

        public static readonly PropertyInfo<string> FolderNameProperty = RegisterProperty<string>(c => c.FolderName);
        public string FolderName
        {
            get { return GetProperty(FolderNameProperty); }
            set { SetProperty(FolderNameProperty, value); }
        }

        public static readonly PropertyInfo<string> BusinessControllerClassProperty = RegisterProperty<string>(c => c.BusinessControllerClass);
        public string BusinessControllerClass
        {
            get { return GetProperty(BusinessControllerClassProperty); }
            set { SetProperty(BusinessControllerClassProperty, value); }
        }

        public static readonly PropertyInfo<string> CodeSubDirectoryProperty = RegisterProperty<string>(c => c.CodeSubDirectory);
        public string CodeSubDirectory
        {
            get { return GetProperty(CodeSubDirectoryProperty); }
            set { SetProperty(CodeSubDirectoryProperty, value); }
        }

        public static readonly PropertyInfo<bool?> IsAdminProperty = RegisterProperty<bool?>(c => c.IsAdmin);
        public bool? IsAdmin
        {
            get { return GetProperty(IsAdminProperty); }
            set { SetProperty(IsAdminProperty, value); }
        }

        public static readonly PropertyInfo<bool?> IsPremiumProperty = RegisterProperty<bool?>(c => c.IsPremium);
        public bool? IsPremium
        {
            get { return GetProperty(IsPremiumProperty); }
            set { SetProperty(IsPremiumProperty, value); }
        }


        public static readonly PropertyInfo<SupportedFeaturesList> SupportedFeaturesProperty = RegisterProperty<SupportedFeaturesList>(c => c.SupportedFeatures);
        public ISupportedFeaturesList SupportedFeatures
        {
            get { return GetProperty(SupportedFeaturesProperty); }
            set { SetProperty(SupportedFeaturesProperty, value); }
        }

        public static readonly PropertyInfo<ModuleDefinitionsList> ModuleDefinitionsProperty = RegisterProperty<ModuleDefinitionsList>(c => c.ModuleDefinitions);
        public IModuleDefinitionsList ModuleDefinitions
        {
            get { return GetProperty(ModuleDefinitionsProperty); }
            set { SetProperty(ModuleDefinitionsProperty, value); }
        }

        public static readonly PropertyInfo<EventMessage> EventMessageProperty = RegisterProperty<EventMessage>(c => c.EventMessage);
        public IEventMessage EventMessage
        {
            get { return GetProperty(EventMessageProperty); }
            set { SetProperty(EventMessageProperty, value); }
        }


        public void Accept(IManifestVisitor visitor)
        {
            visitor.Visit(this);
        }

        public bool IsCompatibleWithPackage(IPackage package)
        {
            return package.Type.ToLowerInvariant() == "module";
        }

        public override string ToString()
        {
            return "Module";
        }
    }
}
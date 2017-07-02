using System;
using Dazinate.Dnn.Manifest.Package.Component.Assembly;
using Dazinate.Dnn.Manifest.Package.Component;
using Dazinate.Dnn.Manifest.Package.Component.AuthenticationSystem;
using Dazinate.Dnn.Manifest.Package.Component.Cleanup;
using Dazinate.Dnn.Manifest.Package.Component.Container;
using Dazinate.Dnn.Manifest.Package.Component.CoreLanguage;
using Dazinate.Dnn.Manifest.Package.Component.DashboardControl;
using Dazinate.Dnn.Manifest.Package.Component.ExtensionLanguage;
using Dazinate.Dnn.Manifest.Package.Component.File;
using Dazinate.Dnn.Manifest.Package.Component.JavascriptFile;
using Dazinate.Dnn.Manifest.Package.Component.Config;
using Dazinate.Dnn.Manifest.Package.Component.JavascriptLibrary;
using Dazinate.Dnn.Manifest.Package.Component.Module;
using Dazinate.Dnn.Manifest.Package.Component.Provider;
using Dazinate.Dnn.Manifest.Package.Component.ResourceFile;
using Dazinate.Dnn.Manifest.Package.Component.Script;
using Dazinate.Dnn.Manifest.Package.Component.Skin;
using Dazinate.Dnn.Manifest.Package.Component.SkinObject;
using Dazinate.Dnn.Manifest.Package.Component.UrlProvider;

namespace Dazinate.Dnn.Manifest.Factory
{

    [Serializable]
    internal class ComponentFactory : IComponentFactory
    {

        public T CreateNewComponent<T>()
            where T : class, IComponent
        {
            if (typeof(T) == typeof(IAssemblyComponent))
            {
                return Csla.DataPortal.Create<AssemblyComponent>(ComponentType.Assembly) as T;
            }

            if (typeof(T) == typeof(IAuthenticationSystemComponent))
            {
                return Csla.DataPortal.Create<AuthenticationSystemComponent>(ComponentType.AuthenticationSystem) as T;
            }

            if (typeof(T) == typeof(ICleanupComponent))
            {
                return Csla.DataPortal.Create<CleanupComponent>(ComponentType.Cleanup) as T;
            }

            if (typeof(T) == typeof(IConfigComponent))
            {
                return Csla.DataPortal.Create<ConfigComponent>(ComponentType.Config) as T;
            }

            if (typeof(T) == typeof(IContainerComponent))
            {
                return Csla.DataPortal.Create<ContainerComponent>(ComponentType.Container) as T;
            }

            if (typeof(T) == typeof(ICoreLanguageComponent))
            {
                return Csla.DataPortal.Create<CoreLanguageComponent>(ComponentType.CoreLanguage) as T;
            }

            if (typeof(T) == typeof(IDashboardControlComponent))
            {
                return Csla.DataPortal.Create<DashboardControlComponent>(ComponentType.DashboardControl) as T;
            }

            if (typeof(T) == typeof(IExtensionLanguageComponent))
            {
                return Csla.DataPortal.Create<ExtensionLanguageComponent>(ComponentType.ExtensionLanguage) as T;
            }

            if (typeof(T) == typeof(IFileComponent))
            {
                return Csla.DataPortal.Create<FileComponent>(ComponentType.File) as T;
            }

            if (typeof(T) == typeof(IJavascriptFileComponent))
            {
                return Csla.DataPortal.Create<JavascriptFileComponent>(ComponentType.JavaScriptFile) as T;
            }

            if (typeof(T) == typeof(IJavascriptLibraryComponent))
            {
                return Csla.DataPortal.Create<JavascriptLibraryComponent>(ComponentType.JavaScript_Library) as T;
            }

            if (typeof(T) == typeof(IModuleComponent))
            {
                return Csla.DataPortal.Create<ModuleComponent>(ComponentType.Module) as T;
            }

            if (typeof(T) == typeof(IProviderComponent))
            {
                return Csla.DataPortal.Create<ProviderComponent>(ComponentType.Provider) as T;
            }

            if (typeof(T) == typeof(IResourceFileComponent))
            {
                return Csla.DataPortal.Create<ResourceFileComponent>(ComponentType.ResourceFile) as T;
            }

            if (typeof(T) == typeof(IScriptComponent))
            {
                return Csla.DataPortal.Create<ScriptComponent>(ComponentType.Script) as T;
            }

            if (typeof(T) == typeof(ISkinComponent))
            {
                return Csla.DataPortal.Create<SkinComponent>(ComponentType.Skin) as T;
            }

            if (typeof(T) == typeof(ISkinObjectComponent))
            {
                return Csla.DataPortal.Create<SkinObjectComponent>(ComponentType.SkinObject) as T;
            }

            if (typeof(T) == typeof(IUrlProviderComponent))
            {
                return Csla.DataPortal.Create<UrlProviderComponent>(ComponentType.UrlProvider) as T;
            }

            throw new NotSupportedException();

        }

       
    }
}
﻿using System;
using System.Collections.Concurrent;
using Csla.Server;
using Dazinate.Dnn.Manifest.Base;
using Dazinate.Dnn.Manifest.Factory;
using Dazinate.Dnn.Manifest.ObjectFactory;
using Dazinate.Dnn.Manifest.Package.Component.Assembly.ObjectFactory;
using Dazinate.Dnn.Manifest.Package.Component.AuthenticationSystem.ObjectFactory;
using Dazinate.Dnn.Manifest.Package.Component.Cleanup.ObjectFactory;
using Dazinate.Dnn.Manifest.Package.Component.Config.ObjectFactory;
using Dazinate.Dnn.Manifest.Package.Component.Container.ObjectFactory;
using Dazinate.Dnn.Manifest.Package.Component.CoreLanguage.ObjectFactory;
using Dazinate.Dnn.Manifest.Package.Component.DashboardControl.ObjectFactory;
using Dazinate.Dnn.Manifest.Package.Component.ExtensionLanguage.ObjectFactory;
using Dazinate.Dnn.Manifest.Package.Component.File.ObjectFactory;
using Dazinate.Dnn.Manifest.Package.Component.JavascriptFile.ObjectFactory;
using Dazinate.Dnn.Manifest.Package.Component.JavascriptLibrary.ObjectFactory;
using Dazinate.Dnn.Manifest.Package.Component.Module.ObjectFactory;
using Dazinate.Dnn.Manifest.Package.Component.ObjectFactory;
using Dazinate.Dnn.Manifest.Package.Component.Provider.ObjectFactory;
using Dazinate.Dnn.Manifest.Package.Component.ResourceFile.ObjectFactory;
using Dazinate.Dnn.Manifest.Package.Component.Script.ObjectFactory;
using Dazinate.Dnn.Manifest.Package.Component.Shared.File.ObjectFactory;
using Dazinate.Dnn.Manifest.Package.Component.Shared.LanguageFile.ObjectFactory;
using Dazinate.Dnn.Manifest.Package.Component.Skin.ObjectFactory;
using Dazinate.Dnn.Manifest.Package.Component.SkinObject.ObjectFactory;
using Dazinate.Dnn.Manifest.Package.Component.UrlProvider.ObjectFactory;
using Dazinate.Dnn.Manifest.Package.Dependency;
using Dazinate.Dnn.Manifest.Package.Dependency.ObjectFactory;
using Dazinate.Dnn.Manifest.Package.ObjectFactory;
using Assembly = System.Reflection.Assembly;
using TinyIoC;
using Microsoft.Extensions.DependencyInjection;

namespace Dazinate.Dnn.Manifest.Ioc
{
    public class TinyIocObjectFactoryLoader : IObjectFactoryLoader
    {

        static TinyIocObjectFactoryLoader()
        {

            SerializationWorkaround();

        }

        private readonly TinyIoCContainer _container;

        private readonly ConcurrentDictionary<string, Type> _cachedTypes;

        /// <summary>
        /// Creates an object factory loader that uses autofac to resolve types. 
        /// </summary>
        public TinyIocObjectFactoryLoader() : this(TinyIoCContainer.Current)
        {

        }

        public TinyIocObjectFactoryLoader(TinyIoCContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException(nameof(container));
            }
            _container = container;
            SetupContainer(_container);
            _cachedTypes = new ConcurrentDictionary<string, Type>();
        }

        private void SetupContainer(TinyIoCContainer container)
        {
            container.Register<IObjectActivator, TinyIocObjectActivator>();
            container.Register<IPackagesDnnManifestObjectFactory, PackagesDnnManifestObjectFactory>();
            container.Register<IDnnManifest, PackagesDnnManifest>();
            container.Register<IDnnManifestFactory<IPackagesDnnManifest>, PackagesDnnManifestFactory>();
            container.Register<IDnnManifestFactory<IDnnManifest>, PackagesDnnManifestFactory>();
            container.Register<IPackageTypeListFactory>(new PackageTypeListFactory());
            container.Register<IPackageTypeListObjectFactory, PackageTypeListObjectFactory>();
            container.Register<IPackagesListObjectFactory, PackagesListObjectFactory>();
            container.Register<IPackageFactory, PackageFactory>();
            container.Register<IPackageObjectFactory, PackageObjectFactory>();

            container.RegisterMultiple(typeof(IDependency), new[] { typeof(PackageDependency), typeof(CoreVersionDependency), typeof(ManagedPackageDependency) });
            container.Register<IDependenciesListObjectFactory, DependenciesListObjectFactory>();
            container.Register<IDependencyObjectFactory, DependencyObjectFactory>();

            container.Register<IDependencyFactory, DependencyFactory>();

            container.Register<IComponentFactory, ComponentFactory>();
            container.Register<IComponentsListObjectFactory, ComponentsListObjectFactory>();
            container.Register<IComponentObjectFactory, ComponentObjectFactory>();


            container.Register<IComponentSubObjectFactory, AssemblyComponentSubObjectFactory>();
            container.Register<IComponentSubObjectFactory, AuthenticationSystemSubObjectFactory>();
            container.Register<IComponentSubObjectFactory, CleanupComponentSubObjectFactory>();
            container.Register<IComponentSubObjectFactory, ConfigComponentSubObjectFactory>();
            container.Register<IComponentSubObjectFactory, ContainerComponentSubObjectFactory>();
            container.Register<IComponentSubObjectFactory, DashboardControlComponentSubObjectFactory>();
            container.Register<IComponentSubObjectFactory, FileComponentSubObjectFactory>();
            container.Register<IComponentSubObjectFactory, CoreLanguageComponentSubObjectFactory>();
            container.Register<IComponentSubObjectFactory, ExtensionLanguageComponentSubObjectFactory>();

            container.Register<IComponentSubObjectFactory, ModuleComponentSubObjectFactory>();
            container.Register<IComponentSubObjectFactory, ProviderComponentSubObjectFactory>();
            container.Register<IComponentSubObjectFactory, ResourceFileComponentSubObjectFactory>();
            container.Register<IComponentSubObjectFactory, ScriptComponentSubObjectFactory>();
            container.Register<IComponentSubObjectFactory, UrlProviderComponentSubObjectFactory>();
            container.Register<IComponentSubObjectFactory, SkinObjectComponentSubObjectFactory>();
            container.Register<IComponentSubObjectFactory, SkinComponentSubObjectFactory>();
            container.Register<IComponentSubObjectFactory, JavascriptFileComponentSubObjectFactory>();
            container.Register<IComponentSubObjectFactory, JavascriptLibraryComponentSubObjectFactory>();



            container.RegisterMultiple(typeof(IComponentSubObjectFactory),
                new[] { typeof(AssemblyComponentSubObjectFactory),
                    typeof(AuthenticationSystemSubObjectFactory),
                    typeof(CleanupComponentSubObjectFactory),
                    typeof(ConfigComponentSubObjectFactory),
                    typeof(ContainerComponentSubObjectFactory),
                    typeof(DashboardControlComponentSubObjectFactory),
                    typeof(FileComponentSubObjectFactory),
                    typeof(CoreLanguageComponentSubObjectFactory),
                    typeof(ExtensionLanguageComponentSubObjectFactory),
                    typeof(ModuleComponentSubObjectFactory),
                    typeof(ProviderComponentSubObjectFactory),
                    typeof(ResourceFileComponentSubObjectFactory),
                    typeof(ScriptComponentSubObjectFactory),
                    typeof(UrlProviderComponentSubObjectFactory),
                    typeof(SkinObjectComponentSubObjectFactory),
                    typeof(SkinComponentSubObjectFactory),
                    typeof(JavascriptFileComponentSubObjectFactory),
                    typeof(JavascriptLibraryComponentSubObjectFactory)
                });

            // Assembly component
            container.Register<IAssembliesListObjectFactory, AssembliesListObjectFactory>();
            container.Register<IAssemblyObjectFactory, AssemblyObjectFactory>();

            // file component and cleanup component
            container.Register<IFileObjectFactory, FileObjectFactory>();
            container.Register<IFilesListObjectFactory, FilesListObjectFactory>();

            // config component
            container.Register<INodesListObjectFactory, NodesListObjectFactory>();
            container.Register<INodeObjectFactory, NodeObjectFactory>();

            // container component
            container.Register<IContainerFilesListObjectFactory, ContainerFilesListObjectFactory>();
            container.Register<IContainerFileObjectFactory, ContainerFileObjectFactory>();

            // dashboard control component
            container.Register<IDashboardControlsListObjectFactory, DashboardControlsListObjectFactory>();
            container.Register<IDashboardControlObjectFactory, DashboardControlObjectFactory>();

            // extension language and core language component
            container.Register<ILanguageFilesListObjectFactory, LanguageFilesListObjectFactory>();
            container.Register<ILanguageFileObjectFactory, LanguageFileObjectFactory>();

            // Module component
            container.Register<IEventMessageObjectFactory, EventMessageObjectFactory>();

            container.Register<IEventAttributesListObjectFactory, EventAttributesListObjectFactory>();
            container.Register<IEventAttributeObjectFactory, EventAttributeObjectFactory>();

            container.Register<ISupportedFeaturesListObjectFactory, SupportedFeaturesListObjectFactory>();
            container.Register<ISupportedFeatureObjectFactory, SupportedFeatureObjectFactory>();

            container.Register<IModuleDefinitionsListObjectFactory, ModuleDefinitionsListObjectFactory>();
            container.Register<IModuleDefinitionObjectFactory, ModuleDefinitionObjectFactory>();

            container.Register<IModulePermissionsListObjectFactory, ModulePermissionsListObjectFactory>();
            container.Register<IModulePermissionObjectFactory, ModulePermissionObjectFactory>();

            container.Register<IModuleControlsListObjectFactory, ModuleControlsListObjectFactory>();
            container.Register<IModuleControlObjectFactory, ModuleControlObjectFactory>();

            // Resource File component
            container.Register<IResourceFilesListObjectFactory, ResourceFilesListObjectFactory>();
            container.Register<IResourceFileObjectFactory, ResourceFileObjectFactory>();

            // Script component
            container.Register<IScriptsListObjectFactory, ScriptsListObjectFactory>();
            container.Register<IScriptObjectFactory, ScriptObjectFactory>();

            // skin component
            container.Register<ISkinFilesListObjectFactory, SkinFilesListObjectFactory>();
            container.Register<ISkinFileObjectFactory, SkinFileObjectFactory>();

            //  jsfile component
            container.Register<IJavascriptFilesListObjectFactory, JavascriptFilesListObjectFactory>();
            container.Register<IJavascriptFileObjectFactory, JavascriptFileObjectFactory>();

        }

        public object GetFactory(string factoryName)
        {
            Type type = GetFactoryType(factoryName);
            try
            {
                var implementation = _container.Resolve(type);
                return implementation;
            }
            catch (TinyIoCResolutionException cnrex)
            {
                throw new ObjectFactoryNotRegisteredException(factoryName, cnrex);
            }
        }

        public Type GetFactoryType(string factoryName)
        {
            Type type;
            if (_cachedTypes.ContainsKey(factoryName))
            {
                type = _cachedTypes[factoryName];
            }
            else
            {
                type = Type.GetType(factoryName);
                if (type == null)
                {
                    throw new TypeLoadException($"Could not load a type with name: {factoryName}");
                }
                _cachedTypes.AddOrUpdate(factoryName, (a) => type, (a, b) => type);
            }
            return type;
        }

        public void ClearCache()
        {
            _cachedTypes.Clear();
        }



        #region Serialization bug workaround

        private static void SerializationWorkaround()
        {
            // hook up the AssemblyResolve
            // event so deep serialization works properly
            // this is a workaround for a bug in the .NET runtime


            AppDomain currentDomain = AppDomain.CurrentDomain;

            currentDomain.AssemblyResolve +=
              new ResolveEventHandler(ResolveEventHandler);


#if NETDESKTOP
#else
          //  DependencyContext.Default.RuntimeLibraries.
         //  AssemblyLoadContext.
#endif

        }


        private static Assembly ResolveEventHandler(
          object sender, ResolveEventArgs args)
        {

            // get a list of all the assemblies loaded in our appdomain
            Assembly[] list = AppDomain.CurrentDomain.GetAssemblies();

            // search the list to find the assembly that was not found automatically
            // and return the assembly from the list

            foreach (Assembly asm in list)
                if (asm.FullName == args.Name)
                    return asm;


            //    var assies = AssemblyLoadContext.Default.Assemblies;

            // if the assembly wasn't already in the appdomain, then try to load it.
            //  return Assembly.Load(args.Name);
            return null;
        }
#if NETDESKTOP
#endif
        #endregion





    }
}

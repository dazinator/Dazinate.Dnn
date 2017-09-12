using System;
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
using Microsoft.Extensions.DependencyInjection;

using System.Reflection;
using System.Collections.Generic;
#if !NETDESKTOP
using System.Runtime.Loader;
#endif

namespace Dazinate.Dnn.Manifest.Ioc
{
    public class ServiceProviderObjectFactoryLoader : IObjectFactoryLoader
    {
        private readonly IServiceProvider _serviceProvider;


        static ServiceProviderObjectFactoryLoader()
        {
            SerializationWorkaround();
        }

        private readonly ConcurrentDictionary<string, Type> _cachedTypes;

        public ServiceProviderObjectFactoryLoader(IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }
            // _services = services;
            _serviceProvider = ConfigureServices(services);
            _cachedTypes = new ConcurrentDictionary<string, Type>();
        }

        private IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IObjectActivator, ServiceProviderObjectActivator>();
            services.AddTransient<IPackagesDnnManifestObjectFactory, PackagesDnnManifestObjectFactory>();
            services.AddTransient<IDnnManifest, PackagesDnnManifest>();
            services.AddTransient<IDnnManifestFactory<IPackagesDnnManifest>, PackagesDnnManifestFactory>();
            services.AddTransient<IDnnManifestFactory<IDnnManifest>, PackagesDnnManifestFactory>();
            services.AddSingleton<IPackageTypeListFactory>(new PackageTypeListFactory());
            services.AddTransient<IPackageTypeListObjectFactory, PackageTypeListObjectFactory>();
            services.AddTransient<IPackagesListObjectFactory, PackagesListObjectFactory>();
            services.AddTransient<IPackageFactory, PackageFactory>();
            services.AddTransient<IPackageObjectFactory, PackageObjectFactory>();

            services.AddTransient<IDependency, PackageDependency>();
            services.AddTransient<IDependency, CoreVersionDependency>();
            services.AddTransient<IDependency, ManagedPackageDependency>();


            //  container.RegisterMultiple(typeof(IDependency), new[] { typeof(PackageDependency), typeof(CoreVersionDependency), typeof(ManagedPackageDependency) });
            services.AddTransient<IDependenciesListObjectFactory, DependenciesListObjectFactory>();
            services.AddTransient<IDependencyObjectFactory, DependencyObjectFactory>();

            services.AddTransient<IDependencyFactory, DependencyFactory>();

            services.AddTransient<IComponentFactory, ComponentFactory>();
            services.AddTransient<IComponentsListObjectFactory, ComponentsListObjectFactory>();
            services.AddTransient<IComponentObjectFactory, ComponentObjectFactory>();




            //container.RegisterMultiple(typeof(IComponentSubObjectFactory),
            //    new[] { typeof(AssemblyComponentSubObjectFactory),
            //        typeof(AuthenticationSystemSubObjectFactory),
            //        typeof(CleanupComponentSubObjectFactory),
            //        typeof(ConfigComponentSubObjectFactory),
            //        typeof(ContainerComponentSubObjectFactory),
            //        typeof(DashboardControlComponentSubObjectFactory),
            //        typeof(FileComponentSubObjectFactory),
            //        typeof(CoreLanguageComponentSubObjectFactory),
            //        typeof(ExtensionLanguageComponentSubObjectFactory),
            //        typeof(ModuleComponentSubObjectFactory),
            //        typeof(ProviderComponentSubObjectFactory),
            //        typeof(ResourceFileComponentSubObjectFactory),
            //        typeof(ScriptComponentSubObjectFactory),
            //        typeof(UrlProviderComponentSubObjectFactory),
            //        typeof(SkinObjectComponentSubObjectFactory),
            //        typeof(SkinComponentSubObjectFactory),
            //        typeof(JavascriptFileComponentSubObjectFactory),
            //        typeof(JavascriptLibraryComponentSubObjectFactory)
            //    });

            services.AddTransient<IComponentSubObjectFactory, AssemblyComponentSubObjectFactory>();
            services.AddTransient<IComponentSubObjectFactory, AuthenticationSystemSubObjectFactory>();
            services.AddTransient<IComponentSubObjectFactory, CleanupComponentSubObjectFactory>();
            services.AddTransient<IComponentSubObjectFactory, ConfigComponentSubObjectFactory>();
            services.AddTransient<IComponentSubObjectFactory, ContainerComponentSubObjectFactory>();
            services.AddTransient<IComponentSubObjectFactory, DashboardControlComponentSubObjectFactory>();
            services.AddTransient<IComponentSubObjectFactory, FileComponentSubObjectFactory>();
            services.AddTransient<IComponentSubObjectFactory, CoreLanguageComponentSubObjectFactory>();
            services.AddTransient<IComponentSubObjectFactory, ExtensionLanguageComponentSubObjectFactory>();

            services.AddTransient<IComponentSubObjectFactory, ModuleComponentSubObjectFactory>();
            services.AddTransient<IComponentSubObjectFactory, ProviderComponentSubObjectFactory>();
            services.AddTransient<IComponentSubObjectFactory, ResourceFileComponentSubObjectFactory>();
            services.AddTransient<IComponentSubObjectFactory, ScriptComponentSubObjectFactory>();
            services.AddTransient<IComponentSubObjectFactory, UrlProviderComponentSubObjectFactory>();
            services.AddTransient<IComponentSubObjectFactory, SkinObjectComponentSubObjectFactory>();
            services.AddTransient<IComponentSubObjectFactory, SkinComponentSubObjectFactory>();
            services.AddTransient<IComponentSubObjectFactory, JavascriptFileComponentSubObjectFactory>();
            services.AddTransient<IComponentSubObjectFactory, JavascriptLibraryComponentSubObjectFactory>();

            // Assembly component
            services.AddTransient<IAssembliesListObjectFactory, AssembliesListObjectFactory>();
            services.AddTransient<IAssemblyObjectFactory, AssemblyObjectFactory>();

            // file component and cleanup component
            services.AddTransient<IFileObjectFactory, FileObjectFactory>();
            services.AddTransient<IFilesListObjectFactory, FilesListObjectFactory>();

            // config component
            services.AddTransient<INodesListObjectFactory, NodesListObjectFactory>();
            services.AddTransient<INodeObjectFactory, NodeObjectFactory>();

            // container component
            services.AddTransient<IContainerFilesListObjectFactory, ContainerFilesListObjectFactory>();
            services.AddTransient<IContainerFileObjectFactory, ContainerFileObjectFactory>();

            // dashboard control component
            services.AddTransient<IDashboardControlsListObjectFactory, DashboardControlsListObjectFactory>();
            services.AddTransient<IDashboardControlObjectFactory, DashboardControlObjectFactory>();

            // extension language and core language component
            services.AddTransient<ILanguageFilesListObjectFactory, LanguageFilesListObjectFactory>();
            services.AddTransient<ILanguageFileObjectFactory, LanguageFileObjectFactory>();

            // Module component
            services.AddTransient<IEventMessageObjectFactory, EventMessageObjectFactory>();

            services.AddTransient<IEventAttributesListObjectFactory, EventAttributesListObjectFactory>();
            services.AddTransient<IEventAttributeObjectFactory, EventAttributeObjectFactory>();

            services.AddTransient<ISupportedFeaturesListObjectFactory, SupportedFeaturesListObjectFactory>();
            services.AddTransient<ISupportedFeatureObjectFactory, SupportedFeatureObjectFactory>();

            services.AddTransient<IModuleDefinitionsListObjectFactory, ModuleDefinitionsListObjectFactory>();
            services.AddTransient<IModuleDefinitionObjectFactory, ModuleDefinitionObjectFactory>();

            services.AddTransient<IModulePermissionsListObjectFactory, ModulePermissionsListObjectFactory>();
            services.AddTransient<IModulePermissionObjectFactory, ModulePermissionObjectFactory>();

            services.AddTransient<IModuleControlsListObjectFactory, ModuleControlsListObjectFactory>();
            services.AddTransient<IModuleControlObjectFactory, ModuleControlObjectFactory>();

            // Resource File component
            services.AddTransient<IResourceFilesListObjectFactory, ResourceFilesListObjectFactory>();
            services.AddTransient<IResourceFileObjectFactory, ResourceFileObjectFactory>();

            // Script component
            services.AddTransient<IScriptsListObjectFactory, ScriptsListObjectFactory>();
            services.AddTransient<IScriptObjectFactory, ScriptObjectFactory>();

            // skin component
            services.AddTransient<ISkinFilesListObjectFactory, SkinFilesListObjectFactory>();
            services.AddTransient<ISkinFileObjectFactory, SkinFileObjectFactory>();

            //  jsfile component
            services.AddTransient<IJavascriptFilesListObjectFactory, JavascriptFilesListObjectFactory>();
            services.AddTransient<IJavascriptFileObjectFactory, JavascriptFileObjectFactory>();

            return services.BuildServiceProvider();

        }

        public object GetFactory(string factoryName)
        {
            Type type = GetFactoryType(factoryName);
            try
            {
                var implementation = _serviceProvider.GetRequiredService(type);
                return implementation;
            }
            catch (InvalidOperationException cnrex)
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

#if NETDESKTOP
            AppDomain currentDomain = AppDomain.CurrentDomain;

            currentDomain.AssemblyResolve +=
              new ResolveEventHandler(ResolveEventHandler);



#else

            //var libManager = new GluonApplicationLibraryManager("Dazinate.Dnn.Manifest", new string[] { });
            //System.Runtime.Loader.AssemblyLoadContext.Default.Resolving += new Func<AssemblyLoadContext, AssemblyName, Assembly>((a, b) =>
            //{

            //    try
            //    {
                    
            //        var assy = libManager.LoadAssembly(b);
            //        //var assy = a.LoadFromAssemblyName(b);
            //        return assy;
            //    }
            //    catch (global::System.Exception ex)
            //    {
            //        return null;
            //        //throw;
            //    }



            //});
            //    var loader = System.Runtime.Loader.AssemblyLoadContext.Default.Resolving.AssemblyResolve += new System.ResolveEventHandler(ResolveEventHandler);
            //     var myAssembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(@"C:\MyDirectory\bin\Custom.Thing.dll");
            //   var myType = myAssembly.GetType("Custom.Thing.SampleClass");
            //   var myInstance = Activator.CreateInstance(myType);

            //  DependencyContext.Default.RuntimeLibraries.
            //  AssemblyLoadContext.
#endif

        }

#if ASSEMBLYRESOLVE

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

#endif

        #endregion
    }


}

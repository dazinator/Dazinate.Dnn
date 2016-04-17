using System;
using System.Collections.Concurrent;
using Csla.Server;
using Dazinate.Dnn.Manifest.Factory;
using Dazinate.Dnn.Manifest.Model;
using Dazinate.Dnn.Manifest.Model.AssembliesList.ObjectFactory;
using Dazinate.Dnn.Manifest.Model.Assembly.ObjectFactory;
using Dazinate.Dnn.Manifest.Model.Component;
using Dazinate.Dnn.Manifest.Model.Component.ObjectFactory;
using Dazinate.Dnn.Manifest.Model.Component.SubObjectFactory;
using Dazinate.Dnn.Manifest.Model.ComponentsList.ObjectFactory;
using Dazinate.Dnn.Manifest.Model.ContainerFile.ObjectFactory;
using Dazinate.Dnn.Manifest.Model.ContainerFilesList.ObjectFactory;
using Dazinate.Dnn.Manifest.Model.DashboardControl.ObjectFactory;
using Dazinate.Dnn.Manifest.Model.DashboardControlsList.ObjectFactory;
using Dazinate.Dnn.Manifest.Model.Dependency;
using Dazinate.Dnn.Manifest.Model.Dependency.ObjectFactory;
using Dazinate.Dnn.Manifest.Model.DependencyList.ObjectFactory;
using Dazinate.Dnn.Manifest.Model.EventAttribute;
using Dazinate.Dnn.Manifest.Model.EventAttribute.ObjectFactory;
using Dazinate.Dnn.Manifest.Model.EventAttributesList;
using Dazinate.Dnn.Manifest.Model.EventAttributesList.ObjectFactory;
using Dazinate.Dnn.Manifest.Model.EventMessage;
using Dazinate.Dnn.Manifest.Model.EventMessage.ObjectFactory;
using Dazinate.Dnn.Manifest.Model.File.ObjectFactory;
using Dazinate.Dnn.Manifest.Model.FilesList.ObjectFactory;
using Dazinate.Dnn.Manifest.Model.JavascriptFile.ObjectFactory;
using Dazinate.Dnn.Manifest.Model.JavascriptFilesList.ObjectFactory;
using Dazinate.Dnn.Manifest.Model.LanguageFile.ObjectFactory;
using Dazinate.Dnn.Manifest.Model.LanguageFilesList;
using Dazinate.Dnn.Manifest.Model.LanguageFilesList.ObjectFactory;
using Dazinate.Dnn.Manifest.Model.Manifest;
using Dazinate.Dnn.Manifest.Model.Manifest.ObjectFactory;
using Dazinate.Dnn.Manifest.Model.ModuleControl.ObjectFactory;
using Dazinate.Dnn.Manifest.Model.ModuleControlsList.ObjectFactory;
using Dazinate.Dnn.Manifest.Model.ModuleDefinition.ObjectFactory;
using Dazinate.Dnn.Manifest.Model.ModuleDefinitionsList.ObjectFactory;
using Dazinate.Dnn.Manifest.Model.ModulePermission.ObjectFactory;
using Dazinate.Dnn.Manifest.Model.ModulePermissionsList.ObjectFactory;
using Dazinate.Dnn.Manifest.Model.Node.ObjectFactory;
using Dazinate.Dnn.Manifest.Model.NodesList.ObjectFactory;
using Dazinate.Dnn.Manifest.Model.Package.ObjectFactory;
using Dazinate.Dnn.Manifest.Model.PackagesList.ObjectFactory;
using Dazinate.Dnn.Manifest.Model.PackageType.ObjectFactory;
using Dazinate.Dnn.Manifest.Model.ResourceFile.ObjectFactory;
using Dazinate.Dnn.Manifest.Model.ResourceFilesList.ObjectFactory;
using Dazinate.Dnn.Manifest.Model.Script.ObjectFactory;
using Dazinate.Dnn.Manifest.Model.ScriptsList.ObjectFactory;
using Dazinate.Dnn.Manifest.Model.SkinFile.ObjectFactory;
using Dazinate.Dnn.Manifest.Model.SkinFilesList.ObjectFactory;
using Dazinate.Dnn.Manifest.Model.SupportedFeature.ObjectFactory;
using Dazinate.Dnn.Manifest.Model.SupportedFeaturesList.ObjectFactory;
using Assembly = System.Reflection.Assembly;

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

            container.Register<IComponentsListObjectFactory, ComponentsListObjectFactory>();
            container.Register<IComponentObjectFactory, ComponentObjectFactory>();

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
                    typeof(JavascriptFileComponentSubObjectFactory)
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

            // if the assembly wasn't already in the appdomain, then try to load it.
            //  return Assembly.Load(args.Name);
            return null;
        }

        #endregion


    }
}

using System;
using System.Collections.Concurrent;
using System.Reflection;
using Csla.Server;
using Dazinate.Dnn.Manifest.Factory;
using Dazinate.Dnn.Manifest.ObjectFactory;
using TinyIoC;

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

            container.RegisterMultiple(typeof(IPackageDependency), new[] { typeof(PackageDependency), typeof(CoreVersionPackageDependency), typeof(ManagedPackageDependency) });
            container.Register<IPackageDependenciesListObjectFactory, PackageDependenciesListObjectFactory>();
            container.Register<IPackageDependencyObjectFactory, PackageDependencyObjectFactory>();
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

using System;
using System.Collections.Concurrent;
using System.Reflection;
using Autofac;
using Autofac.Core.Registration;
using Csla.Server;

namespace Dazinate.Dnn.Manifest.Ioc
{
    public class AutoFacObjectFactoryLoader : IObjectFactoryLoader
    {

        static AutoFacObjectFactoryLoader()
        {
            SerializationWorkaround();
        }

        private readonly IComponentContext _container;

        private readonly ConcurrentDictionary<string, Type> _cachedTypes;

        /// <summary>
        /// Creates an object factory loader that uses autofac to resolve types. 
        /// </summary>
        public AutoFacObjectFactoryLoader()
        {
            var builder = new ContainerBuilder();
            builder.RegisterAssemblyModules(typeof(AutofacObjectActivator).Assembly);
            _container = builder.Build();
            _cachedTypes = new ConcurrentDictionary<string, Type>();
        }

        public AutoFacObjectFactoryLoader(IComponentContext container)
        {
            if (container == null)
            {
                throw new ArgumentNullException(nameof(container));
            }
            _container = container;
            _cachedTypes = new ConcurrentDictionary<string, Type>();
        }

        public object GetFactory(string factoryName)
        {
            Type type = GetFactoryType(factoryName);
            try
            {
                var implementation = _container.Resolve(type);
                return implementation;
            }
            catch (ComponentNotRegisteredException cnrex)
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

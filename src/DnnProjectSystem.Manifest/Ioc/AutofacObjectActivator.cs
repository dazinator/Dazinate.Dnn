using System;
using Autofac;

namespace Dnn.Contrib.Manifest.Ioc
{
    public class AutofacObjectActivator : IObjectActivator
    {
        private IComponentContext _componentContext;

        public AutofacObjectActivator(IComponentContext componentContext)
        {
            _componentContext = componentContext;
        }
        public T Activate<T>()
        {
            try
            {
                if (_componentContext.IsRegistered<T>())
                {
                    var instance = _componentContext.Resolve<T>();
                    return instance;
                }

                return CreateInstance<T>();

            }
            catch (global::Autofac.Core.Registration.ComponentNotRegisteredException)
            {
                // fallback to creating an instance using actviator directly. 
                return CreateInstance<T>();
            }

        }

        protected T CreateInstance<T>()
        {
            var instance = Activator.CreateInstance<T>();
            return instance;
        }
    }
}

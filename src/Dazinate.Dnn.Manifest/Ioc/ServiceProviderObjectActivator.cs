using System;

namespace Dazinate.Dnn.Manifest.Ioc
{
    public class ServiceProviderObjectActivator : IObjectActivator
    {

        private readonly IServiceProvider _serviceProvider;

        public ServiceProviderObjectActivator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public T Activate<T>() where T : class
        {
            var instance = (T)Microsoft.Extensions.DependencyInjection.ActivatorUtilities.GetServiceOrCreateInstance(_serviceProvider, typeof(T));
            return instance;
        }
    }
}

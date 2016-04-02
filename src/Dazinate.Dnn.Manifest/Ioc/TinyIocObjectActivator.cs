using System;

namespace Dazinate.Dnn.Manifest.Ioc
{
    public class TinyIocObjectActivator : IObjectActivator
    {
        private TinyIoCContainer _container;

        public TinyIocObjectActivator(TinyIoCContainer container)
        {
            _container = container;
        }
        public T Activate<T>() where T : class
        {
            try
            {
                if (_container.CanResolve<T>())
                {
                    var instance = _container.Resolve<T>();
                    return instance;
                }

                return CreateInstance<T>();

            }
            catch (global::Dazinate.Dnn.Manifest.Ioc.TinyIoCResolutionException)
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

using System;
using Dnn.Contrib.Manifest.Ioc;

namespace Dnn.Contrib.Manifest.ObjectFactory
{
    public abstract class BaseObjectFactory : global::Csla.Server.ObjectFactory
    {

        private readonly IObjectActivator _activator;

        public BaseObjectFactory(IObjectActivator activator)
        {
            if (activator == null)
            {
                throw new ArgumentNullException("activator");
            }
            _activator = activator;
        }

        protected T CreateNew<T>()
        {
            var obj = CreateInstance<T>();
            MarkNew(obj);
            return (T)obj;
        }

        protected TType CreateInstance<TType>()
        {
            var instance = _activator.Activate<TType>();
            return instance;
        }

        
    }
}

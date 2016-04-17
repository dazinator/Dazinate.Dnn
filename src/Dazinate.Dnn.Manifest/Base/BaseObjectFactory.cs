using System;
using Csla;
using Dazinate.Dnn.Manifest.Ioc;

namespace Dazinate.Dnn.Manifest.Base
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

        protected T CreateNew<T>() where T : class
        {
            var obj = CreateInstance<T>();
            MarkNew(obj);
            return (T)obj;
        }

        protected TType CreateInstance<TType>() where TType : class
        {
            var instance = _activator.Activate<TType>();
            return instance;
        }

        protected void MarkListSaved<T>(IBusinessListBase<T> list)
        {
            var deletedItems = GetDeletedList<T>(list);
            deletedItems.Clear();
            MarkOld(list);
        }

        public IObjectActivator Activator { get { return _activator; } }


    }
}

using System;

namespace Dnn.Contrib.Manifest.Ioc
{
    public class ObjectFactoryNotRegisteredException : Exception
    {

        private string _factoryName;

        public ObjectFactoryNotRegisteredException(string factoryName)
            : this(factoryName, null)
        {
            _factoryName = factoryName;

        }

        public ObjectFactoryNotRegisteredException(string factoryName, Exception baseException) : base(string.Empty, baseException)
        {
            _factoryName = factoryName;
        }

        public override string Message
        {
            get
            {
                return string.Format("There is no class registered in the container that implements: {0}.", _factoryName);
            }
        }
    }
}

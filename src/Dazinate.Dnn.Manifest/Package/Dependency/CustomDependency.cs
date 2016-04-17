using System;
using Csla;
using Csla.Server;
using Dazinate.Dnn.Manifest.Package.Dependency.ObjectFactory;
using Dazinate.Dnn.Manifest.Writer;

namespace Dazinate.Dnn.Manifest.Package.Dependency
{
    [ObjectFactory(typeof(IDependencyObjectFactory))]
    [Serializable]
    public class CustomDependency : BusinessBase<CustomDependency>, IDependency
    {
        public static readonly PropertyInfo<string> TypeProperty = RegisterProperty<string>(c => c.Type);
        public string Type
        {
            get { return GetProperty(TypeProperty); }
            set { SetProperty(TypeProperty, value); }
        }

        public static readonly PropertyInfo<string> ValueProperty = RegisterProperty<string>(c => c.Value);
        public string Value
        {
            get { return GetProperty(ValueProperty); }
            set { SetProperty(ValueProperty, value); }
        }

        public void Accept(IManifestXmlWriterVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
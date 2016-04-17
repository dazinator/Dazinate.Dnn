using System;
using Csla;
using Csla.Server;
using Dazinate.Dnn.Manifest.Base;
using Dazinate.Dnn.Manifest.Package.Dependency.ObjectFactory;

namespace Dazinate.Dnn.Manifest.Package.Dependency
{
    [ObjectFactory(typeof(IDependencyObjectFactory))]
    [Serializable]
    public class TypeDependency : BusinessBase<TypeDependency>, IDependency
    {
        public static readonly PropertyInfo<string> TypeNameProperty = RegisterProperty<string>(c => c.TypeName);
        public string TypeName
        {
            get { return GetProperty(TypeNameProperty); }
            set { SetProperty(TypeNameProperty, value); }
        }

        public void Accept(IManifestVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override string ToString()
        {
            return "Type";
        }
    }
}
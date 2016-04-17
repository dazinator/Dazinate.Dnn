using System;
using Csla;
using Csla.Server;
using Dazinate.Dnn.Manifest.Base;
using Dazinate.Dnn.Manifest.Package.Dependency.ObjectFactory;

namespace Dazinate.Dnn.Manifest.Package.Dependency
{
    [ObjectFactory(typeof(IDependencyObjectFactory))]
    [Serializable]
    public class PackageDependency : BusinessBase<PackageDependency>, IDependency
    {
        public static readonly PropertyInfo<string> PackageNameProperty = RegisterProperty<string>(c => c.PackageName);
        public string PackageName
        {
            get { return GetProperty(PackageNameProperty); }
            set { SetProperty(PackageNameProperty, value); }
        }

        public void Accept(IManifestVisitor visitor)
        {
           visitor.Visit(this);
        }
    }
}
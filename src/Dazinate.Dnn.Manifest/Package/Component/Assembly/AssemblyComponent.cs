using System;
using Csla;
using Csla.Server;
using Dazinate.Dnn.Manifest.Base;
using Dazinate.Dnn.Manifest.Package.Component.ObjectFactory;

namespace Dazinate.Dnn.Manifest.Package.Component.Assembly
{
    [ObjectFactory(typeof(IComponentObjectFactory))]
    [Serializable]
    public class AssemblyComponent : BusinessBase<AssemblyComponent>, IAssemblyComponent
    {

        public static readonly PropertyInfo<AssembliesList> AssembliesListProperty = RegisterProperty<AssembliesList>(c => c.Assemblies);
        public IAssembliesList Assemblies
        {
            get { return GetProperty(AssembliesListProperty); }
            set { SetProperty(AssembliesListProperty, value); }
        }

        public void Accept(IManifestVisitor visitor)
        {
            visitor.Visit(this);
        }

        public bool IsCompatibleWithPackage(IPackage package)
        {
            // generic components are compatible with all packages.
            return true;
        }

        public override string ToString()
        {
            return "Assembly";
        }
    }
}
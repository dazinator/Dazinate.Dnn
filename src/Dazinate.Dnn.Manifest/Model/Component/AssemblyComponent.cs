using System;
using Csla;
using Csla.Server;
using Dazinate.Dnn.Manifest.Model.AssembliesList;
using Dazinate.Dnn.Manifest.Model.Component.ObjectFactory;
using Dazinate.Dnn.Manifest.Model.Package;
using Dazinate.Dnn.Manifest.Writer;

namespace Dazinate.Dnn.Manifest.Model.Component
{
    [ObjectFactory(typeof(IComponentObjectFactory))]
    [Serializable]
    public class AssemblyComponent : BusinessBase<AssemblyComponent>, IAssemblyComponent
    {

        public static readonly PropertyInfo<AssembliesList.AssembliesList> AssembliesListProperty = RegisterProperty<AssembliesList.AssembliesList>(c => c.Assemblies);
        public IAssembliesList Assemblies
        {
            get { return GetProperty(AssembliesListProperty); }
            set { SetProperty(AssembliesListProperty, value); }
        }

        public void Accept(IManifestXmlWriterVisitor visitor)
        {
            visitor.Visit(this);
        }

        public bool IsCompatibleWithPackage(IPackage package)
        {
            // generic components are compatible with all packages.
            return true;
        }
    }
}
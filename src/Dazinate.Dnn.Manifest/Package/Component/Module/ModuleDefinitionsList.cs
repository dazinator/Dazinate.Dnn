using System;
using Csla;
using Csla.Server;
using Dazinate.Dnn.Manifest.Base;
using Dazinate.Dnn.Manifest.Package.Component.Module.ObjectFactory;

namespace Dazinate.Dnn.Manifest.Package.Component.Module
{
    [ObjectFactory(typeof(IModuleDefinitionsListObjectFactory))]
    [Serializable]
    public class ModuleDefinitionsList : BusinessListBase<ModuleDefinitionsList, IModuleDefinition>, IModuleDefinitionsList
    {
        // private readonly IPackageFactory _factory;

        public ModuleDefinitionsList()
        {
        }

        public void Accept(IManifestVisitor visitor)
        {
            visitor.Visit(this);
        }

        protected override IModuleDefinition AddNewCore()
        {
            var item = Csla.DataPortal.Create<ModuleDefinition>();
            this.Add(item);
            return item;            
        }

    }
}
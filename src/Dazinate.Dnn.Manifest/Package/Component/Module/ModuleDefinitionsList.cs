using System;
using Csla;
using Csla.Server;
using Dazinate.Dnn.Manifest.Package.Component.Module.ObjectFactory;
using Dazinate.Dnn.Manifest.Writer;

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

        public void Accept(IManifestXmlWriterVisitor visitor)
        {
            visitor.Visit(this);
        }

    }
}
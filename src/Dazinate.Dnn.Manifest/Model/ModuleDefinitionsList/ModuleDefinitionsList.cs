using System;
using Csla;
using Csla.Server;
using Dazinate.Dnn.Manifest.Model.ModuleDefinition;
using Dazinate.Dnn.Manifest.Model.ModuleDefinitionsList.ObjectFactory;
using Dazinate.Dnn.Manifest.Writer;

namespace Dazinate.Dnn.Manifest.Model.ModuleDefinitionsList
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
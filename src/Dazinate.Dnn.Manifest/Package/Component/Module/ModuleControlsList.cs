using System;
using Csla;
using Csla.Server;
using Dazinate.Dnn.Manifest.Package.Component.Module.ObjectFactory;
using Dazinate.Dnn.Manifest.Writer;

namespace Dazinate.Dnn.Manifest.Package.Component.Module
{
    [ObjectFactory(typeof(IModuleControlsListObjectFactory))]
    [Serializable]
    public class ModuleControlsList : BusinessListBase<ModuleControlsList, IModuleControl>, IModuleControlsList
    {
        // private readonly IPackageFactory _factory;

        public ModuleControlsList()
        {
        }

        public void Accept(IManifestXmlWriterVisitor visitor)
        {
            visitor.Visit(this);
        }

    }
}
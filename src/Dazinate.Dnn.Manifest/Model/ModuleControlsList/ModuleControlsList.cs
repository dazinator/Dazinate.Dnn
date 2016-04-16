using System;
using Csla;
using Csla.Server;
using Dazinate.Dnn.Manifest.Model.ModuleControl;
using Dazinate.Dnn.Manifest.Model.ModuleControlsList.ObjectFactory;
using Dazinate.Dnn.Manifest.Writer;

namespace Dazinate.Dnn.Manifest.Model.ModuleControlsList
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
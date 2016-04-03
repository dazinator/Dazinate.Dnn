using System;
using Csla;
using Csla.Server;
using Dazinate.Dnn.Manifest.Model.AssembliesList.ObjectFactory;
using Dazinate.Dnn.Manifest.Model.Assembly;
using Dazinate.Dnn.Manifest.Writer;

namespace Dazinate.Dnn.Manifest.Model.AssembliesList
{
    [ObjectFactory(typeof(IAssembliesListObjectFactory))]
    [Serializable]
    public class AssembliesList : BusinessListBase<AssembliesList, IAssembly>, IAssembliesList
    {
        // private readonly IPackageFactory _factory;

        public AssembliesList()
        {
        }

        public void Accept(IManifestXmlWriterVisitor visitor)
        {
            visitor.Visit(this);
        }

    }
}
using System;
using Csla;
using Csla.Server;
using Dazinate.Dnn.Manifest.Model.ContainerFile;
using Dazinate.Dnn.Manifest.Model.ContainerFilesList.ObjectFactory;
using Dazinate.Dnn.Manifest.Writer;

namespace Dazinate.Dnn.Manifest.Model.ContainerFilesList
{
    [ObjectFactory(typeof(IContainerFilesListObjectFactory))]
    [Serializable]
    public class ContainerFilesList : BusinessListBase<ContainerFilesList, IContainerFile>, IContainerFilesList
    {
        // private readonly IPackageFactory _factory;

        public ContainerFilesList()
        {
        }

        public void Accept(IManifestXmlWriterVisitor visitor)
        {
            visitor.Visit(this);
        }

    }
}
using System;
using Csla;
using Csla.Server;
using Dazinate.Dnn.Manifest.Package.Component.ResourceFile.ObjectFactory;
using Dazinate.Dnn.Manifest.Writer;

namespace Dazinate.Dnn.Manifest.Package.Component.ResourceFile
{
    [ObjectFactory(typeof(IResourceFilesListObjectFactory))]
    [Serializable]
    public class ResourceFilesList : BusinessListBase<ResourceFilesList, IResourceFile>, IResourceFilesList
    {
        // private readonly IPackageFactory _factory;

        public ResourceFilesList()
        {
        }

        public void Accept(IManifestXmlWriterVisitor visitor)
        {
            visitor.Visit(this);
        }

    }
}
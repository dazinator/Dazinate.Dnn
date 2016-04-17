using System;
using Csla;
using Csla.Server;
using Dazinate.Dnn.Manifest.Package.Component.Shared.File.ObjectFactory;
using Dazinate.Dnn.Manifest.Writer;

namespace Dazinate.Dnn.Manifest.Package.Component.Shared.File
{
    [ObjectFactory(typeof(IFilesListObjectFactory))]
    [Serializable]
    public class FilesList : BusinessListBase<FilesList, IFile>, IFilesList
    {
        // private readonly IPackageFactory _factory;

        public FilesList()
        {
        }

        public void Accept(IManifestXmlWriterVisitor visitor)
        {
            visitor.Visit(this);
        }

    }
}
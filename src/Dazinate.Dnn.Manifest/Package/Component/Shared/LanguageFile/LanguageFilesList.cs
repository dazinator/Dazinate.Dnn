using System;
using Csla;
using Csla.Server;
using Dazinate.Dnn.Manifest.Package.Component.Shared.LanguageFile.ObjectFactory;
using Dazinate.Dnn.Manifest.Writer;

namespace Dazinate.Dnn.Manifest.Package.Component.Shared.LanguageFile
{
    [ObjectFactory(typeof(ILanguageFilesListObjectFactory))]
    [Serializable]
    public class LanguageFilesList : BusinessListBase<LanguageFilesList, ILanguageFile>, ILanguageFilesList
    {
        // private readonly IPackageFactory _factory;

        public LanguageFilesList()
        {
        }

        public void Accept(IManifestXmlWriterVisitor visitor)
        {
            visitor.Visit(this);
        }

    }
}
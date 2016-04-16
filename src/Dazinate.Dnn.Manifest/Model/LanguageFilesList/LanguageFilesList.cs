using System;
using Csla;
using Csla.Server;
using Dazinate.Dnn.Manifest.Model.LanguageFile;
using Dazinate.Dnn.Manifest.Model.LanguageFilesList.ObjectFactory;
using Dazinate.Dnn.Manifest.Writer;

namespace Dazinate.Dnn.Manifest.Model.LanguageFilesList
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
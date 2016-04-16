using System;
using Csla;
using Csla.Server;
using Dazinate.Dnn.Manifest.Model.SkinFile;
using Dazinate.Dnn.Manifest.Model.SkinFilesList.ObjectFactory;
using Dazinate.Dnn.Manifest.Writer;

namespace Dazinate.Dnn.Manifest.Model.SkinFilesList
{
    [ObjectFactory(typeof(ISkinFilesListObjectFactory))]
    [Serializable]
    public class SkinFilesList : BusinessListBase<SkinFilesList, ISkinFile>, ISkinFilesList
    {
        // private readonly IPackageFactory _factory;

        public SkinFilesList()
        {
        }

        public void Accept(IManifestXmlWriterVisitor visitor)
        {
            visitor.Visit(this);
        }

    }
}
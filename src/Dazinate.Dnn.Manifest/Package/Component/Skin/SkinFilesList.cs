using System;
using Csla;
using Csla.Server;
using Dazinate.Dnn.Manifest.Package.Component.Skin.ObjectFactory;
using Dazinate.Dnn.Manifest.Writer;

namespace Dazinate.Dnn.Manifest.Package.Component.Skin
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
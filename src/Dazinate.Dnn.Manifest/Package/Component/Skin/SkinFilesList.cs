using System;
using Csla;
using Csla.Server;
using Dazinate.Dnn.Manifest.Base;
using Dazinate.Dnn.Manifest.Package.Component.Skin.ObjectFactory;

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

        public void Accept(IManifestVisitor visitor)
        {
            visitor.Visit(this);
        }

        protected override ISkinFile AddNewCore()
        {
            var item = Csla.DataPortal.Create<SkinFile>();
            this.Add(item);
            return item;
        }

    }
}
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

#if NETDESKTOP
        protected override ISkinFile AddNewCore()
        {
            //base.AddNewCore();
            var item = Csla.DataPortal.Create<SkinFile>();
            Add(item);
            return item;
        }
#else
        protected override void AddNewCore()
        {
            //base.AddNewCore();
             var item = Csla.DataPortal.Create<SkinFile>();
            Add(item);           
        }
#endif
      
    }
}
using System;
using Csla;
using Csla.Server;
using Dazinate.Dnn.Manifest.Package.Component.ObjectFactory;
using Dazinate.Dnn.Manifest.Writer;

namespace Dazinate.Dnn.Manifest.Package.Component.Skin
{
    [ObjectFactory(typeof(IComponentObjectFactory))]
    [Serializable]
    public class SkinComponent : BusinessBase<SkinComponent>, ISkinComponent
    {

        public static readonly PropertyInfo<string> BasePathProperty = RegisterProperty<string>(c => c.BasePath);
        public string BasePath
        {
            get { return GetProperty(BasePathProperty); }
            set { SetProperty(BasePathProperty, value); }
        }

        public static readonly PropertyInfo<string> SkinNameProperty = RegisterProperty<string>(c => c.SkinName);
        public string SkinName
        {
            get { return GetProperty(SkinNameProperty); }
            set { SetProperty(SkinNameProperty, value); }
        }

        public static readonly PropertyInfo<SkinFilesList> FilesProperty = RegisterProperty<SkinFilesList>(c => c.Files);
        public ISkinFilesList Files
        {
            get { return GetProperty(FilesProperty); }
            set { SetProperty(FilesProperty, value); }
        }

        public void Accept(IManifestXmlWriterVisitor visitor)
        {
            visitor.Visit(this);
        }

        public bool IsCompatibleWithPackage(IPackage package)
        {
            return package.Type.ToLowerInvariant() == "skin";
        }
    }
}
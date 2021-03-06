using System;
using Csla;
using Csla.Server;
using Dazinate.Dnn.Manifest.Base;
using Dazinate.Dnn.Manifest.Package.Component.ObjectFactory;

namespace Dazinate.Dnn.Manifest.Package.Component.ResourceFile
{
    [ObjectFactory(typeof(IComponentObjectFactory))]
    [Serializable]
    public class ResourceFileComponent : BusinessBase<ResourceFileComponent>, IResourceFileComponent
    {

        public static readonly PropertyInfo<string> BasePathProperty = RegisterProperty<string>(c => c.BasePath);
        public string BasePath
        {
            get { return GetProperty(BasePathProperty); }
            set { SetProperty(BasePathProperty, value); }
        }

        public static readonly PropertyInfo<ResourceFilesList> FilesProperty = RegisterProperty<ResourceFilesList>(c => c.Files);
        public IResourceFilesList Files
        {
            get { return GetProperty(FilesProperty); }
            set { SetProperty(FilesProperty, value); }
        }

        public void Accept(IManifestVisitor visitor)
        {
            visitor.Visit(this);
        }

        public bool IsCompatibleWithPackage(IPackage package)
        {
            return true;
        }

        public override string ToString()
        {
            return "Resource File";
        }
    }
}
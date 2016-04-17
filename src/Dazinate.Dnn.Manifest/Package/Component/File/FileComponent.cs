using System;
using Csla;
using Csla.Server;
using Dazinate.Dnn.Manifest.Base;
using Dazinate.Dnn.Manifest.Package.Component.ObjectFactory;
using Dazinate.Dnn.Manifest.Package.Component.Shared.File;

namespace Dazinate.Dnn.Manifest.Package.Component.File
{
    [ObjectFactory(typeof(IComponentObjectFactory))]
    [Serializable]
    public class FileComponent : BusinessBase<FileComponent>, IFileComponent
    {

        public static readonly PropertyInfo<string> BasePathProperty = RegisterProperty<string>(c => c.BasePath);
        public string BasePath
        {
            get { return GetProperty(BasePathProperty); }
            set { SetProperty(BasePathProperty, value); }
        }
        

        public static readonly PropertyInfo<FilesList> FilesProperty = RegisterProperty<FilesList>(c => c.Files);
        public IFilesList Files
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
            return "File";
        }
    }
}
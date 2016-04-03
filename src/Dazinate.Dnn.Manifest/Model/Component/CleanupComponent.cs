using System;
using Csla;
using Csla.Server;
using Dazinate.Dnn.Manifest.Model.AssembliesList;
using Dazinate.Dnn.Manifest.Model.Component.ObjectFactory;
using Dazinate.Dnn.Manifest.Model.FilesList;
using Dazinate.Dnn.Manifest.Model.Package;
using Dazinate.Dnn.Manifest.Writer;

namespace Dazinate.Dnn.Manifest.Model.Component
{
    [ObjectFactory(typeof(IComponentObjectFactory))]
    [Serializable]
    public class CleanupComponent : BusinessBase<CleanupComponent>, ICleanupComponent
    {

        public static readonly PropertyInfo<string> VersionProperty = RegisterProperty<string>(c => c.Version);
        public string Version
        {
            get { return GetProperty(VersionProperty); }
            set { SetProperty(VersionProperty, value); }
        }

        public static readonly PropertyInfo<string> FileNameProperty = RegisterProperty<string>(c => c.FileName);
        public string FileName
        {
            get { return GetProperty(FileNameProperty); }
            set { SetProperty(FileNameProperty, value); }
        }

        public static readonly PropertyInfo<FilesList.FilesList> FilesListProperty = RegisterProperty<FilesList.FilesList>(c => c.Files);
        public IFilesList Files
        {
            get { return GetProperty(FilesListProperty); }
            set { SetProperty(FilesListProperty, value); }
        }

        public void Accept(IManifestXmlWriterVisitor visitor)
        {
            visitor.Visit(this);
        }

        public bool IsCompatibleWithPackage(IPackage package)
        {
            // generic components are compatible with all packages.
            return true;
        }
    }
}
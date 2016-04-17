using System;
using Csla;
using Csla.Server;
using Dazinate.Dnn.Manifest.Model.Component.ObjectFactory;
using Dazinate.Dnn.Manifest.Model.JavascriptFilesList;
using Dazinate.Dnn.Manifest.Model.Package;
using Dazinate.Dnn.Manifest.Writer;

namespace Dazinate.Dnn.Manifest.Model.Component
{
    [ObjectFactory(typeof(IComponentObjectFactory))]
    [Serializable]
    public class JavascriptFileComponent : BusinessBase<JavascriptFileComponent>, IJavascriptFileComponent
    {

        public static readonly PropertyInfo<string> LibraryFolderNameProperty = RegisterProperty<string>(c => c.LibraryFolderName);
        public string LibraryFolderName
        {
            get { return GetProperty(LibraryFolderNameProperty); }
            set { SetProperty(LibraryFolderNameProperty, value); }
        }


        public static readonly PropertyInfo<JavascriptFilesList.JavascriptFilesList> FilesProperty = RegisterProperty<JavascriptFilesList.JavascriptFilesList>(c => c.Files);
        public IJavascriptFilesList Files
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
            return package.Type.ToLowerInvariant() == "javascript_library";
        }
    }
}
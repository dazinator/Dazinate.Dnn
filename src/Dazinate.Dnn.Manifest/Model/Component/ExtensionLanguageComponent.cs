using System;
using Csla;
using Csla.Server;
using Dazinate.Dnn.Manifest.Model.Component.ObjectFactory;
using Dazinate.Dnn.Manifest.Model.LanguageFilesList;
using Dazinate.Dnn.Manifest.Model.Package;
using Dazinate.Dnn.Manifest.Writer;

namespace Dazinate.Dnn.Manifest.Model.Component
{
    [ObjectFactory(typeof(IComponentObjectFactory))]
    [Serializable]
    public class ExtensionLanguageComponent : BusinessBase<ExtensionLanguageComponent>, IExtensionLanguageComponent
    {

        public static readonly PropertyInfo<string> CodeProperty = RegisterProperty<string>(c => c.Code);
        public string Code
        {
            get { return GetProperty(CodeProperty); }
            set { SetProperty(CodeProperty, value); }
        }

        public static readonly PropertyInfo<string> PackageProperty = RegisterProperty<string>(c => c.Package);
        public string Package
        {
            get { return GetProperty(PackageProperty); }
            set { SetProperty(PackageProperty, value); }
        }

        public static readonly PropertyInfo<string> BasePathProperty = RegisterProperty<string>(c => c.BasePath);
        public string BasePath
        {
            get { return GetProperty(BasePathProperty); }
            set { SetProperty(BasePathProperty, value); }
        }

        public static readonly PropertyInfo<LanguageFilesList.LanguageFilesList> FilesProperty = RegisterProperty<LanguageFilesList.LanguageFilesList>(c => c.Files);
        public ILanguageFilesList Files
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
            return package.Type.ToLowerInvariant() == "extensionlanguagepack";
        }
    }
}
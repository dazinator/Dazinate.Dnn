using System;
using Csla;
using Csla.Server;
using Dazinate.Dnn.Manifest.Base;
using Dazinate.Dnn.Manifest.Package.Component.ObjectFactory;
using Dazinate.Dnn.Manifest.Package.Component.Shared.LanguageFile;

namespace Dazinate.Dnn.Manifest.Package.Component.ExtensionLanguage
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

        public static readonly PropertyInfo<LanguageFilesList> FilesProperty = RegisterProperty<LanguageFilesList>(c => c.Files);
        public ILanguageFilesList Files
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
            return package.Type.ToLowerInvariant() == "extensionlanguagepack";
        }

        public override string ToString()
        {
            return "Extension Language";
        }
    }
}
using System;
using Csla;
using Csla.Server;
using Dazinate.Dnn.Manifest.Package.Component.ObjectFactory;
using Dazinate.Dnn.Manifest.Package.Component.Shared.LanguageFile;
using Dazinate.Dnn.Manifest.Writer;

namespace Dazinate.Dnn.Manifest.Package.Component.CoreLanguage
{
    [ObjectFactory(typeof(IComponentObjectFactory))]
    [Serializable]
    public class CoreLanguageComponent : BusinessBase<CoreLanguageComponent>, ICoreLanguageComponent
    {

        public static readonly PropertyInfo<string> CodeProperty = RegisterProperty<string>(c => c.Code);
        public string Code
        {
            get { return GetProperty(CodeProperty); }
            set { SetProperty(CodeProperty, value); }
        }

        public static readonly PropertyInfo<string> DisplayNameProperty = RegisterProperty<string>(c => c.DisplayName);
        public string DisplayName
        {
            get { return GetProperty(DisplayNameProperty); }
            set { SetProperty(DisplayNameProperty, value); }
        }

        public static readonly PropertyInfo<string> FallbackProperty = RegisterProperty<string>(c => c.Fallback);
        public string Fallback
        {
            get { return GetProperty(FallbackProperty); }
            set { SetProperty(FallbackProperty, value); }
        }

        public static readonly PropertyInfo<LanguageFilesList> FilesProperty = RegisterProperty<LanguageFilesList>(c => c.Files);
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
            return package.Type.ToLowerInvariant() == "corelanguagepack";
        }
    }
}
using System;
using Csla;
using Csla.Server;
using Dazinate.Dnn.Manifest.Model.Component.ObjectFactory;
using Dazinate.Dnn.Manifest.Model.Package;
using Dazinate.Dnn.Manifest.Writer;

namespace Dazinate.Dnn.Manifest.Model.Component
{
    [ObjectFactory(typeof(IComponentObjectFactory))]
    [Serializable]
    public class JavascriptLibraryComponent : BusinessBase<JavascriptLibraryComponent>, IJavascriptLibraryComponent
    {

        public static readonly PropertyInfo<string> LibraryNameProperty = RegisterProperty<string>(c => c.LibraryName);
        public string LibraryName
        {
            get { return GetProperty(LibraryNameProperty); }
            set { SetProperty(LibraryNameProperty, value); }
        }

        public static readonly PropertyInfo<string> FileNameProperty = RegisterProperty<string>(c => c.FileName);
        public string FileName
        {
            get { return GetProperty(FileNameProperty); }
            set { SetProperty(FileNameProperty, value); }
        }
        
        public static readonly PropertyInfo<string> PreferredScriptLocationProperty = RegisterProperty<string>(c => c.PreferredScriptLocation);
        public string PreferredScriptLocation
        {
            get { return GetProperty(PreferredScriptLocationProperty); }
            set { SetProperty(PreferredScriptLocationProperty, value); }
        }

        public static readonly PropertyInfo<string> CdnPathProperty = RegisterProperty<string>(c => c.CdnPath);
        public string CdnPath
        {
            get { return GetProperty(CdnPathProperty); }
            set { SetProperty(CdnPathProperty, value); }
        }

        public static readonly PropertyInfo<string> ObjectNameProperty = RegisterProperty<string>(c => c.ObjectName);
        public string ObjectName
        {
            get { return GetProperty(ObjectNameProperty); }
            set { SetProperty(ObjectNameProperty, value); }
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
using System;
using Csla;
using Csla.Server;
using Dazinate.Dnn.Manifest.Package.Component.ObjectFactory;
using Dazinate.Dnn.Manifest.Writer;

namespace Dazinate.Dnn.Manifest.Package.Component.AuthenticationSystem
{
    [ObjectFactory(typeof(IComponentObjectFactory))]
    [Serializable]
    public class AuthenticationSystemComponent : BusinessBase<AuthenticationSystemComponent>, IAuthenticationSystemComponent
    {

        public static readonly PropertyInfo<string> TypeProperty = RegisterProperty<string>(c => c.Type);
        public string Type
        {
            get { return GetProperty(TypeProperty); }
            set { SetProperty(TypeProperty, value); }
        }

        public static readonly PropertyInfo<string> SettingsControlSourceProperty = RegisterProperty<string>(c => c.SettingsControlSource);
        public string SettingsControlSource
        {
            get { return GetProperty(SettingsControlSourceProperty); }
            set { SetProperty(SettingsControlSourceProperty, value); }
        }

        public static readonly PropertyInfo<string> LoginControlSourceProperty = RegisterProperty<string>(c => c.LoginControlSource);
        public string LoginControlSource
        {
            get { return GetProperty(LoginControlSourceProperty); }
            set { SetProperty(LoginControlSourceProperty, value); }
        }

        public static readonly PropertyInfo<string> LogoffControlSourceProperty = RegisterProperty<string>(c => c.LogoffControlSource);
        public string LogoffControlSource
        {
            get { return GetProperty(LogoffControlSourceProperty); }
            set { SetProperty(LogoffControlSourceProperty, value); }
        }

        public void Accept(IManifestXmlWriterVisitor visitor)
        {
            visitor.Visit(this);
        }

        public bool IsCompatibleWithPackage(IPackage package)
        {
            return package.Type.ToLowerInvariant() == "auth_system";
        }
    }
}
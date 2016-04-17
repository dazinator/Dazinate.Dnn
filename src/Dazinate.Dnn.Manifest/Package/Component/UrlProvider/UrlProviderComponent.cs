using System;
using Csla;
using Csla.Server;
using Dazinate.Dnn.Manifest.Base;
using Dazinate.Dnn.Manifest.Package.Component.ObjectFactory;

namespace Dazinate.Dnn.Manifest.Package.Component.UrlProvider
{
    [ObjectFactory(typeof(IComponentObjectFactory))]
    [Serializable]
    public class UrlProviderComponent : BusinessBase<UrlProviderComponent>, IUrlProviderComponent
    {

        public static readonly PropertyInfo<string> NameProperty = RegisterProperty<string>(c => c.Name);
        /// <summary>
        /// the friendly name as shown to Administrators.
        /// </summary>
        public string Name
        {
            get { return GetProperty(NameProperty); }
            set { SetProperty(NameProperty, value); }
        }

        public static readonly PropertyInfo<string> TypeProperty = RegisterProperty<string>(c => c.Type);
        /// <summary>
        /// The fully-qualified type of the class in the provider which inherits from the ExtensionUrlProvider type.
        /// </summary>
        public string Type
        {
            get { return GetProperty(TypeProperty); }
            set { SetProperty(TypeProperty, value); }
        }


        public static readonly PropertyInfo<string> SettingsControlSourceProperty = RegisterProperty<string>(c => c.SettingsControlSource);
        /// <summary>
        /// Relative path location of the User Control used to load the settings page. This is loaded in a popup directly from the provider definition in the Admin->Site Settings page.
        /// </summary>
        public string SettingsControlSource
        {
            get { return GetProperty(SettingsControlSourceProperty); }
            set { SetProperty(SettingsControlSourceProperty, value); }
        }


        public static readonly PropertyInfo<bool> RedirectAllUrlsProperty = RegisterProperty<bool>(c => c.RedirectAllUrls);
        /// <summary>
        /// if true, the provider ‘redirect’ method overload will be called for every request for the portal.
        /// </summary>
        public bool RedirectAllUrls
        {
            get { return GetProperty(RedirectAllUrlsProperty); }
            set { SetProperty(RedirectAllUrlsProperty, value); }
        }

        public static readonly PropertyInfo<bool> ReplaceAllUrlsProperty = RegisterProperty<bool>(c => c.ReplaceAllUrls);
        /// <summary>
        /// if true, the provider ‘change friendly url’ method overload will be called for each NavigateURL() call in DNN for the portal.
        /// </summary>
        public bool ReplaceAllUrls
        {
            get { return GetProperty(ReplaceAllUrlsProperty); }
            set { SetProperty(ReplaceAllUrlsProperty, value); }
        }

        public static readonly PropertyInfo<bool> RewriteAllUrlsProperty = RegisterProperty<bool>(c => c.RewriteAllUrls);
        /// <summary>
        /// if true, the provider ‘rewrite’ method will be called for each request for the portal
        /// </summary>
        public bool RewriteAllUrls
        {
            get { return GetProperty(RewriteAllUrlsProperty); }
            set { SetProperty(RewriteAllUrlsProperty, value); }
        }

        public static readonly PropertyInfo<string> DesktopModuleProperty = RegisterProperty<string>(c => c.DesktopModule);
        /// <summary>
        /// the installed DesktopModule the provider should be associated with, for when the redirect, rewrite and replace calls should only occur on pages associated with the module.
        /// </summary>
        public string DesktopModule
        {
            get { return GetProperty(DesktopModuleProperty); }
            set { SetProperty(DesktopModuleProperty, value); }
        }

        public void Accept(IManifestVisitor visitor)
        {
            visitor.Visit(this);
        }

        public bool IsCompatibleWithPackage(IPackage package)
        {
            return package.Type.ToLowerInvariant() == "provider";
        }

        public override string ToString()
        {
            return "Url Provider";
        }
    }
}
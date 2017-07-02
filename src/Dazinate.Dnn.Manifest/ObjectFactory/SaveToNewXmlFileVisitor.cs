using System.Linq;
using System.Xml;
using Dazinate.Dnn.Manifest.Base;
using Dazinate.Dnn.Manifest.Ioc;
using Dazinate.Dnn.Manifest.Package;
using Dazinate.Dnn.Manifest.Package.Component;
using Dazinate.Dnn.Manifest.Package.Component.Assembly;
using Dazinate.Dnn.Manifest.Package.Component.AuthenticationSystem;
using Dazinate.Dnn.Manifest.Package.Component.Cleanup;
using Dazinate.Dnn.Manifest.Package.Component.Config;
using Dazinate.Dnn.Manifest.Package.Component.Container;
using Dazinate.Dnn.Manifest.Package.Component.CoreLanguage;
using Dazinate.Dnn.Manifest.Package.Component.DashboardControl;
using Dazinate.Dnn.Manifest.Package.Component.ExtensionLanguage;
using Dazinate.Dnn.Manifest.Package.Component.File;
using Dazinate.Dnn.Manifest.Package.Component.JavascriptFile;
using Dazinate.Dnn.Manifest.Package.Component.JavascriptLibrary;
using Dazinate.Dnn.Manifest.Package.Component.Module;
using Dazinate.Dnn.Manifest.Package.Component.Provider;
using Dazinate.Dnn.Manifest.Package.Component.ResourceFile;
using Dazinate.Dnn.Manifest.Package.Component.Script;
using Dazinate.Dnn.Manifest.Package.Component.Shared.File;
using Dazinate.Dnn.Manifest.Package.Component.Shared.LanguageFile;
using Dazinate.Dnn.Manifest.Package.Component.Skin;
using Dazinate.Dnn.Manifest.Package.Component.SkinObject;
using Dazinate.Dnn.Manifest.Package.Component.UrlProvider;
using Dazinate.Dnn.Manifest.Package.Dependency;
using Dazinate.Dnn.Manifest.Utils;

namespace Dazinate.Dnn.Manifest.ObjectFactory
{
    public class SaveToNewXmlFileVisitor : BaseObjectFactory, IManifestVisitor
    {

        private XmlWriter _writer;

        public SaveToNewXmlFileVisitor(IObjectActivator activator, XmlWriter writer) : base(activator)
        {
            _writer = writer;
        }

        public void Visit(IPackagesDnnManifest packagesManifest)
        {
            _writer.WriteStartDocument();
            _writer.WriteStartElement("dotnetnuke");
            _writer.WriteAttributeString("type", packagesManifest.Type.ToString());
            _writer.WriteAttributeString("version", packagesManifest.Version.ToString());

            packagesManifest.Packages?.Accept(this);

            _writer.WriteEndElement();

            MarkOld(packagesManifest);

        }

        public void Visit(IPackagesList list)
        {
            if (list.Any())
            {
                _writer.WriteStartElement("packages");
                foreach (var package in list)
                {
                    package.Accept(this);
                }
                _writer.WriteEndElement();
            }

            MarkListSaved<IPackage>(list);
        }

        public void Visit(IPackage package)
        {

            _writer.WriteStartElement("package");
            _writer.WriteAttributeString("name", package.Name.ToString());
            _writer.WriteAttributeString("type", package.Type.ToString());
            _writer.WriteAttributeString("version", package.Version.ToString());

            _writer.WriteElementString("friendlyName", package.FriendlyName.ToString());
            _writer.WriteElementString("description", package.Description.ToString());

            WriteElementIfNotEmpty("iconFile", package.IconFile);

            package.Owner?.Accept(this);
            package.License?.Accept(this);
            package.ReleaseNotes?.Accept(this);

            if (package.AzureCompatible.HasValue)
            {
                _writer.WriteElementString("azureCompatible", package.AzureCompatible.Value.ToString().ToLowerInvariant());
            }

            package.Dependencies.Accept(this);
            package.Components.Accept(this);

            _writer.WriteEndElement();

            MarkOld(package);
        }

        public void Visit(IOwner owner)
        {

            if (!owner.IsEmpty())
            {
                _writer.WriteStartElement("owner");
                _writer.WriteElementString("name", owner.Name);
                _writer.WriteElementString("organization", owner.Organisation);
                _writer.WriteElementString("url", owner.Url);
                _writer.WriteElementString("email", owner.Email);
                _writer.WriteEndElement();
            }

            MarkOld(owner);
        }

        public void Visit(ILicense licence)
        {
            if (!licence.IsEmpty())
            {

                _writer.WriteStartElement("license");

                WriteAttributeIfNotEmpty("src", licence.SourceFile);

                if (!string.IsNullOrWhiteSpace(licence.Contents))
                {
                    _writer.WriteInsideCDataIfNecessary(licence.Contents);
                }

                _writer.WriteEndElement();

            }

            MarkOld(licence);
        }

        public void Visit(IReleaseNotes releaseNotes)
        {
            if (!releaseNotes.IsEmpty())
            {
                _writer.WriteStartElement("releaseNotes");

                WriteAttributeIfNotEmpty("src", releaseNotes.SourceFile);

                if (!string.IsNullOrWhiteSpace(releaseNotes.Contents))
                {
                    _writer.WriteInsideCDataIfNecessary(releaseNotes.Contents);
                }

                _writer.WriteEndElement();
            }

            MarkOld(releaseNotes);

        }

        #region Dependencies

        public void Visit(IDependenciesList dependenciesList)
        {
            if (dependenciesList.Any())
            {
                _writer.WriteStartElement("dependencies");
                foreach (var dep in dependenciesList)
                {
                    dep.Accept(this);
                }
                _writer.WriteEndElement();
            }
           

            MarkListSaved<IDependency>(dependenciesList);
        }

        public void Visit(CoreVersionDependency dependency)
        {
            _writer.WriteStartElement("dependency");
            _writer.WriteAttributeString("type", "coreVersion");
            _writer.WriteString(dependency.Version);
            _writer.WriteEndElement();

            MarkOld(dependency);
        }

        public void Visit(ManagedPackageDependency dependency)
        {
            _writer.WriteStartElement("dependency");
            _writer.WriteAttributeString("type", "managedPackage");
            _writer.WriteAttributeString("version", dependency.Version);
            _writer.WriteString(dependency.PackageName);
            _writer.WriteEndElement();

            MarkOld(dependency);
        }

        public void Visit(PackageDependency dependency)
        {
            _writer.WriteStartElement("dependency");
            _writer.WriteAttributeString("type", "package");
            _writer.WriteString(dependency.PackageName);
            _writer.WriteEndElement();

            MarkOld(dependency);
        }

        public void Visit(TypeDependency dependency)
        {
            _writer.WriteStartElement("dependency");
            _writer.WriteAttributeString("type", "type");
            _writer.WriteString(dependency.TypeName);
            _writer.WriteEndElement();

            MarkOld(dependency);
        }

        public void Visit(CustomDependency dependency)
        {
            _writer.WriteStartElement("dependency");
            _writer.WriteAttributeString("type", dependency.Type);
            _writer.WriteString(dependency.Value);
            _writer.WriteEndElement();

            MarkOld(dependency);
        }

        #endregion

        #region components

        public void Visit(IComponentsList list)
        {
            if (list.Any())
            {
                _writer.WriteStartElement("components");

                foreach (var item in list)
                {
                    item.Accept(this);
                }

                _writer.WriteEndElement();
            }

            MarkListSaved<IComponent>(list);

        }

        public void Visit(IAuthenticationSystemComponent component)
        {

            _writer.WriteStartElement("component");
            _writer.WriteAttributeString("type", "AuthenticationSystem");

            _writer.WriteStartElement("authenticationService");

            _writer.WriteElementString("type", component.Type);
            _writer.WriteElementString("settingsControlSrc", component.SettingsControlSource);
            _writer.WriteElementString("loginControlSrc", component.LoginControlSource);
            _writer.WriteElementString("logoffControlSrc", component.LogoffControlSource);

            _writer.WriteEndElement();
            _writer.WriteEndElement();

            MarkOld(component);

        }

        public void Visit(File file)
        {
            _writer.WriteStartElement("file");

            _writer.WriteElementString("path", file.Path);
            _writer.WriteElementString("name", file.Name);

            WriteElementIfNotEmpty("sourceFileName", file.SourceFileName);

            _writer.WriteEndElement();

            MarkOld(file);
        }

        public void Visit(IFilesList list)
        {
            if (list.Any())
            {
                foreach (var item in list)
                {
                    item.Accept(this);
                }
            }

            MarkListSaved<IFile>(list);
        }

        public void Visit(ICleanupComponent component)
        {
            _writer.WriteStartElement("component");
            _writer.WriteAttributeString("type", "Cleanup");

            WriteAttributeIfNotEmpty("version", component.Version);
            WriteAttributeIfNotEmpty("fileName", component.FileName);

            if (component.Files.Any())
            {
                _writer.WriteStartElement("files");

                component.Files.Accept(this);

                _writer.WriteEndElement();
            }

            _writer.WriteEndElement();

            MarkOld(component);
        }

        public void Visit(INode node)
        {
            _writer.WriteStartElement("node");

            WriteAttributeIfNotEmpty("path", node.Path);

            if (node.Action != null)
            {
                var action = node.Action.ToString().ToLowerInvariant();
                if (!string.IsNullOrWhiteSpace(action))
                {
                    _writer.WriteAttributeString("action", action);
                }
            }

            WriteAttributeIfNotEmpty("targetpath", node.TargetPath);
            WriteAttributeIfNotEmpty("key", node.Key);


            if (node.Collision != null)
            {
                var collision = node.Collision.ToString().ToLowerInvariant();
                if (!string.IsNullOrWhiteSpace(collision))
                {
                    _writer.WriteAttributeString("collision", collision);
                }
            }

            WriteAttributeIfNotEmpty("name", node.Name);
            WriteAttributeIfNotEmpty("value", node.Value);
            WriteAttributeIfNotEmpty("nameSpace", node.Namespace);
            WriteAttributeIfNotEmpty("nameSpacePrefix", node.NamespacePrefix);

            _writer.WriteRaw(node.InnerXml);

            _writer.WriteEndElement();

            MarkOld(node);
        }

        public void Visit(INodesList list)
        {
            if (list.Any())
            {
                _writer.WriteStartElement("nodes");
                foreach (var node in list)
                {
                    node.Accept(this);
                }
                _writer.WriteEndElement();
            }

            MarkListSaved<INode>(list);
        }

        public void Visit(IConfigComponent component)
        {
            _writer.WriteStartElement("component");
            _writer.WriteAttributeString("type", "Config");

            _writer.WriteStartElement("config");

            WriteElementIfNotEmpty("configFile", component.ConfigFile);

            // install nodes list.
            _writer.WriteStartElement("install");
            _writer.WriteStartElement("configuration");

            component.InstallNodes.Accept(this);

            _writer.WriteEndElement();
            _writer.WriteEndElement();

            // uninstall nodes list.
            _writer.WriteStartElement("uninstall");
            _writer.WriteStartElement("configuration");

            component.UninstallNodes.Accept(this);

            _writer.WriteEndElement();
            _writer.WriteEndElement();


            _writer.WriteEndElement();
            _writer.WriteEndElement();

            MarkOld(component);
        }

        public void Visit(IContainerFilesList list)
        {
            foreach (var file in list)
            {
                file.Accept(this);
            }

            MarkListSaved<IContainerFile>(list);
        }

        public void Visit(IContainerComponent component)
        {
            _writer.WriteStartElement("component");
            _writer.WriteAttributeString("type", "Container");

            _writer.WriteStartElement("containerFiles");

            _writer.WriteElementString("basePath", component.BasePath);
            _writer.WriteElementString("containerName", component.ContainerName);

            component.Files?.Accept(this);

            _writer.WriteEndElement();
            _writer.WriteEndElement();

            MarkOld(component);

        }

        public void Visit(ContainerFile file)
        {
            _writer.WriteStartElement("containerFile");

            _writer.WriteElementString("path", file.Path);
            _writer.WriteElementString("name", file.Name);

            WriteElementIfNotEmpty("sourceFileName", file.SourceFileName);

            _writer.WriteEndElement();

            MarkOld(file);

        }

        public void Visit(DashboardControlComponent component)
        {
            _writer.WriteStartElement("component");
            _writer.WriteAttributeString("type", "DashboardControl");

            component.Controls?.Accept(this);

            _writer.WriteEndElement();

            MarkOld(component);

        }

        public void Visit(DashboardControlsList list)
        {
            foreach (var item in list)
            {
                item.Accept(this);
            }

            MarkListSaved<IDashboardControl>(list);
        }

        public void Visit(DashboardControl dashboardControl)
        {
            _writer.WriteStartElement("dashboardControl");

            _writer.WriteElementString("key", dashboardControl.Key);
            _writer.WriteElementString("src", dashboardControl.Source);

            _writer.WriteElementString("localResources", dashboardControl.LocalResourcesFile);
            _writer.WriteElementString("controllerClass", dashboardControl.ControllerClass);
            _writer.WriteElementString("isEnabled", dashboardControl.IsEnabled.ToString().ToLowerInvariant());
            _writer.WriteElementString("viewOrder", dashboardControl.ViewOrder.ToString());


            _writer.WriteEndElement();

            MarkOld(dashboardControl);
        }

        public void Visit(FileComponent component)
        {
            _writer.WriteStartElement("component");
            _writer.WriteAttributeString("type", "File");

            if (component.Files.Any())
            {
                _writer.WriteStartElement("files");

                WriteElementIfNotEmpty("basePath", component.BasePath);

                component.Files.Accept(this);

                _writer.WriteEndElement();
            }


            _writer.WriteEndElement();

            MarkOld(component);

        }

        public void Visit(LanguageFilesList list)
        {
            foreach (var file in list)
            {
                file.Accept(this);
            }

            MarkListSaved<ILanguageFile>(list);
        }

        public void Visit(LanguageFile file)
        {
            _writer.WriteStartElement("languageFile");

            _writer.WriteElementString("path", file.Path);
            _writer.WriteElementString("name", file.Name);

            _writer.WriteEndElement();

            MarkOld(file);
        }

        public void Visit(CoreLanguageComponent component)
        {
            _writer.WriteStartElement("component");
            _writer.WriteAttributeString("type", "CoreLanguage");

            if (component.Files.Any())
            {
                _writer.WriteStartElement("languageFiles");

                WriteElementIfNotEmpty("code", component.Code);
                WriteElementIfNotEmpty("displayName", component.DisplayName);
                WriteElementIfNotEmpty("fallback", component.Fallback);

                component.Files.Accept(this);

                _writer.WriteEndElement();
            }


            _writer.WriteEndElement();

            MarkOld(component);
        }

        public void Visit(ExtensionLanguageComponent component)
        {
            _writer.WriteStartElement("component");
            _writer.WriteAttributeString("type", "ExtensionLanguage");

            if (component.Files.Any())
            {
                _writer.WriteStartElement("languageFiles");

                WriteElementIfNotEmpty("code", component.Code);
                WriteElementIfNotEmpty("package", component.Package);
                WriteElementIfNotEmpty("basePath", component.BasePath);

                component.Files.Accept(this);

                _writer.WriteEndElement();
            }


            _writer.WriteEndElement();

            MarkOld(component);

        }

        public void Visit(ModuleComponent component)
        {
            _writer.WriteStartElement("component");
            _writer.WriteAttributeString("type", "Module");

            _writer.WriteStartElement("desktopModule");

            _writer.WriteElementString("moduleName", component.ModuleName);
            _writer.WriteElementString("foldername", component.FolderName);

            WriteElementIfNotEmpty("businessControllerClass", component.BusinessControllerClass);

            WriteElementIfNotEmpty("codeSubdirectory", component.CodeSubDirectory);

            WriteElementIfNotNull("isAdmin", component.IsAdmin);
            WriteElementIfNotNull("isPremium", component.IsPremium);


            if (component.SupportedFeatures.Any())
            {
                component.SupportedFeatures.Accept(this);
            }

            if (component.ModuleDefinitions.Any())
            {
                component.ModuleDefinitions.Accept(this);
            }

            if (component.EventMessage != null)
            {
                component.EventMessage.Accept(this);
            }

            _writer.WriteEndElement();
            _writer.WriteEndElement();

            MarkOld(component);

        }

        public void Visit(EventMessage eventMessage)
        {
            _writer.WriteStartElement("eventMessage");

            WriteElementIfNotEmpty("processorType", eventMessage.ProcessorCommand);
            WriteElementIfNotEmpty("processorCommand", eventMessage.ProcessorType);

            if (eventMessage.Attributes.Any())
            {
                eventMessage.Attributes.Accept(this);
            }

            _writer.WriteEndElement();

            MarkOld(eventMessage);

        }

        public void Visit(EventAttribute eventAttribute)
        {
            _writer.WriteElementString(eventAttribute.Name, eventAttribute.Value);

            MarkOld(eventAttribute);
        }

        public void Visit(EventAttributesList list)
        {
            if (list.Any())
            {
                _writer.WriteStartElement("attributes");

                foreach (var att in list)
                {
                    att.Accept(this);
                }

                _writer.WriteEndElement();
            }

            MarkListSaved<IEventAttribute>(list);
        }

        public void Visit(SupportedFeaturesList list)
        {
            if (list.Any())
            {
                _writer.WriteStartElement("supportedFeatures");

                foreach (var item in list)
                {
                    item.Accept(this);
                }

                _writer.WriteEndElement();
            }
            

            MarkListSaved<ISupportedFeature>(list);

        }

        public void Visit(SupportedFeature supportedFeature)
        {
            _writer.WriteStartElement("supportedFeature");
            _writer.WriteAttributeString("type", supportedFeature.FeatureType.ToString());
            _writer.WriteEndElement();

            MarkOld(supportedFeature);
        }

        public void Visit(ModuleDefinitionsList list)
        {
            if (list.Any())
            {
                _writer.WriteStartElement("moduleDefinitions");
                foreach (var item in list)
                {
                    item.Accept(this);
                }
                _writer.WriteEndElement();
            }

            MarkListSaved<IModuleDefinition>(list);
        }

        public void Visit(ProviderComponent component)
        {
            _writer.WriteStartElement("component");
            _writer.WriteAttributeString("type", "Provider");
            _writer.WriteEndElement();

            MarkOld(component);
        }

        public void Visit(ResourceFileComponent component)
        {
            _writer.WriteStartElement("component");
            _writer.WriteAttributeString("type", "ResourceFile");

            _writer.WriteStartElement("resourceFiles");

            WriteElementIfNotEmpty("basePath", component.BasePath);

            component.Files?.Accept(this);

            _writer.WriteEndElement();
            _writer.WriteEndElement();

            MarkOld(component);
        }

        public void Visit(ResourceFilesList list)
        {
            foreach (var item in list)
            {
                item.Accept(this);
            }

            MarkListSaved<IResourceFile>(list);
        }

        public void Visit(ResourceFile file)
        {
            _writer.WriteStartElement("resourceFile");

            WriteElementIfNotEmpty("name", file.Name);
            WriteElementIfNotEmpty("path", file.Path);
            WriteElementIfNotEmpty("sourceFileName", file.SourceFileName);

            _writer.WriteEndElement();

            MarkOld(file);
        }

        public void Visit(Script script)
        {
            _writer.WriteStartElement("script");

            _writer.WriteAttributeString("type", script.Type.ToString());

            WriteElementIfNotEmpty("path", script.Path);
            WriteElementIfNotEmpty("name", script.Name);
            WriteElementIfNotEmpty("version", script.Version);

            WriteElementIfNotEmpty("sourceFileName", script.SourceFileName);

            _writer.WriteEndElement();

            MarkOld(script);

        }

        public void Visit(ScriptsList list)
        {
            foreach (var item in list)
            {
                item.Accept(this);
            }

            MarkListSaved<IScript>(list);
        }

        public void Visit(ScriptComponent component)
        {
            _writer.WriteStartElement("component");
            _writer.WriteAttributeString("type", "Script");

            _writer.WriteStartElement("scripts");

            WriteElementIfNotEmpty("basePath", component.BasePath);

            component.Scripts?.Accept(this);

            _writer.WriteEndElement();
            _writer.WriteEndElement();

            MarkOld(component);
        }

        public void Visit(UrlProviderComponent component)
        {
            _writer.WriteStartElement("component");
            _writer.WriteAttributeString("type", "UrlProvider");

            _writer.WriteStartElement("urlProvider");

            WriteElementIfNotEmpty("name", component.Name);
            WriteElementIfNotEmpty("type", component.Type);
            WriteElementIfNotEmpty("settingsControlSrc", component.SettingsControlSource);
            WriteElementIfNotEmpty("redirectAllUrls", component.RedirectAllUrls.ToString());
            WriteElementIfNotEmpty("replaceAllUrls", component.ReplaceAllUrls.ToString());
            WriteElementIfNotEmpty("rewriteAllUrls", component.RewriteAllUrls.ToString());
            _writer.WriteElementString("desktopModule", component.DesktopModule);

            _writer.WriteEndElement();
            _writer.WriteEndElement();

            MarkOld(component);

        }

        public void Visit(SkinObjectComponent component)
        {
            _writer.WriteStartElement("component");
            _writer.WriteAttributeString("type", "SkinObject");

            _writer.WriteStartElement("moduleControl");

            WriteElementIfNotEmpty("controlKey", component.ControlKey);
            WriteElementIfNotEmpty("controlSrc", component.ControlSource);
            WriteElementIfNotEmpty("supportsPartialRendering", component.SupportsPartialRendering.ToString());

            _writer.WriteEndElement();
            _writer.WriteEndElement();

            MarkOld(component);

        }

        public void Visit(SkinComponent component)
        {
            _writer.WriteStartElement("component");
            _writer.WriteAttributeString("type", "Skin");

            _writer.WriteStartElement("skinFiles");

            WriteElementIfNotEmpty("skinName", component.SkinName);
            WriteElementIfNotEmpty("basePath", component.BasePath);

            component.Files?.Accept(this);

            _writer.WriteEndElement();
            _writer.WriteEndElement();

            MarkOld(component);
        }

        public void Visit(JavascriptFile file)
        {
            _writer.WriteStartElement("jsfile");

            WriteElementIfNotEmpty("name", file.Name);
            WriteElementIfNotEmpty("path", file.Path);
            WriteElementIfNotEmpty("sourceFileName", file.SourceFileName);

            _writer.WriteEndElement();

            MarkOld(file);

        }

        public void Visit(JavascriptFilesList list)
        {
            foreach (var item in list)
            {
                item.Accept(this);
            }

            MarkListSaved<IJavascriptFile>(list);
        }

        public void Visit(JavascriptFileComponent component)
        {
            _writer.WriteStartElement("component");
            _writer.WriteAttributeString("type", "JavaScriptFile");

            _writer.WriteStartElement("jsfiles");

            WriteElementIfNotEmpty("libraryFolderName", component.LibraryFolderName);

            component.Files.Accept(this);

            _writer.WriteEndElement();
            _writer.WriteEndElement();

            MarkOld(component);
        }

        public void Visit(JavascriptLibraryComponent component)
        {
            _writer.WriteStartElement("component");
            _writer.WriteAttributeString("type", "JavaScript_Library");

            WriteElementIfNotEmpty("libraryName", component.LibraryName);
            WriteElementIfNotEmpty("fileName", component.FileName);
            WriteElementIfNotEmpty("preferredScriptLocation", component.PreferredScriptLocation);
            WriteElementIfNotEmpty("CDNPath", component.CdnPath);
            WriteElementIfNotEmpty("objectName", component.ObjectName);

            _writer.WriteEndElement();

            MarkOld(component);
        }

        public void Visit(SkinFilesList list)
        {
            foreach (var file in list)
            {
                file.Accept(this);
            }

            MarkListSaved<ISkinFile>(list);
        }

        public void Visit(SkinFile file)
        {
            _writer.WriteStartElement("skinFile");

            _writer.WriteElementString("path", file.Path);
            _writer.WriteElementString("name", file.Name);

            WriteElementIfNotEmpty("sourceFileName", file.SourceFileName);

            _writer.WriteEndElement();

            MarkOld(file);
        }

        public void Visit(ModuleDefinition moduleDefinition)
        {
            _writer.WriteStartElement("moduleDefinition");

            WriteElementIfNotEmpty("friendlyName", moduleDefinition.FriendlyName);
            WriteElementIfNotNull("defaultCacheTime", moduleDefinition.DefaultCacheTime);

            moduleDefinition.ModuleControls?.Accept(this);

            moduleDefinition.ModulePermissions?.Accept(this);

            _writer.WriteEndElement();

            MarkOld(moduleDefinition);
        }

        public void Visit(ModuleControlsList list)
        {
            if (list.Any())
            {
                _writer.WriteStartElement("moduleControls");

                foreach (var item in list)
                {
                    item.Accept(this);
                }

                _writer.WriteEndElement();
            }

            MarkListSaved<IModuleControl>(list);
        }

        public void Visit(ModuleControl moduleControl)
        {
            _writer.WriteStartElement("moduleControl");

            _writer.WriteElementString("controlKey", moduleControl.ControlKey);
            // WriteElementIfNotEmpty("controlKey", moduleControl.ControlKey);

            WriteElementIfNotEmpty("controlSrc", moduleControl.ControlSource);
            WriteElementIfNotNull("supportsPartialRendering", moduleControl.SupportsPartialRendering);
            WriteElementIfNotNull("controlTitle", moduleControl.ControlTitle);
            WriteElementIfNotEmpty("controlType", moduleControl.ControlType);
            WriteElementIfNotNull("iconFile", moduleControl.IconFile);
            WriteElementIfNotNull("helpUrl", moduleControl.HelpUrl);
            WriteElementIfNotNull("viewOrder", moduleControl.ViewOrder);

            _writer.WriteEndElement();

            MarkOld(moduleControl);
        }

        public void Visit(ModulePermissionsList list)
        {
            if (list.Any())
            {
                _writer.WriteStartElement("permissions");

                foreach (var item in list)
                {
                    item.Accept(this);
                }

                _writer.WriteEndElement();
            }

            MarkListSaved<IModulePermission>(list);
        }

        public void Visit(ModulePermission modulePermission)
        {

            _writer.WriteStartElement("permission");

            WriteAttributeIfNotEmpty("code", modulePermission.Code);
            WriteAttributeIfNotEmpty("key", modulePermission.Key);
            WriteAttributeIfNotEmpty("name", modulePermission.Name);

            _writer.WriteEndElement();

            MarkOld(modulePermission);

        }

        public void Visit(IAssemblyComponent component)
        {
            if (component.Assemblies.Any())
            {
                _writer.WriteStartElement("component");
                _writer.WriteAttributeString("type", "Assembly");

                component.Assemblies.Accept(this);

                _writer.WriteEndElement();
            }

            MarkOld(component);
        }

        public void Visit(IAssembliesList list)
        {
            if (list.Any())
            {
                _writer.WriteStartElement("assemblies");

                foreach (var assy in list)
                {
                    assy.Accept(this);
                }

                _writer.WriteEndElement();
            }

            MarkListSaved<IAssembly>(list);


        }

        public void Visit(IAssembly assembly)
        {
            _writer.WriteStartElement("assembly");
            if (assembly.Action != AssemblyAction.Install)
            {
                _writer.WriteAttributeString("action", assembly.Action.ToString());
            }

            WriteElementIfNotEmpty("path", assembly.Path);
            WriteElementIfNotEmpty("name", assembly.Name);
            WriteElementIfNotEmpty("version", assembly.Version);

            _writer.WriteEndElement();

            MarkOld(assembly);

        }


        #endregion

        #region Util Methods

        private void WriteElementIfNotEmpty(string elementName, string value)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                _writer.WriteElementString(elementName, value);
            }
        }

        private void WriteAttributeIfNotEmpty(string attributeName, string value)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                _writer.WriteAttributeString(attributeName, value);
            }
        }

        private void WriteElementIfNotNull<T>(string elementName, T value)
        {
            if (value != null)
            {
                _writer.WriteElementString(elementName, value.ToString());
            }
        }

        #endregion

    }
}

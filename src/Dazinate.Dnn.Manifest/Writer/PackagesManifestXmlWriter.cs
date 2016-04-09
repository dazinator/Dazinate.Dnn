using System.Linq;
using System.Reflection;
using System.Xml;
using Dazinate.Dnn.Manifest.Model;
using Dazinate.Dnn.Manifest.Model.AssembliesList;
using Dazinate.Dnn.Manifest.Model.Assembly;
using Dazinate.Dnn.Manifest.Model.Assembly.ObjectFactory;
using Dazinate.Dnn.Manifest.Model.Component;
using Dazinate.Dnn.Manifest.Model.ComponentsList;
using Dazinate.Dnn.Manifest.Model.Dependency;
using Dazinate.Dnn.Manifest.Model.DependencyList;
using Dazinate.Dnn.Manifest.Model.File;
using Dazinate.Dnn.Manifest.Model.FilesList;
using Dazinate.Dnn.Manifest.Model.Manifest;
using Dazinate.Dnn.Manifest.Model.Node;
using Dazinate.Dnn.Manifest.Model.NodesList;
using Dazinate.Dnn.Manifest.Model.Package;
using Dazinate.Dnn.Manifest.Model.PackagesList;
using Dazinate.Dnn.Manifest.Utils;

namespace Dazinate.Dnn.Manifest.Writer
{
    public class PackagesDnnManifestXmlWriter : IManifestXmlWriterVisitor
    {

        private XmlWriter _writer;

        public PackagesDnnManifestXmlWriter(XmlWriter writer)
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

        }

        public void Visit(IPackagesList packagesList)
        {
            _writer.WriteStartElement("packages");
            foreach (var package in packagesList)
            {
                package.Accept(this);
            }
            _writer.WriteEndElement();
        }

        public void Visit(IDependenciesList dependenciesList)
        {
            _writer.WriteStartElement("dependencies");
            foreach (var dep in dependenciesList)
            {
                dep.Accept(this);
            }
            _writer.WriteEndElement();
        }

        public void Visit(CoreVersionDependency dependency)
        {
            _writer.WriteStartElement("dependency");
            _writer.WriteAttributeString("type", "coreVersion");
            _writer.WriteString(dependency.Version);
            _writer.WriteEndElement();
        }

        public void Visit(ManagedPackageDependency managedPackageDependency)
        {
            _writer.WriteStartElement("dependency");
            _writer.WriteAttributeString("type", "managedPackage");
            _writer.WriteAttributeString("version", managedPackageDependency.Version);
            _writer.WriteString(managedPackageDependency.PackageName);
            _writer.WriteEndElement();
        }

        public void Visit(PackageDependency packageDependency)
        {
            _writer.WriteStartElement("dependency");
            _writer.WriteAttributeString("type", "package");
            _writer.WriteString(packageDependency.PackageName);
            _writer.WriteEndElement();
        }

        public void Visit(TypeDependency typeDependency)
        {
            _writer.WriteStartElement("dependency");
            _writer.WriteAttributeString("type", "type");
            _writer.WriteString(typeDependency.TypeName);
            _writer.WriteEndElement();
        }

        public void Visit(CustomDependency managedPackageDependency)
        {
            _writer.WriteStartElement("dependency");
            _writer.WriteAttributeString("type", managedPackageDependency.Type);
            _writer.WriteString(managedPackageDependency.Value);
            _writer.WriteEndElement();
        }

        public void Visit(IPackage package)
        {

            _writer.WriteStartElement("package");
            _writer.WriteAttributeString("name", package.Name.ToString());
            _writer.WriteAttributeString("type", package.Type.ToString());
            _writer.WriteAttributeString("version", package.Version.ToString());

            _writer.WriteElementString("friendlyName", package.FriendlyName.ToString());
            _writer.WriteElementString("description", package.Description.ToString());

            if (!string.IsNullOrWhiteSpace(package.IconFile))
            {
                _writer.WriteElementString("iconFile", package.IconFile.ToString());
            }

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
        }

        public void Visit(IOwner owner)
        {
            _writer.WriteStartElement("owner");
            _writer.WriteElementString("name", owner.Name);
            _writer.WriteElementString("organization", owner.Organisation);
            _writer.WriteElementString("url", owner.Url);
            _writer.WriteElementString("email", owner.Email);
            _writer.WriteEndElement();
        }

        public void Visit(ILicense licence)
        {
            if (licence.IsEmpty())
            {
                return;
            }

            _writer.WriteStartElement("license");

            if (!string.IsNullOrWhiteSpace(licence.SourceFile))
            {
                _writer.WriteAttributeString("src", licence.SourceFile);
            }

            if (!string.IsNullOrWhiteSpace(licence.Contents))
            {
                _writer.WriteInsideCDataIfNecessary(licence.Contents);
            }

            _writer.WriteEndElement();
        }

        public void Visit(IReleaseNotes releaseNotes)
        {
            if (releaseNotes.IsEmpty())
            {
                return;
            }

            _writer.WriteStartElement("releaseNotes");

            if (!string.IsNullOrWhiteSpace(releaseNotes.SourceFile))
            {
                _writer.WriteAttributeString("src", releaseNotes.SourceFile);
            }

            if (!string.IsNullOrWhiteSpace(releaseNotes.Contents))
            {
                _writer.WriteInsideCDataIfNecessary(releaseNotes.Contents);
            }

            _writer.WriteEndElement();

        }

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


        }

        public void Visit(IFile file)
        {
            _writer.WriteStartElement("file");

            _writer.WriteElementString("path", file.Path);
            _writer.WriteElementString("name", file.Name);

            _writer.WriteEndElement();
        }

        public void Visit(IFilesList list)
        {
            if (list.Any())
            {
                _writer.WriteStartElement("files");

                foreach (var item in list)
                {
                    item.Accept(this);
                }

                _writer.WriteEndElement();
            }


        }

        public void Visit(ICleanupComponent component)
        {
            _writer.WriteStartElement("component");
            _writer.WriteAttributeString("type", "Cleanup");

            if (!string.IsNullOrWhiteSpace(component.Version))
            {
                _writer.WriteAttributeString("version", component.Version);
            }

            if (!string.IsNullOrWhiteSpace(component.FileName))
            {
                _writer.WriteAttributeString("fileName", component.FileName);
            }

            component.Files.Accept(this);

            _writer.WriteEndElement();
        }

        public void Visit(INode node)
        {
            _writer.WriteStartElement("node");

            if (!string.IsNullOrWhiteSpace(node.Path))
            {
                _writer.WriteAttributeString("path", node.Path);
            }

            if (node.Action != null)
            {
                var action = node.Action.ToString().ToLowerInvariant();
                if (!string.IsNullOrWhiteSpace(action))
                {
                    _writer.WriteAttributeString("action", action);
                }
            }

            if (!string.IsNullOrWhiteSpace(node.TargetPath))
            {
                _writer.WriteAttributeString("targetpath", node.TargetPath);
            }

            if (!string.IsNullOrWhiteSpace(node.Key))
            {
                _writer.WriteAttributeString("key", node.Key);
            }

            if (node.Collision != null)
            {
                var collision = node.Collision.ToString().ToLowerInvariant();
                if (!string.IsNullOrWhiteSpace(collision))
                {
                    _writer.WriteAttributeString("collision", collision);
                }
            }
           

            if (!string.IsNullOrWhiteSpace(node.Name))
            {
                _writer.WriteAttributeString("name", node.Name);
            }

            if (!string.IsNullOrWhiteSpace(node.Value))
            {
                _writer.WriteAttributeString("value", node.Value);
            }

            if (!string.IsNullOrWhiteSpace(node.Namespace))
            {
                _writer.WriteAttributeString("nameSpace", node.Namespace);
            }

            if (!string.IsNullOrWhiteSpace(node.NamespacePrefix))
            {
                _writer.WriteAttributeString("nameSpacePrefix", node.NamespacePrefix);
            }

            _writer.WriteRaw(node.InnerXml);

            _writer.WriteEndElement();
        }

        public void Visit(INodesList list)
        {
            _writer.WriteStartElement("nodes");
            foreach (var node in list)
            {
                node.Accept(this);
            }
            _writer.WriteEndElement();
        }

        public void Visit(IConfigComponent component)
        {
            _writer.WriteStartElement("component");
            _writer.WriteAttributeString("type", "Config");

            _writer.WriteStartElement("config");

            if (!string.IsNullOrWhiteSpace(component.ConfigFile))
            {
                _writer.WriteElementString("configFile", component.ConfigFile);
            }

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
        }

        public void Visit(IAssembliesList assembliesList)
        {
            _writer.WriteStartElement("assemblies");

            foreach (var assy in assembliesList)
            {
                assy.Accept(this);
            }

            _writer.WriteEndElement();

        }

        public void Visit(IAssembly assembly)
        {
            _writer.WriteStartElement("assembly");
            if (assembly.Action != AssemblyAction.Install)
            {
                _writer.WriteAttributeString("action", assembly.Action.ToString());
            }

            if (!string.IsNullOrWhiteSpace(assembly.Path))
            {
                _writer.WriteElementString("path", assembly.Path);
            }
            if (!string.IsNullOrWhiteSpace(assembly.Name))
            {
                _writer.WriteElementString("name", assembly.Name);
            }
            if (!string.IsNullOrWhiteSpace(assembly.Version))
            {
                _writer.WriteElementString("version", assembly.Version);
            }

            _writer.WriteEndElement();

        }



        #endregion
    }
}

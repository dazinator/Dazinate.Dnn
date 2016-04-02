using System.Xml;
using Dazinate.Dnn.Manifest.Model;
using Dazinate.Dnn.Manifest.Model.Dependency;
using Dazinate.Dnn.Manifest.Model.Package;
using Dazinate.Dnn.Manifest.Wip;

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
    }
}

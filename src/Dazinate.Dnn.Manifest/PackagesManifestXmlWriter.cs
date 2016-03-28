using System.Xml;
using Dazinate.Dnn.Manifest.Wip;

namespace Dazinate.Dnn.Manifest
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

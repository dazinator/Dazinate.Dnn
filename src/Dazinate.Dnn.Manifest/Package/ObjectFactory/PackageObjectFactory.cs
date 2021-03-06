using System.Xml.XPath;
using Dazinate.Dnn.Manifest.Base;
using Dazinate.Dnn.Manifest.Ioc;
using Dazinate.Dnn.Manifest.Package.Component.ObjectFactory;
using Dazinate.Dnn.Manifest.Package.Dependency.ObjectFactory;
using Dazinate.Dnn.Manifest.Utils;

namespace Dazinate.Dnn.Manifest.Package.ObjectFactory
{
    public class PackageObjectFactory : BaseObjectFactory, IPackageObjectFactory
    {

        private readonly IDependenciesListObjectFactory _dependenciesListFactory;
        private readonly IComponentsListObjectFactory _componentsListFactory;

        public PackageObjectFactory(IObjectActivator activator, IDependenciesListObjectFactory dependenciesListFactory, IComponentsListObjectFactory componentsListFactory) : base(activator)
        {
            _dependenciesListFactory = dependenciesListFactory;
            _componentsListFactory = componentsListFactory;
        }

        public Package Fetch(XPathNavigator packageNav)
        {
            var package = CreateInstance<Package>();
            //  package.Manifest = dnnPackagesManifest; // to set the parent on each child so a child business rule can access parents properties.

            LoadProperty(package, Package.NameProperty, XmlUtils.ReadRequiredAttribute(packageNav, "name"));
            LoadProperty(package, Package.TypeProperty, XmlUtils.ReadRequiredAttribute(packageNav, "type"));
            LoadProperty(package, Package.VersionProperty, XmlUtils.ReadRequiredAttribute(packageNav, "version"));

            LoadProperty(package, Package.FriendlyNameProperty, XmlUtils.ReadElement(packageNav, "friendlyName"));
            LoadProperty(package, Package.DescriptionProperty, XmlUtils.ReadElement(packageNav, "description"));
            LoadProperty(package, Package.IconFileProperty, XmlUtils.ReadElement(packageNav, "iconFile"));

            var ownerNode = packageNav.SelectSingleNode("owner");
            IOwner owner = GetOwner(ownerNode);
            LoadProperty(package, Package.OwnerProperty, owner);

            var licenseNode = packageNav.SelectSingleNode("license");
            ILicense license = GetLicense(licenseNode);
            LoadProperty(package, Package.LicenseProperty, license);


            //todo: release notes
            var releaseNotesNode = packageNav.SelectSingleNode("releaseNotes");
            IReleaseNotes releaseNotes = GetReleaseNotes(releaseNotesNode);
            LoadProperty(package, Package.ReleaseNotesProperty, releaseNotes);


            var azureCompatString = XmlUtils.ReadElement(packageNav, "azureCompatible");
            if (!string.IsNullOrWhiteSpace(azureCompatString))
            {
                bool azureCompat = bool.Parse(azureCompatString);
                LoadProperty(package, Package.AzureCompatibleProperty, azureCompat);
            }

            var dependenciesList = _dependenciesListFactory.Fetch(packageNav);
            LoadProperty(package, Package.DependenciesListProperty, dependenciesList);

            var componentsList = _componentsListFactory.Fetch(packageNav);
            LoadProperty(package, Package.ComponentsListProperty, componentsList);

            MarkAsChild(package);
            MarkOld(package);
            CheckRules(package);

            //todo: dependencies
            return package;

        }

        private IReleaseNotes GetReleaseNotes(XPathNavigator releaseNotesNode)
        {
            ReleaseNotes releaseNotes = CreateInstance<ReleaseNotes>();
            if (releaseNotesNode != null)
            {
                var sourceAttribute = releaseNotesNode.SelectSingleNode("@src");
                if (sourceAttribute != null)
                {
                    LoadProperty(releaseNotes, ReleaseNotes.SourceFileProperty, sourceAttribute.Value);
                }

                var contents = releaseNotesNode.Value;
                if (!string.IsNullOrWhiteSpace(contents))
                {
                    LoadProperty(releaseNotes, ReleaseNotes.ContentsProperty, contents);
                }

            }
            else
            {
                //   LoadProperty(releaseNotes, ReleaseNotes.SourceFileProperty, string.Empty);
                // LoadProperty(releaseNotes, ReleaseNotes.ContentsProperty, string.Empty);
            }



            MarkAsChild(releaseNotes);
            MarkOld(releaseNotes);

            return releaseNotes;
        }

        private ILicense GetLicense(XPathNavigator licenseNode)
        {
            License license = CreateInstance<License>();
            if (licenseNode != null)
            {
                var sourceAttribute = licenseNode.SelectSingleNode("@src");
                if (sourceAttribute != null)
                {
                    LoadProperty(license, License.SourceFileProperty, sourceAttribute.Value);
                }

                var contents = licenseNode.Value;
                if (!string.IsNullOrWhiteSpace(contents))
                {
                    LoadProperty(license, License.ContentsProperty, contents);
                }

            }
            else
            {
                //  LoadProperty(license, License.SourceFileProperty, string.Empty);
                //   LoadProperty(license, License.ContentsProperty, string.Empty);
            }

            MarkAsChild(license);
            MarkOld(license);

            return license;

        }

        private IOwner GetOwner(XPathNavigator ownerNode)
        {
            var owner = CreateInstance<Owner>();
            if (ownerNode != null)
            {
                LoadProperty(owner, Owner.NameProperty, XmlUtils.ReadElement(ownerNode, "name"));
                LoadProperty(owner, Owner.OrganisationProperty, XmlUtils.ReadElement(ownerNode, "organization"));
                LoadProperty(owner, Owner.UrlProperty, XmlUtils.ReadElement(ownerNode, "url"));
                LoadProperty(owner, Owner.EmailProperty, XmlUtils.ReadElement(ownerNode, "email"));
            }


            MarkAsChild(owner);
            MarkOld(owner);
            CheckRules(owner);
            return owner;

        }

        public Package Create()
        {
            var package = CreateInstance<Package>();
            package.Owner = CreateInstance<Owner>();
            package.License = CreateInstance<License>();
            package.ReleaseNotes = CreateInstance<ReleaseNotes>();

            package.Dependencies = _dependenciesListFactory.Create();
            package.Components = _componentsListFactory.Create();

            MarkAsChild(package.Owner);
            MarkAsChild(package.License);
            MarkAsChild(package.ReleaseNotes);
            MarkAsChild(package);

            CheckRules(package);
            CheckRules(package.License);
            CheckRules(package.ReleaseNotes);
            CheckRules(package.Owner);

            return package;
        }

    }
}
using System.Xml;
using Csla;

namespace Dazinate.Dnn.Manifest.Factory
{
    public class PackagesDnnManifestFactory : DnnManifestFactory, IDnnManifestFactory<IPackagesDnnManifest>
    {
        public override IDnnManifest Get(string xmlContents)
        {
            var crit = new SingleCriteria<string>(xmlContents);
            return Csla.DataPortal.Fetch<PackagesDnnManifest>(crit);
        }

        public override IDnnManifest Get(XmlReader reader)
        {
            var xdoc = new XmlDocument();
            xdoc.Load(reader);
            var xmlContents = xdoc.OuterXml;
            var crit = new SingleCriteria<string>(xmlContents);
            return Csla.DataPortal.Fetch<PackagesDnnManifest>(crit);
        }

        IPackagesDnnManifest IDnnManifestFactory<IPackagesDnnManifest>.Get(XmlReader reader)
        {
            var xdoc = new XmlDocument();
            xdoc.Load(reader);
            var xmlContents = xdoc.OuterXml;
            var crit = new SingleCriteria<string>(xmlContents);
            return Csla.DataPortal.Fetch<PackagesDnnManifest>(crit);
        }

        IPackagesDnnManifest IDnnManifestFactory<IPackagesDnnManifest>.Get(string xmlContents)
        {
            var crit = new SingleCriteria<string>(xmlContents);
            return Csla.DataPortal.Fetch<PackagesDnnManifest>(crit);
        }
    }
}
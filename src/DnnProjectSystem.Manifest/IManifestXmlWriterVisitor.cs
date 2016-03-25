namespace Dnn.Contrib.Manifest
{
    public interface IManifestXmlWriterVisitor
    {
        void Visit(IPackagesDnnManifest packagesManifest);

    }
}
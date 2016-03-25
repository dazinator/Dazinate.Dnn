namespace Dazinate.Dnn.Manifest
{
    public interface IManifestXmlWriterVisitor
    {
        void Visit(IPackagesDnnManifest packagesManifest);

    }
}
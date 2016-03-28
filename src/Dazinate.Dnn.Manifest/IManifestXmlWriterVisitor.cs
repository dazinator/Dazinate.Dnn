namespace Dazinate.Dnn.Manifest
{
    public interface IManifestXmlWriterVisitor
    {
        void Visit(IPackagesDnnManifest packagesManifest);
        void Visit(ILicense license);
        void Visit(IOwner owner);
        void Visit(IPackage package);
        void Visit(IPackagesList packagesList);
        void Visit(IReleaseNotes releaseNotes);
    }
}
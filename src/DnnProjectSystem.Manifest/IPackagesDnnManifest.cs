namespace Dnn.Contrib.Manifest
{
    public interface IPackagesDnnManifest : IDnnManifest
    {
        IPackagesList Packages { get; }
    }

    
}
namespace Dazinate.Dnn.Manifest
{
    public interface IPackagesDnnManifest : IDnnManifest
    {
        IPackagesList Packages { get; }
    }

    
}
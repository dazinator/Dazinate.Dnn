namespace Dazinate.Dnn.Manifest.Package.Component.Skin
{
    public interface ISkinComponent : IComponent
    {

        string BasePath { get; }

        string SkinName { get; }

        ISkinFilesList Files { get; }

    }
}
namespace Dazinate.Dnn.Manifest.Package.Component.Skin
{
    public interface ISkinComponent : IComponent
    {

        string BasePath { get; set; }

        string SkinName { get; set; }

        ISkinFilesList Files { get; }

    }
}
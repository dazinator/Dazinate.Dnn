namespace Dazinate.Dnn.Manifest.Package.Component.JavascriptFile
{
    public interface IJavascriptFileComponent : IComponent
    {
        IJavascriptFilesList Files { get; }

        string LibraryFolderName { get; set; }

    }
}
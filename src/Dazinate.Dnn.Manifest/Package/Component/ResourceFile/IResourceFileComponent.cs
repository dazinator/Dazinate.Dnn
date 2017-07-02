namespace Dazinate.Dnn.Manifest.Package.Component.ResourceFile
{
    public interface IResourceFileComponent : IComponent
    {

        /// <summary>
        /// Target folder where the contents of the zip file will be installed. Relative to the root of the DNN installation.
        /// </summary>
        string BasePath { get; set; }

        IResourceFilesList Files { get; }

      

    }
}
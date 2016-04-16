using Dazinate.Dnn.Manifest.Model.ResourceFilesList;

namespace Dazinate.Dnn.Manifest.Model.Component
{
    public interface IResourceFileComponent : IComponent
    {

        /// <summary>
        /// Target folder where the contents of the zip file will be installed. Relative to the root of the DNN installation.
        /// </summary>
        string BasePath { get; }

        IResourceFilesList Files { get; }

      

    }
}
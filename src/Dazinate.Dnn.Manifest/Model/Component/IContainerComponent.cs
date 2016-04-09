using Dazinate.Dnn.Manifest.Model.ContainerFilesList;

namespace Dazinate.Dnn.Manifest.Model.Component
{
    public interface IContainerComponent : IComponent
    {

        /// <summary>
        ///  Target base folder for the component installation.Relative to the root of the DNN installation.
        /// </summary>
        string BasePath { get; set; }
        string ContainerName { get; set; }
        IContainerFilesList Files { get; }
    }
}
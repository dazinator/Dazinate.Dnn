using Dazinate.Dnn.Manifest.Package.Component.Shared.LanguageFile;

namespace Dazinate.Dnn.Manifest.Package.Component.ExtensionLanguage
{
    public interface IExtensionLanguageComponent : IComponent
    {

        string Code { get; set; }

        /// <summary>
        /// Name of another package that contains the extension that this language pack is intended for.
        /// </summary>
        string Package { get; set; }

        /// <summary>
        /// Target base folder for the component installation. Relative to the root of the DNN installation.
        /// </summary>
        string BasePath { get; set; }

        ILanguageFilesList Files { get; }
    }
}
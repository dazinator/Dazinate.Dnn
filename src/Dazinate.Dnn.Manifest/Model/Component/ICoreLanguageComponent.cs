using Dazinate.Dnn.Manifest.Model.LanguageFilesList;

namespace Dazinate.Dnn.Manifest.Model.Component
{
    public interface ICoreLanguageComponent : IComponent
    {

        string Code { get; set; }
        string DisplayName { get; set; }

        /// <summary>
        /// Code for the alternative language. Used if a resource is not found in the current language pack.
        /// </summary>
        string Fallback { get; set; }

        ILanguageFilesList Files { get; }
    }
}
using Dazinate.Dnn.Manifest.Model.EventMessage;
using Dazinate.Dnn.Manifest.Model.ModuleDefinitionsList;
using Dazinate.Dnn.Manifest.Model.SupportedFeaturesList;

namespace Dazinate.Dnn.Manifest.Model.Component
{
    public interface IModuleComponent : IComponent
    {

        string ModuleName { get; set; }
        string FolderName { get; set; }
        string BusinessControllerClass { get; set; }
        string CodeSubDirectory { get; set; }
        bool IsAdmin { get; set; }
        bool IsPremium { get; set; }

        ISupportedFeaturesList SupportedFeatures { get; }

        IModuleDefinitionsList ModuleDefinitions { get; }

        IEventMessage EventMessage { get; set; }

    }
}
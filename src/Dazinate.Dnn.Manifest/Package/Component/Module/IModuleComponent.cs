namespace Dazinate.Dnn.Manifest.Package.Component.Module
{
    public interface IModuleComponent : IComponent
    {

        string ModuleName { get; set; }
        string FolderName { get; set; }
        string BusinessControllerClass { get; set; }
        string CodeSubDirectory { get; set; }
        bool? IsAdmin { get; set; }
        bool? IsPremium { get; set; }

        ISupportedFeaturesList SupportedFeatures { get; }

        IModuleDefinitionsList ModuleDefinitions { get; }

        IEventMessage EventMessage { get; set; }

    }
}
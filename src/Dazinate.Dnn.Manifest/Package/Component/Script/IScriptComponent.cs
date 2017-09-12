namespace Dazinate.Dnn.Manifest.Package.Component.Script
{
    public interface IScriptComponent : IComponent
    {
        string BasePath { get; set; }
        IScriptsList Scripts { get; }

    }
}
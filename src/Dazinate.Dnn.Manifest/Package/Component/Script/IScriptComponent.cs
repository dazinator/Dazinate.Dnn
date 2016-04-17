namespace Dazinate.Dnn.Manifest.Package.Component.Script
{
    public interface IScriptComponent : IComponent
    {
        string BasePath { get; }
        IScriptsList Scripts { get; }

    }
}
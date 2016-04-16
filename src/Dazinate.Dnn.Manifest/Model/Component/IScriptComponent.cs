using Dazinate.Dnn.Manifest.Model.ScriptsList;

namespace Dazinate.Dnn.Manifest.Model.Component
{
    public interface IScriptComponent : IComponent
    {
        string BasePath { get; }
        IScriptsList Scripts { get; }

    }
}
using Dazinate.Dnn.Manifest.Model.ScriptsList;
using Dazinate.Dnn.Manifest.Model.SkinFilesList;

namespace Dazinate.Dnn.Manifest.Model.Component
{
    public interface ISkinComponent : IComponent
    {

        string BasePath { get; }

        string SkinName { get; }

        ISkinFilesList Files { get; }

    }
}
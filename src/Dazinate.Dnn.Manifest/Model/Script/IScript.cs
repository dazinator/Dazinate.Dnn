using Dazinate.Dnn.Manifest.Model.File;

namespace Dazinate.Dnn.Manifest.Model.Script
{
    public interface IScript : IFile
    {

        string Version { get; set; }

        ScriptType Type { get; set; }

    }
}
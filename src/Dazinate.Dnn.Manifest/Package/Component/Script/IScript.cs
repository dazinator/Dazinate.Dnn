using Dazinate.Dnn.Manifest.Package.Component.Shared.File;

namespace Dazinate.Dnn.Manifest.Package.Component.Script
{
    public interface IScript : IFile
    {

        string Version { get; set; }

        ScriptType Type { get; set; }

    }
}
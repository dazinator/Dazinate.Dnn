using Dazinate.Dnn.Manifest.Package.Component.Assembly;

namespace Dazinate.Dnn.Manifest.Factory
{

    public interface IComponentFactory
    {
        IAssemblyComponent CreateNewAssemblyComponent();

    }
}
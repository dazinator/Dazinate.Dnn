using Dazinate.Dnn.Manifest.Package.Component;
using Dazinate.Dnn.Manifest.Package.Component.Assembly;

namespace Dazinate.Dnn.Manifest.Factory
{

    public interface IComponentFactory
    {

        T CreateNewComponent<T>()
        where T : class, IComponent;


        //  IAssemblyComponent CreateNewAssemblyComponent();

        //  IAssemblyComponent CreateNewAuthenticationSystemComponent();

    }
}
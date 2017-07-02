using System;
using Dazinate.Dnn.Manifest.Package.Component.Assembly;
using Dazinate.Dnn.Manifest.Package.Component;

namespace Dazinate.Dnn.Manifest.Factory
{

    [Serializable]
    internal class ComponentFactory : IComponentFactory
    {

        public IAssemblyComponent CreateNewAssemblyComponent()
        {
            return Csla.DataPortal.Create<AssemblyComponent>(ComponentType.Assembly);
        }
    }
}
using System;
using Csla;
using Csla.Server;
using Dazinate.Dnn.Manifest.Base;
using Dazinate.Dnn.Manifest.Package.Component.ObjectFactory;

namespace Dazinate.Dnn.Manifest.Package.Component.Container
{
    [ObjectFactory(typeof(IComponentObjectFactory))]
    [Serializable]
    public class ContainerComponent : BusinessBase<ContainerComponent>, IContainerComponent
    {

        public static readonly PropertyInfo<string> BasePathProperty = RegisterProperty<string>(c => c.BasePath);
        public string BasePath
        {
            get { return GetProperty(BasePathProperty); }
            set { SetProperty(BasePathProperty, value); }
        }

        public static readonly PropertyInfo<string> ContainerNameProperty = RegisterProperty<string>(c => c.ContainerName);
        public string ContainerName
        {
            get { return GetProperty(ContainerNameProperty); }
            set { SetProperty(ContainerNameProperty, value); }
        }

        public static readonly PropertyInfo<ContainerFilesList> FilesProperty = RegisterProperty<ContainerFilesList>(c => c.Files);
        public IContainerFilesList Files
        {
            get { return GetProperty(FilesProperty); }
            set { SetProperty(FilesProperty, value); }
        }

        public void Accept(IManifestVisitor visitor)
        {
            visitor.Visit(this);
        }

        public bool IsCompatibleWithPackage(IPackage package)
        {
            return package.Type.ToLowerInvariant() == "container";
        }

        public override string ToString()
        {
            return "Container";
        }
    }
}
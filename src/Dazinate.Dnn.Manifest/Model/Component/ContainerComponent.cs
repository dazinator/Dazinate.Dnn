using System;
using Csla;
using Csla.Server;
using Dazinate.Dnn.Manifest.Model.Component.ObjectFactory;
using Dazinate.Dnn.Manifest.Model.ContainerFilesList;
using Dazinate.Dnn.Manifest.Model.Package;
using Dazinate.Dnn.Manifest.Writer;

namespace Dazinate.Dnn.Manifest.Model.Component
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

        public static readonly PropertyInfo<ContainerFilesList.ContainerFilesList> FilesProperty = RegisterProperty<ContainerFilesList.ContainerFilesList>(c => c.Files);
        public IContainerFilesList Files
        {
            get { return GetProperty(FilesProperty); }
            set { SetProperty(FilesProperty, value); }
        }

        public void Accept(IManifestXmlWriterVisitor visitor)
        {
            visitor.Visit(this);
        }

        public bool IsCompatibleWithPackage(IPackage package)
        {
            return package.Type.ToLowerInvariant() == "container";
        }
    }
}
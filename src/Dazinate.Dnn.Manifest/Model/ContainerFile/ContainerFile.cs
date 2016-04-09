using System;
using Csla;
using Csla.Server;
using Dazinate.Dnn.Manifest.Model.ContainerFile.ObjectFactory;
using Dazinate.Dnn.Manifest.Writer;

namespace Dazinate.Dnn.Manifest.Model.ContainerFile
{
    [ObjectFactory(typeof(IContainerFileObjectFactory))]
    [Serializable]
    public class ContainerFile : BusinessBase<ContainerFile>, IContainerFile
    {
        public static readonly PropertyInfo<string> PathProperty = RegisterProperty<string>(c => c.Path);
        /// <summary>
        /// Target file folder. Relative to basePath.
        /// </summary>
        public string Path
        {
            get { return GetProperty(PathProperty); }
            set { SetProperty(PathProperty, value); }
        }

        public static readonly PropertyInfo<string> NameProperty = RegisterProperty<string>(c => c.Name);
        public string Name
        {
            get { return GetProperty(NameProperty); }
            set { SetProperty(NameProperty, value); }
        }

        public void Accept(IManifestXmlWriterVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
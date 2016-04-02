using System;
using Csla;
using Csla.Server;
using Dazinate.Dnn.Manifest.Model.Dependency.ObjectFactory;
using Dazinate.Dnn.Manifest.Writer;

namespace Dazinate.Dnn.Manifest.Model.Dependency
{
    [ObjectFactory(typeof(IDependencyObjectFactory))]
    [Serializable]
    public class CoreVersionDependency : BusinessBase<CoreVersionDependency>, IDependency
    {

        public static readonly PropertyInfo<string> VersionProperty = RegisterProperty<string>(c => c.Version);
        public string Version
        {
            get { return GetProperty(VersionProperty); }
            set { SetProperty(VersionProperty, value); }
        }

        public void Accept(IManifestXmlWriterVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
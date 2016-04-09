using System;
using Csla;
using Csla.Server;
using Dazinate.Dnn.Manifest.Model.AssembliesList;
using Dazinate.Dnn.Manifest.Model.Component.ObjectFactory;
using Dazinate.Dnn.Manifest.Model.FilesList;
using Dazinate.Dnn.Manifest.Model.NodesList;
using Dazinate.Dnn.Manifest.Model.Package;
using Dazinate.Dnn.Manifest.Writer;

namespace Dazinate.Dnn.Manifest.Model.Component
{
    [ObjectFactory(typeof(IComponentObjectFactory))]
    [Serializable]
    public class ConfigComponent : BusinessBase<ConfigComponent>, IConfigComponent
    {

        public static readonly PropertyInfo<string> ConfigFileProperty = RegisterProperty<string>(c => c.ConfigFile);
        public string ConfigFile
        {
            get { return GetProperty(ConfigFileProperty); }
            set { SetProperty(ConfigFileProperty, value); }
        }

        public static readonly PropertyInfo<NodesList.NodesList> InstallNodesProperty = RegisterProperty<NodesList.NodesList>(c => c.InstallNodes);
        public INodesList InstallNodes
        {
            get { return GetProperty(InstallNodesProperty); }
            set { SetProperty(InstallNodesProperty, value); }
        }

        public static readonly PropertyInfo<NodesList.NodesList> UninstallNodesProperty = RegisterProperty<NodesList.NodesList>(c => c.UninstallNodes);
        public INodesList UninstallNodes
        {
            get { return GetProperty(UninstallNodesProperty); }
            set { SetProperty(UninstallNodesProperty, value); }
        }

        public void Accept(IManifestXmlWriterVisitor visitor)
        {
            visitor.Visit(this);
        }

        public bool IsCompatibleWithPackage(IPackage package)
        {
            // generic components are compatible with all packages.
            return true;
        }
    }
}
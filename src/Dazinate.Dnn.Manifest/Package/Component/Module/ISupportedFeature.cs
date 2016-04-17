using Csla;
using Dazinate.Dnn.Manifest.Base;

namespace Dazinate.Dnn.Manifest.Package.Component.Module
{
    public interface ISupportedFeature : IBusinessBase, IVisitable<IManifestVisitor>
    {
        SupportedFeatureType FeatureType { get; set; }
    }
}
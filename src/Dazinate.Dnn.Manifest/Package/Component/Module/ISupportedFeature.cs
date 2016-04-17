using Csla;
using Dazinate.Dnn.Manifest.Base;
using Dazinate.Dnn.Manifest.Writer;

namespace Dazinate.Dnn.Manifest.Package.Component.Module
{
    public interface ISupportedFeature : IBusinessBase, IVisitable<IManifestXmlWriterVisitor>
    {
        SupportedFeatureType FeatureType { get; set; }
    }
}
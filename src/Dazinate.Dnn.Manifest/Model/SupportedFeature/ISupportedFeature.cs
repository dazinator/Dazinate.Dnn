using Csla;
using Dazinate.Dnn.Manifest.Model.Node;
using Dazinate.Dnn.Manifest.Writer;

namespace Dazinate.Dnn.Manifest.Model.SupportedFeature
{
    public interface ISupportedFeature : IBusinessBase, IVisitable<IManifestXmlWriterVisitor>
    {
        SupportedFeatureType FeatureType { get; set; }
    }
}
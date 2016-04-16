using Csla;
using Dazinate.Dnn.Manifest.Model.DashboardControlsList.ObjectFactory;
using Dazinate.Dnn.Manifest.Model.SupportedFeature;
using Dazinate.Dnn.Manifest.Writer;

namespace Dazinate.Dnn.Manifest.Model.SupportedFeaturesList
{
    public interface ISupportedFeaturesList : IBusinessListBase<ISupportedFeature>, IVisitable<IManifestXmlWriterVisitor>
    {

    }
}
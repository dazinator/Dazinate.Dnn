using System;
using Csla;
using Csla.Server;
using Dazinate.Dnn.Manifest.Model.SupportedFeature;
using Dazinate.Dnn.Manifest.Model.SupportedFeaturesList.ObjectFactory;
using Dazinate.Dnn.Manifest.Writer;

namespace Dazinate.Dnn.Manifest.Model.SupportedFeaturesList
{
    [ObjectFactory(typeof(ISupportedFeaturesListObjectFactory))]
    [Serializable]
    public class SupportedFeaturesList : BusinessListBase<SupportedFeaturesList, ISupportedFeature>, ISupportedFeaturesList
    {
        // private readonly IPackageFactory _factory;

        public SupportedFeaturesList()
        {
        }

        public void Accept(IManifestXmlWriterVisitor visitor)
        {
            visitor.Visit(this);
        }

    }
}
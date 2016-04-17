using System;
using Csla;
using Csla.Server;
using Dazinate.Dnn.Manifest.Package.Component.Module.ObjectFactory;
using Dazinate.Dnn.Manifest.Writer;

namespace Dazinate.Dnn.Manifest.Package.Component.Module
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
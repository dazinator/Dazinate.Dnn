using System;
using Csla;
using Csla.Server;
using Dazinate.Dnn.Manifest.Model.SupportedFeature.ObjectFactory;
using Dazinate.Dnn.Manifest.Writer;

namespace Dazinate.Dnn.Manifest.Model.SupportedFeature
{
    [ObjectFactory(typeof(ISupportedFeatureObjectFactory))]
    [Serializable]
    public class SupportedFeature : BusinessBase<SupportedFeature>, ISupportedFeature
    {

        public static readonly PropertyInfo<SupportedFeatureType> FeatureTypeProperty = RegisterProperty<SupportedFeatureType>(c => c.FeatureType);
        public SupportedFeatureType FeatureType
        {
            get { return GetProperty(FeatureTypeProperty); }
            set { SetProperty(FeatureTypeProperty, value); }
        }

        public void Accept(IManifestXmlWriterVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
using System;
using Csla;
using Csla.Server;
using Dazinate.Dnn.Manifest.Base;
using Dazinate.Dnn.Manifest.Package.Component.Module.ObjectFactory;

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

        public void Accept(IManifestVisitor visitor)
        {
            visitor.Visit(this);
        }

        protected override ISupportedFeature AddNewCore()
        {
            var item = Csla.DataPortal.Create<SupportedFeature>();
            this.Add(item);
            return item;

        }

    }
}
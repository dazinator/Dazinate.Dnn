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

#if !AddNewCoreReturnVoid
        protected override ISupportedFeature AddNewCore()
        {
            //base.AddNewCore();
            var item = Csla.DataPortal.Create<SupportedFeature>();
            Add(item);
            return item;
        }
#else
        protected override void AddNewCore()
        {
            //base.AddNewCore();
             var item = Csla.DataPortal.Create<SupportedFeature>();
            Add(item);           
        }
#endif        

    }
}
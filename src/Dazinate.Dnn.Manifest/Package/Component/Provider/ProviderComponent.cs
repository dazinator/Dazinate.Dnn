using System;
using Csla;
using Csla.Server;
using Dazinate.Dnn.Manifest.Base;
using Dazinate.Dnn.Manifest.Package.Component.ObjectFactory;

namespace Dazinate.Dnn.Manifest.Package.Component.Provider
{
    [ObjectFactory(typeof(IComponentObjectFactory))]
    [Serializable]
    public class ProviderComponent : BusinessBase<ProviderComponent>, IProviderComponent
    {

        public void Accept(IManifestVisitor visitor)
        {
            visitor.Visit(this);
        }

        public bool IsCompatibleWithPackage(IPackage package)
        {
            return package.Type.ToLowerInvariant() == "provider";
        }

        public override string ToString()
        {
            return "Provider";
        }
    }
}
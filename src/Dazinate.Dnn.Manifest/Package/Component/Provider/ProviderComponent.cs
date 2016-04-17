using System;
using Csla;
using Csla.Server;
using Dazinate.Dnn.Manifest.Package.Component.ObjectFactory;
using Dazinate.Dnn.Manifest.Writer;

namespace Dazinate.Dnn.Manifest.Package.Component.Provider
{
    [ObjectFactory(typeof(IComponentObjectFactory))]
    [Serializable]
    public class ProviderComponent : BusinessBase<ProviderComponent>, IProviderComponent
    {

        public void Accept(IManifestXmlWriterVisitor visitor)
        {
            visitor.Visit(this);
        }

        public bool IsCompatibleWithPackage(IPackage package)
        {
            return package.Type.ToLowerInvariant() == "provider";
        }
    }
}
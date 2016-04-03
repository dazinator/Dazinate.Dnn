using Csla;
using Dazinate.Dnn.Manifest.Model.Dependency;
using Dazinate.Dnn.Manifest.Model.Package;
using Dazinate.Dnn.Manifest.Writer;

namespace Dazinate.Dnn.Manifest.Model.Component
{
    public interface IComponent : IBusinessBase, IVisitable<IManifestXmlWriterVisitor>
    {
        /// <summary>
        /// The component returns whether it is compatible with the particular package.
        /// </summary>
        /// <param name="package"></param>
        /// <returns></returns>
        bool IsCompatibleWithPackage(IPackage package);
       
    }
}
using Csla;
using Dazinate.Dnn.Manifest.Base;

namespace Dazinate.Dnn.Manifest.Package.Component
{
    public interface IComponent : IBusinessBase, IVisitable<IManifestVisitor>
    {
        /// <summary>
        /// The component returns whether it is compatible with the particular package.
        /// </summary>
        /// <param name="package"></param>
        /// <returns></returns>
        bool IsCompatibleWithPackage(IPackage package);
       
    }
}
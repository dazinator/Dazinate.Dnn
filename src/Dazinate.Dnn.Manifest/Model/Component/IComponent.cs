using System;
using System.Xml.XPath;
using Csla;
using Csla.Server;
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


    [ObjectFactory(typeof(IComponentObjectFactory))]
    [Serializable]
    public class AssemblyComponent : BusinessBase<AssemblyComponent>, IComponent
    {
        public void Accept(IManifestXmlWriterVisitor visitor)
        {
            visitor.Visit(this);
        }

        public bool IsCompatibleWithPackage(IPackage package)
        {
            // generic components are compatible with all packages.
            return true;
        }
    }

    public interface IComponentObjectFactory
    {
        IComponent Fetch(XPathNavigator xpathNavigator);
    }
}
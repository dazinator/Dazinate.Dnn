using System.Xml.XPath;
using Dazinate.Dnn.Manifest.Ioc;
using Dazinate.Dnn.Manifest.Utils;

namespace Dazinate.Dnn.Manifest.Model.ContainerFile.ObjectFactory
{
    public class ContainerFileObjectFactory : BaseObjectFactory, IContainerFileObjectFactory
    {

        public ContainerFileObjectFactory(IObjectActivator activator) : base(activator)
        {
            //_packagesListFactory = packagesListFactory;
        }

        public IContainerFile Fetch(XPathNavigator nav)
        {
            // Create the correct concrete dependency based on the xml.
            var businessObject = CreateInstance<Model.ContainerFile.ContainerFile>();
           
            var path = XmlUtils.ReadElement(nav, "path");
            LoadProperty(businessObject, File.File.PathProperty, path);

            var name = XmlUtils.ReadElement(nav, "name");
            LoadProperty(businessObject, File.File.NameProperty, name);
         
            MarkAsChild(businessObject);
            MarkOld(businessObject);
            CheckRules(businessObject);
            return businessObject;
        }
    }
}
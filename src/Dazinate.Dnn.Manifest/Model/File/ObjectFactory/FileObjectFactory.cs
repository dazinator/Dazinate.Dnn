using System.Xml.XPath;
using Dazinate.Dnn.Manifest.Ioc;
using Dazinate.Dnn.Manifest.Utils;

namespace Dazinate.Dnn.Manifest.Model.File.ObjectFactory
{
    public class FileObjectFactory : BaseObjectFactory, IFileObjectFactory
    {

        public FileObjectFactory(IObjectActivator activator) : base(activator)
        {
            //_packagesListFactory = packagesListFactory;
        }

        public IFile Fetch(XPathNavigator nav)
        {
            // Create the correct concrete dependency based on the xml.
            var businessObject = CreateInstance<File>();
           
            var path = XmlUtils.ReadElement(nav, "path");
            LoadProperty(businessObject, File.PathProperty, path);

            var name = XmlUtils.ReadElement(nav, "name");
            LoadProperty(businessObject, File.NameProperty, name);

            var sourceFileName = XmlUtils.ReadElement(nav, "sourceFileName");
            LoadProperty(businessObject, File.SourceFileNameProperty, sourceFileName);

            MarkAsChild(businessObject);
            MarkOld(businessObject);
            CheckRules(businessObject);
            return businessObject;
        }
    }
}
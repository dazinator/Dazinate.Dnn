using System.Xml.XPath;
using Dazinate.Dnn.Manifest.Base;
using Dazinate.Dnn.Manifest.Ioc;
using Dazinate.Dnn.Manifest.Utils;

namespace Dazinate.Dnn.Manifest.Package.Component.JavascriptFile.ObjectFactory
{
    public class JavascriptFileObjectFactory : BaseObjectFactory, IJavascriptFileObjectFactory
    {

        public JavascriptFileObjectFactory(IObjectActivator activator) : base(activator)
        {
            //_packagesListFactory = packagesListFactory;
        }
        public IJavascriptFile Create()
        {
            var businessObject = CreateInstance<JavascriptFile>();
            MarkNew(businessObject);
            MarkAsChild(businessObject);
            return businessObject;
        }

        public IJavascriptFile Fetch(XPathNavigator nav)
        {
            // Create the correct concrete dependency based on the xml.
            var businessObject = CreateInstance<JavascriptFile>();

            var path = XmlUtils.ReadElement(nav, "path");
            LoadProperty(businessObject, JavascriptFile.PathProperty, path);

            var name = XmlUtils.ReadElement(nav, "name");
            LoadProperty(businessObject, JavascriptFile.NameProperty, name);

            var sourceFileName = XmlUtils.ReadElement(nav, "sourceFileName");
            LoadProperty(businessObject, JavascriptFile.SourceFileNameProperty, sourceFileName);

            MarkAsChild(businessObject);
            MarkOld(businessObject);
            CheckRules(businessObject);
            return businessObject;
        }
    }
}
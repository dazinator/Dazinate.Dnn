using System.Xml.XPath;
using Dazinate.Dnn.Manifest.Ioc;
using Dazinate.Dnn.Manifest.Utils;

namespace Dazinate.Dnn.Manifest.Model.SkinFile.ObjectFactory
{
    public class SkinFileObjectFactory : BaseObjectFactory, ISkinFileObjectFactory
    {

        public SkinFileObjectFactory(IObjectActivator activator) : base(activator)
        {
            //_packagesListFactory = packagesListFactory;
        }

        public ISkinFile Fetch(XPathNavigator nav)
        {
            // Create the correct concrete dependency based on the xml.
            var businessObject = CreateInstance<SkinFile>();

            var path = XmlUtils.ReadElement(nav, "path");
            LoadProperty(businessObject, SkinFile.PathProperty, path);

            var name = XmlUtils.ReadElement(nav, "name");
            LoadProperty(businessObject, SkinFile.NameProperty, name);

            var sourceFileName = XmlUtils.ReadElement(nav, "sourceFileName");
            LoadProperty(businessObject, SkinFile.SourceFileNameProperty, sourceFileName);

            MarkAsChild(businessObject);
            MarkOld(businessObject);
            CheckRules(businessObject);
            return businessObject;
        }
    }
}
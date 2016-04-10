using System.Xml.XPath;
using Dazinate.Dnn.Manifest.Ioc;
using Dazinate.Dnn.Manifest.Utils;

namespace Dazinate.Dnn.Manifest.Model.LanguageFile.ObjectFactory
{
    public class LanguageFileObjectFactory : BaseObjectFactory, ILanguageFileObjectFactory
    {

        public LanguageFileObjectFactory(IObjectActivator activator) : base(activator)
        {
            //_packagesListFactory = packagesListFactory;
        }

        public ILanguageFile Fetch(XPathNavigator nav)
        {
            // Create the correct concrete dependency based on the xml.
            var businessObject = CreateInstance<LanguageFile>();

            var path = XmlUtils.ReadElement(nav, "path");
            LoadProperty(businessObject, LanguageFile.PathProperty, path);

            var name = XmlUtils.ReadElement(nav, "name");
            LoadProperty(businessObject, LanguageFile.NameProperty, name);
         
            MarkAsChild(businessObject);
            MarkOld(businessObject);
            CheckRules(businessObject);
            return businessObject;
        }
    }
}
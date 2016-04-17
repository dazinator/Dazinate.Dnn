using System.Xml.XPath;
using Dazinate.Dnn.Manifest.Base;
using Dazinate.Dnn.Manifest.Ioc;

namespace Dazinate.Dnn.Manifest.Package.Component.Shared.LanguageFile.ObjectFactory
{
    public class LanguageFilesListObjectFactory : BaseObjectFactory, ILanguageFilesListObjectFactory
    {
        private readonly ILanguageFileObjectFactory _fileObjectFactory;

        public LanguageFilesListObjectFactory(IObjectActivator activator, ILanguageFileObjectFactory fileObjectFactory) : base(activator)
        {
            _fileObjectFactory = fileObjectFactory;
        }

        public ILanguageFilesList Fetch(XPathNavigator xpathNavigator)
        {
            //  var packagesNav = xpathNavigator.Select("packages/package");

            var list = CreateInstance<LanguageFilesList>();
            list.RaiseListChangedEvents = false;

            // loop through packages.
            foreach (XPathNavigator dependencyNav in xpathNavigator.Select("languageFile"))
            {
                LoadFileItem(dependencyNav, list);
            }

            list.RaiseListChangedEvents = true;

            MarkOld(list);
            MarkAsChild(list);
            CheckRules(list);
            return list;
        }


        private void LoadFileItem(XPathNavigator nav, ILanguageFilesList list)
        {
            var item = _fileObjectFactory.Fetch(nav);
            list.Add(item);
        }

    }
}
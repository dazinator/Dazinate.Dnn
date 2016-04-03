using System.Xml.XPath;
using Dazinate.Dnn.Manifest.Ioc;
using Dazinate.Dnn.Manifest.Model.File.ObjectFactory;

namespace Dazinate.Dnn.Manifest.Model.FilesList.ObjectFactory
{
    public class FilesListObjectFactory : BaseObjectFactory, IFilesListObjectFactory
    {
        private readonly IFileObjectFactory _fileObjectFactory;

        public FilesListObjectFactory(IObjectActivator activator, IFileObjectFactory fileObjectFactory) : base(activator)
        {
            _fileObjectFactory = fileObjectFactory;
        }

        public IFilesList Fetch(XPathNavigator xpathNavigator)
        {
            //  var packagesNav = xpathNavigator.Select("packages/package");

            var list = CreateInstance<FilesList>();
            list.RaiseListChangedEvents = false;

            // loop through packages.
            foreach (XPathNavigator dependencyNav in xpathNavigator.Select("files/file"))
            {
                LoadFileItem(dependencyNav, list);
            }

            list.RaiseListChangedEvents = true;

            MarkOld(list);
            MarkAsChild(list);
            CheckRules(list);
            return list;
        }


        private void LoadFileItem(XPathNavigator nav, IFilesList list)
        {
            var item = _fileObjectFactory.Fetch(nav);
            list.Add(item);
        }

    }
}
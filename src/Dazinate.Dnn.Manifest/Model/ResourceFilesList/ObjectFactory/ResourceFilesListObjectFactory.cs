using System.Xml.XPath;
using Dazinate.Dnn.Manifest.Ioc;
using Dazinate.Dnn.Manifest.Model.ResourceFile.ObjectFactory;

namespace Dazinate.Dnn.Manifest.Model.ResourceFilesList.ObjectFactory
{
    public class ResourceFilesListObjectFactory : BaseObjectFactory, IResourceFilesListObjectFactory
    {
        private readonly IResourceFileObjectFactory _fileObjectFactory;

        public ResourceFilesListObjectFactory(IObjectActivator activator, IResourceFileObjectFactory fileObjectFactory) : base(activator)
        {
            _fileObjectFactory = fileObjectFactory;
        }

        public IResourceFilesList Fetch(XPathNavigator xpathNavigator)
        {
            //  var packagesNav = xpathNavigator.Select("packages/package");

            var list = CreateInstance<ResourceFilesList>();
            list.RaiseListChangedEvents = false;

            // loop through packages.
            foreach (XPathNavigator item in xpathNavigator.Select("resourceFiles/resourceFile"))
            {
                LoadFileItem(item, list);
            }

            list.RaiseListChangedEvents = true;

            MarkOld(list);
            MarkAsChild(list);
            CheckRules(list);
            return list;
        }


        private void LoadFileItem(XPathNavigator nav, IResourceFilesList list)
        {
            var item = _fileObjectFactory.Fetch(nav);
            list.Add(item);
        }

    }
}
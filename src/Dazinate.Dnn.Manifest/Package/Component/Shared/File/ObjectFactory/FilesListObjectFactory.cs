using System;
using System.Xml.XPath;
using Dazinate.Dnn.Manifest.Base;
using Dazinate.Dnn.Manifest.Ioc;

namespace Dazinate.Dnn.Manifest.Package.Component.Shared.File.ObjectFactory
{
    public class FilesListObjectFactory : BaseObjectFactory, IFilesListObjectFactory
    {
        private readonly IFileObjectFactory _fileObjectFactory;

        public FilesListObjectFactory(IObjectActivator activator, IFileObjectFactory fileObjectFactory) : base(activator)
        {
            _fileObjectFactory = fileObjectFactory;
        }

        public IFilesList Create()
        {
            var list = CreateInstance<FilesList>();
            MarkNew(list);
            MarkAsChild(list);
            return list;
        }

        public IFilesList Fetch(XPathNavigator xpathNavigator)
        {
            //  var packagesNav = xpathNavigator.Select("packages/package");

            var list = CreateInstance<FilesList>();
            list.RaiseListChangedEvents = false;

            if (xpathNavigator != null)
            {
                foreach (XPathNavigator item in xpathNavigator.Select("files/file"))
                {
                    LoadFileItem(item, list);
                }
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
using System;
using System.Xml.XPath;
using Dazinate.Dnn.Manifest.Base;
using Dazinate.Dnn.Manifest.Ioc;

namespace Dazinate.Dnn.Manifest.Package.Component.Skin.ObjectFactory
{
    public class SkinFilesListObjectFactory : BaseObjectFactory, ISkinFilesListObjectFactory
    {
        private readonly ISkinFileObjectFactory _fileObjectFactory;

        public SkinFilesListObjectFactory(IObjectActivator activator, ISkinFileObjectFactory fileObjectFactory) : base(activator)
        {
            _fileObjectFactory = fileObjectFactory;
        }

        public ISkinFilesList Create()
        {
            var list = CreateInstance<SkinFilesList>();
            MarkNew(list);
            MarkAsChild(list);
            return list;
        }

        public ISkinFilesList Fetch(XPathNavigator xpathNavigator)
        {
            //  var packagesNav = xpathNavigator.Select("packages/package");

            var list = CreateInstance<SkinFilesList>();
            list.RaiseListChangedEvents = false;

            if (xpathNavigator != null)
            {
                foreach (XPathNavigator item in xpathNavigator.Select("skinFiles/skinFile"))
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


        private void LoadFileItem(XPathNavigator nav, ISkinFilesList list)
        {
            var item = _fileObjectFactory.Fetch(nav);
            list.Add(item);
        }

    }
}
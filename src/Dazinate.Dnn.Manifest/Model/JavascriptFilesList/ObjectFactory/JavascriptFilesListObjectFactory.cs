﻿using System.Xml.XPath;
using Dazinate.Dnn.Manifest.Ioc;
using Dazinate.Dnn.Manifest.Model.JavascriptFile.ObjectFactory;

namespace Dazinate.Dnn.Manifest.Model.JavascriptFilesList.ObjectFactory
{
    public class JavascriptFilesListObjectFactory : BaseObjectFactory, IJavascriptFilesListObjectFactory
    {
        private readonly IJavascriptFileObjectFactory _fileObjectFactory;

        public JavascriptFilesListObjectFactory(IObjectActivator activator, IJavascriptFileObjectFactory fileObjectFactory) : base(activator)
        {
            _fileObjectFactory = fileObjectFactory;
        }

        public IJavascriptFilesList Fetch(XPathNavigator xpathNavigator)
        {

            var list = CreateInstance<Model.JavascriptFilesList.JavascriptFilesList>();
            list.RaiseListChangedEvents = false;
          
            foreach (XPathNavigator dependencyNav in xpathNavigator.Select("jsfiles/jsfile"))
            {
                LoadFileItem(dependencyNav, list);
            }

            list.RaiseListChangedEvents = true;

            MarkOld(list);
            MarkAsChild(list);
            CheckRules(list);
            return list;
        }


        private void LoadFileItem(XPathNavigator nav, IJavascriptFilesList list)
        {
            var item = _fileObjectFactory.Fetch(nav);
            list.Add(item);
        }

    }
}